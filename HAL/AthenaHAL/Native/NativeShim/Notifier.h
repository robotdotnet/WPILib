void* initializeNotifierShim(void (*process)(uint64_t, void*), void *param, int32_t *status);
void cleanNotifierShim(void* notifier_pointer, int32_t *status);