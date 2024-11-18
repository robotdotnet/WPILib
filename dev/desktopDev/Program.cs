using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using UnitsNet.NumberExtensions.NumberToAngle;
using WPIHal.Natives.Simulation;
using WPIMath.Geometry;
using WPIUtil.Serialization.Struct;

namespace DesktopDev;

class Program
{


    static void Main(string[] args)
    {
        var arr = typeof(HalAccelerometerData).Assembly.GetTypes().Where(x => x.Name.StartsWith("Hal") && x.Name.EndsWith("Data")).ToArray();

        foreach (var item in arr)
        {
            var nameWithoutHal = item.Name[3..];
            var nameWithoutData = nameWithoutHal[..^4];
            Console.WriteLine(nameWithoutData);
            var file2 = $@"C:\Users\thadh\Documents\GitHub\robotdotnet\WPILib\src\hal\Natives\Simulation\{item.Name}.cs";
            var txt = File.ReadAllText(file2);
            foreach (var func in item.GetMethods())
            {
                var funcName = func.Name.Replace(nameWithoutData, "");

                txt = txt.Replace($"{func.Name}(", $"{funcName}(");
                //Console.WriteLine(func);
            }
            File.WriteAllText(file2, txt);
            //Console.WriteLine(item);
        }

        Rotation2d rot = new(5.Radians());
        string serialized = JsonSerializer.Serialize(rot);
        Console.WriteLine(serialized);

        // NativeLibrary.SetDllImportResolver(typeof(MJpegServer).Assembly, (name, assembly, searchpath) =>
        // {
        //     if (name != "cscore")
        //     {
        //         throw new InvalidOperationException();
        //     }
        //     Console.WriteLine(searchpath);
        //     return NativeLibrary.Load(@"C:\Users\thadh\Documents\GitHub\thadhouse\allwpilib\myRobot\build\install\myRobotCpp\windowsx86-64\lib\cscored.dll");
        // });

        // NativeLibrary.SetDllImportResolver(typeof(NativeRawFrame).Assembly, (name, assembly, searchpath) =>
        // {
        //     if (name != "wpiutil")
        //     {
        //         throw new InvalidOperationException();
        //     }
        //     return NativeLibrary.Load(@"C:\Users\thadh\Documents\GitHub\thadhouse\allwpilib\myRobot\build\install\myRobotCpp\windowsx86-64\lib\wpiutild.dll");
        // });

        // Rotation2d r = JsonSerializer.Deserialize<Rotation2d>(serialized);
        // Console.WriteLine(r.Angle.Radians);

        // UsbCamera camera = new UsbCamera("Camera", 0)
        // {
        //     ConnectionStrategy = ConnectionStrategy.ConnectionKeepOpen
        // };
        // MJpegServer server = new MJpegServer("Server", 1181)
        // {
        //     Source = camera
        // };

        // RawSink sink = new RawSink("Sink")
        // {
        //     Source = camera
        // };

        // RawSource source = new RawSource("Source", new VideoMode
        // {
        //     Fps = 30,
        //     Width = 640,
        //     Height = 480,
        //     PixelFormat = WPIUtil.PixelFormat.Yuyv
        // })
        // {
        //     ConnectionStrategy = ConnectionStrategy.ConnectionKeepOpen
        // };

        // MJpegServer sinkServer = new MJpegServer("SinkServer", 1182)
        // {
        //     Source = source
        // };

        // RawFrameReader reader = new RawFrameReader();

        // while (true)
        // {
        //     long ts = sink.GrabFrame(reader);
        //     if (ts <= 0)
        //     {
        //         continue;
        //     }

        //     source.PutFrame(reader.ToWriter());
        // }


        // // StructArrayTopic<TrackedTag> n = null!;
        // // var subscriber = n.Subscribe([], PubSubOptions.None);
        // // TrackedTag[] tags = subscriber.Get();
    }
}

public class TrackedTagStruct : IStruct<TrackedTag>
{
    public string TypeString => "struct:TrackedTag";

    public int Size => Rotation2d.Struct.Size + IStructBase.SizeInt16;

    public string Schema { get; } = $"int16 id;{Rotation2d.Struct.Schema}";

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
        return new TrackedTag()
        {
            Id = id,
            Tfr = rot,
        };
    }
}

public struct TrackedTag : IStructSerializable<TrackedTag>
{
    public static IStruct<TrackedTag> Struct { get; } = new TrackedTagStruct();

    public Rotation2d Tfr { get; set; }
    public short Id { get; set; }
}
