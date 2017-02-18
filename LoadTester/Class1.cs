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
using System.IO;

namespace LoadTester
{
    public class MyRobot : IterativeRobot
    {
        Joystick joystick;
        Servo servo;
        AnalogGyro gyro;

        public override void RobotInit()
        {
            int status = 0;
            var ret = HALThreads.HAL_SetCurrentThreadPriority(true, 25, ref status);
            Console.WriteLine(status);
            Console.WriteLine(ret);
            Thread cameraThread = new Thread(() =>
            {
                // Get the USB Camera from the camera server
                UsbCamera camera = CameraServer.Instance.StartAutomaticCapture();
                camera.SetResolution(640, 480);

                // Get a CvSink. This will capture Mats from the Camera
                CvSink cvSink = CameraServer.Instance.GetVideo();
                // Setup a CvSource. This will send images back to the dashboard
                CvSource outputStream = CameraServer.Instance.PutVideo("Rectangle", 640, 480);

                // Mats are very expensive. Let's reuse this Mat.

                Mat mat = new Mat();

                while (true)
                {
                    // Tell the CvSink to grab a frame from the camera and put it
                    // in the source mat.  If there is an error notify the output.
                    if (cvSink.GrabFrame(mat) == 0)
                    {
                        Console.WriteLine("Timeout");
                        // Send the output the error.
                        outputStream.NotifyError(cvSink.GetError());
                        // skip the rest of the current iteration
                        continue;
                    }
                    // Put a rectangle on the image
                    Cv2.Rectangle(mat, new Point(100, 100), new Point(400, 400),
                            new Scalar(255, 255, 255), 5);
                    // Give the output stream a new image to display
                    outputStream.PutFrame(mat);
                }
            });
            cameraThread.IsBackground = true;
            cameraThread.Start();

            servo = new Servo(0);

            joystick = new Joystick(0);
            gyro = new AnalogGyro(0);
        }

        int count = 0;

        public override void DisabledInit()
        {
            
        }
        public override void DisabledPeriodic()
        {
            SmartDashboard.PutNumber("DisabledCount", count++);
        }

        public override void TeleopPeriodic()
        {
            if (joystick.GetRawButton(1))
            {
                servo.Set(0.0);
            }
            else if (joystick.GetRawButton(2))
            {
                servo.Set(1.0);
            }

            else
            {
                servo.Set(0.5);
            }
        }

        public override void RobotPeriodic()
        {
            //Console.WriteLine("Getting Gyro");
            SmartDashboard.PutNumber("Gyro", gyro.GetAngle());
        }

        public static void Main(string[] args)
        {
            RobotBase.Main(null, typeof(MyRobot));
            ;
        }
    }
}
