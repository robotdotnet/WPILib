/*
using System;
using System.Runtime.InteropServices;
using System.Threading;
using NIVision;
using NIVision.IMAQdx;
using static NIVision.IMAQdx.PublicMethods;
// ReSharper disable InconsistentNaming
#pragma warning disable 1591
#pragma warning disable 169

namespace WPILib.Vision
{
    
    public class USBCamera
    {

        public static String kDefaultCameraName = "cam0";


        private static String ATTR_VIDEO_MODE = "AcquisitionAttributes::VideoMode";
        private static String ATTR_WB_MODE = "CameraAttributes::WhiteBalance::Mode";
        private static String ATTR_WB_VALUE = "CameraAttributes::WhiteBalance::Value";
        private static String ATTR_EX_MODE = "CameraAttributes::Exposure::Mode";
        private static String ATTR_EX_VALUE = "CameraAttributes::Exposure::Value";
        private static String ATTR_BR_MODE = "CameraAttributes::Brightness::Mode";
        private static String ATTR_BR_VALUE = "CameraAttributes::Brightness::Value";

        public class WhiteBalance
        {
            public const int kFixedIndoor = 3000;
            public const int kFixedOutdoor1 = 4000;
            public const int kFixedOutdoor2 = 5000;
            public const int kFixedFluorescent1 = 5100;
            public const int kFixedFlourescent2 = 5200;
        }

        //private Pattern m_reMode = Pattern.compile("(?<width>[0-9]+)\\s*x\\s*(?<height>[0-9]+)\\s+(?<format>.*?)\\s+(?<fps>[0-9.]+)\\s*fps");

        private String m_name = kDefaultCameraName;
        private int m_id = -1;
        private bool m_active = false;
        private bool m_useJpeg = true;
        private int m_width = 320;
        private int m_height = 240;
        private int m_fps = 30;

        private String m_whiteBalance = "auto";

        private int m_whiteBalanceValue = -1;
        private String m_exposure = "auto";
        private int m_exposureValue = -1;
        private int m_brightness = 50;
        private bool m_needSettingsUpdate = true;


        public USBCamera()
        {
            OpenCamera();
        }

        public USBCamera(string name)
        {
            m_name = name;
            OpenCamera();
        }

        private readonly object m_lockObject = new object();

        public void OpenCamera()
        {
            lock (m_lockObject)
            {
                if (m_id != -1) return;
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        m_id = (int)IMAQdxOpenCamera(m_name, IMAQdxCameraControlMode.IMAQdxCameraControlModeController);
                    }
                    catch (VisionException e)
                    {
                        if (i == 2)
                            throw e;
                        Thread.Sleep(2000);
                        continue;
                    }
                    break;
                }
            }
        }

        public void CloseCamera()
        {
            lock (m_lockObject)
            {
                if (m_id == - 1)
                    return;
                IMAQdxCloseCamera((uint)m_id);
                m_id = -1;
            }
        }

        public void StartCapture()
        {
            lock (m_lockObject)
            {
                if (m_id == -1 || m_active)
                    return;
                IMAQdxConfigureGrab((uint)m_id);
                IMAQdxStartAcquisition((uint)m_id);
                m_active = true;
            }
        }

        public void StopCapture()
        {
            if (m_id == -1 || !m_active)
                return;
            IMAQdxStopAcquisition((uint)m_id);
            IMAQdxUnconfigureAcquisition((uint)m_id);
            m_active = false;
        }

        public void UpdateSettings()
        {
            lock (m_lockObject)
            {
                
            }
        }

        public void SetFPS(int fps)
        {
            lock (m_lockObject)
            {
                if (fps != m_fps)
                {
                    m_needSettingsUpdate = true;
                    m_fps = fps;
                }
            }
        }

        public void SetSize(int width, int height)
        {
            lock (m_lockObject)
            {
                if (width != m_width || height != m_height)
                {
                    m_needSettingsUpdate = true;
                    m_width = width;
                    m_height = height;
                }
            }
        }

        public void GetImage(Image image)
        {
            if (m_needSettingsUpdate || m_useJpeg)
            {
                m_needSettingsUpdate = false;
                m_useJpeg = false;
                UpdateSettings();
            }
            IMAQdxGrab((uint) m_id, image, 1);
        }

        public uint GetImageData(IntPtr data, uint size)
        {
            if (m_needSettingsUpdate || !m_useJpeg)
            {
                m_needSettingsUpdate = false;
                m_useJpeg = true;
                UpdateSettings();
            }
            IMAQdxGetImageData((uint)m_id, data, size, IMAQdxBufferNumberMode.IMAQdxBufferNumberModeLast, 0);
            return GetJpegSize(data, size);
        }


        public static uint GetJpegSize(IntPtr image, uint size)
        {
            if (Marshal.ReadByte(image, 0) != 0xFF && Marshal.ReadByte(image, 1) != 0xD8) throw new VisionException("Invalid Image");
            int pos = 2;
            while (pos < size)
            {
                if (Marshal.ReadByte(image, pos) != 0xff)
                    throw new VisionException("Invalid Image at pos " + pos + " (" +
                                              Marshal.ReadByte(image, pos) + ")");
                byte t = Marshal.ReadByte(image, pos + 1);
                if (t == 0x01 || (t >= 0xd0 && t <= 0xd7))
                {
                    pos += 2;
                }
                else if (t == 0xd9)
                {
                    return (uint)pos + 2;
                }
                else if (t == 0xd8)
                {
                    throw new VisionException("Invalid Image");
                }
                else if (t == 0xda)
                {
                    int len = (((Marshal.ReadByte(image, pos + 2) & 0xff)) << 8 |
                                (Marshal.ReadByte(image, pos + 3) & 0xff));
                    pos += len + 2;
                    while (Marshal.ReadByte(image, pos) != 0xff ||
                           Marshal.ReadByte(image, pos + 1) == 0x00 ||
                           (Marshal.ReadByte(image, pos + 1) >= 0xd0 &&
                            Marshal.ReadByte(image, pos + 1) <= 0xd7))
                    {
                        pos += 1;
                        if (pos >= size) return 0;
                    }
                }
                else
                {
                    int len = (((Marshal.ReadByte(image, pos + 2) & 0xff)) << 8 |
                               (Marshal.ReadByte(image, pos + 3) & 0xff));
                    pos += len + 2;
                }
            }
            return 0;
        }
        
    }
    
}
#pragma warning restore 1591
#pragma warning restore 169

    */