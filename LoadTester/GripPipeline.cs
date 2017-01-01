using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

//namespace LoadTester
//{
    public class GripPipelinee
    {
        private Action<GripPipelinee> OnFinish;
        public GripPipelinee(Action<GripPipelinee> finished)
        {
            OnFinish = finished;
        }

        private Mat cvFlipOutput = new Mat();
        private Mat blurOutput = new Mat();
        private Mat hsvThreshold0Output = new Mat();
        private List<MatOfPoint> findContours0Output = new List<MatOfPoint>();
        private List<MatOfPoint> convexHulls0Output = new List<MatOfPoint>();
        public List<MatOfPoint> filterContoursOutput = new List<MatOfPoint>();
        private Mat hsvThreshold1Output = new Mat();
        private List<MatOfPoint> findContours1Output = new List<MatOfPoint>();
        private List<MatOfPoint> convexHulls1Output = new List<MatOfPoint>();

        public Mat lastImage = new Mat();

        public void process(Mat source0)
        {
            
            // Step CV_flip0:
            Mat cvFlipSrc = source0;
            //FlipCode cvFlipFlipcode = FlipCode.Y_AXIS;
            cvFlip(cvFlipSrc, FlipMode.Y, cvFlipOutput);
            cvFlipOutput.CopyTo(lastImage);
            // Step Blur0:
            Mat blurInput = cvFlipOutput;
            BlurType blurType = BlurType.Gaussian;
            double blurRadius = 12.612612612612613;
            blur(blurInput, blurType, blurRadius, blurOutput);

            // Step HSV_Threshold0:
            Mat hsvThreshold0Input = blurOutput;
            double[] hsvThreshold0Hue = {0.0, 41.774744027303754};
            double[] hsvThreshold0Saturation = {77.96762589928058, 255.0};
            double[] hsvThreshold0Value = {176.57374100719423, 255.0};
            hsvThreshold(hsvThreshold0Input, hsvThreshold0Hue, hsvThreshold0Saturation, hsvThreshold0Value,
                hsvThreshold0Output);

            // Step Find_Contours0:
            Mat findContours0Input = hsvThreshold0Output;
            bool findContours0ExternalOnly = false;
            findContours(findContours0Input, findContours0ExternalOnly, findContours0Output);

            // Step Convex_Hulls0:
            List<MatOfPoint> convexHulls0Contours = findContours0Output;
            convexHulls(convexHulls0Contours, convexHulls0Output);

            // Step Filter_Contours0:
            List<MatOfPoint> filterContoursContours = convexHulls0Output;
            double filterContoursMinArea = 5000.0;
            double filterContoursMinPerimeter = 0.0;
            double filterContoursMinWidth = 0.0;
            double filterContoursMaxWidth = 1000.0;
            double filterContoursMinHeight = 0.0;
            double filterContoursMaxHeight = 1000.0;
            double[] filterContoursSolidity = {0, 100};
            double filterContoursMaxVertices = 1000000.0;
            double filterContoursMinVertices = 0.0;
            double filterContoursMinRatio = 0.0;
            double filterContoursMaxRatio = 1000.0;
            filterContours(filterContoursContours, filterContoursMinArea, filterContoursMinPerimeter,
                filterContoursMinWidth, filterContoursMaxWidth, filterContoursMinHeight, filterContoursMaxHeight,
                filterContoursSolidity, filterContoursMaxVertices, filterContoursMinVertices, filterContoursMinRatio,
                filterContoursMaxRatio, filterContoursOutput);

            // Step HSV_Threshold1:
            Mat hsvThreshold1Input = blurOutput;
            double[] hsvThreshold1Hue = {93.88489208633094, 115.49488054607508};
            double[] hsvThreshold1Saturation = {66.50179856115108, 239.76962457337885};
            double[] hsvThreshold1Value = {0.0, 255.0};
            hsvThreshold(hsvThreshold1Input, hsvThreshold1Hue, hsvThreshold1Saturation, hsvThreshold1Value,
                hsvThreshold1Output);

            // Step Find_Contours1:
            Mat findContours1Input = hsvThreshold1Output;
            bool findContours1ExternalOnly = false;
            findContours(findContours1Input, findContours1ExternalOnly, findContours1Output);

            // Step Convex_Hulls1:
            List<MatOfPoint> convexHulls1Contours = findContours1Output;
            convexHulls(convexHulls1Contours, convexHulls1Output);
            OnFinish?.Invoke(this);
        }

        private void cvFlip(Mat src, FlipMode flipcode, Mat dst)
        {
            Cv2.Flip(src, dst, flipcode);
        }

        enum BlurType
        {
            Bilateral,
            Box,
            Gaussian,
            Median
        }

        private void blur(Mat input, BlurType type, double dradius, Mat output)
        {
            int radius = (int) (dradius + 0.5);
            int kernalSize;
            switch (type)
            {
                case BlurType.Bilateral:
                    Cv2.BilateralFilter(input, output, -1, radius, radius);
                    break;
                case BlurType.Box:
                    kernalSize = 2*radius + 1;
                    Cv2.Blur(input, output, new Size(kernalSize, kernalSize));
                    break;
                case BlurType.Gaussian:
                    kernalSize = 6*radius + 1;
                    Cv2.GaussianBlur(input, output, new Size(kernalSize, kernalSize), radius );
                    break;
                case BlurType.Median:
                    kernalSize = 2*radius + 1;
                    Cv2.MedianBlur(input, output, kernalSize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void filterContours(List<MatOfPoint> inputContours, double minArea,
            double minPerimeter, double minWidth, double maxWidth, double minHeight, double
                maxHeight, double[] solidity, double maxVertexCount, double minVertexCount, double
                minRatio, double maxRatio, List<MatOfPoint> output)
        {
            MatOfPoint hull = new MatOfPoint();
            output.Clear();
            foreach (MatOfPoint contour in inputContours)
            {
                Rect bb = Cv2.BoundingRect((InputArray) contour);
                if (bb.Width < minWidth || bb.Width > maxWidth) continue;
                if (bb.Height < minHeight || bb.Height > maxHeight) continue;
                double area = Cv2.ContourArea((InputArray) contour);
                if (area < minArea) continue;
                if (Cv2.ArcLength((InputArray)contour, true) < minPerimeter) continue;
                Cv2.ConvexHull(contour, hull);
                double solid = 100 * area / Cv2.ContourArea((InputArray)hull);
                if (solid < solidity[0] || solid > solidity[1]) continue;
                if (contour.Rows < minVertexCount || contour.Rows > maxVertexCount) continue;
                double ratio = bb.Width / (double)bb.Height;
                if (ratio < minRatio || ratio > maxRatio) continue;
                output.Add(contour);
            }
        }

        private void hsvThreshold(Mat input, double[] hue, double[] sat, double[] val, Mat output)
        {
            Cv2.CvtColor(input, output, ColorConversionCodes.BGR2HSV);
            Cv2.InRange(output, new Scalar(hue[0], sat[0], val[0]),
            new Scalar(hue[1], sat[1], val[1]), output);
        }

        private void findContours(Mat input, bool externalOnly, List<MatOfPoint> contours)
        {
            contours.Clear();
            RetrievalModes mode = externalOnly ? RetrievalModes.External : RetrievalModes.List;
            ContourApproximationModes method = ContourApproximationModes.ApproxSimple;
            Cv2.FindContours(input, contours, mode, method);
        }

        private void convexHulls(List<MatOfPoint> inputContours, List<MatOfPoint> outputContours)
        {
            outputContours.Clear();
            outputContours.Capacity = inputContours.Count;
            foreach (MatOfPoint points in inputContours)
            {
                Mat mat = new Mat();
                Cv2.ConvexHull(points, mat);
                outputContours.Add(new MatOfPoint(mat));
            }
        }
    }
//}
