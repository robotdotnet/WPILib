using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HAL.Base;
using HAL.Simulator;
using WPILib;
using WPILib.SmartDashboard;
using HAL = HAL.Base.HAL;
using OpenCvSharp;
using CSCore;

namespace LoadTester
{
    public class Class1 : IterativeRobot
    {

        Notifier notifier1;

        Thread thread;
        public override void RobotInit()
        {
            
            thread = new Thread(() =>
            {
                UsbCamera camera = new UsbCamera("Cam1", 0);
                camera.SetVideoMode(PixelFormat.Mjpeg, 640, 480, 30);

                MjpegServer MjpegServer = new MjpegServer("httpserver", 8080);
                MjpegServer.Source = camera;

                CvSink cvSink = new CvSink("cvsink");
                cvSink.Source = camera;

                CvSource cvSource = new CvSource("CvSource", PixelFormat.Mjpeg, 320, 240, 30);

                MjpegServer cvMjpegServer = new MjpegServer("cvhttpserver", 8081);
                cvMjpegServer.Source = cvSource;

                Mat test = new Mat();
                Mat shrink = new Mat();
                Mat flip = new Mat();
            });

            thread.Start();
            

            //CameraServer.Instance.StartAutomaticCapture();

            notifier1 = new Notifier(() =>
            {
                //Console.WriteLine("Notifier 1 called");
            });

            notifier1.StartPeriodic(0.01);
        }

        public override void DisabledPeriodic()
        {
        }

        public override void TeleopPeriodic()
        {
        }

        public static void Main(string[] args)
        {

            RobotBase.Main(null, typeof(Class1));
        }
    }
}
