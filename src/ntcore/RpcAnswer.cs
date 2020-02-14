using NetworkTables.Natives;
using System;
using WPIUtil;

namespace NetworkTables
{
#pragma warning disable CA1066 // Type {0} should implement IEquatable<T> because it overrides Equals
#pragma warning disable CA1815 // Override equals and operator equals on value types
    public readonly ref struct RpcAnswer
#pragma warning restore CA1815 // Override equals and operator equals on value types
#pragma warning restore CA1066 // Type {0} should implement IEquatable<T> because it overrides Equals
    {
        public readonly NtEntry EntryHandle { get; }
        public readonly NtRpcCall Call { get; }
        public readonly string Name { get; }
        public readonly ReadOnlySpan<byte> Params { get; }
        public readonly ConnectionInfo Conn { get; }
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
