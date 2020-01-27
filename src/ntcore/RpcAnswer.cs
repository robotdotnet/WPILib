using NetworkTables.Natives;
using System;
using WPIUtil;

namespace NetworkTables
{
    public readonly ref struct RpcAnswer
    {
        public readonly NtEntry EntryHandle;
        public readonly NtRpcCall Call;
        public readonly string Name;
        public readonly ReadOnlySpan<byte> Params;
        public readonly ConnectionInfo Conn;
        private readonly Span<bool> m_wasRespondedTo;
        public NetworkTableEntry Entry => new NetworkTableEntry(m_instance, EntryHandle);
        private readonly NetworkTableInstance m_instance;

        internal unsafe RpcAnswer(NetworkTableInstance inst, in NtRpcAnswer answer, Span<bool> respondedTo)
        {
            EntryHandle = answer.entry;
            Call = answer.call;
            Name = UTF8String.ReadUTF8String(answer.name.str, answer.name.len);
            Params = new Span<byte>(answer.@params.str, (int)answer.@params.len);
            Conn = new ConnectionInfo(answer.conn);
            m_wasRespondedTo = respondedTo;
            m_instance = inst;
        }

        public bool IsValid => m_wasRespondedTo[0] == false;

        public void PostResponse(ReadOnlySpan<byte> result)
        {
            NtCore.PostRpcResponse(EntryHandle, Call, result);
            m_wasRespondedTo[0] = true;
        }
    }
}
