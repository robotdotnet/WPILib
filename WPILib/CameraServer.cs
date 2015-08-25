using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NIVision;
using WPILib.Vision;
using static NIVision.PublicMethods;

namespace WPILib
{
    public class CameraServer
    {
        private const int Port = 1180;
        private static readonly byte[] MagicNumber = { 0x01, 0x00, 0x00, 0x00 };
        private const int kSize640x480 = 0;
        private const int kSize320x240 = 1;
        private const int kSize160x120 = 2;
        private const int kHardwareCompression = -1;
        private const string kDefaultCameraName = "cam1";
        private const int kMaxImageSize = 200000;
        private static CameraServer server;

        public static CameraServer Instance
        {
            get { return server ?? (server = new CameraServer()); }
        }

        private Thread serverThread;
        private int m_quality;
        private bool m_autoCaptureStarted;
        private bool m_hwClient = true;
        private USBCamera m_camera;
        private CameraData m_imageData;
        private LinkedList<IntPtr> m_imageDataPool;
        //private Deque<ByteBuffer> m_imageDataPool;

        private class CameraData
        {
            internal RawData data;
            internal int start;

            public CameraData(RawData d, int s)
            {
                data = d;
                start = s;
            }
        }

        private CameraServer()
        {
            m_quality = 50;
            m_camera = null;
            m_imageData = null;
            m_imageDataPool = new LinkedList<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                m_imageDataPool.AddLast(IntPtr.Zero);
            }

            serverThread = new Thread(() =>
            {
                try
                {
                    Serve();
                }
                catch (Exception)
                {
                }
            });

            serverThread.Name = "CameraServer Send Thread";
            serverThread.Start();
        }

        private readonly object m_lockObject = new object();


        private void SetImageData(RawData data, int start)
        {
            try
            {
                Monitor.Enter(m_lockObject);
                if (m_imageData?.data != null)
                {
                    m_imageData.data.Dispose();
                    //Free Data
                    if (m_imageData.data.Data != IntPtr.Zero)
                        m_imageDataPool.AddLast(data.Data);
                    m_imageData = null;
                }
                m_imageData = new CameraData(data, start);
                Monitor.PulseAll(m_lockObject);
            }
            finally
            {
                Monitor.Exit(m_lockObject);
            }
        }

        public void SetImage(Image image)
        {
            RawData data = ImaqFlatten(image, FlattenType.IMAQ_FLATTEN_IMAGE, CompressionType.IMAQ_COMPRESSION_JPEG,
                10 * m_quality);
            //Raw data here is a byte[]

            IntPtr buffer = data.Data;

            bool hwClient;

            lock (m_lockObject)
            {
                hwClient = m_hwClient;
            }

            int index = 0;
            if (hwClient)
            {
                while (index < data.Size - 1)
                {
                    if (Marshal.ReadByte(buffer, index) == 0xFF && Marshal.ReadByte(buffer, index + 1) == 0xD8) break;
                    index++;
                }
            }

            if (data.Size - index - 1 <= 2)
            {
                throw new VisionException("Data Size Wrong");
            }

            SetImageData(data, index);
        }

        public void StartAutomaticCapture()
        {
            StartAutomaticCapture(USBCamera.kDefaultCameraName);
        }

        public void StartAutomaticCapture(string cameraName)
        {
            try
            {
                USBCamera camera = new USBCamera(cameraName);
                camera.OpenCamera();
                StartAutomaticCapture(camera);
            }
            catch (VisionException ex)
            {
                DriverStation.ReportError("Error when starting the camera: " + cameraName + " " + ex, true);
                throw;
            }
        }

        public void StartAutomaticCapture(USBCamera camera)
        {
            lock (m_lockObject)
            {
                if (m_autoCaptureStarted) return;
                m_camera = camera;

                m_camera.StartCapture();

                Thread captureThread = new Thread(Capture);

                captureThread.Name = "Camera Capture Thread";
                captureThread.Start();
            }
        }

        protected void Capture()
        {
            Image frame = ImaqCreateImage(ImageType.IMAQ_IMAGE_RGB, 0);

            while (true)
            {
                bool hwClient;
                IntPtr dataBuffer = IntPtr.Zero;
                lock (m_lockObject)
                {
                    hwClient = m_hwClient;
                    if (hwClient)
                    {
                        dataBuffer = m_imageDataPool.Last.Value;
                        m_imageDataPool.RemoveLast();
                    }
                }

                try
                {
                    if (hwClient && dataBuffer != IntPtr.Zero)
                    {
                        uint size = m_camera.GetImageData(dataBuffer, kMaxImageSize);
                        SetImageData(new RawData(dataBuffer, size), 0);
                    }
                    else
                    {
                        m_camera.GetImage(frame);
                        SetImage(frame);
                    }
                }
                catch (VisionException ex)
                {
                    DriverStation.ReportError("Error when getting image from the camera: " + ex, true);
                    if (dataBuffer != null)
                    {
                        lock (m_lockObject)
                        {
                            m_imageDataPool.AddLast(dataBuffer);
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public bool IsAutoCaptureStarted()
        {
            lock (m_lockObject)
            {
                return m_autoCaptureStarted;
            }
        }

        public void SetSize(int size)
        {
            lock (m_lockObject)
            {
                if (m_camera == null) return;
                switch (size)
                {
                    case kSize640x480:
                        m_camera.SetSize(640, 480);
                        break;
                    case kSize320x240:
                        m_camera.SetSize(320, 240);
                        break;
                    case kSize160x120:
                        m_camera.SetSize(160, 120);
                        break;
                }
            }
        }

        public void SetQuality(int quality)
        {
            lock (m_lockObject)
            {
                m_quality = quality > 100 ? 100 : quality < 0 ? 0 : quality;
            }
        }

        protected void Serve()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Unspecified);

            socket.ExclusiveAddressUse = false;

            socket.Bind(new IPEndPoint(IPAddress.Any, Port));

            byte[] buffer = new byte[255];

            while (true)
            {
                try
                {
                    Socket s = socket.Accept();

                    NetworkStream stream = new NetworkStream(s);

                    stream.Read(buffer, 0, 12);

                    int fps = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
                    int compression = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
                    int size = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));

                    if (compression != kHardwareCompression)
                    {
                        DriverStation.ReportError("Choose \"USB Camera HW\" on the dashboard", false);
                        s.Close();
                        continue;
                    }
                    try
                    {
                        Monitor.Enter(m_lockObject);
                        Console.WriteLine("Camera not ready yet, awaiting image");
                        if (m_camera == null) Monitor.Wait(m_lockObject);
                        m_hwClient = compression == kHardwareCompression;
                        if (!m_hwClient) SetQuality(100 - compression);
                        else
                        {
                            m_camera?.SetFPS(fps);
                        }
                        SetSize(size);
                    }
                    finally
                    {
                        Monitor.Exit(m_lockObject);
                    }

                    long period = (long) (1000 / (1.0 * fps));
                    Stopwatch sw = new Stopwatch();
                    while (true)
                    {
                        sw.Start();
                        CameraData imageData = null;
                        try
                        {
                            Monitor.Enter(m_lockObject);
                            Monitor.Wait(m_lockObject);
                            imageData = m_imageData;
                            m_imageData = null;
                        }
                        finally
                        {
                            Monitor.Exit(m_lockObject);
                        }

                        if (imageData == null) continue;

                        byte[] imageArray = new byte[imageData.data.Size];
                        Marshal.Copy(imageData.data.Data, imageArray, 0, (int)imageData.data.Size);

                        try
                        {
                            stream.Write(MagicNumber, 0, MagicNumber.Length);
                            byte[] imgLength = BitConverter.GetBytes(imageArray.Length);
                            stream.Write(imgLength, 0, imgLength.Length);
                            stream.Write(imageArray, 0, imageArray.Length);
                            stream.Flush();

                            var dt = sw.ElapsedMilliseconds;
                            if (dt < period)
                                Thread.Sleep((int) (period - dt));
                        }
                        catch (IOException ioEx)
                        {
                            DriverStation.ReportError(ioEx.ToString(), true);
                            break;
                        }
                        finally
                        {
                            imageData.data.Dispose();
                            if (imageData.data.Data != IntPtr.Zero)
                            {
                                lock (m_lockObject)
                                {
                                    m_imageDataPool.AddLast(imageData.data.Data);
                                }
                            }
                        }
                    }

                }
                catch (IOException ex)
                {
                    DriverStation.ReportError(ex.ToString(), true);
                    continue;
                }
            }
        }
    }
}
