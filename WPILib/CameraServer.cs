using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CSCore;
using NetworkTables.Tables;
using CSCore.Native;
using NetworkTables;

namespace WPILib
{
    /// <summary>
    /// Singleton class for creating and keeping camera servers,
    /// and publishing information to NetworkTables
    /// </summary>
    public class CameraServer
    {
        /// <summary>
        /// The initial port used to create streams
        /// </summary>
        public const int BasePort = 1181;

        private const string PublishName = "/CameraPublisher";
        private static CameraServer server;

        /// <summary>
        /// Gets the CameraServer instance
        /// </summary>
        public static CameraServer Instance
        {
            get
            {
                if (server == null)
                {
                    CameraServer d = new CameraServer();
                    Interlocked.CompareExchange(ref server, d, null);
                }
                return server;
            }
        }

        private int m_defaultUsbDevice;
        private string m_primarySourceName;
        private Dictionary<string, VideoSource> m_sources;
        private Dictionary<string, VideoSink> m_sinks;
        private Dictionary<int, ITable> m_tables; // indexed by source handle;
        private ITable m_publishTable;
        private VideoListener m_videoListener;
        private int m_tableListener;
        private int m_nextPort;
        private List<string> m_addresses;

        private readonly object m_lockObject;

        private static string MakeSourceValue(int source)
        {
            switch (NativeMethods.GetSourceKind(source))
            {
                case SourceKind.Usb:
                    return $"usb:{NativeMethods.GetUsbCameraPath(source)}";
                case SourceKind.Http:
                    var urls = NativeMethods.GetHttpCameraUrls(source);
                    return urls.Count > 0 ? $"ip: {urls[0]}" : "ip:";
                case SourceKind.CV:
                    return "usb:";
                default:
                    return "unknown:";
            }
        }

        private static string MakeStreamValue(string address, int port)
        {
            return $"mjpg:http://{address}:{port.ToString()}/?action=stream";
        }

        private ITable GetSourceTable(int source)
        {
            lock (m_lockObject)
            {
                ITable table;
                m_tables.TryGetValue(source, out table);
                return table;
            }
        }

        private List<string> GetSinkStreamValues(int sink)
        {
            // Ignore all but mjpeg server
            if (NativeMethods.GetSinkKind(sink) != SinkKind.Mjpeg)
            {
                return new List<string>();
            }

            int port = NativeMethods.GetMjpegServerPort(sink);

            lock (m_lockObject)
            {
                List<string> values = new List<string>(m_addresses.Count + 1);
                string listenAddress = NativeMethods.GetMjpegServerListenAddress(sink);
                if (!string.IsNullOrWhiteSpace(listenAddress))
                {
                    values.Add(MakeStreamValue(listenAddress, port));
                }
                else
                {
                    values.Add(MakeStreamValue($"{NativeMethods.GetHostName()}.local", port));
                    foreach (var address in m_addresses)
                    {
                        if (address == "127.0.0.1")
                        {
                            continue;
                        }
                        values.Add(MakeStreamValue(address, port));
                    }
                }
                return values;
            }
        }

        private List<string> GetSourceStreamValues(int source)
        {
            // ignore all but httpcamera
            if (NativeMethods.GetSourceKind(source) != SourceKind.Http)
            {
                return new List<string>();
            }

            List<string> values = NativeMethods.GetHttpCameraUrls(source);
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = $"mjpeg:{values[i]}";
            }

            lock (m_lockObject)
            {
                foreach (VideoSink i in m_sinks.Values)
                {
                    int sink = i.Handle;
                    int sinkSource = NativeMethods.GetSinkSource(sink);
                    if (source == sinkSource && NativeMethods.GetSinkKind(sink) == SinkKind.Mjpeg)
                    {
                        List<string> finalValues = new List<string>(values);
                        int port = NativeMethods.GetMjpegServerPort(sink);
                        finalValues.Add(MakeStreamValue("172.22.11.2", port));
                        return finalValues;
                    }
                }
            }

            return values;
        }

        private void UpdateStreamValues()
        {
            lock (m_lockObject)
            {
                foreach (VideoSink i in m_sinks.Values)
                {
                    int sink = i.Handle;

                    int source = NativeMethods.GetSinkSource(sink);
                    if (source == 0) continue;
                    ITable table;
                    m_tables.TryGetValue(source, out table);
                    if (table != null)
                    {
                        // Don't set stream values if this is a HttpCamera passthrough
                        if (NativeMethods.GetSourceKind(source) == SourceKind.Http) continue;
                        var values = GetSinkStreamValues(sink);
                        if (values.Count > 0)
                        {
                            table.PutStringArray("streams", values);
                        }
                    }
                }

                foreach (var i in m_sources.Values)
                {
                    int source = i.Handle;

                    ITable table;
                    m_tables.TryGetValue(source, out table);
                    if (table != null)
                    {
                        var values = GetSourceStreamValues(source);
                        if (values.Count > 0)
                        {
                            table.PutStringArray("streams", values);
                        }
                    }
                }
            }
        }

        private static string PixelFormatToString(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Mjpeg:
                    return "MJPEG";
                case PixelFormat.YUYV:
                    return "YUYV";
                case PixelFormat.RGB565:
                    return "RGB565";
                case PixelFormat.BGR:
                    return "BGR";
                case PixelFormat.GRAY:
                    return "Gray";
                default:
                    return "Unknown";
            }
        }

        private static PixelFormat PixelFormatFromString(string pixelFormatStr)
        {
            switch (pixelFormatStr)
            {
                case "MJPEG":
                case "mjpeg":
                case "JPEG":
                case "jpeg":
                    return PixelFormat.Mjpeg;
                case "YUYV":
                case "yuyv":
                    return PixelFormat.YUYV;
                case "RGB565":
                case "rgb565":
                    return PixelFormat.RGB565;
                case "BGR":
                case "bgr":
                    return PixelFormat.BGR;
                case "GRAY":
                case "Gray":
                case "gray":
                    return PixelFormat.GRAY;
                default:
                    return PixelFormat.Unknown;
            }
        }

        private const string ReMode = "(?<width>[0-9]+)\\s*x\\s*(?<height>[0-9]+)\\s+(?<format>.*?)\\s+"
                                 + "(?<fps>[0-9.]+)\\s*fps";

        private static readonly Regex m_matcher = new Regex(ReMode);

        private static VideoMode VideoModeFromString(string modeStr)
        {
            var match = m_matcher.Match(modeStr);
            if (!match.Success)
            {
                return new VideoMode(PixelFormat.Unknown, 0, 0, 0);
            }
            PixelFormat format = PixelFormatFromString(match.Groups["format"].Value);
            int width;
            int height;
            double fps;
            if (!int.TryParse(match.Groups["width"].Value, out width))
            {
                return new VideoMode(PixelFormat.Unknown, 0, 0, 0);
            }
            if (!int.TryParse(match.Groups["height"].Value, out height))
            {
                return new VideoMode(PixelFormat.Unknown, 0, 0, 0);
            }
            if (!double.TryParse(match.Groups["fps"].Value, out fps))
            {
                return new VideoMode(PixelFormat.Unknown, 0, 0, 0);
            }
            return new VideoMode(format, width, height, (int)fps);
        }

        private static string VideoModeToString(VideoMode mode)
        {
            return $"{mode.Width.ToString()}x{mode.Height.ToString()} {PixelFormatToString(mode.PixelFormat)} {mode.FPS.ToString()} fps";
        }

        private static List<string> GetSourceModeValues(int sourceHandle)
        {
            var modes = NativeMethods.EnumerateSourceVideoModes(sourceHandle);
            List<string> modeStrings = new List<string>(modes.Count);
            foreach (VideoMode videoMode in modes)
            {
                modeStrings.Add(VideoModeToString(videoMode));
            }
            return modeStrings;
        }

        private static void PutSourcePropertyValue(ITable table, VideoEvent evnt, bool isNew)
        {
            string name;
            string infoName;
            if (evnt.Name.StartsWith("raw_"))
            {
                name = $"RawProperty/{evnt.Name}";
                infoName = $"RawPropertyInfo/{evnt.Name}";
            }
            else
            {
                name = $"Property/{evnt.Name}";
                infoName = $"PropertyInfo/{evnt.Name}";
            }

            switch (evnt.PropertyKind)
            {
                case PropertyKind.Boolean:
                    if (isNew)
                    {
                        table.SetDefaultBoolean(name, evnt.Value != 0);
                    }
                    else
                    {
                        table.PutBoolean(name, evnt.Value != 0);
                    }
                    break;
                case PropertyKind.Enum:
                case PropertyKind.Integer:
                    if (isNew)
                    {
                        table.SetDefaultNumber(name, evnt.Value);
                        table.PutNumber($"{infoName}/min", NativeMethods.GetPropertyMin(evnt.PropertyHandle));
                        table.PutNumber($"{infoName}/max", NativeMethods.GetPropertyMax(evnt.PropertyHandle));
                        table.PutNumber($"{infoName}/step", NativeMethods.GetPropertyStep(evnt.PropertyHandle));
                        table.PutNumber($"{infoName}/default", NativeMethods.GetPropertyDefault(evnt.PropertyHandle));
                    }
                    else
                    {
                        table.PutNumber(name, evnt.Value);
                    }
                    break;
                case PropertyKind.String:
                    if (isNew)
                    {
                        table.SetDefaultString(name, evnt.ValueStr);
                    }
                    else
                    {
                        table.PutString(name, evnt.ValueStr);
                    }
                    break;
                default:
                    break;
            }
        }

        private CameraServer()
        {
            m_defaultUsbDevice = 0;
            m_lockObject = new object();
            m_sources = new Dictionary<string, VideoSource>();
            m_sinks = new Dictionary<string, VideoSink>();
            m_tables = new Dictionary<int, ITable>();
            m_publishTable = NetworkTable.GetTable(PublishName);
            m_nextPort = BasePort;
            m_addresses = new List<string>();

            m_videoListener = new VideoListener((vidEvent) =>
            {
                switch (vidEvent.Kind)
                {
                    case EventKind.SourceCreated:
                        {
                            // Create subtable for the camera
                            ITable table = m_publishTable.GetSubTable(vidEvent.Name);
                            lock (m_lockObject)
                            {
                                m_tables.Add(vidEvent.SourceHandle, table);
                            }
                            table.PutString("source", MakeSourceValue(vidEvent.SourceHandle));
                            table.PutString("description",
                                  NativeMethods.GetSourceDescription(vidEvent.SourceHandle));
                            table.PutBoolean("connected", NativeMethods.IsSourceConnected(vidEvent.SourceHandle));
                            table.PutStringArray("streams", GetSourceStreamValues(vidEvent.SourceHandle));
                            try
                            {
                                VideoMode mode = NativeMethods.GetSourceVideoMode(vidEvent.SourceHandle);
                                table.SetDefaultString("mode", VideoModeToString(mode));
                                table.PutStringArray("modes", GetSourceModeValues(vidEvent.SourceHandle));
                            }
                            catch (VideoException)
                            {
                                // Do nothing
                            }
                            break;
                        }
                    case EventKind.SourceDestroyed:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                table.PutString("source", "");
                                table.PutStringArray("streams", new string[0]);
                                table.PutStringArray("modes", new string[0]);
                            }
                            break;
                        }
                    case EventKind.SourceConnected:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                // update the description too (as it may have changed)
                                table.PutString("description",
                                    NativeMethods.GetSourceDescription(vidEvent.SourceHandle));
                                table.PutBoolean("connected", true);
                            }
                            break;
                        }
                    case EventKind.SourceDisconnected:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            table?.PutBoolean("connected", false);
                            break;
                        }
                    case EventKind.SourceVideoModesUpdated:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                table.PutStringArray("modes", GetSourceModeValues(vidEvent.SourceHandle));
                            }
                            break;
                        }
                    case EventKind.SourceVideoModeChanged:
                        {

                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                table.PutString("mode", VideoModeToString(vidEvent.Mode));
                            }
                            break;
                        }
                    case EventKind.SourcePropertyCreated:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                PutSourcePropertyValue(table, vidEvent, true);
                            }
                            break;
                        }
                    case EventKind.SourcePropertyValueUpdated:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                PutSourcePropertyValue(table, vidEvent, false);
                            }
                            break;
                        }
                    case EventKind.SourcePropertyChoicesUpdated:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                List<string> choices = NativeMethods.GetEnumPropertyChoices(vidEvent.PropertyHandle);
                                table.PutStringArray($"PropertyInfo/{vidEvent.Name}/choices", choices);
                            }
                            break;
                        }
                    case EventKind.SinkSourceChanged:
                    case EventKind.SinkCreated:
                    case EventKind.SinkDestroyed:
                        {
                            UpdateStreamValues();
                            break;
                        }
                    case EventKind.NetworkInterfacesChanged:
                        {
                            m_addresses = NativeMethods.GetNetworkInterfaces();
                            break;
                        }
                    default:
                        break;
                }
            }, (EventKind)0x4fff, true);

            m_tableListener = NtCore.AddEntryListener($"{PublishName}/", (uid, key, value, flags) =>
            {
                string relativeKey = key.Substring(PublishName.Length + 1);

                int subKeyIndex = relativeKey.IndexOf('/');
                if (subKeyIndex == -1) return;
                string sourceName = relativeKey.Substring(0, subKeyIndex);
                VideoSource source;
                if (!m_sources.TryGetValue(sourceName, out source))
                {
                    return;
                }

                relativeKey = relativeKey.Substring(subKeyIndex + 1);

                string propName;
                if (relativeKey == "mode")
                {
                    // reset to current mode
                    NtCore.SetEntryString(key, VideoModeToString(source.GetVideoMode()));
                    return;
                }
                else if (relativeKey.StartsWith("Property/"))
                {
                    propName = relativeKey.Substring(9);
                }
                else if (relativeKey.StartsWith("RawProperty/"))
                {
                    propName = relativeKey.Substring(12);
                }
                else
                {
                    return;
                }

                VideoProperty prop = source.GetProperty(propName);
                switch (prop.Kind)
                {
                    case PropertyKind.None:
                        return;
                    case PropertyKind.Boolean:
                        NtCore.SetEntryBoolean(key, prop.Get() != 0);
                        break;
                    case PropertyKind.Integer:
                    case PropertyKind.Enum:
                        NtCore.SetEntryDouble(key, prop.Get());
                        break;
                    case PropertyKind.String:
                        NtCore.SetEntryString(key, prop.GetString());
                        break;
                    default:
                        return;
                }

            }, NotifyFlags.NotifyImmediate | NotifyFlags.NotifyUpdate);
        }

        /// <summary>
        /// Start automatically capturing images to send to the dashboard.
        /// </summary>
        /// <remarks>
        /// You should call this method to see a camera feed on the dashboard.
        /// If you also want to perform vision processing on the roboRIO, use
        /// <see cref="GetVideo()"/> to get access to the camera images
        /// 
        /// <para>
        /// The first time this overload is called, it calls <see cref="StartAutomaticCapture(int)"/>
        /// with device 0, creating a camera named "USB Camera 0". Subsequent calls increment the device
        /// number (e.g. 1, 2, etc).
        /// </para>
        /// </remarks>
        /// <returns>The <see cref="UsbCamera"/> object that was created to stream from</returns>
        public UsbCamera StartAutomaticCapture()
        {
            // returns new value, so subtract 1 to get old value
            return StartAutomaticCapture(Interlocked.Increment(ref m_defaultUsbDevice) - 1);
        }

        /// <summary>
        /// Start automatically capturing images to send to the dashboard.
        /// </summary>
        /// <remarks>
        /// You should call this method to see a camera feed on the dashboard.
        /// If you also want to perform vision processing on the roboRIO, use
        /// <see cref="GetVideo()"/> to get access to the camera images
        /// 
        /// <para>
        /// This overload calls <see cref="StartAutomaticCapture(string, int)"/> with device 0,
        /// creating a camera named "USB Camera {dev}"
        /// </para>
        /// </remarks>
        /// <param name="dev">The device number for the camera interface</param>
        /// <returns>The <see cref="UsbCamera"/> object that was created to stream from</returns>
        public UsbCamera StartAutomaticCapture(int dev)
        {
            UsbCamera camera = new UsbCamera($"USB Camera {dev.ToString()}", dev);
            StartAutomaticCapture(camera);
            return camera;
        }

        /// <summary>
        /// Start automatically capturing images to send to the dashboard.
        /// </summary>
        /// <remarks>
        /// You should call this method to see a camera feed on the dashboard.
        /// If you also want to perform vision processing on the roboRIO, use
        /// <see cref="GetVideo()"/> to get access to the camera images
        /// </remarks>
        /// <param name="name">The name to give the camera</param>
        /// <param name="dev">The device number for the camera interface</param>
        /// <returns>The <see cref="UsbCamera"/> object that was created to stream from</returns>
        public UsbCamera StartAutomaticCapture(string name, int dev)
        {
            UsbCamera camera = new UsbCamera(name, dev);
            StartAutomaticCapture(camera);
            return camera;
        }

        /// <summary>
        /// Start automatically capturing images to send to the dashboard.
        /// </summary>
        /// <remarks>
        /// You should call this method to see a camera feed on the dashboard.
        /// If you also want to perform vision processing on the roboRIO, use
        /// <see cref="GetVideo()"/> to get access to the camera images
        /// </remarks>
        /// <param name="name">The name to give the camera</param>
        /// <param name="path">The device path (e.g. "/dev/video0") of the camera</param>
        /// <returns>The <see cref="UsbCamera"/> object that was created to stream from</returns>
        public UsbCamera StartAutomaticCapture(string name, string path)
        {
            UsbCamera camera = new UsbCamera(name, path);
            StartAutomaticCapture(camera);
            return camera;
        }

        /// <summary>
        /// Start automatically capturing images from an existing camera to send to 
        /// the dashboard.
        /// </summary>
        /// <remarks>
        /// You should call this method to see a camera feed on the dashboard.
        /// If you also want to perform vision processing on the roboRIO, use
        /// <see cref="GetVideo()"/> to get access to the camera images
        /// </remarks>
        /// <param name="camera">The camera to stream from.</param>
        public void StartAutomaticCapture(VideoSource camera)
        {
            AddCamera(camera);
            VideoSink server = AddServer($"serve_{camera.Name}");
            server.Source = camera;
        }

        /// <summary>
        /// Adds an Axis IP Camera
        /// </summary>
        /// <remarks>
        /// This overload calls <see cref="AddAxisCamera(string, string)"/> with
        /// the name "Axis Camera"
        /// </remarks>
        /// <param name="host">The Camera host IP or DNS name (e.g. "10.x.y.11")</param>
        /// <returns>The AxisCamera created to stream from</returns>
        public AxisCamera AddAxisCamera(string host)
        {
            return AddAxisCamera("Axis Camera", host);
        }

        /// <summary>
        /// Adds an Axis IP Camera.
        /// </summary>
        /// <remarks>
        /// This overload calls <see cref="AddAxisCamera(string, IList{string})"/> with the
        /// name "Axis Camera"
        /// </remarks>
        /// <param name="hosts">List of Camera host IPs/DNS names</param>
        /// <returns>The AxisCamera created to stream from</returns>
        public AxisCamera AddAxisCamera(IList<string> hosts)
        {
            return AddAxisCamera("Axis Camera", hosts);
        }

        /// <summary>
        /// Adds an Axis IP Camera.
        /// </summary>
        /// <param name="name">The name to give the camera</param>
        /// <param name="host">The Camera host IP or DNS name (e.g. "10.x.y.11")</param>
        /// <returns>The AxisCamera created to stream from</returns>
        public AxisCamera AddAxisCamera(string name, string host)
        {
            AxisCamera camera = new AxisCamera(name, host);
            // Create a passthrough server for USB access
            StartAutomaticCapture(camera);
            return camera;
        }

        /// <summary>
        /// Adds an Axis IP Camera.
        /// </summary>
        /// <param name="name">The name to give the camera</param>
        /// <param name="hosts">List of Camera host IPs/DNS names</param>
        /// <returns>The AxisCamera created to stream from</returns>
        public AxisCamera AddAxisCamera(string name, IList<string> hosts)
        {
            AxisCamera camera = new AxisCamera(name, hosts);
            // Create a passthrough server for USB access
            StartAutomaticCapture(camera);
            return camera;
        }

        /// <summary>
        /// Get OpenCV access to the primary camera feed.
        /// </summary>
        /// <remarks>
        /// This allows you to get images from the camera for image processing on the roboRIO.
        /// <para>This is only valid to call after a camera feed has been added with
        /// <see cref="StartAutomaticCapture()"/> or <see cref="AddServer(string)"/>.</para>
        /// </remarks>
        /// <returns>The <see cref="CvSink"/> to get frames from.</returns>
        public CvSink GetVideo()
        {
            VideoSource source;
            lock (m_lockObject)
            {
                if (m_primarySourceName == null)
                {
                    throw new VideoException("No camera available");
                }
                m_sources.TryGetValue(m_primarySourceName, out source);
            }
            if (source == null)
            {
                throw new VideoException("No camera available");
            }
            return GetVideo(source);
        }

        /// <summary>
        /// Get OpenCV access to the specified camera.
        /// </summary>
        /// <remarks>
        /// This allows you to get images from the camera for image processing on the roboRIO.
        /// </remarks>
        /// <param name="camera">The camera to get OpenCV access from 
        /// (e.g. as returned by <see cref="StartAutomaticCapture()"/>.</param>
        /// <returns>The <see cref="CvSink"/> to get frames from.</returns>
        public CvSink GetVideo(VideoSource camera)
        {
            string name = $"opencv_{camera.Name}";

            lock (m_lockObject)
            {
                VideoSink sink;
                m_sinks.TryGetValue(name, out sink);
                if (sink != null)
                {
                    SinkKind kind = sink.Kind;
                    if (kind != SinkKind.CV)
                    {
                        throw new VideoException($"expected OpenCV sink, but got {kind}");
                    }
                    return (CvSink)sink;
                }
            }

            CvSink newSink = new CvSink(name);
            newSink.Source = camera;
            AddServer(newSink);
            return newSink;
        }

        /// <summary>
        /// Get OpenCV access to the specified camera
        /// </summary>
        /// <remarks>
        /// This allows you to get images from the camera for image processing on the roboRIO.
        /// </remarks>
        /// <param name="name">The camera name to get OpenCV access from.</param>
        /// <returns>The <see cref="CvSink"/> to get frames from.</returns>
        public CvSink GetVideo(string name)
        {
            VideoSource source;
            lock (m_lockObject)
            {
                m_sources.TryGetValue(name, out source);
                if (source == null)
                {
                    throw new VideoException($"could not find camera {name}");
                }
            }
            return GetVideo(source);
        }

        /// <summary>
        /// Create an MJPEG stream with OpenCV input. 
        /// </summary>
        /// <remarks>
        /// This can be called to pass custom annotated images to the dashboard.
        /// </remarks>
        /// <param name="name">The name to give the stream</param>
        /// <param name="width">The width of the image being sent</param>
        /// <param name="height">The height of the image being sent</param>
        /// <returns>The <see cref="CvSource"/> to put frames into.</returns>
        public CvSource PutVideo(string name, int width, int height)
        {
            CvSource source = new CvSource(name, PixelFormat.Mjpeg, width, height, 30);
            StartAutomaticCapture(source);
            return source;
        }

        /// <summary>
        /// Adds an MJPEG server at the next available port.
        /// </summary>
        /// <param name="name">The server name</param>
        /// <returns>The created <see cref="MjpegServer"/></returns>
        public MjpegServer AddServer(string name)
        {
            int port;
            lock (m_lockObject)
            {
                port = m_nextPort;
                m_nextPort++;
            }
            return AddServer(name, port);
        }

        /// <summary>
        /// Adds an MJPEG server
        /// </summary>
        /// <param name="name">The server name</param>
        /// <param name="port">The server port</param>
        /// <returns>The created <see cref="MjpegServer"/></returns>
        public MjpegServer AddServer(string name, int port)
        {
            MjpegServer server = new MjpegServer(name, port);
            AddServer(server);
            return server;
        }

        /// <summary>
        /// Adds an already created server.
        /// </summary>
        /// <param name="server">The server to add</param>
        public void AddServer(VideoSink server)
        {
            lock (m_lockObject)
            {
                m_sinks.Add(server.Name, server);
            }
        }

        /// <summary>
        /// Removes a server by name
        /// </summary>
        /// <param name="name">The server name</param>
        public void RemoveServer(string name)
        {
            lock (m_lockObject)
            {
                m_sinks.Remove(name);
            }
        }

        /// <summary>
        /// Get server for the primary camera feed.
        /// </summary>
        /// <remarks>
        /// This is only valid to call after a camera feed has been added with
        /// <see cref="StartAutomaticCapture()"/> or <see cref="AddServer(string)"/>.
        /// </remarks>
        /// <returns>VideoSink for the primary camera feed.</returns>
        public VideoSink GetServer()
        {
            lock (m_lockObject)
            {
                if (m_primarySourceName == null)
                {
                    throw new VideoException("no camera available");
                }
                return GetServer($"serve_{m_primarySourceName}");
            }
        }

        /// <summary>
        /// Gets a server by name
        /// </summary>
        /// <param name="name">Server name</param>
        /// <returns>VideoSink if it exists, otherwise null</returns>
        public VideoSink GetServer(string name)
        {
            lock (m_lockObject)
            {
                VideoSink sink;
                m_sinks.TryGetValue(name, out sink);
                return sink;
            }
        }

        /// <summary>
        /// Adds an already created camera.
        /// </summary>
        /// <param name="camera">The camera to add</param>
        public void AddCamera(VideoSource camera)
        {
            string name = camera.Name;
            lock (m_lockObject)
            {
                if (m_primarySourceName == null)
                {
                    m_primarySourceName = name;
                }
                m_sources.Add(name, camera);
            }
        }

        /// <summary>
        /// Removes a camera by name
        /// </summary>
        /// <param name="name">The camera name</param>
        public void RemoveCamera(string name)
        {
            lock (m_lockObject)
            {
                m_sources.Remove(name);
            }
        }
    }
}
