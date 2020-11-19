using System;

namespace NetworkTables.Natives
{
    public interface INtCore
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores
        unsafe NtInst NT_GetDefaultInstance();
        unsafe NtInst NT_CreateInstance();
        unsafe void NT_DestroyInstance(NtInst inst);
        unsafe NtInst NT_GetInstanceFromHandle(NtHandle handle);
        unsafe NtEntry NT_GetEntry(NtInst inst, byte* name, UIntPtr name_len);
        unsafe NtEntry* NT_GetEntries(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count);
        unsafe byte* NT_GetEntryName(NtEntry entry, UIntPtr* name_len);




        unsafe NtType NT_GetEntryType(NtEntry entry);




        unsafe ulong NT_GetEntryLastChange(NtEntry entry);




        unsafe void NT_GetEntryValue(NtEntry entry, NtValue* value);




        unsafe NtBool NT_SetDefaultEntryValue(NtEntry entry, NtValue* default_value);




        unsafe NtBool NT_SetEntryValue(NtEntry entry, NtValue* value);




        unsafe void NT_SetEntryTypeValue(NtEntry entry, NtValue* value);




        unsafe void NT_SetEntryFlags(NtEntry entry, uint flags);




        unsafe uint NT_GetEntryFlags(NtEntry entry);




        unsafe void NT_DeleteEntry(NtEntry entry);




        unsafe void NT_DeleteAllEntries(NtInst inst);




        unsafe NtEntryInfo* NT_GetEntryInfo(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count);




        unsafe NtBool NT_GetEntryInfoHandle(NtEntry entry, NtEntryInfo* info);




        unsafe NtEntryListenerPoller NT_CreateEntryListenerPoller(NtInst inst);




        unsafe void NT_DestroyEntryListenerPoller(NtEntryListenerPoller poller);




        unsafe NtEntryListener NT_AddPolledEntryListener(NtEntryListenerPoller poller, byte* prefix, UIntPtr prefix_len, uint flags);




        unsafe NtEntryListener NT_AddPolledEntryListenerSingle(NtEntryListenerPoller poller, NtEntry entry, uint flags);




        unsafe NtEntryNotification* NT_PollEntryListener(NtEntryListenerPoller poller, UIntPtr* len);




        unsafe NtEntryNotification* NT_PollEntryListenerTimeout(NtEntryListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out);




        unsafe void NT_CancelPollEntryListener(NtEntryListenerPoller poller);




        unsafe void NT_RemoveEntryListener(NtEntryListener entry_listener);




        unsafe NtBool NT_WaitForEntryListenerQueue(NtInst inst, double timeout);




        unsafe NtConnectionListenerPoller NT_CreateConnectionListenerPoller(NtInst inst);




        unsafe void NT_DestroyConnectionListenerPoller(NtConnectionListenerPoller poller);




        unsafe NtConnectionListener NT_AddPolledConnectionListener(NtConnectionListenerPoller poller, NtBool immediate_notify);




        unsafe NtConnectionNotification* NT_PollConnectionListener(NtConnectionListenerPoller poller, UIntPtr* len);




        unsafe NtConnectionNotification* NT_PollConnectionListenerTimeout(NtConnectionListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out);




        unsafe void NT_CancelPollConnectionListener(NtConnectionListenerPoller poller);




        unsafe void NT_RemoveConnectionListener(NtConnectionListener conn_listener);




        unsafe NtBool NT_WaitForConnectionListenerQueue(NtInst inst, double timeout);




        unsafe NtRpcCallPoller NT_CreateRpcCallPoller(NtInst inst);




        unsafe void NT_DestroyRpcCallPoller(NtRpcCallPoller poller);




        unsafe void NT_CreatePolledRpc(NtEntry entry, byte* def, UIntPtr def_len, NtRpcCallPoller poller);




        unsafe NtRpcAnswer* NT_PollRpc(NtRpcCallPoller poller, UIntPtr* len);




        unsafe NtRpcAnswer* NT_PollRpcTimeout(NtRpcCallPoller poller, UIntPtr* len, double timeout, NtBool* timed_out);




        unsafe void NT_CancelPollRpc(NtRpcCallPoller poller);




        unsafe NtBool NT_WaitForRpcCallQueue(NtInst inst, double timeout);




        unsafe void NT_PostRpcResponse(NtEntry entry, NtRpcCall rpccall, byte* result, UIntPtr result_len);




        unsafe NtRpcCall NT_CallRpc(NtEntry entry, byte* callparams, UIntPtr params_len);




        unsafe byte* NT_GetRpcResult(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len);




        unsafe byte* NT_GetRpcResultTimeout(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len, double timeout, NtBool* timed_out);




        unsafe void NT_CancelRpcResult(NtEntry entry, NtRpcCall rpccall);




        unsafe byte* NT_PackRpcDefinition(NtRpcDefinition def, UIntPtr* packed_len);




        unsafe NtBool NT_UnpackRpcDefinition(byte* packed, UIntPtr packed_len, NtRpcDefinition* def);




        unsafe byte* NT_PackRpcValues(NtValue** values, UIntPtr values_len, UIntPtr* packed_len);




        unsafe NtValue** NT_UnpackRpcValues(byte* packed, UIntPtr packed_len, NtType* types, UIntPtr types_len);




        unsafe void NT_SetNetworkIdentity(NtInst inst, byte* name, UIntPtr name_len);




        unsafe uint NT_GetNetworkMode(NtInst inst);




        unsafe void NT_StartServer(NtInst inst, byte* persist_filename, byte* listen_address, uint port);




        unsafe void NT_StopServer(NtInst inst);




        unsafe void NT_StartClientNone(NtInst inst);




        unsafe void NT_StartClient(NtInst inst, byte* server_name, uint port);




        unsafe void NT_StartClientMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports);




        unsafe void NT_StartClientTeam(NtInst inst, uint team, uint port);




        unsafe void NT_StopClient(NtInst inst);




        unsafe void NT_SetServer(NtInst inst, byte* server_name, uint port);




        unsafe void NT_SetServerMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports);




        unsafe void NT_SetServerTeam(NtInst inst, uint team, uint port);




        unsafe void NT_StartDSClient(NtInst inst, uint port);




        unsafe void NT_StopDSClient(NtInst inst);




        unsafe void NT_SetUpdateRate(NtInst inst, double interval);




        unsafe void NT_Flush(NtInst inst);




        unsafe NtConnectionInfo* NT_GetConnections(NtInst inst, UIntPtr* count);




        unsafe NtBool NT_IsConnected(NtInst inst);




        unsafe byte* NT_SavePersistent(NtInst inst, byte* filename);




        unsafe byte* NT_LoadPersistent(NtInst inst, byte* filename, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc);




        unsafe byte* NT_SaveEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len);




        unsafe byte* NT_LoadEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc);




        unsafe void NT_DisposeValue(NtValue* value);




        unsafe void NT_InitValue(NtValue* value);




        unsafe void NT_DisposeString(NtString* str);




        unsafe void NT_InitString(NtString* str);




        unsafe void NT_DisposeEntryArray(NtEntry* arr, UIntPtr count);




        unsafe void NT_DisposeConnectionInfoArray(NtConnectionInfo* arr, UIntPtr count);




        unsafe void NT_DisposeEntryInfoArray(NtEntryInfo* arr, UIntPtr count);




        unsafe void NT_DisposeEntryInfo(NtEntryInfo* info);




        unsafe void NT_DisposeRpcDefinition(NtRpcDefinition* def);




        unsafe void NT_DisposeRpcAnswerArray(NtRpcAnswer* arr, UIntPtr count);




        unsafe void NT_DisposeRpcAnswer(NtRpcAnswer* answer);




        unsafe void NT_DisposeEntryNotificationArray(NtEntryNotification* arr, UIntPtr count);




        unsafe void NT_DisposeEntryNotification(NtEntryNotification* info);




        unsafe void NT_DisposeConnectionNotificationArray(NtConnectionNotification* arr, UIntPtr count);




        unsafe void NT_DisposeConnectionNotification(NtConnectionNotification* info);




        unsafe void NT_DisposeLogMessageArray(NtLogMessage* arr, UIntPtr count);




        unsafe void NT_DisposeLogMessage(NtLogMessage* info);




        unsafe ulong NT_Now();




        unsafe NtLoggerPoller NT_CreateLoggerPoller(NtInst inst);




        unsafe void NT_DestroyLoggerPoller(NtLoggerPoller poller);




        unsafe NtLogger NT_AddPolledLogger(NtLoggerPoller poller, uint min_level, uint max_level);




        unsafe NtLogMessage* NT_PollLogger(NtLoggerPoller poller, UIntPtr* len);




        unsafe NtLogMessage* NT_PollLoggerTimeout(NtLoggerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out);




        unsafe void NT_CancelPollLogger(NtLoggerPoller poller);




        unsafe void NT_RemoveLogger(NtLogger logger);




        unsafe NtBool NT_WaitForLoggerQueue(NtInst inst, double timeout);




        unsafe byte* NT_AllocateCharArray(UIntPtr size);




        unsafe NtBool* NT_AllocateBooleanArray(UIntPtr size);




        unsafe double* NT_AllocateDoubleArray(UIntPtr size);




        unsafe NtString* NT_AllocateStringArray(UIntPtr size);




        unsafe void NT_FreeCharArray(byte* v_char);




        unsafe void NT_FreeDoubleArray(double* v_double);




        unsafe void NT_FreeBooleanArray(NtBool* v_boolean);




        unsafe void NT_FreeStringArray(NtString* v_string, UIntPtr arr_size);


    }
}

