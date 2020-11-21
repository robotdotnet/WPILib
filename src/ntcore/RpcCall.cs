using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkTables
{
    public readonly struct RpcCall : IDisposable, IEquatable<RpcCall>
    {
        public RpcCall(NetworkTableEntry entry, NtRpcCall call)
        {
            Handle = call;
            Entry = entry;
        }

        public NtRpcCall Handle { get; }
        public NetworkTableEntry Entry { get; }

        public bool IsValid => Handle.Get() != 0;

        public void Dispose()
        {
            if (IsValid)
            {
                CancelResult();
            }
        }

        public ReadOnlySpan<byte> GetResult()
        {
            Span<byte> store = Span<byte>.Empty;
            var result = NtCore.GetRpcResult(Entry.Handle, Handle, store);
            return result;
        }

        public ReadOnlySpan<byte> GetResult(Span<byte> store)
        {
            var result = NtCore.GetRpcResult(Entry.Handle, Handle, store);
            return result;
        }

        public ReadOnlySpan<byte> GetResult(double timeout)
        {
            Span<byte> store = Span<byte>.Empty;
            var result = NtCore.GetRpcResult(Entry.Handle, Handle, timeout, store);
            return result;
        }

        public ReadOnlySpan<byte> GetResult(double timeout, Span<byte> store)
        {
            var result = NtCore.GetRpcResult(Entry.Handle, Handle, timeout, store);
            return result;
        }

        public Task<byte[]> GetResultAsync(CancellationToken token = default)
        {
            NtEntry handle = Entry.Handle;
            NtRpcCall call = Handle;
            token.Register(() =>
            {
                NtCore.CancelRpcResult(handle, call);
            });
            return Task.Run(() =>
            {
                return NtCore.GetRpcResult(handle, call, Span<byte>.Empty).ToArray();
            });
        }

        public void CancelResult()
        {
            NtCore.CancelRpcResult(Entry.Handle, Handle);
        }

        public override bool Equals(object? obj)
        {
            return obj is RpcCall call && Equals(call);
        }

        public bool Equals(RpcCall other)
        {
            return Handle.Equals(other.Handle) &&
                   EqualityComparer<NetworkTableEntry>.Default.Equals(Entry, other.Entry);
        }

        public override int GetHashCode()
        {
            var hashCode = 121557022;
            hashCode = hashCode * -1521134295 + Handle.GetHashCode();
            hashCode = hashCode * -1521134295 + Entry.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(RpcCall left, RpcCall right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RpcCall left, RpcCall right)
        {
            return !(left == right);
        }
    }
}
