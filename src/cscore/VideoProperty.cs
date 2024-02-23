using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoProperty : IEquatable<VideoProperty?>
{
    public CsProperty Handle { get; }

    public PropertyKind Kind { get; }

    public string Name
    {
        get
        {
            CsNative.GetPropertyName(Handle, out var value);

            return value;
        }
    }

    public bool IsValid => Kind != PropertyKind.None;

    public bool IsBoolean => Kind == PropertyKind.Boolean;

    public bool IsInteger => Kind == PropertyKind.Integer;

    public bool IsString => Kind == PropertyKind.String;

    public bool IsEnum => Kind == PropertyKind.Enum;

    public int Value
    {
        get
        {
            int res = CsNative.GetProperty(Handle);

            return res;
        }
        set
        {
            CsNative.SetProperty(Handle, value);

        }
    }

    public int Min
    {
        get
        {
            int res = CsNative.GetPropertyMin(Handle);

            return res;
        }
    }

    public int Max
    {
        get
        {
            int res = CsNative.GetPropertyMax(Handle);

            return res;
        }
    }

    public int Step
    {
        get
        {
            int res = CsNative.GetPropertyStep(Handle);

            return res;
        }
    }

    public int Default
    {
        get
        {
            int res = CsNative.GetPropertyDefault(Handle);

            return res;
        }
    }

    public string StringValue
    {
        get
        {
            CsNative.GetStringProperty(Handle, out var res);

            return res;
        }
        set
        {
            CsNative.SetStringProperty(Handle, value);

        }
    }

    public string[] EnumChoices
    {
        get
        {
            string[] res = CsNative.GetEnumPropertyChoices(Handle);

            return res;
        }
    }

    public int Get()
    {
        int res = CsNative.GetProperty(Handle);

        return res;
    }

    public void Set(int value)
    {
        CsNative.SetProperty(Handle, value);
    }

    internal VideoProperty(CsProperty handle)
    {
        Handle = handle;
        Kind = CsNative.GetPropertyKind(Handle);

    }

    internal VideoProperty(CsProperty handle, PropertyKind kind)
    {
        Handle = handle;
        Kind = kind;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoProperty);
    }

    public bool Equals(VideoProperty? other)
    {
        return other is not null &&
               Handle.Equals(other.Handle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Handle);
    }

    public static bool operator ==(VideoProperty? left, VideoProperty? right)
    {
        return EqualityComparer<VideoProperty>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoProperty? left, VideoProperty? right)
    {
        return !(left == right);
    }
}
