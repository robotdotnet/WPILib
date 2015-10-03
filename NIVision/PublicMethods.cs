using System;

namespace NIVision
{
    public abstract class OpaquePointer : IDisposable
    {
        private bool owned;

        protected OpaquePointer()
        {
            NativeObj = IntPtr.Zero;
            owned = false;
        }

        protected OpaquePointer(IntPtr obj, bool owned)
        {
            NativeObj = obj;
            this.owned = owned;
        }

        public void Dispose()
        {
            if (owned)
            {
                Interop.imaqDispose(NativeObj);
                NativeObj = IntPtr.Zero;
                owned = false;
            }
            GC.SuppressFinalize(this);
        }

        ~OpaquePointer()
        {
            Dispose();
        }

        public IntPtr NativeObj { get; private set; }
    }

    public class RawData : IDisposable
    {
        public IntPtr Data;
        public uint Size;
        private bool owned;

        public RawData()
        {
            owned = false;
        }

        public RawData(IntPtr data, uint size, bool owned = false)
        {
            Data = data;
            Size = size;
            this.owned = owned;
        }

        public void Dispose()
        {
            if (owned)
            {
                Interop.imaqDispose(Data);
                owned = false;
                Data = IntPtr.Zero;
            }
        }
    }
    
    public class Image : OpaquePointer
    {
        internal Image()
        {
        }

        internal Image(IntPtr obj, bool owned) : base(obj, owned)
        {
        }
    }

    public static class PublicMethods
    {
        public static RawData ImaqFlatten(Image image, FlattenType type, CompressionType compression, int quality)
        {
            uint size = 0;
            IntPtr rv = Interop.imaqFlatten(image.NativeObj, type, compression, quality, ref size);

            return new RawData(rv, size, true);
        }

        public static Image ImaqCreateImage(ImageType type, int borderSize)
        {
            IntPtr rv = Interop.imaqCreateImage(type, borderSize);
            return new Image(rv, true);
        }
    }
}
