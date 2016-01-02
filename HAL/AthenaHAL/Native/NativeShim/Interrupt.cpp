#include <atomic>
#include <condition_variable>
#include <mutex>
#include <thread>

class SafeThread {
 public:
  virtual ~SafeThread() = default;
  virtual void Main() = 0;

  std::mutex m_mutex;
  bool m_active = true;
  std::condition_variable m_cond;
};

namespace detailInterrupt {

// Non-template proxy base class for common proxy code.
class SafeThreadProxyBase {
 public:
  SafeThreadProxyBase(SafeThread* thr) : m_thread(thr) {
    if (!m_thread) return;
    std::unique_lock<std::mutex>(m_thread->m_mutex).swap(m_lock);
    if (!m_thread->m_active) {
      m_lock.unlock();
      m_thread = nullptr;
      return;
    }
  }
  explicit operator bool() const { return m_thread != nullptr; }
  std::unique_lock<std::mutex>& GetLock() { return m_lock; }

 protected:
  SafeThread* m_thread;
  std::unique_lock<std::mutex> m_lock;
};

// A proxy for SafeThread.
// Also serves as a scoped lock on SafeThread::m_mutex.
template <typename T>
class SafeThreadProxy : public SafeThreadProxyBase {
 public:
  SafeThreadProxy(SafeThread* thr) : SafeThreadProxyBase(thr) {}
  T& operator*() const { return *static_cast<T*>(m_thread); }
  T* operator->() const { return static_cast<T*>(m_thread); }
};

// Non-template owner base class for common owner code.
class SafeThreadOwnerBase {
 public:
  void Stop();

 protected:
  SafeThreadOwnerBase() { m_thread = nullptr; }
  SafeThreadOwnerBase(const SafeThreadOwnerBase&) = delete;
  SafeThreadOwnerBase& operator=(const SafeThreadOwnerBase&) = delete;
  ~SafeThreadOwnerBase() { Stop(); }

  void Start(SafeThread* thr);
  SafeThread* GetThread() { return m_thread.load(); }

 private:
  std::atomic<SafeThread*> m_thread;
};

}  // namespace detailInterrupt

template <typename T>
class SafeThreadOwner : public detailInterrupt::SafeThreadOwnerBase {
 public:
  void Start() { Start(new T); }
  void Start(T* thr) { detailInterrupt::SafeThreadOwnerBase::Start(thr); }

  using Proxy = typename detailInterrupt::SafeThreadProxy<T>;
  Proxy GetThread() { return Proxy(detailInterrupt::SafeThreadOwnerBase::GetThread()); }
};

void detailInterrupt::SafeThreadOwnerBase::Start(SafeThread* thr) {
  SafeThread* curthr = nullptr;
  SafeThread* newthr = thr;
  if (!m_thread.compare_exchange_strong(curthr, newthr)) {
    delete newthr;
    return;
  }
  std::thread([=]() {
    newthr->Main();
    delete newthr;
  }).detach();
}

void detailInterrupt::SafeThreadOwnerBase::Stop() {
  SafeThread* thr = m_thread.exchange(nullptr);
  if (!thr) return;
  std::lock_guard<std::mutex> lock(thr->m_mutex);
  thr->m_active = false;
  thr->m_cond.notify_one();
}

class InterruptThreadJNI : public SafeThread {
 public:
  void Main();

  bool m_notify = false;
  uint32_t m_mask = 0;
  void* m_func = nullptr;
  InterruptHandlerFunction m_mid;
};

class InterruptShim : public SafeThreadOwner<InterruptThreadJNI> {
 public:
  void SetFunc(InterruptHandlerFunction mid);

  void Notify(uint32_t mask) {
    auto thr = GetThread();
    if (!thr) return;
    thr->m_notify = true;
    thr->m_mask = mask;
    thr->m_cond.notify_one();
  }
};

void InterruptShim::SetFunc(InterruptHandlerFunction mid) {
  auto thr = GetThread();
  if (!thr) return;
  thr->m_mid = mid;
}

void InterruptThreadJNI::Main() {

  std::unique_lock<std::mutex> lock(m_mutex);
  while (m_active) {
    m_cond.wait(lock, [&] { return !m_active || m_notify; });
    if (!m_active) break;
    m_notify = false;
    InterruptHandlerFunction mid = m_mid;
    uint32_t mask = m_mask;
    lock.unlock();  // don't hold mutex during callback execution
    mid(mask, nullptr);
    lock.lock();
  }
}

void interruptHandler(uint32_t mask, void* param) {
  ((InterruptShim*)param)->Notify(mask);
}

void attachInterruptHandlerShim(void* interrupt_pointer, InterruptHandlerFunction handler,
			void* param, int32_t *status)
{
	InterruptShim* intr = new InterruptShim;
	intr->Start();
	intr->SetFunc(handler);
	
	attachInterruptHandler(interrupt_pointer, interruptHandler, intr, status);
}
