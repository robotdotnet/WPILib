#include <atomic>
#include <cstdlib>
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

namespace detailNotifier {

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

}  // namespace detailNotifier

template <typename T>
class SafeThreadOwner : public detailNotifier::SafeThreadOwnerBase {
 public:
  void Start() { Start(new T); }
  void Start(T* thr) { detailNotifier::SafeThreadOwnerBase::Start(thr); }

  using Proxy = typename detailNotifier::SafeThreadProxy<T>;
  Proxy GetThread() { return Proxy(detailNotifier::SafeThreadOwnerBase::GetThread()); }
};

void detailNotifier::SafeThreadOwnerBase::Start(SafeThread* thr) {
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

void detailNotifier::SafeThreadOwnerBase::Stop() {
  SafeThread* thr = m_thread.exchange(nullptr);
  if (!thr) return;
  std::lock_guard<std::mutex> lock(thr->m_mutex);
  thr->m_active = false;
  thr->m_cond.notify_one();
}


class NotifierThreadJNI : public SafeThread {
 public:
  void Main();

  bool m_notify = false;
  void* m_func = nullptr;
  void (*process)(uint64_t, void*);
  uint64_t m_currentTime;
};

class NotifierShim : public SafeThreadOwner<NotifierThreadJNI> {
 public:
  void SetFunc(void (*process)(uint64_t, void*));

  void Notify(uint64_t currentTime) {
    auto thr = GetThread();
    if (!thr) return;
    thr->m_currentTime = currentTime;
    thr->m_notify = true;
    thr->m_cond.notify_one();
  }
};

void NotifierShim::SetFunc(void (*process)(uint64_t, void*)) {
  auto thr = GetThread();
  if (!thr) return;
  thr->process = process;
}

void NotifierThreadJNI::Main() {

  std::unique_lock<std::mutex> lock(m_mutex);
  while (m_active) {
    m_cond.wait(lock, [&] { return !m_active || m_notify; });
    if (!m_active) break;
    m_notify = false;
    uint64_t currentTime = m_currentTime;
    lock.unlock();  // don't hold mutex during callback execution
	process(currentTime, nullptr);
    lock.lock();
  }
}

void notifierHandler(uint64_t currentTimeInt, void* param) {
  ((NotifierShim*)param)->Notify(currentTimeInt);
}

void* initializeNotifierShim(void (*process)(uint64_t, void*), void *param, int32_t *status)
{
	NotifierShim* notify = new NotifierShim;
	notify->Start();
	notify->SetFunc(process);
	
	void *notifierPtr = initializeNotifier(notifierHandler, notify, status);
	
	if (!notifierPtr || *status != 0)
	{
		delete notify;
	}
	
	return notifierPtr;
}

void cleanNotifierShim(void* notifier_pointer, int32_t *status)
{
	NotifierShim* notify = (NotifierShim*)getNotifierParam(notifier_pointer, status);
	cleanNotifier(notifier_pointer, status);
	delete notify;
}
