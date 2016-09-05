#include <stdint.h>
#include <atomic>
#include <condition_variable>
#include <cstdlib>
#include <iostream>
#include <mutex>
#include <thread>
#include "HAL/Interrupts.h"
#include "HAL/Notifier.h"
#include "errno.h"

class SafeThread {
 public:
  virtual ~SafeThread() = default;
  virtual void Main() = 0;

  std::mutex m_mutex;
  bool m_active = true;
  std::condition_variable m_cond;
};

namespace detail {

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

}  // namespace detail

template <typename T>
class SafeThreadOwner : public detail::SafeThreadOwnerBase {
 public:
  void Start() { Start(new T); }
  void Start(T* thr) { detail::SafeThreadOwnerBase::Start(thr); }

  using Proxy = typename detail::SafeThreadProxy<T>;
  Proxy GetThread() { return Proxy(detail::SafeThreadOwnerBase::GetThread()); }
};

void detail::SafeThreadOwnerBase::Start(SafeThread* thr) {
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

void detail::SafeThreadOwnerBase::Stop() {
  SafeThread* thr = m_thread.exchange(nullptr);
  if (!thr) return;
  std::lock_guard<std::mutex> lock(thr->m_mutex);
  thr->m_active = false;
  thr->m_cond.notify_one();
}

// Notifier Fixes
class NotifierThreadJNI : public SafeThread {
 public:
  void Main();

  bool m_notify = false;
  HAL_NotifierHandle m_handle = HAL_kInvalidHandle;
  void (*process)(uint64_t, HAL_NotifierHandle);
  uint64_t m_currentTime;
};

class NotifierShim : public SafeThreadOwner<NotifierThreadJNI> {
 public:
  void SetFunc(void (*process)(uint64_t, HAL_NotifierHandle));

  void Notify(uint64_t currentTime, HAL_NotifierHandle handle) {
    auto thr = GetThread();
    if (!thr) return;
    thr->m_currentTime = currentTime;
    thr->m_handle = handle;
    thr->m_notify = true;
    thr->m_cond.notify_one();
  }
};

void NotifierShim::SetFunc(void (*process)(uint64_t, HAL_NotifierHandle)) {
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
    HAL_NotifierHandle handle = m_handle;
    lock.unlock();  // don't hold mutex during callback execution
    process(currentTime, handle);
    lock.lock();
  }
}

void notifierHandler(uint64_t currentTimeInt, HAL_NotifierHandle handle) {
  int32_t status = 0;
  auto notifierPointer = HAL_GetNotifierParam(handle, &status);
  if (notifierPointer == nullptr) return;
  NotifierShim* shim = static_cast<NotifierShim*>(notifierPointer);
  shim->Notify(currentTimeInt, handle);
}
extern "C" {

HAL_NotifierHandle HAL_InitializeNotifierShim(void (*process)(uint64_t, HAL_NotifierHandle),
                                         void* param, int32_t* status) {
  NotifierShim* notify = new NotifierShim;
  notify->Start();
  notify->SetFunc(process);

  auto notifierHandle = HAL_InitializeNotifier(notifierHandler, notify, status);

  if (notifierHandle == HAL_kInvalidHandle || *status != 0) {
    delete notify;
    return HAL_kInvalidHandle;
  }

  return notifierHandle;
}

void HAL_CleanNotifierShim(HAL_NotifierHandle notifier_handle, int32_t* status) {
  NotifierShim* notify =
      (NotifierShim*)HAL_GetNotifierParam(notifier_handle, status);
  HAL_CleanNotifier(notifier_handle, status);
  delete notify;
}
}

// Interrupt Fixes
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
extern "C" {

void interruptHandler(uint32_t mask, void* param) {
  ((InterruptShim*)param)->Notify(mask);
}

void HAL_AttachInterruptHandlerShim(HAL_InterruptHandle interrupt_handle,
                                InterruptHandlerFunction handler, void* param,
                                int32_t* status) {
  InterruptShim* intr = new InterruptShim;
  intr->Start();
  intr->SetFunc(handler);

  HAL_AttachInterruptHandler(interrupt_handle, interruptHandler, intr, status);
}
}
