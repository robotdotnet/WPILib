using System;
using System.Text.Json;
using NetworkTables;
using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Geometry;
using WPIUtil.Serialization.Struct;

namespace DesktopDev;

class Program
{
    static void Main(string[] args)
    {
        Rotation2d rot = new(5.Radians());
        string serialized = JsonSerializer.Serialize(rot);
        Console.WriteLine(serialized);

        Rotation2d r = JsonSerializer.Deserialize<Rotation2d>(serialized);
        Console.WriteLine(r.Angle.Radians);

        // StructArrayTopic<TrackedTag> n = null!;
        // var subscriber = n.Subscribe([], PubSubOptions.None);
        // TrackedTag[] tags = subscriber.Get();
    }
}

public class TrackedTagStruct : IStruct<TrackedTag>
{
    public string TypeString => "struct:TrackedTag";

    public int Size => Rotation2d.Struct.Size + IStructBase.SizeInt16;

    public string Schema {get;} = $"int16 id;{Rotation2d.Struct.Schema}";

    public void Pack(ref StructPacker buffer, TrackedTag value)
    {
        var rotStruct = Rotation2d.Struct;
        buffer.Write16(value.Id);
        rotStruct.Pack(ref buffer, value.Tfr);
    }

    public TrackedTag Unpack(ref StructUnpacker buffer)
    {
        short id = buffer.Read16();
        Rotation2d rot = Rotation2d.Struct.Unpack(ref buffer);
        return new TrackedTag() {
            Id = id,
            Tfr = rot,
        };
    }
}

public struct TrackedTag : IStructSerializable<TrackedTag>
{
    public static IStruct<TrackedTag> Struct {get;} = new TrackedTagStruct();

    public Rotation2d Tfr {get;set;}
    public short Id {get;set;}
}
