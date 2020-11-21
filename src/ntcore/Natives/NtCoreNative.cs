using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace NetworkTables.Natives
{
    public unsafe class NtCoreNative : INtCore
    {
        [NativeFunctionPointer("NT_DisposeEntryNotificationArray")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryNotification*, UIntPtr, void> NT_DisposeEntryNotificationArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryNotificationArray(NtEntryNotification* arr, UIntPtr count)
        {
            NT_DisposeEntryNotificationArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeEntryNotification")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryNotification*, void> NT_DisposeEntryNotificationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryNotification(NtEntryNotification* info)
        {
            NT_DisposeEntryNotificationFunc(info);
        }


        [NativeFunctionPointer("NT_DisposeConnectionNotificationArray")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionNotification*, UIntPtr, void> NT_DisposeConnectionNotificationArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeConnectionNotificationArray(NtConnectionNotification* arr, UIntPtr count)
        {
            NT_DisposeConnectionNotificationArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeConnectionNotification")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionNotification*, void> NT_DisposeConnectionNotificationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeConnectionNotification(NtConnectionNotification* info)
        {
            NT_DisposeConnectionNotificationFunc(info);
        }


        [NativeFunctionPointer("NT_DisposeLogMessageArray")]
        private readonly delegate* unmanaged[Cdecl]<NtLogMessage*, UIntPtr, void> NT_DisposeLogMessageArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeLogMessageArray(NtLogMessage* arr, UIntPtr count)
        {
            NT_DisposeLogMessageArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeLogMessage")]
        private readonly delegate* unmanaged[Cdecl]<NtLogMessage*, void> NT_DisposeLogMessageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeLogMessage(NtLogMessage* info)
        {
            NT_DisposeLogMessageFunc(info);
        }


        [NativeFunctionPointer("NT_Now")]
        private readonly delegate* unmanaged[Cdecl]<ulong> NT_NowFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NT_Now()
        {
            return NT_NowFunc();
        }


        [NativeFunctionPointer("NT_CreateLoggerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, NtLoggerPoller> NT_CreateLoggerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLoggerPoller NT_CreateLoggerPoller(NtInst inst)
        {
            return NT_CreateLoggerPollerFunc(inst);
        }


        [NativeFunctionPointer("NT_DestroyLoggerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, void> NT_DestroyLoggerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyLoggerPoller(NtLoggerPoller poller)
        {
            NT_DestroyLoggerPollerFunc(poller);
        }


        [NativeFunctionPointer("NT_AddPolledLogger")]
        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, uint, uint, NtLogger> NT_AddPolledLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogger NT_AddPolledLogger(NtLoggerPoller poller, uint min_level, uint max_level)
        {
            return NT_AddPolledLoggerFunc(poller, min_level, max_level);
        }


        [NativeFunctionPointer("NT_PollLogger")]
        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, UIntPtr*, NtLogMessage*> NT_PollLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogMessage* NT_PollLogger(NtLoggerPoller poller, UIntPtr* len)
        {
            return NT_PollLoggerFunc(poller, len);
        }


        [NativeFunctionPointer("NT_PollLoggerTimeout")]
        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, UIntPtr*, double, NtBool*, NtLogMessage*> NT_PollLoggerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogMessage* NT_PollLoggerTimeout(NtLoggerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollLoggerTimeoutFunc(poller, len, timeout, timed_out);
        }


        [NativeFunctionPointer("NT_CancelPollLogger")]
        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, void> NT_CancelPollLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollLogger(NtLoggerPoller poller)
        {
            NT_CancelPollLoggerFunc(poller);
        }


        [NativeFunctionPointer("NT_RemoveLogger")]
        private readonly delegate* unmanaged[Cdecl]<NtLogger, void> NT_RemoveLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveLogger(NtLogger logger)
        {
            NT_RemoveLoggerFunc(logger);
        }


        [NativeFunctionPointer("NT_WaitForLoggerQueue")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForLoggerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForLoggerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForLoggerQueueFunc(inst, timeout);
        }


        [NativeFunctionPointer("NT_AllocateCharArray")]
        private readonly delegate* unmanaged[Cdecl]<UIntPtr, byte*> NT_AllocateCharArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_AllocateCharArray(UIntPtr size)
        {
            return NT_AllocateCharArrayFunc(size);
        }


        [NativeFunctionPointer("NT_AllocateBooleanArray")]
        private readonly delegate* unmanaged[Cdecl]<UIntPtr, NtBool*> NT_AllocateBooleanArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool* NT_AllocateBooleanArray(UIntPtr size)
        {
            return NT_AllocateBooleanArrayFunc(size);
        }


        [NativeFunctionPointer("NT_AllocateDoubleArray")]
        private readonly delegate* unmanaged[Cdecl]<UIntPtr, double*> NT_AllocateDoubleArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double* NT_AllocateDoubleArray(UIntPtr size)
        {
            return NT_AllocateDoubleArrayFunc(size);
        }


        [NativeFunctionPointer("NT_AllocateStringArray")]
        private readonly delegate* unmanaged[Cdecl]<UIntPtr, NtString*> NT_AllocateStringArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtString* NT_AllocateStringArray(UIntPtr size)
        {
            return NT_AllocateStringArrayFunc(size);
        }


        [NativeFunctionPointer("NT_FreeCharArray")]
        private readonly delegate* unmanaged[Cdecl]<byte*, void> NT_FreeCharArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeCharArray(byte* v_char)
        {
            NT_FreeCharArrayFunc(v_char);
        }


        [NativeFunctionPointer("NT_FreeDoubleArray")]
        private readonly delegate* unmanaged[Cdecl]<double*, void> NT_FreeDoubleArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeDoubleArray(double* v_double)
        {
            NT_FreeDoubleArrayFunc(v_double);
        }


        [NativeFunctionPointer("NT_FreeBooleanArray")]
        private readonly delegate* unmanaged[Cdecl]<NtBool*, void> NT_FreeBooleanArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeBooleanArray(NtBool* v_boolean)
        {
            NT_FreeBooleanArrayFunc(v_boolean);
        }


        [NativeFunctionPointer("NT_FreeStringArray")]
        private readonly delegate* unmanaged[Cdecl]<NtString*, UIntPtr, void> NT_FreeStringArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeStringArray(NtString* v_string, UIntPtr arr_size)
        {
            NT_FreeStringArrayFunc(v_string, arr_size);
        }


        [NativeFunctionPointer("NT_GetDefaultInstance")]
        private readonly delegate* unmanaged[Cdecl]<NtInst> NT_GetDefaultInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_GetDefaultInstance()
        {
            return NT_GetDefaultInstanceFunc();
        }


        [NativeFunctionPointer("NT_CreateInstance")]
        private readonly delegate* unmanaged[Cdecl]<NtInst> NT_CreateInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_CreateInstance()
        {
            return NT_CreateInstanceFunc();
        }


        [NativeFunctionPointer("NT_DestroyInstance")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_DestroyInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyInstance(NtInst inst)
        {
            NT_DestroyInstanceFunc(inst);
        }


        [NativeFunctionPointer("NT_GetInstanceFromHandle")]
        private readonly delegate* unmanaged[Cdecl]<NtHandle, NtInst> NT_GetInstanceFromHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_GetInstanceFromHandle(NtHandle handle)
        {
            return NT_GetInstanceFromHandleFunc(handle);
        }


        [NativeFunctionPointer("NT_GetEntry")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, NtEntry> NT_GetEntryFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntry NT_GetEntry(NtInst inst, byte* name, UIntPtr name_len)
        {
            return NT_GetEntryFunc(inst, name, name_len);
        }


        [NativeFunctionPointer("NT_GetEntries")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, uint, UIntPtr*, NtEntry*> NT_GetEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntry* NT_GetEntries(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count)
        {
            return NT_GetEntriesFunc(inst, prefix, prefix_len, types, count);
        }


        [NativeFunctionPointer("NT_GetEntryName")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, UIntPtr*, byte*> NT_GetEntryNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetEntryName(NtEntry entry, UIntPtr* name_len)
        {
            return NT_GetEntryNameFunc(entry, name_len);
        }


        [NativeFunctionPointer("NT_GetEntryType")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtType> NT_GetEntryTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtType NT_GetEntryType(NtEntry entry)
        {
            return NT_GetEntryTypeFunc(entry);
        }


        [NativeFunctionPointer("NT_GetEntryLastChange")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, ulong> NT_GetEntryLastChangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NT_GetEntryLastChange(NtEntry entry)
        {
            return NT_GetEntryLastChangeFunc(entry);
        }


        [NativeFunctionPointer("NT_GetEntryValue")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, void> NT_GetEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_GetEntryValue(NtEntry entry, NtValue* value)
        {
            NT_GetEntryValueFunc(entry, value);
        }


        [NativeFunctionPointer("NT_SetDefaultEntryValue")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, NtBool> NT_SetDefaultEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_SetDefaultEntryValue(NtEntry entry, NtValue* default_value)
        {
            return NT_SetDefaultEntryValueFunc(entry, default_value);
        }


        [NativeFunctionPointer("NT_SetEntryValue")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, NtBool> NT_SetEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_SetEntryValue(NtEntry entry, NtValue* value)
        {
            return NT_SetEntryValueFunc(entry, value);
        }


        [NativeFunctionPointer("NT_SetEntryTypeValue")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, void> NT_SetEntryTypeValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetEntryTypeValue(NtEntry entry, NtValue* value)
        {
            NT_SetEntryTypeValueFunc(entry, value);
        }


        [NativeFunctionPointer("NT_SetEntryFlags")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, uint, void> NT_SetEntryFlagsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetEntryFlags(NtEntry entry, uint flags)
        {
            NT_SetEntryFlagsFunc(entry, flags);
        }


        [NativeFunctionPointer("NT_GetEntryFlags")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, uint> NT_GetEntryFlagsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NT_GetEntryFlags(NtEntry entry)
        {
            return NT_GetEntryFlagsFunc(entry);
        }


        [NativeFunctionPointer("NT_DeleteEntry")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, void> NT_DeleteEntryFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DeleteEntry(NtEntry entry)
        {
            NT_DeleteEntryFunc(entry);
        }


        [NativeFunctionPointer("NT_DeleteAllEntries")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_DeleteAllEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DeleteAllEntries(NtInst inst)
        {
            NT_DeleteAllEntriesFunc(inst);
        }


        [NativeFunctionPointer("NT_GetEntryInfo")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, uint, UIntPtr*, NtEntryInfo*> NT_GetEntryInfoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryInfo* NT_GetEntryInfo(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count)
        {
            return NT_GetEntryInfoFunc(inst, prefix, prefix_len, types, count);
        }


        [NativeFunctionPointer("NT_GetEntryInfoHandle")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtEntryInfo*, NtBool> NT_GetEntryInfoHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_GetEntryInfoHandle(NtEntry entry, NtEntryInfo* info)
        {
            return NT_GetEntryInfoHandleFunc(entry, info);
        }


        [NativeFunctionPointer("NT_CreateEntryListenerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, NtEntryListenerPoller> NT_CreateEntryListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListenerPoller NT_CreateEntryListenerPoller(NtInst inst)
        {
            return NT_CreateEntryListenerPollerFunc(inst);
        }


        [NativeFunctionPointer("NT_DestroyEntryListenerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, void> NT_DestroyEntryListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyEntryListenerPoller(NtEntryListenerPoller poller)
        {
            NT_DestroyEntryListenerPollerFunc(poller);
        }


        [NativeFunctionPointer("NT_AddPolledEntryListener")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, byte*, UIntPtr, uint, NtEntryListener> NT_AddPolledEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListener NT_AddPolledEntryListener(NtEntryListenerPoller poller, byte* prefix, UIntPtr prefix_len, uint flags)
        {
            return NT_AddPolledEntryListenerFunc(poller, prefix, prefix_len, flags);
        }


        [NativeFunctionPointer("NT_AddPolledEntryListenerSingle")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, NtEntry, uint, NtEntryListener> NT_AddPolledEntryListenerSingleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListener NT_AddPolledEntryListenerSingle(NtEntryListenerPoller poller, NtEntry entry, uint flags)
        {
            return NT_AddPolledEntryListenerSingleFunc(poller, entry, flags);
        }


        [NativeFunctionPointer("NT_PollEntryListener")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, UIntPtr*, NtEntryNotification*> NT_PollEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryNotification* NT_PollEntryListener(NtEntryListenerPoller poller, UIntPtr* len)
        {
            return NT_PollEntryListenerFunc(poller, len);
        }


        [NativeFunctionPointer("NT_PollEntryListenerTimeout")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, UIntPtr*, double, NtBool*, NtEntryNotification*> NT_PollEntryListenerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryNotification* NT_PollEntryListenerTimeout(NtEntryListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollEntryListenerTimeoutFunc(poller, len, timeout, timed_out);
        }


        [NativeFunctionPointer("NT_CancelPollEntryListener")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, void> NT_CancelPollEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollEntryListener(NtEntryListenerPoller poller)
        {
            NT_CancelPollEntryListenerFunc(poller);
        }


        [NativeFunctionPointer("NT_RemoveEntryListener")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryListener, void> NT_RemoveEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveEntryListener(NtEntryListener entry_listener)
        {
            NT_RemoveEntryListenerFunc(entry_listener);
        }


        [NativeFunctionPointer("NT_WaitForEntryListenerQueue")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForEntryListenerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForEntryListenerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForEntryListenerQueueFunc(inst, timeout);
        }


        [NativeFunctionPointer("NT_CreateConnectionListenerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, NtConnectionListenerPoller> NT_CreateConnectionListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListenerPoller NT_CreateConnectionListenerPoller(NtInst inst)
        {
            return NT_CreateConnectionListenerPollerFunc(inst);
        }


        [NativeFunctionPointer("NT_DestroyConnectionListenerPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, void> NT_DestroyConnectionListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyConnectionListenerPoller(NtConnectionListenerPoller poller)
        {
            NT_DestroyConnectionListenerPollerFunc(poller);
        }


        [NativeFunctionPointer("NT_AddPolledConnectionListener")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, NtBool, NtConnectionListener> NT_AddPolledConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListener NT_AddPolledConnectionListener(NtConnectionListenerPoller poller, NtBool immediate_notify)
        {
            return NT_AddPolledConnectionListenerFunc(poller, immediate_notify);
        }


        [NativeFunctionPointer("NT_PollConnectionListener")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, UIntPtr*, NtConnectionNotification*> NT_PollConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionNotification* NT_PollConnectionListener(NtConnectionListenerPoller poller, UIntPtr* len)
        {
            return NT_PollConnectionListenerFunc(poller, len);
        }


        [NativeFunctionPointer("NT_PollConnectionListenerTimeout")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, UIntPtr*, double, NtBool*, NtConnectionNotification*> NT_PollConnectionListenerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionNotification* NT_PollConnectionListenerTimeout(NtConnectionListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollConnectionListenerTimeoutFunc(poller, len, timeout, timed_out);
        }


        [NativeFunctionPointer("NT_CancelPollConnectionListener")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, void> NT_CancelPollConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollConnectionListener(NtConnectionListenerPoller poller)
        {
            NT_CancelPollConnectionListenerFunc(poller);
        }


        [NativeFunctionPointer("NT_RemoveConnectionListener")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionListener, void> NT_RemoveConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveConnectionListener(NtConnectionListener conn_listener)
        {
            NT_RemoveConnectionListenerFunc(conn_listener);
        }


        [NativeFunctionPointer("NT_WaitForConnectionListenerQueue")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForConnectionListenerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForConnectionListenerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForConnectionListenerQueueFunc(inst, timeout);
        }


        [NativeFunctionPointer("NT_CreateRpcCallPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, NtRpcCallPoller> NT_CreateRpcCallPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCallPoller NT_CreateRpcCallPoller(NtInst inst)
        {
            return NT_CreateRpcCallPollerFunc(inst);
        }


        [NativeFunctionPointer("NT_DestroyRpcCallPoller")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, void> NT_DestroyRpcCallPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyRpcCallPoller(NtRpcCallPoller poller)
        {
            NT_DestroyRpcCallPollerFunc(poller);
        }


        [NativeFunctionPointer("NT_CreatePolledRpc")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, byte*, UIntPtr, NtRpcCallPoller, void> NT_CreatePolledRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CreatePolledRpc(NtEntry entry, byte* def, UIntPtr def_len, NtRpcCallPoller poller)
        {
            NT_CreatePolledRpcFunc(entry, def, def_len, poller);
        }


        [NativeFunctionPointer("NT_PollRpc")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, UIntPtr*, NtRpcAnswer*> NT_PollRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcAnswer* NT_PollRpc(NtRpcCallPoller poller, UIntPtr* len)
        {
            return NT_PollRpcFunc(poller, len);
        }


        [NativeFunctionPointer("NT_PollRpcTimeout")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, UIntPtr*, double, NtBool*, NtRpcAnswer*> NT_PollRpcTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcAnswer* NT_PollRpcTimeout(NtRpcCallPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollRpcTimeoutFunc(poller, len, timeout, timed_out);
        }


        [NativeFunctionPointer("NT_CancelPollRpc")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, void> NT_CancelPollRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollRpc(NtRpcCallPoller poller)
        {
            NT_CancelPollRpcFunc(poller);
        }


        [NativeFunctionPointer("NT_WaitForRpcCallQueue")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForRpcCallQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForRpcCallQueue(NtInst inst, double timeout)
        {
            return NT_WaitForRpcCallQueueFunc(inst, timeout);
        }


        [NativeFunctionPointer("NT_PostRpcResponse")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, byte*, UIntPtr, void> NT_PostRpcResponseFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_PostRpcResponse(NtEntry entry, NtRpcCall rpccall, byte* result, UIntPtr result_len)
        {
            NT_PostRpcResponseFunc(entry, rpccall, result, result_len);
        }


        [NativeFunctionPointer("NT_CallRpc")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, byte*, UIntPtr, NtRpcCall> NT_CallRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCall NT_CallRpc(NtEntry entry, byte* callparams, UIntPtr params_len)
        {
            return NT_CallRpcFunc(entry, callparams, params_len);
        }


        [NativeFunctionPointer("NT_GetRpcResult")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, UIntPtr*, byte*> NT_GetRpcResultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetRpcResult(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len)
        {
            return NT_GetRpcResultFunc(entry, rpccall, result_len);
        }


        [NativeFunctionPointer("NT_GetRpcResultTimeout")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, UIntPtr*, double, NtBool*, byte*> NT_GetRpcResultTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetRpcResultTimeout(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len, double timeout, NtBool* timed_out)
        {
            return NT_GetRpcResultTimeoutFunc(entry, rpccall, result_len, timeout, timed_out);
        }


        [NativeFunctionPointer("NT_CancelRpcResult")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, void> NT_CancelRpcResultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelRpcResult(NtEntry entry, NtRpcCall rpccall)
        {
            NT_CancelRpcResultFunc(entry, rpccall);
        }


        [NativeFunctionPointer("NT_PackRpcDefinition")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcDefinition, UIntPtr*, byte*> NT_PackRpcDefinitionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_PackRpcDefinition(NtRpcDefinition def, UIntPtr* packed_len)
        {
            return NT_PackRpcDefinitionFunc(def, packed_len);
        }


        [NativeFunctionPointer("NT_UnpackRpcDefinition")]
        private readonly delegate* unmanaged[Cdecl]<byte*, UIntPtr, NtRpcDefinition*, NtBool> NT_UnpackRpcDefinitionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_UnpackRpcDefinition(byte* packed, UIntPtr packed_len, NtRpcDefinition* def)
        {
            return NT_UnpackRpcDefinitionFunc(packed, packed_len, def);
        }


        [NativeFunctionPointer("NT_PackRpcValues")]
        private readonly delegate* unmanaged[Cdecl]<NtValue**, UIntPtr, UIntPtr*, byte*> NT_PackRpcValuesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_PackRpcValues(NtValue** values, UIntPtr values_len, UIntPtr* packed_len)
        {
            return NT_PackRpcValuesFunc(values, values_len, packed_len);
        }


        [NativeFunctionPointer("NT_UnpackRpcValues")]
        private readonly delegate* unmanaged[Cdecl]<byte*, UIntPtr, NtType*, UIntPtr, NtValue**> NT_UnpackRpcValuesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtValue** NT_UnpackRpcValues(byte* packed, UIntPtr packed_len, NtType* types, UIntPtr types_len)
        {
            return NT_UnpackRpcValuesFunc(packed, packed_len, types, types_len);
        }


        [NativeFunctionPointer("NT_SetNetworkIdentity")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, void> NT_SetNetworkIdentityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetNetworkIdentity(NtInst inst, byte* name, UIntPtr name_len)
        {
            NT_SetNetworkIdentityFunc(inst, name, name_len);
        }


        [NativeFunctionPointer("NT_GetNetworkMode")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, uint> NT_GetNetworkModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NT_GetNetworkMode(NtInst inst)
        {
            return NT_GetNetworkModeFunc(inst);
        }


        [NativeFunctionPointer("NT_StartServer")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, uint, void> NT_StartServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartServer(NtInst inst, byte* persist_filename, byte* listen_address, uint port)
        {
            NT_StartServerFunc(inst, persist_filename, listen_address, port);
        }


        [NativeFunctionPointer("NT_StopServer")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopServer(NtInst inst)
        {
            NT_StopServerFunc(inst);
        }


        [NativeFunctionPointer("NT_StartClientNone")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StartClientNoneFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientNone(NtInst inst)
        {
            NT_StartClientNoneFunc(inst);
        }


        [NativeFunctionPointer("NT_StartClient")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, uint, void> NT_StartClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClient(NtInst inst, byte* server_name, uint port)
        {
            NT_StartClientFunc(inst, server_name, port);
        }


        [NativeFunctionPointer("NT_StartClientMulti")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr, byte**, uint*, void> NT_StartClientMultiFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports)
        {
            NT_StartClientMultiFunc(inst, count, server_names, ports);
        }


        [NativeFunctionPointer("NT_StartClientTeam")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, uint, void> NT_StartClientTeamFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientTeam(NtInst inst, uint team, uint port)
        {
            NT_StartClientTeamFunc(inst, team, port);
        }


        [NativeFunctionPointer("NT_StopClient")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopClient(NtInst inst)
        {
            NT_StopClientFunc(inst);
        }


        [NativeFunctionPointer("NT_SetServer")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, uint, void> NT_SetServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServer(NtInst inst, byte* server_name, uint port)
        {
            NT_SetServerFunc(inst, server_name, port);
        }


        [NativeFunctionPointer("NT_SetServerMulti")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr, byte**, uint*, void> NT_SetServerMultiFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServerMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports)
        {
            NT_SetServerMultiFunc(inst, count, server_names, ports);
        }


        [NativeFunctionPointer("NT_SetServerTeam")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, uint, void> NT_SetServerTeamFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServerTeam(NtInst inst, uint team, uint port)
        {
            NT_SetServerTeamFunc(inst, team, port);
        }


        [NativeFunctionPointer("NT_StartDSClient")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, void> NT_StartDSClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartDSClient(NtInst inst, uint port)
        {
            NT_StartDSClientFunc(inst, port);
        }


        [NativeFunctionPointer("NT_StopDSClient")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopDSClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopDSClient(NtInst inst)
        {
            NT_StopDSClientFunc(inst);
        }


        [NativeFunctionPointer("NT_SetUpdateRate")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, double, void> NT_SetUpdateRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetUpdateRate(NtInst inst, double interval)
        {
            NT_SetUpdateRateFunc(inst, interval);
        }


        [NativeFunctionPointer("NT_Flush")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_FlushFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_Flush(NtInst inst)
        {
            NT_FlushFunc(inst);
        }


        [NativeFunctionPointer("NT_GetConnections")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr*, NtConnectionInfo*> NT_GetConnectionsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionInfo* NT_GetConnections(NtInst inst, UIntPtr* count)
        {
            return NT_GetConnectionsFunc(inst, count);
        }


        [NativeFunctionPointer("NT_IsConnected")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, NtBool> NT_IsConnectedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_IsConnected(NtInst inst)
        {
            return NT_IsConnectedFunc(inst);
        }


        [NativeFunctionPointer("NT_SavePersistent")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*> NT_SavePersistentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_SavePersistent(NtInst inst, byte* filename)
        {
            return NT_SavePersistentFunc(inst, filename);
        }


        [NativeFunctionPointer("NT_LoadPersistent")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void>, byte*> NT_LoadPersistentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_LoadPersistent(NtInst inst, byte* filename, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc)
        {
            return NT_LoadPersistentFunc(inst, filename, warnFunc);
        }


        [NativeFunctionPointer("NT_SaveEntries")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, UIntPtr, byte*> NT_SaveEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_SaveEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len)
        {
            return NT_SaveEntriesFunc(inst, filename, prefix, prefix_len);
        }


        [NativeFunctionPointer("NT_LoadEntries")]
        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, UIntPtr, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void>, byte*> NT_LoadEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_LoadEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc)
        {
            return NT_LoadEntriesFunc(inst, filename, prefix, prefix_len, warnFunc);
        }


        [NativeFunctionPointer("NT_DisposeValue")]
        private readonly delegate* unmanaged[Cdecl]<NtValue*, void> NT_DisposeValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeValue(NtValue* value)
        {
            NT_DisposeValueFunc(value);
        }


        [NativeFunctionPointer("NT_InitValue")]
        private readonly delegate* unmanaged[Cdecl]<NtValue*, void> NT_InitValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_InitValue(NtValue* value)
        {
            NT_InitValueFunc(value);
        }


        [NativeFunctionPointer("NT_DisposeString")]
        private readonly delegate* unmanaged[Cdecl]<NtString*, void> NT_DisposeStringFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeString(NtString* str)
        {
            NT_DisposeStringFunc(str);
        }


        [NativeFunctionPointer("NT_InitString")]
        private readonly delegate* unmanaged[Cdecl]<NtString*, void> NT_InitStringFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_InitString(NtString* str)
        {
            NT_InitStringFunc(str);
        }


        [NativeFunctionPointer("NT_DisposeEntryArray")]
        private readonly delegate* unmanaged[Cdecl]<NtEntry*, UIntPtr, void> NT_DisposeEntryArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryArray(NtEntry* arr, UIntPtr count)
        {
            NT_DisposeEntryArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeConnectionInfoArray")]
        private readonly delegate* unmanaged[Cdecl]<NtConnectionInfo*, UIntPtr, void> NT_DisposeConnectionInfoArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeConnectionInfoArray(NtConnectionInfo* arr, UIntPtr count)
        {
            NT_DisposeConnectionInfoArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeEntryInfoArray")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryInfo*, UIntPtr, void> NT_DisposeEntryInfoArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryInfoArray(NtEntryInfo* arr, UIntPtr count)
        {
            NT_DisposeEntryInfoArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeEntryInfo")]
        private readonly delegate* unmanaged[Cdecl]<NtEntryInfo*, void> NT_DisposeEntryInfoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryInfo(NtEntryInfo* info)
        {
            NT_DisposeEntryInfoFunc(info);
        }


        [NativeFunctionPointer("NT_DisposeRpcDefinition")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcDefinition*, void> NT_DisposeRpcDefinitionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeRpcDefinition(NtRpcDefinition* def)
        {
            NT_DisposeRpcDefinitionFunc(def);
        }


        [NativeFunctionPointer("NT_DisposeRpcAnswerArray")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcAnswer*, UIntPtr, void> NT_DisposeRpcAnswerArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeRpcAnswerArray(NtRpcAnswer* arr, UIntPtr count)
        {
            NT_DisposeRpcAnswerArrayFunc(arr, count);
        }


        [NativeFunctionPointer("NT_DisposeRpcAnswer")]
        private readonly delegate* unmanaged[Cdecl]<NtRpcAnswer*, void> NT_DisposeRpcAnswerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeRpcAnswer(NtRpcAnswer* answer)
        {
            NT_DisposeRpcAnswerFunc(answer);
        }



    }
}
