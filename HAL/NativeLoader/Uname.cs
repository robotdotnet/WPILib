using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace HAL.NativeLoader
{
    internal sealed class Utsname
        : IEquatable<Utsname>
    {
        public string sysname;
        public string nodename;
        public string release;
        public string version;
        public string machine;
        public string domainname;

        public override int GetHashCode()
        {
            // ReSharper disable NonReadonlyMemberInGetHashCode
            return sysname.GetHashCode() ^ nodename.GetHashCode() ^
                release.GetHashCode() ^ version.GetHashCode() ^
                machine.GetHashCode() ^ domainname.GetHashCode();
            // ReSharper restore NonReadonlyMemberInGetHashCode
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Utsname u = (Utsname)obj;
            return Equals(u);
        }

        public bool Equals(Utsname value)
        {
            return value.sysname == sysname && value.nodename == nodename &&
                value.release == release && value.version == version &&
                value.machine == machine && value.domainname == domainname;
        }

        // Generate string in /etc/passwd format
        public override string ToString()
        {
            return $"{sysname} {nodename} {release} {version} {machine}";
        }

        public static bool operator ==(Utsname lhs, Utsname rhs)
        {
            return Object.Equals(lhs, rhs);
        }

        public static bool operator !=(Utsname lhs, Utsname rhs)
        {
            return !Object.Equals(lhs, rhs);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct _Utsname
    {
        public IntPtr sysname;
        public IntPtr nodename;
        public IntPtr release;
        public IntPtr version;
        public IntPtr machine;
        public IntPtr domainname;
        public IntPtr _buf_;
    }

    internal class Uname
    {
        private static void CopyUtsname(ref Utsname to, ref _Utsname from)
        {
            try
            {
                to = new Utsname
                {
                    sysname = Marshal.PtrToStringAnsi(from.sysname),
                    nodename = Marshal.PtrToStringAnsi(from.nodename),
                    release = Marshal.PtrToStringAnsi(from.release),
                    version = Marshal.PtrToStringAnsi(from.version),
                    machine = Marshal.PtrToStringAnsi(from.machine),
                    domainname = Marshal.PtrToStringAnsi(from.domainname)
                };
            }
            finally
            {
                free(from._buf_);
                from._buf_ = IntPtr.Zero;
            }
        }

        internal const string MPH = "MonoPosixHelper";
        internal const string LIBC = "libc";

        [DllImport(LIBC, CallingConvention = CallingConvention.Cdecl)]
        public static extern void free(IntPtr ptr);

        [DllImport(MPH, SetLastError = true,
             EntryPoint = "Mono_Posix_Syscall_uname")]
        private static extern int sys_uname(out _Utsname buf);

        public static int uname(out Utsname buf)
        {
            _Utsname _buf;
            int r = sys_uname(out _buf);
            buf = new Utsname();
            if (r == 0)
            {
                CopyUtsname(ref buf, ref _buf);
            }
            return r;
        }
    }
}
