using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace NetworkTables.Natives
{
    public unsafe class NtCoreNative
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores

        public NtCoreNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }
            NT_DisposeEntryNotificationArrayFunc = (delegate* unmanaged[Cdecl] < NtEntryNotification *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeEntryNotificationArray");
            NT_DisposeEntryNotificationFunc = (delegate* unmanaged[Cdecl] < NtEntryNotification *, void >)loader.GetProcAddress("NT_DisposeEntryNotification");
            NT_DisposeConnectionNotificationArrayFunc = (delegate* unmanaged[Cdecl] < NtConnectionNotification *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeConnectionNotificationArray");
            NT_DisposeConnectionNotificationFunc = (delegate* unmanaged[Cdecl] < NtConnectionNotification *, void >)loader.GetProcAddress("NT_DisposeConnectionNotification");
            NT_DisposeLogMessageArrayFunc = (delegate* unmanaged[Cdecl] < NtLogMessage *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeLogMessageArray");
            NT_DisposeLogMessageFunc = (delegate* unmanaged[Cdecl] < NtLogMessage *, void >)loader.GetProcAddress("NT_DisposeLogMessage");
            NT_NowFunc = (delegate* unmanaged[Cdecl] < ulong >)loader.GetProcAddress("NT_Now");
            NT_CreateLoggerPollerFunc = (delegate* unmanaged[Cdecl] < NtInst, NtLoggerPoller >)loader.GetProcAddress("NT_CreateLoggerPoller");
            NT_DestroyLoggerPollerFunc = (delegate* unmanaged[Cdecl] < NtLoggerPoller, void >)loader.GetProcAddress("NT_DestroyLoggerPoller");
            NT_AddPolledLoggerFunc = (delegate* unmanaged[Cdecl] < NtLoggerPoller, uint, uint, NtLogger >)loader.GetProcAddress("NT_AddPolledLogger");
            NT_PollLoggerFunc = (delegate* unmanaged[Cdecl] < NtLoggerPoller, UIntPtr *, NtLogMessage *>)loader.GetProcAddress("NT_PollLogger");
            NT_PollLoggerTimeoutFunc = (delegate* unmanaged[Cdecl] < NtLoggerPoller, UIntPtr *, double, NtBool *, NtLogMessage *>)loader.GetProcAddress("NT_PollLoggerTimeout");
            NT_CancelPollLoggerFunc = (delegate* unmanaged[Cdecl] < NtLoggerPoller, void >)loader.GetProcAddress("NT_CancelPollLogger");
            NT_RemoveLoggerFunc = (delegate* unmanaged[Cdecl] < NtLogger, void >)loader.GetProcAddress("NT_RemoveLogger");
            NT_WaitForLoggerQueueFunc = (delegate* unmanaged[Cdecl] < NtInst, double, NtBool >)loader.GetProcAddress("NT_WaitForLoggerQueue");
            NT_AllocateCharArrayFunc = (delegate* unmanaged[Cdecl] < UIntPtr, byte *>)loader.GetProcAddress("NT_AllocateCharArray");
            NT_AllocateBooleanArrayFunc = (delegate* unmanaged[Cdecl] < UIntPtr, NtBool *>)loader.GetProcAddress("NT_AllocateBooleanArray");
            NT_AllocateDoubleArrayFunc = (delegate* unmanaged[Cdecl] < UIntPtr, double *>)loader.GetProcAddress("NT_AllocateDoubleArray");
            NT_AllocateStringArrayFunc = (delegate* unmanaged[Cdecl] < UIntPtr, NtString *>)loader.GetProcAddress("NT_AllocateStringArray");
            NT_FreeCharArrayFunc = (delegate* unmanaged[Cdecl] < byte *, void >)loader.GetProcAddress("NT_FreeCharArray");
            NT_FreeDoubleArrayFunc = (delegate* unmanaged[Cdecl] < double *, void >)loader.GetProcAddress("NT_FreeDoubleArray");
            NT_FreeBooleanArrayFunc = (delegate* unmanaged[Cdecl] < NtBool *, void >)loader.GetProcAddress("NT_FreeBooleanArray");
            NT_FreeStringArrayFunc = (delegate* unmanaged[Cdecl] < NtString *, UIntPtr, void >)loader.GetProcAddress("NT_FreeStringArray");
            NT_GetDefaultInstanceFunc = (delegate* unmanaged[Cdecl] < NtInst >)loader.GetProcAddress("NT_GetDefaultInstance");
            NT_CreateInstanceFunc = (delegate* unmanaged[Cdecl] < NtInst >)loader.GetProcAddress("NT_CreateInstance");
            NT_DestroyInstanceFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_DestroyInstance");
            NT_GetInstanceFromHandleFunc = (delegate* unmanaged[Cdecl] < NtHandle, NtInst >)loader.GetProcAddress("NT_GetInstanceFromHandle");
            NT_GetEntryFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, UIntPtr, NtEntry >)loader.GetProcAddress("NT_GetEntry");
            NT_GetEntriesFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, UIntPtr, uint, UIntPtr *, NtEntry *>)loader.GetProcAddress("NT_GetEntries");
            NT_GetEntryNameFunc = (delegate* unmanaged[Cdecl] < NtEntry, UIntPtr *, byte *>)loader.GetProcAddress("NT_GetEntryName");
            NT_GetEntryTypeFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtType >)loader.GetProcAddress("NT_GetEntryType");
            NT_GetEntryLastChangeFunc = (delegate* unmanaged[Cdecl] < NtEntry, ulong >)loader.GetProcAddress("NT_GetEntryLastChange");
            NT_GetEntryValueFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtValue *, void >)loader.GetProcAddress("NT_GetEntryValue");
            NT_SetDefaultEntryValueFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtValue *, NtBool >)loader.GetProcAddress("NT_SetDefaultEntryValue");
            NT_SetEntryValueFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtValue *, NtBool >)loader.GetProcAddress("NT_SetEntryValue");
            NT_SetEntryTypeValueFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtValue *, void >)loader.GetProcAddress("NT_SetEntryTypeValue");
            NT_SetEntryFlagsFunc = (delegate* unmanaged[Cdecl] < NtEntry, uint, void >)loader.GetProcAddress("NT_SetEntryFlags");
            NT_GetEntryFlagsFunc = (delegate* unmanaged[Cdecl] < NtEntry, uint >)loader.GetProcAddress("NT_GetEntryFlags");
            NT_DeleteEntryFunc = (delegate* unmanaged[Cdecl] < NtEntry, void >)loader.GetProcAddress("NT_DeleteEntry");
            NT_DeleteAllEntriesFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_DeleteAllEntries");
            NT_GetEntryInfoFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, UIntPtr, uint, UIntPtr *, NtEntryInfo *>)loader.GetProcAddress("NT_GetEntryInfo");
            NT_GetEntryInfoHandleFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtEntryInfo *, NtBool >)loader.GetProcAddress("NT_GetEntryInfoHandle");
            NT_CreateEntryListenerPollerFunc = (delegate* unmanaged[Cdecl] < NtInst, NtEntryListenerPoller >)loader.GetProcAddress("NT_CreateEntryListenerPoller");
            NT_DestroyEntryListenerPollerFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, void >)loader.GetProcAddress("NT_DestroyEntryListenerPoller");
            NT_AddPolledEntryListenerFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, byte *, UIntPtr, uint, NtEntryListener >)loader.GetProcAddress("NT_AddPolledEntryListener");
            NT_AddPolledEntryListenerSingleFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, NtEntry, uint, NtEntryListener >)loader.GetProcAddress("NT_AddPolledEntryListenerSingle");
            NT_PollEntryListenerFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, UIntPtr *, NtEntryNotification *>)loader.GetProcAddress("NT_PollEntryListener");
            NT_PollEntryListenerTimeoutFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, UIntPtr *, double, NtBool *, NtEntryNotification *>)loader.GetProcAddress("NT_PollEntryListenerTimeout");
            NT_CancelPollEntryListenerFunc = (delegate* unmanaged[Cdecl] < NtEntryListenerPoller, void >)loader.GetProcAddress("NT_CancelPollEntryListener");
            NT_RemoveEntryListenerFunc = (delegate* unmanaged[Cdecl] < NtEntryListener, void >)loader.GetProcAddress("NT_RemoveEntryListener");
            NT_WaitForEntryListenerQueueFunc = (delegate* unmanaged[Cdecl] < NtInst, double, NtBool >)loader.GetProcAddress("NT_WaitForEntryListenerQueue");
            NT_CreateConnectionListenerPollerFunc = (delegate* unmanaged[Cdecl] < NtInst, NtConnectionListenerPoller >)loader.GetProcAddress("NT_CreateConnectionListenerPoller");
            NT_DestroyConnectionListenerPollerFunc = (delegate* unmanaged[Cdecl] < NtConnectionListenerPoller, void >)loader.GetProcAddress("NT_DestroyConnectionListenerPoller");
            NT_AddPolledConnectionListenerFunc = (delegate* unmanaged[Cdecl] < NtConnectionListenerPoller, NtBool, NtConnectionListener >)loader.GetProcAddress("NT_AddPolledConnectionListener");
            NT_PollConnectionListenerFunc = (delegate* unmanaged[Cdecl] < NtConnectionListenerPoller, UIntPtr *, NtConnectionNotification *>)loader.GetProcAddress("NT_PollConnectionListener");
            NT_PollConnectionListenerTimeoutFunc = (delegate* unmanaged[Cdecl] < NtConnectionListenerPoller, UIntPtr *, double, NtBool *, NtConnectionNotification *>)loader.GetProcAddress("NT_PollConnectionListenerTimeout");
            NT_CancelPollConnectionListenerFunc = (delegate* unmanaged[Cdecl] < NtConnectionListenerPoller, void >)loader.GetProcAddress("NT_CancelPollConnectionListener");
            NT_RemoveConnectionListenerFunc = (delegate* unmanaged[Cdecl] < NtConnectionListener, void >)loader.GetProcAddress("NT_RemoveConnectionListener");
            NT_WaitForConnectionListenerQueueFunc = (delegate* unmanaged[Cdecl] < NtInst, double, NtBool >)loader.GetProcAddress("NT_WaitForConnectionListenerQueue");
            NT_CreateRpcCallPollerFunc = (delegate* unmanaged[Cdecl] < NtInst, NtRpcCallPoller >)loader.GetProcAddress("NT_CreateRpcCallPoller");
            NT_DestroyRpcCallPollerFunc = (delegate* unmanaged[Cdecl] < NtRpcCallPoller, void >)loader.GetProcAddress("NT_DestroyRpcCallPoller");
            NT_CreatePolledRpcFunc = (delegate* unmanaged[Cdecl] < NtEntry, byte *, UIntPtr, NtRpcCallPoller, void >)loader.GetProcAddress("NT_CreatePolledRpc");
            NT_PollRpcFunc = (delegate* unmanaged[Cdecl] < NtRpcCallPoller, UIntPtr *, NtRpcAnswer *>)loader.GetProcAddress("NT_PollRpc");
            NT_PollRpcTimeoutFunc = (delegate* unmanaged[Cdecl] < NtRpcCallPoller, UIntPtr *, double, NtBool *, NtRpcAnswer *>)loader.GetProcAddress("NT_PollRpcTimeout");
            NT_CancelPollRpcFunc = (delegate* unmanaged[Cdecl] < NtRpcCallPoller, void >)loader.GetProcAddress("NT_CancelPollRpc");
            NT_WaitForRpcCallQueueFunc = (delegate* unmanaged[Cdecl] < NtInst, double, NtBool >)loader.GetProcAddress("NT_WaitForRpcCallQueue");
            NT_PostRpcResponseFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtRpcCall, byte *, UIntPtr, void >)loader.GetProcAddress("NT_PostRpcResponse");
            NT_CallRpcFunc = (delegate* unmanaged[Cdecl] < NtEntry, byte *, UIntPtr, NtRpcCall >)loader.GetProcAddress("NT_CallRpc");
            NT_GetRpcResultFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtRpcCall, UIntPtr *, byte *>)loader.GetProcAddress("NT_GetRpcResult");
            NT_GetRpcResultTimeoutFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtRpcCall, UIntPtr *, double, NtBool *, byte *>)loader.GetProcAddress("NT_GetRpcResultTimeout");
            NT_CancelRpcResultFunc = (delegate* unmanaged[Cdecl] < NtEntry, NtRpcCall, void >)loader.GetProcAddress("NT_CancelRpcResult");
            NT_PackRpcDefinitionFunc = (delegate* unmanaged[Cdecl] < NtRpcDefinition, UIntPtr *, byte *>)loader.GetProcAddress("NT_PackRpcDefinition");
            NT_UnpackRpcDefinitionFunc = (delegate* unmanaged[Cdecl] < byte *, UIntPtr, NtRpcDefinition *, NtBool >)loader.GetProcAddress("NT_UnpackRpcDefinition");
            NT_PackRpcValuesFunc = (delegate* unmanaged[Cdecl] < NtValue * *, UIntPtr, UIntPtr *, byte *>)loader.GetProcAddress("NT_PackRpcValues");
            NT_UnpackRpcValuesFunc = (delegate* unmanaged[Cdecl] < byte *, UIntPtr, NtType *, UIntPtr, NtValue * *>)loader.GetProcAddress("NT_UnpackRpcValues");
            NT_SetNetworkIdentityFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, UIntPtr, void >)loader.GetProcAddress("NT_SetNetworkIdentity");
            NT_GetNetworkModeFunc = (delegate* unmanaged[Cdecl] < NtInst, uint >)loader.GetProcAddress("NT_GetNetworkMode");
            NT_StartServerFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, byte *, uint, void >)loader.GetProcAddress("NT_StartServer");
            NT_StopServerFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_StopServer");
            NT_StartClientNoneFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_StartClientNone");
            NT_StartClientFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, uint, void >)loader.GetProcAddress("NT_StartClient");
            NT_StartClientMultiFunc = (delegate* unmanaged[Cdecl] < NtInst, UIntPtr, byte * *, uint *, void >)loader.GetProcAddress("NT_StartClientMulti");
            NT_StartClientTeamFunc = (delegate* unmanaged[Cdecl] < NtInst, uint, uint, void >)loader.GetProcAddress("NT_StartClientTeam");
            NT_StopClientFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_StopClient");
            NT_SetServerFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, uint, void >)loader.GetProcAddress("NT_SetServer");
            NT_SetServerMultiFunc = (delegate* unmanaged[Cdecl] < NtInst, UIntPtr, byte * *, uint *, void >)loader.GetProcAddress("NT_SetServerMulti");
            NT_SetServerTeamFunc = (delegate* unmanaged[Cdecl] < NtInst, uint, uint, void >)loader.GetProcAddress("NT_SetServerTeam");
            NT_StartDSClientFunc = (delegate* unmanaged[Cdecl] < NtInst, uint, void >)loader.GetProcAddress("NT_StartDSClient");
            NT_StopDSClientFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_StopDSClient");
            NT_SetUpdateRateFunc = (delegate* unmanaged[Cdecl] < NtInst, double, void >)loader.GetProcAddress("NT_SetUpdateRate");
            NT_FlushFunc = (delegate* unmanaged[Cdecl] < NtInst, void >)loader.GetProcAddress("NT_Flush");
            NT_GetConnectionsFunc = (delegate* unmanaged[Cdecl] < NtInst, UIntPtr *, NtConnectionInfo *>)loader.GetProcAddress("NT_GetConnections");
            NT_IsConnectedFunc = (delegate* unmanaged[Cdecl] < NtInst, NtBool >)loader.GetProcAddress("NT_IsConnected");
            NT_SavePersistentFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, byte *>)loader.GetProcAddress("NT_SavePersistent");
            NT_LoadPersistentFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, delegate* unmanaged[Cdecl] < UIntPtr, byte *, void >, byte *>)loader.GetProcAddress("NT_LoadPersistent");
            NT_SaveEntriesFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, byte *, UIntPtr, byte *>)loader.GetProcAddress("NT_SaveEntries");
            NT_LoadEntriesFunc = (delegate* unmanaged[Cdecl] < NtInst, byte *, byte *, UIntPtr, delegate* unmanaged[Cdecl] < UIntPtr, byte *, void >, byte *>)loader.GetProcAddress("NT_LoadEntries");
            NT_DisposeValueFunc = (delegate* unmanaged[Cdecl] < NtValue *, void >)loader.GetProcAddress("NT_DisposeValue");
            NT_InitValueFunc = (delegate* unmanaged[Cdecl] < NtValue *, void >)loader.GetProcAddress("NT_InitValue");
            NT_DisposeStringFunc = (delegate* unmanaged[Cdecl] < NtString *, void >)loader.GetProcAddress("NT_DisposeString");
            NT_InitStringFunc = (delegate* unmanaged[Cdecl] < NtString *, void >)loader.GetProcAddress("NT_InitString");
            NT_DisposeEntryArrayFunc = (delegate* unmanaged[Cdecl] < NtEntry *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeEntryArray");
            NT_DisposeConnectionInfoArrayFunc = (delegate* unmanaged[Cdecl] < NtConnectionInfo *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeConnectionInfoArray");
            NT_DisposeEntryInfoArrayFunc = (delegate* unmanaged[Cdecl] < NtEntryInfo *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeEntryInfoArray");
            NT_DisposeEntryInfoFunc = (delegate* unmanaged[Cdecl] < NtEntryInfo *, void >)loader.GetProcAddress("NT_DisposeEntryInfo");
            NT_DisposeRpcDefinitionFunc = (delegate* unmanaged[Cdecl] < NtRpcDefinition *, void >)loader.GetProcAddress("NT_DisposeRpcDefinition");
            NT_DisposeRpcAnswerArrayFunc = (delegate* unmanaged[Cdecl] < NtRpcAnswer *, UIntPtr, void >)loader.GetProcAddress("NT_DisposeRpcAnswerArray");
            NT_DisposeRpcAnswerFunc = (delegate* unmanaged[Cdecl] < NtRpcAnswer *, void >)loader.GetProcAddress("NT_DisposeRpcAnswer");
        }


        private readonly delegate* unmanaged[Cdecl]<NtEntryNotification*, UIntPtr, void> NT_DisposeEntryNotificationArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryNotificationArray(NtEntryNotification* arr, UIntPtr count)
        {
            NT_DisposeEntryNotificationArrayFunc(arr, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryNotification*, void> NT_DisposeEntryNotificationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeEntryNotification(NtEntryNotification* info)
        {
            NT_DisposeEntryNotificationFunc(info);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionNotification*, UIntPtr, void> NT_DisposeConnectionNotificationArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeConnectionNotificationArray(NtConnectionNotification* arr, UIntPtr count)
        {
            NT_DisposeConnectionNotificationArrayFunc(arr, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionNotification*, void> NT_DisposeConnectionNotificationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeConnectionNotification(NtConnectionNotification* info)
        {
            NT_DisposeConnectionNotificationFunc(info);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLogMessage*, UIntPtr, void> NT_DisposeLogMessageArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeLogMessageArray(NtLogMessage* arr, UIntPtr count)
        {
            NT_DisposeLogMessageArrayFunc(arr, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLogMessage*, void> NT_DisposeLogMessageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DisposeLogMessage(NtLogMessage* info)
        {
            NT_DisposeLogMessageFunc(info);
        }



        private readonly delegate* unmanaged[Cdecl]<ulong> NT_NowFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NT_Now()
        {
            return NT_NowFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, NtLoggerPoller> NT_CreateLoggerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLoggerPoller NT_CreateLoggerPoller(NtInst inst)
        {
            return NT_CreateLoggerPollerFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, void> NT_DestroyLoggerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyLoggerPoller(NtLoggerPoller poller)
        {
            NT_DestroyLoggerPollerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, uint, uint, NtLogger> NT_AddPolledLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogger NT_AddPolledLogger(NtLoggerPoller poller, uint min_level, uint max_level)
        {
            return NT_AddPolledLoggerFunc(poller, min_level, max_level);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, UIntPtr*, NtLogMessage*> NT_PollLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogMessage* NT_PollLogger(NtLoggerPoller poller, UIntPtr* len)
        {
            return NT_PollLoggerFunc(poller, len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, UIntPtr*, double, NtBool*, NtLogMessage*> NT_PollLoggerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogMessage* NT_PollLoggerTimeout(NtLoggerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollLoggerTimeoutFunc(poller, len, timeout, timed_out);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLoggerPoller, void> NT_CancelPollLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollLogger(NtLoggerPoller poller)
        {
            NT_CancelPollLoggerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtLogger, void> NT_RemoveLoggerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveLogger(NtLogger logger)
        {
            NT_RemoveLoggerFunc(logger);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForLoggerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForLoggerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForLoggerQueueFunc(inst, timeout);
        }



        private readonly delegate* unmanaged[Cdecl]<UIntPtr, byte*> NT_AllocateCharArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_AllocateCharArray(UIntPtr size)
        {
            return NT_AllocateCharArrayFunc(size);
        }



        private readonly delegate* unmanaged[Cdecl]<UIntPtr, NtBool*> NT_AllocateBooleanArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool* NT_AllocateBooleanArray(UIntPtr size)
        {
            return NT_AllocateBooleanArrayFunc(size);
        }



        private readonly delegate* unmanaged[Cdecl]<UIntPtr, double*> NT_AllocateDoubleArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double* NT_AllocateDoubleArray(UIntPtr size)
        {
            return NT_AllocateDoubleArrayFunc(size);
        }



        private readonly delegate* unmanaged[Cdecl]<UIntPtr, NtString*> NT_AllocateStringArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtString* NT_AllocateStringArray(UIntPtr size)
        {
            return NT_AllocateStringArrayFunc(size);
        }



        private readonly delegate* unmanaged[Cdecl]<byte*, void> NT_FreeCharArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeCharArray(byte* v_char)
        {
            NT_FreeCharArrayFunc(v_char);
        }



        private readonly delegate* unmanaged[Cdecl]<double*, void> NT_FreeDoubleArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeDoubleArray(double* v_double)
        {
            NT_FreeDoubleArrayFunc(v_double);
        }



        private readonly delegate* unmanaged[Cdecl]<NtBool*, void> NT_FreeBooleanArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeBooleanArray(NtBool* v_boolean)
        {
            NT_FreeBooleanArrayFunc(v_boolean);
        }



        private readonly delegate* unmanaged[Cdecl]<NtString*, UIntPtr, void> NT_FreeStringArrayFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_FreeStringArray(NtString* v_string, UIntPtr arr_size)
        {
            NT_FreeStringArrayFunc(v_string, arr_size);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst> NT_GetDefaultInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_GetDefaultInstance()
        {
            return NT_GetDefaultInstanceFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst> NT_CreateInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_CreateInstance()
        {
            return NT_CreateInstanceFunc();
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_DestroyInstanceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyInstance(NtInst inst)
        {
            NT_DestroyInstanceFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtHandle, NtInst> NT_GetInstanceFromHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst NT_GetInstanceFromHandle(NtHandle handle)
        {
            return NT_GetInstanceFromHandleFunc(handle);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, NtEntry> NT_GetEntryFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntry NT_GetEntry(NtInst inst, byte* name, UIntPtr name_len)
        {
            return NT_GetEntryFunc(inst, name, name_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, uint, UIntPtr*, NtEntry*> NT_GetEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntry* NT_GetEntries(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count)
        {
            return NT_GetEntriesFunc(inst, prefix, prefix_len, types, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, UIntPtr*, byte*> NT_GetEntryNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetEntryName(NtEntry entry, UIntPtr* name_len)
        {
            return NT_GetEntryNameFunc(entry, name_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtType> NT_GetEntryTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtType NT_GetEntryType(NtEntry entry)
        {
            return NT_GetEntryTypeFunc(entry);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, ulong> NT_GetEntryLastChangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong NT_GetEntryLastChange(NtEntry entry)
        {
            return NT_GetEntryLastChangeFunc(entry);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, void> NT_GetEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_GetEntryValue(NtEntry entry, NtValue* value)
        {
            NT_GetEntryValueFunc(entry, value);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, NtBool> NT_SetDefaultEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_SetDefaultEntryValue(NtEntry entry, NtValue* default_value)
        {
            return NT_SetDefaultEntryValueFunc(entry, default_value);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, NtBool> NT_SetEntryValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_SetEntryValue(NtEntry entry, NtValue* value)
        {
            return NT_SetEntryValueFunc(entry, value);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtValue*, void> NT_SetEntryTypeValueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetEntryTypeValue(NtEntry entry, NtValue* value)
        {
            NT_SetEntryTypeValueFunc(entry, value);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, uint, void> NT_SetEntryFlagsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetEntryFlags(NtEntry entry, uint flags)
        {
            NT_SetEntryFlagsFunc(entry, flags);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, uint> NT_GetEntryFlagsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NT_GetEntryFlags(NtEntry entry)
        {
            return NT_GetEntryFlagsFunc(entry);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, void> NT_DeleteEntryFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DeleteEntry(NtEntry entry)
        {
            NT_DeleteEntryFunc(entry);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_DeleteAllEntriesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DeleteAllEntries(NtInst inst)
        {
            NT_DeleteAllEntriesFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, uint, UIntPtr*, NtEntryInfo*> NT_GetEntryInfoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryInfo* NT_GetEntryInfo(NtInst inst, byte* prefix, UIntPtr prefix_len, uint types, UIntPtr* count)
        {
            return NT_GetEntryInfoFunc(inst, prefix, prefix_len, types, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtEntryInfo*, NtBool> NT_GetEntryInfoHandleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_GetEntryInfoHandle(NtEntry entry, NtEntryInfo* info)
        {
            return NT_GetEntryInfoHandleFunc(entry, info);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, NtEntryListenerPoller> NT_CreateEntryListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListenerPoller NT_CreateEntryListenerPoller(NtInst inst)
        {
            return NT_CreateEntryListenerPollerFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, void> NT_DestroyEntryListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyEntryListenerPoller(NtEntryListenerPoller poller)
        {
            NT_DestroyEntryListenerPollerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, byte*, UIntPtr, uint, NtEntryListener> NT_AddPolledEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListener NT_AddPolledEntryListener(NtEntryListenerPoller poller, byte* prefix, UIntPtr prefix_len, uint flags)
        {
            return NT_AddPolledEntryListenerFunc(poller, prefix, prefix_len, flags);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, NtEntry, uint, NtEntryListener> NT_AddPolledEntryListenerSingleFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListener NT_AddPolledEntryListenerSingle(NtEntryListenerPoller poller, NtEntry entry, uint flags)
        {
            return NT_AddPolledEntryListenerSingleFunc(poller, entry, flags);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, UIntPtr*, NtEntryNotification*> NT_PollEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryNotification* NT_PollEntryListener(NtEntryListenerPoller poller, UIntPtr* len)
        {
            return NT_PollEntryListenerFunc(poller, len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, UIntPtr*, double, NtBool*, NtEntryNotification*> NT_PollEntryListenerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryNotification* NT_PollEntryListenerTimeout(NtEntryListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollEntryListenerTimeoutFunc(poller, len, timeout, timed_out);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListenerPoller, void> NT_CancelPollEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollEntryListener(NtEntryListenerPoller poller)
        {
            NT_CancelPollEntryListenerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntryListener, void> NT_RemoveEntryListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveEntryListener(NtEntryListener entry_listener)
        {
            NT_RemoveEntryListenerFunc(entry_listener);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForEntryListenerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForEntryListenerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForEntryListenerQueueFunc(inst, timeout);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, NtConnectionListenerPoller> NT_CreateConnectionListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListenerPoller NT_CreateConnectionListenerPoller(NtInst inst)
        {
            return NT_CreateConnectionListenerPollerFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, void> NT_DestroyConnectionListenerPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyConnectionListenerPoller(NtConnectionListenerPoller poller)
        {
            NT_DestroyConnectionListenerPollerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, NtBool, NtConnectionListener> NT_AddPolledConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListener NT_AddPolledConnectionListener(NtConnectionListenerPoller poller, NtBool immediate_notify)
        {
            return NT_AddPolledConnectionListenerFunc(poller, immediate_notify);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, UIntPtr*, NtConnectionNotification*> NT_PollConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionNotification* NT_PollConnectionListener(NtConnectionListenerPoller poller, UIntPtr* len)
        {
            return NT_PollConnectionListenerFunc(poller, len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, UIntPtr*, double, NtBool*, NtConnectionNotification*> NT_PollConnectionListenerTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionNotification* NT_PollConnectionListenerTimeout(NtConnectionListenerPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollConnectionListenerTimeoutFunc(poller, len, timeout, timed_out);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListenerPoller, void> NT_CancelPollConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollConnectionListener(NtConnectionListenerPoller poller)
        {
            NT_CancelPollConnectionListenerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtConnectionListener, void> NT_RemoveConnectionListenerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_RemoveConnectionListener(NtConnectionListener conn_listener)
        {
            NT_RemoveConnectionListenerFunc(conn_listener);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForConnectionListenerQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForConnectionListenerQueue(NtInst inst, double timeout)
        {
            return NT_WaitForConnectionListenerQueueFunc(inst, timeout);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, NtRpcCallPoller> NT_CreateRpcCallPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCallPoller NT_CreateRpcCallPoller(NtInst inst)
        {
            return NT_CreateRpcCallPollerFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, void> NT_DestroyRpcCallPollerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_DestroyRpcCallPoller(NtRpcCallPoller poller)
        {
            NT_DestroyRpcCallPollerFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, byte*, UIntPtr, NtRpcCallPoller, void> NT_CreatePolledRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CreatePolledRpc(NtEntry entry, byte* def, UIntPtr def_len, NtRpcCallPoller poller)
        {
            NT_CreatePolledRpcFunc(entry, def, def_len, poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, UIntPtr*, NtRpcAnswer*> NT_PollRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcAnswer* NT_PollRpc(NtRpcCallPoller poller, UIntPtr* len)
        {
            return NT_PollRpcFunc(poller, len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, UIntPtr*, double, NtBool*, NtRpcAnswer*> NT_PollRpcTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcAnswer* NT_PollRpcTimeout(NtRpcCallPoller poller, UIntPtr* len, double timeout, NtBool* timed_out)
        {
            return NT_PollRpcTimeoutFunc(poller, len, timeout, timed_out);
        }



        private readonly delegate* unmanaged[Cdecl]<NtRpcCallPoller, void> NT_CancelPollRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelPollRpc(NtRpcCallPoller poller)
        {
            NT_CancelPollRpcFunc(poller);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, double, NtBool> NT_WaitForRpcCallQueueFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_WaitForRpcCallQueue(NtInst inst, double timeout)
        {
            return NT_WaitForRpcCallQueueFunc(inst, timeout);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, byte*, UIntPtr, void> NT_PostRpcResponseFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_PostRpcResponse(NtEntry entry, NtRpcCall rpccall, byte* result, UIntPtr result_len)
        {
            NT_PostRpcResponseFunc(entry, rpccall, result, result_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, byte*, UIntPtr, NtRpcCall> NT_CallRpcFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCall NT_CallRpc(NtEntry entry, byte* callparams, UIntPtr params_len)
        {
            return NT_CallRpcFunc(entry, callparams, params_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, UIntPtr*, byte*> NT_GetRpcResultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetRpcResult(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len)
        {
            return NT_GetRpcResultFunc(entry, rpccall, result_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, UIntPtr*, double, NtBool*, byte*> NT_GetRpcResultTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_GetRpcResultTimeout(NtEntry entry, NtRpcCall rpccall, UIntPtr* result_len, double timeout, NtBool* timed_out)
        {
            return NT_GetRpcResultTimeoutFunc(entry, rpccall, result_len, timeout, timed_out);
        }



        private readonly delegate* unmanaged[Cdecl]<NtEntry, NtRpcCall, void> NT_CancelRpcResultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_CancelRpcResult(NtEntry entry, NtRpcCall rpccall)
        {
            NT_CancelRpcResultFunc(entry, rpccall);
        }



        private readonly delegate* unmanaged[Cdecl]<NtRpcDefinition, UIntPtr*, byte*> NT_PackRpcDefinitionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_PackRpcDefinition(NtRpcDefinition def, UIntPtr* packed_len)
        {
            return NT_PackRpcDefinitionFunc(def, packed_len);
        }



        private readonly delegate* unmanaged[Cdecl]<byte*, UIntPtr, NtRpcDefinition*, NtBool> NT_UnpackRpcDefinitionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_UnpackRpcDefinition(byte* packed, UIntPtr packed_len, NtRpcDefinition* def)
        {
            return NT_UnpackRpcDefinitionFunc(packed, packed_len, def);
        }



        private readonly delegate* unmanaged[Cdecl]<NtValue**, UIntPtr, UIntPtr*, byte*> NT_PackRpcValuesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_PackRpcValues(NtValue** values, UIntPtr values_len, UIntPtr* packed_len)
        {
            return NT_PackRpcValuesFunc(values, values_len, packed_len);
        }



        private readonly delegate* unmanaged[Cdecl]<byte*, UIntPtr, NtType*, UIntPtr, NtValue**> NT_UnpackRpcValuesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtValue** NT_UnpackRpcValues(byte* packed, UIntPtr packed_len, NtType* types, UIntPtr types_len)
        {
            return NT_UnpackRpcValuesFunc(packed, packed_len, types, types_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, UIntPtr, void> NT_SetNetworkIdentityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetNetworkIdentity(NtInst inst, byte* name, UIntPtr name_len)
        {
            NT_SetNetworkIdentityFunc(inst, name, name_len);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, uint> NT_GetNetworkModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint NT_GetNetworkMode(NtInst inst)
        {
            return NT_GetNetworkModeFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, uint, void> NT_StartServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartServer(NtInst inst, byte* persist_filename, byte* listen_address, uint port)
        {
            NT_StartServerFunc(inst, persist_filename, listen_address, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopServer(NtInst inst)
        {
            NT_StopServerFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StartClientNoneFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientNone(NtInst inst)
        {
            NT_StartClientNoneFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, uint, void> NT_StartClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClient(NtInst inst, byte* server_name, uint port)
        {
            NT_StartClientFunc(inst, server_name, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr, byte**, uint*, void> NT_StartClientMultiFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports)
        {
            NT_StartClientMultiFunc(inst, count, server_names, ports);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, uint, void> NT_StartClientTeamFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartClientTeam(NtInst inst, uint team, uint port)
        {
            NT_StartClientTeamFunc(inst, team, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopClient(NtInst inst)
        {
            NT_StopClientFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, uint, void> NT_SetServerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServer(NtInst inst, byte* server_name, uint port)
        {
            NT_SetServerFunc(inst, server_name, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr, byte**, uint*, void> NT_SetServerMultiFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServerMulti(NtInst inst, UIntPtr count, byte** server_names, uint* ports)
        {
            NT_SetServerMultiFunc(inst, count, server_names, ports);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, uint, void> NT_SetServerTeamFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetServerTeam(NtInst inst, uint team, uint port)
        {
            NT_SetServerTeamFunc(inst, team, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, uint, void> NT_StartDSClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StartDSClient(NtInst inst, uint port)
        {
            NT_StartDSClientFunc(inst, port);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_StopDSClientFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_StopDSClient(NtInst inst)
        {
            NT_StopDSClientFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, double, void> NT_SetUpdateRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_SetUpdateRate(NtInst inst, double interval)
        {
            NT_SetUpdateRateFunc(inst, interval);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, void> NT_FlushFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void NT_Flush(NtInst inst)
        {
            NT_FlushFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, UIntPtr*, NtConnectionInfo*> NT_GetConnectionsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionInfo* NT_GetConnections(NtInst inst, UIntPtr* count)
        {
            return NT_GetConnectionsFunc(inst, count);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, NtBool> NT_IsConnectedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtBool NT_IsConnected(NtInst inst)
        {
            return NT_IsConnectedFunc(inst);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*> NT_SavePersistentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_SavePersistent(NtInst inst, byte* filename)
        {
            return NT_SavePersistentFunc(inst, filename);
        }



        private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void>, byte*> NT_LoadPersistentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* NT_LoadPersistent(NtInst inst, byte* filename, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc)
        {
            return NT_LoadPersistentFunc(inst, filename, warnFunc);
    }



    private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, UIntPtr, byte*> NT_SaveEntriesFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte* NT_SaveEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len)
    {
        return NT_SaveEntriesFunc(inst, filename, prefix, prefix_len);
    }



    private readonly delegate* unmanaged[Cdecl]<NtInst, byte*, byte*, UIntPtr, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void>, byte*> NT_LoadEntriesFunc;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte* NT_LoadEntries(NtInst inst, byte* filename, byte* prefix, UIntPtr prefix_len, delegate* unmanaged[Cdecl]<UIntPtr, byte*, void> warnFunc)
        {
            return NT_LoadEntriesFunc(inst, filename, prefix, prefix_len, warnFunc);
}



private readonly delegate* unmanaged[Cdecl]<NtValue*, void> NT_DisposeValueFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeValue(NtValue* value)
{
    NT_DisposeValueFunc(value);
}



private readonly delegate* unmanaged[Cdecl]<NtValue*, void> NT_InitValueFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_InitValue(NtValue* value)
{
    NT_InitValueFunc(value);
}



private readonly delegate* unmanaged[Cdecl]<NtString*, void> NT_DisposeStringFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeString(NtString* str)
{
    NT_DisposeStringFunc(str);
}



private readonly delegate* unmanaged[Cdecl]<NtString*, void> NT_InitStringFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_InitString(NtString* str)
{
    NT_InitStringFunc(str);
}



private readonly delegate* unmanaged[Cdecl]<NtEntry*, UIntPtr, void> NT_DisposeEntryArrayFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeEntryArray(NtEntry* arr, UIntPtr count)
{
    NT_DisposeEntryArrayFunc(arr, count);
}



private readonly delegate* unmanaged[Cdecl]<NtConnectionInfo*, UIntPtr, void> NT_DisposeConnectionInfoArrayFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeConnectionInfoArray(NtConnectionInfo* arr, UIntPtr count)
{
    NT_DisposeConnectionInfoArrayFunc(arr, count);
}



private readonly delegate* unmanaged[Cdecl]<NtEntryInfo*, UIntPtr, void> NT_DisposeEntryInfoArrayFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeEntryInfoArray(NtEntryInfo* arr, UIntPtr count)
{
    NT_DisposeEntryInfoArrayFunc(arr, count);
}



private readonly delegate* unmanaged[Cdecl]<NtEntryInfo*, void> NT_DisposeEntryInfoFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeEntryInfo(NtEntryInfo* info)
{
    NT_DisposeEntryInfoFunc(info);
}



private readonly delegate* unmanaged[Cdecl]<NtRpcDefinition*, void> NT_DisposeRpcDefinitionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeRpcDefinition(NtRpcDefinition* def)
{
    NT_DisposeRpcDefinitionFunc(def);
}



private readonly delegate* unmanaged[Cdecl]<NtRpcAnswer*, UIntPtr, void> NT_DisposeRpcAnswerArrayFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeRpcAnswerArray(NtRpcAnswer* arr, UIntPtr count)
{
    NT_DisposeRpcAnswerArrayFunc(arr, count);
}



private readonly delegate* unmanaged[Cdecl]<NtRpcAnswer*, void> NT_DisposeRpcAnswerFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void NT_DisposeRpcAnswer(NtRpcAnswer* answer)
{
    NT_DisposeRpcAnswerFunc(answer);
}



    }
}
