using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSCore;
using NetworkTables.Tables;
using CSCore.Native;
using NetworkTables;

namespace WPILib
{
    public class CameraServer
    {
        public const int BasePort = 1181;

        private const string PublishName = "/CameraPublisher";
        private static CameraServer server;

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
            var c = NativeMethods.GetSourceKind(source);
            Console.WriteLine(c);
            switch (c)
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
            return $"MJPEG:HTTP://{address}:{port.ToString()}/?action=stream";
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

        private static List<string> GetSourceStreamValues(int source)
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
                    ITable table;
                    m_tables.TryGetValue(source, out table);
                    if (table != null)
                    {
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

        private CameraServer()
        {
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
                            break;
                        }
                    case EventKind.SourceDestroyed:
                        {
                            ITable table = GetSourceTable(vidEvent.SourceHandle);
                            if (table != null)
                            {
                                table.PutString("source", "");
                                table.PutStringArray("streams", new string[0]);
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
                            break;
                        }
                    case EventKind.SourceVideoModeChanged:
                        {
                            break;
                        }
                    case EventKind.SourcePropertyCreated:
                        {
                            break;
                        }
                    case EventKind.SourcePropertyValueUpdated:
                        {
                            break;
                        }
                    case EventKind.SourcePropertyChoicesUpdated:
                        {
                            break;
                        }
                    case EventKind.SinkSourceChanged:
                        {
                            UpdateStreamValues();
                            break;
                        }
                    case EventKind.SinkCreated:
                        {
                            break;
                        }
                    case EventKind.SinkDestroyed:
                        {
                            break;
                        }
                    case EventKind.SinkEnabled:
                        {
                            break;
                        }
                    case EventKind.SinkDisabled:
                        {
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
            }, 0x7fff, true);

            m_tableListener = NtCore.AddEntryListener(PublishName, (uid, key, value, flags) =>
            {
                if (!key.StartsWith($"{PublishName}/"))
                {
                    return;
                }
                string relativeKey = key.Substring(PublishName.Length);

            }, NotifyFlags.NotifyImmediate | NotifyFlags.NotifyUpdate);
        }

        public UsbCamera StartAutomaticCapture()
        {
            return StartAutomaticCapture(0);
        }

        public UsbCamera StartAutomaticCapture(int dev)
        {
            UsbCamera camera = new UsbCamera($"USB Camera {dev.ToString()}", dev);
            StartAutomaticCapture(camera);
            return camera;
        }

        public UsbCamera StartAutomaticCapture(string name, int dev)
        {
            UsbCamera camera = new UsbCamera(name, dev);
            StartAutomaticCapture(camera);
            return camera;
        }

        public UsbCamera StartAutomaticCapture(string name, string path)
        {
            UsbCamera camera = new UsbCamera(name, path);
            StartAutomaticCapture(camera);
            return camera;
        }

        public void StartAutomaticCapture(VideoSource camera)
        {
            AddCamera(camera);
            VideoSink server = AddServer($"serve_{camera.Name}");
            server.Source = camera;
        }

        public AxisCamera AddAxisCamera(string host)
        {
            return AddAxisCamera("Axis Camera", host);
        }

        public AxisCamera AddAxisCamera(IList<string> hosts)
        {
            return AddAxisCamera("Axis Camera", hosts);
        }

        public AxisCamera AddAxisCamera(string name, string host)
        {
            AxisCamera camera = new AxisCamera(name, host);
            AddCamera(camera);
            return camera;
        }

        public AxisCamera AddAxisCamera(string name, IList<string> hosts)
        {
            AxisCamera camera = new AxisCamera(name, hosts);
            AddCamera(camera);
            return camera;
        }

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
                    return (CvSink) sink;
                }
            }

            CvSink newSink = new CvSink(name);
            newSink.Source = camera;
            AddServer(newSink);
            return newSink;
        }

        public CvSource PutVideo(string name, int width, int height)
        {
            CvSource source = new CvSource(name, PixelFormat.Mjpeg, width, height, 30);
            StartAutomaticCapture(source);
            return source;
        }

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

        public MjpegServer AddServer(string name, int port)
        {
            MjpegServer server = new MjpegServer(name, port);
            AddServer(server);
            return server;
        }

        public void AddServer(VideoSink server)
        {
            lock (m_lockObject)
            {
                m_sinks.Add(server.Name, server);
            }
        }

        public void RemoveServer(string name)
        {
            lock (m_lockObject)
            {
                m_sinks.Remove(name);
            }
        }

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

        public void RemoveCamera(string name)
        {
            lock (m_lockObject)
            {
                m_sources.Remove(name);
            }
        }
    }
}
