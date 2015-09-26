using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NIVision
{


    public struct String255
    {
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 255)]
        private byte[] array;
    }


    

    public struct ROI
    {
        private IntPtr roiPointer;
    }

    public struct DivisionModel
    {
        public float Kappa;
    }

    public struct FocalLength
    {
        public float Fx;
        public float Fy;
    }

    public struct PolyModel
    {
        public IntPtr Coeffs;
        public uint numCoeffs;
        public float p1;
        public float p2;

        public void WriteCoeffs(float[] coeffs)
        {
            Coeffs = Marshal.AllocHGlobal(sizeof (float) * coeffs.Length);
            Marshal.Copy(coeffs, 0, Coeffs, coeffs.Length);
            numCoeffs = (uint) coeffs.Length;
        }

        public float[] GetCoeffs()
        {
            float[] arr = new float[numCoeffs];
            Marshal.Copy(Coeffs, arr, 0, (int) numCoeffs);
            return arr;
        }
    }

    public struct DistortionModelParams
    {
        public DistortionModel distortionModel;
        public PolyModel polyModel;
        public DivisionModel divisionModel;
    }

    public struct PointFloat
    {
        public float X;
        public float Y;
    }

    public struct InternalParameters
    {
        private byte isInsufficientData;
        private FocalLength focalLength;
        private PointFloat opticalCenter;
    }

    public struct MaxGridSize
    {
        public uint xMax;
        public uint yMax;
    }

    public struct ImageSize
    {
        public uint xRes;
        public uint yRes;
    }

    public struct CalibrationReferencePoints
    {
        private IntPtr pixelCoords;
        public uint numPixelCoords;
        private IntPtr realCoords;
        public uint numRealCoords;
        public CalibrationUnit units;
        public ImageSize imageSize;
    }

    public struct GetCameraParametersReport
    {
        private IntPtr projectionMatrix;
        public uint ProjectionMatrixRows;
        public uint ProjectionMatrixCols;
        public DistortionModelParams distortion;
        public InternalParameters internalParams;
    }

    public struct CalibrationAxisInfo
    {
        public PointFloat center;
        public float RotationAngle;
        public AxisOrientation AxisDirection;
    }

    public struct CalibrationLearnSetupInfo
    {
        public CalibrationMode2 calibrationMethod;
        public DistortionModel distortionModel;
        public ScalingMethod scaleMode;
        public CalibrationROI roiMode;
        public byte LearnCorrectionMode;
    }

    public struct GridDescriptor
    {
        public float xStep;
        public float yStep;
        public CalibrationUnit unit;
    }

    public struct ErrorStatistics
    {
        public double Mean;
        public double Maximum;
        public double StandardDeviation;
        public double Distortion;
    }

    public struct GetCalibrationInfoReport
    {
        private IntPtr userRoi;
        private IntPtr calibrationRoi;
        private CalibrationAxisInfo axisInfo;
        private CalibrationLearnSetupInfo learnSetupInfo;
        private GridDescriptor gridDescriptor;
        private IntPtr errorMap;
        private IntPtr errorMapRows;
        private IntPtr ErrorMapCols;
        private ErrorStatistics errorStatistics;
    }

    public struct EdgePolarity
    {
        private EdgePolaritySearchMode start;
        private EdgePolaritySearchMode end;
    }

    public struct ClampSettings
    {
        private double angleRange;
        private EdgePolarity edgePolarity;
    }

    public struct PointDouble
    {
        private double x;
        private double y;
    }

    public struct PointDoublePair
    {
        private PointDouble start;
        private PointDouble end;
    }

    public struct ClampResults
    {
        private double distancePix; //Defines the Pixel world distance.
        private double distanceRealWorld; //Defines the real world distance.
        private double angleAbs; //Defines the absolute angle.
        private double angleRelative; //Defines the relative angle.
    }

    public struct ClampPoints
    {
        private PointDoublePair pixel; //Specifies the pixel world point pair for clamp.
        private PointDoublePair realWorld; //Specifies the real world point pair for clamp.
    }

    public struct RGBValue
    {
        private byte B; //The blue value of the color.
        private byte G; //The green value of the color.
        private byte R; //The red value of the color.
        private byte alpha; //The alpha value of the color,
    }

    public struct ClampOverlaySettings
    {
        private int showSearchArea; //If TRUE, the function overlays the search area on the image.
        private int showCurves; //If TRUE, the function overlays the curves on the image.
        private int showClampLocation; //If TRUE, the function overlays the clamp location on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue curvesColor; //Specifies the RGB color value to use to overlay the curves.
        private RGBValue clampLocationsColor; //Specifies the RGB color value to use to overlay the clamp locations.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the group overlay name for the step overlays.
    }

    public struct ClampMax2Report
    {
        private ClampResults clampResults; //Specifies the Clamp results information returned by the function.
        private ClampPoints clampPoints; //Specifies the clamp points information returned by the function.
        private uint calibrationValid; //Specifies if the calibration information is valid or not.
    }

    public struct ContourFitSplineReport
    {
        private IntPtr points;
        private int numberOfPoints;
    }

    public struct LineFloat
    {
        private PointFloat start; //The coordinate location of the start of the line.
        private PointFloat end; //The coordinate location of the end of the line.
    }

    public struct LineEquation
    {
        private double a; //The a coefficient of the line equation.
        private double b; //The b coefficient of the line equation.
        private double c; //The c coefficient of the line equation.
    }

    public struct ContourFitLineReport
    {
        private LineFloat lineSegment; //Line Segment represents the intersection of the line equation and the contour.

        private LineEquation lineEquation;
            //Line Equation is a structure of three coefficients A, B, and C of the equation in the normal form (Ax + By + C=0) of the best fit line.
    }

    public struct ContourFitPolynomialReport
    {
        private IntPtr bestFit; //It returns the points of the best-fit polynomial.
        private int numberOfPoints; //Number of Best fit points returned.

        private IntPtr polynomialCoefficients;
            //Polynomial Coefficients returns the coefficients of the polynomial equation.

        private int numberOfCoefficients; //Number of Coefficients returned in the polynomial coefficients array.
    }

    public struct PartialCircle
    {
        private PointFloat center; //Center of the circle.
        private double radius; //Radius of the circle.
        private double startAngle; //Start angle of the fitted structure.
        private double endAngle; //End angle of the fitted structure.
    }

    public struct PartialEllipse
    {
        private PointFloat center; //Center of the Ellipse.
        private double angle; //Angle of the ellipse.
        private double majorRadius; //The length of the semi-major axis of the ellipse.
        private double minorRadius; //The length of the semi-minor axis of the ellipse.
        private double startAngle; //Start angle of the fitted structure.
        private double endAngle; //End angle of the fitted structure.
    }

    public struct SetupMatchPatternData
    {
        private IntPtr matchSetupData; //String containing the match setup data.
        private int numMatchSetupData; //Number of match setup data.
    }

    public struct RangeSettingDouble
    {
        private SettingType settingType;
            //Match Constraints specifies the match option whose values you want to constrain by the given range.

        private double min; //Min is the minimum value of the range for a given Match Constraint.
        private double max; //Max is the maximum value of the range for a given Match Constraint.
    }

    public struct GeometricAdvancedSetupDataOption
    {
        private GeometricSetupDataItem type; //It determines the option you want to use during the matching phase.
        private double value; //Value is the value for the option you want to use during the matching phase.
    }

    public struct ContourInfoReport
    {
        private IntPtr pointsPixel;
            //Points (pixel) specifies the location of every point detected on the curve, in pixels.

        private uint numPointsPixel; //Number of points pixel elements.

        private IntPtr pointsReal;
            //Points (real) specifies the location of every point detected on the curve, in calibrated units.

        private uint numPointsReal; //Number of points real elements.

        private IntPtr curvaturePixel;
            //Curvature Pixel displays the curvature profile for the selected contour, in pixels.

        private uint numCurvaturePixel; //Number of curvature pixels.

        private IntPtr curvatureReal;
            //Curvature Real displays the curvature profile for the selected contour, in calibrated units.

        private uint numCurvatureReal; //Number of curvature Real elements.
        private double length; //Length (pixel) specifies the length, in pixels, of the curves in the image.

        private double lengthReal;
            //Length (real) specifies the length, in calibrated units, of the curves within the curvature range.

        private uint hasEquation; //Has Equation specifies whether the contour has a fitted equation.
    }

    public struct ROILabel
    {
        private IntPtr className; //Specifies the classname you want to segment.
        private uint label; //Label is the label number associated with the Class Name.
    }

    public struct SupervisedColorSegmentationReport
    {
        private IntPtr labelOut; //The Roi labels array.
        private uint numLabelOut; //The number of elements in labelOut array.
    }

    public struct LabelToROIReport
    {
        private IntPtr roiArray; //Array of ROIs.
        private uint numOfROIs; //Number of ROIs in the roiArray.
        private IntPtr labelsOutArray; //Array of labels.
        private uint numOfLabels; //Number of labels.
        private IntPtr isTooManyVectorsArray; //isTooManyVectorsArray array.
        private uint isTooManyVectorsArraySize; //Number of elements in isTooManyVectorsArray.
    }

    public struct ColorSegmenationOptions
    {
        private uint windowX; //X is the window size in x direction.
        private uint windowY; //Y is the window size in y direction.
        private uint stepSize; //Step Size is the distance between two windows.
        private uint minParticleArea; //Min Particle Area is the minimum number of allowed pixels.
        private uint maxParticleArea; //Max Particle Area is the maximum number of allowed pixels.

        private short isFineSegment;
            //When enabled, the step processes the boundary pixels of each segmentation cluster using a step size of 1.
    }

    public struct ClassifiedCurve
    {
        private double length; //Specifies the length, in pixels, of the curves within the curvature range.

        private double lengthReal;
            //specifies the length, in calibrated units, of the curves within the curvature range.

        private double maxCurvature; //specifies the maximum curvature, in pixels, for the selected curvature range.

        private double maxCurvatureReal;
            //specifies the maximum curvature, in calibrated units, for the selected curvature range.

        private uint label; //specifies the class to which the the sample belongs.
        private IntPtr curvePoints; //Curve Points is a point-coordinate cluster that defines the points of the curve.
        private uint numCurvePoints; //Number of curve points.
    }

    public struct RangeDouble
    {
        private double minValue; //The minimum value of the range.
        private double maxValue; //The maximum value of the range.
    }

    public struct RangeLabel
    {
        private RangeDouble range; //Specifies the range of curvature values.
        private uint label; //Class Label specifies the class to which the the sample belongs.
    }

    public struct CurvatureAnalysisReport
    {
        private IntPtr curves;
        private uint numCurves;
    }

    public struct Disparity
    {
        private PointDouble current; //Current is a array of points that defines the target contour.
        private PointDouble reference; //reference is a array of points that defines the template contour.

        private double distance;
            //Specifies the distance, in pixels, between the template contour point and the target contour point.
    }

    public struct ComputeDistancesReport
    {
        private IntPtr distances; //Distances is an array containing the computed distances.
        private uint numDistances; //Number elements in the distances array.

        private IntPtr distancesReal;
            //Distances Real is an array containing the computed distances in calibrated units.

        private uint numDistancesReal; //Number of elements in real distances array.
    }

    public struct MatchMode
    {
        private uint rotation;
            //Rotation When enabled, the Function searches for occurrences of the template in the inspection image, allowing for template matches to be rotated.

        private uint scale;
            //Rotation When enabled, the Function searches for occurrences of the template in the inspection image, allowing for template matches to be rotated.

        private uint occlusion; //Occlusion specifies whether or not to search for occluded versions of the shape.
    }

    public struct ClassifiedDisparity
    {
        private double length;
            //Length (pixel) specifies the length, in pixels, of the curves within the curvature range.

        private double lengthReal;
            //Length (real) specifies the length, in calibrated units, of the curves within the curvature range.

        private double maxDistance;
            //Maximum Distance (pixel) specifies the maximum distance, in pixels, between points along the selected contour and the template contour.

        private double maxDistanceReal;
            //Maximum Distance (real) specifies the maximum distance, in calibrated units, between points along the selected contour and the template contour.

        private uint label; //Class Label specifies the class to which the the sample belongs.

        private IntPtr templateSubsection;
            //Template subsection points is an array of points that defines the boundary of the template.

        private uint numTemplateSubsection; //Number of reference points.

        private IntPtr targetSubsection;
            //Current Points(Target subsection points) is an array of points that defines the boundary of the target.

        private uint numTargetSubsection; //Number of current points.
    }

    public struct ClassifyDistancesReport
    {
        private IntPtr classifiedDistances; //Disparity array containing the classified distances.
        private uint numClassifiedDistances; //Number of elements in the disparity array.
    }

    public struct ContourComputeCurvatureReport
    {
        private IntPtr curvaturePixel;
            //Curvature Pixel displays the curvature profile for the selected contour, in pixels.

        private uint numCurvaturePixel; //Number of curvature pixels.

        private IntPtr curvatureReal;
            //Curvature Real displays the curvature profile for the selected contour, in calibrated units.

        private uint numCurvatureReal; //Number of curvature Real elements.
    }

    public struct ContourOverlaySettings
    {
        private uint overlay; //Overlay specifies whether to display the overlay on the image.
        private RGBValue color; //Color is the color of the overlay.
        private uint width; //Width specifies the width of the overlay in pixels.

        private uint maintainWidth;
            //Maintain Width? specifies whether you want the overlay measured in screen pixels or image pixels.
    }

    public struct CurveParameters
    {
        private ExtractionMode extractionMode; //Specifies the method the function uses to identify curves in the image.
        private int threshold; //Specifies the minimum contrast a seed point must have in order to begin a curve.

        private EdgeFilterSize filterSize;
            //Specifies the width of the edge filter the function uses to identify curves in the image.

        private int minLength; //Specifies the length, in pixels, of the smallest curve the function will extract.

        private int searchStep;
            //Search Step Size specifies the distance, in the y direction, between the image rows that the algorithm inspects for curve seed points.

        private int maxEndPointGap;
            //Specifies the maximum gap, in pixels, between the endpoints of a curve that the function identifies as a closed curve.

        private int subpixel; //Subpixel specifies whether to detect curve points with subpixel accuracy.
    }

    public struct ExtractContourReport
    {
        private IntPtr contourPoints; //Contour Points specifies every point found on the contour.
        private int numContourPoints; //Number of contour points.

        private IntPtr sourcePoints;
            //Source Image Points specifies every point found on the contour in the source image.

        private int numSourcePoints; //Number of source points.
    }

    public struct ConnectionConstraint
    {
        private ConnectionConstraintType constraintType;
            //Constraint Type specifies what criteria to use to consider two curves part of a contour.

        private RangeDouble range; //Specifies range for a given Match Constraint.
    }

    public struct ExtractTextureFeaturesReport
    {
        private IntPtr waveletBands; //The array having all the Wavelet Banks used for extraction.
        private int numWaveletBands; //Number of wavelet banks in the Array.
        private IntPtr textureFeatures; //2-D array to store all the Texture features extracted.
        private int textureFeaturesRows; //Number of Rows in the Texture Features array.
        private int textureFeaturesCols; //Number of Cols in Texture Features array.
    }

    public struct WaveletBandsReport
    {
        private IntPtr LLBand; //2-D array for LL Band.
        private IntPtr LHBand; //2-D array for LH Band.
        private IntPtr HLBand; //2-D array for HL Band.
        private IntPtr HHBand; //2-D array for HH Band.
        private IntPtr LLLBand; //2-D array for LLL Band.
        private IntPtr LLHBand; //2-D array for LLH Band.
        private float LHLBand; //2-D array for LHL Band.
        private IntPtr LHHBand; //2-D array for LHH Band.
        private int rows; //Number of Rows for each of the 2-D arrays.
        private int cols; //Number of Columns for each of the 2-D arrays.
    }

    public struct CircleFitOptions
    {
        private int maxRadius;
            //Specifies the acceptable distance, in pixels, that a point determined to belong to the circle can be from the perimeter of the circle.

        private double stepSize; //Step Size is the angle, in degrees, between each radial line in the annular region.
        private RakeProcessType processType; //Method used to process the data extracted for edge detection.
    }

    public struct EdgeOptions2
    {
        private EdgePolaritySearchMode polarity; //Specifies the polarity of the edges to be found.
        private uint kernelSize; //Specifies the size of the edge detection kernel.

        private uint width;
            //Specifies the number of pixels averaged perpendicular to the search direction to compute the edge profile strength at each point along the search ROI.

        private float minThreshold;
            //Specifies the minimum edge strength (gradient magnitude) required for a detected edge.

        private InterpolationMethod interpolationType;
            //Specifies the interpolation method used to locate the edge position.

        private ColumnProcessingMode columnProcessingMode; //Specifies the method used to find the straight edge.
    }

    public struct FindCircularEdgeOptions
    {
        private SpokeDirection direction; //Specifies the Spoke direction to search in the ROI.
        private int showSearchArea; //If TRUE, the function overlays the search area on the image.

        private int showSearchLines;
            //If TRUE, the function overlays the search lines used to locate the edges on the image.

        private int showEdgesFound; //If TRUE, the function overlays the locations of the edges found on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue searchLinesColor; //Specifies the RGB color value to use to overlay the search lines.
        private RGBValue searchEdgesColor; //Specifies the RGB color value to use to overlay the search edges.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the overlay group name to assign to the overlays.
        private EdgeOptions2 edgeOptions; //Specifies the edge detection options along a single search line.
    }

    public struct FindConcentricEdgeOptions
    {
        private ConcentricRakeDirection direction; //Specifies the Concentric Rake direction.
        private int showSearchArea; //If TRUE, the function overlays the search area on the image.

        private int showSearchLines;
            //If TRUE, the function overlays the search lines used to locate the edges on the image.

        private int showEdgesFound; //If TRUE, the function overlays the locations of the edges found on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue searchLinesColor; //Specifies the RGB color value to use to overlay the search lines.
        private RGBValue searchEdgesColor; //Specifies the RGB color value to use to overlay the search edges.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the overlay group name to assign to the overlays.
        private EdgeOptions2 edgeOptions; //Specifies the edge detection options along a single search line.
    }

    public struct ConcentricEdgeFitOptions
    {
        private int maxRadius;
            //Specifies the acceptable distance, in pixels, that a point determined to belong to the circle can be from the perimeter of the circle.

        private double stepSize; //The sampling factor that determines the gap between the rake lines.
        private RakeProcessType processType; //Method used to process the data extracted for edge detection.
    }

    public struct FindConcentricEdgeReport
    {
        private PointFloat startPt; //Pixel Coordinates for starting point of the edge.
        private PointFloat endPt; //Pixel Coordinates for end point of the edge.
        private PointFloat startPtCalibrated; //Real world Coordinates for starting point of the edge.
        private PointFloat endPtCalibrated; //Real world Coordinates for end point of the edge.
        private double angle; //Angle of the edge found.
        private double angleCalibrated; //Calibrated angle of the edge found.
        private double straightness; //The straightness value of the detected straight edge.
        private double avgStrength; //Average strength of the egde found.
        private double avgSNR; //Average SNR(Signal to Noise Ratio) for the edge found.
        private int lineFound; //If the edge is found or not.
    }

    public struct FindCircularEdgeReport
    {
        private PointFloat centerCalibrated; //Real world Coordinates of the Center.
        private double radiusCalibrated; //Real world radius of the Circular Edge found.
        private PointFloat center; //Pixel Coordinates of the Center.
        private double radius; //Radius in pixels of the Circular Edge found.
        private double roundness; //The roundness of the calculated circular edge.
        private double avgStrength; //Average strength of the egde found.
        private double avgSNR; //Average SNR(Signal to Noise Ratio) for the edge found.
        private int circleFound; //If the circlular edge is found or not.
    }

    public struct WindowSize
    {
        private int x; //Window lenght on X direction.
        private int y; //Window lenght on Y direction.
        private int stepSize; //Distance between windows.
    }

    public struct DisplacementVector
    {
        private int x; //length on X direction.
        private int y; //length on Y direction.
    }

    public struct WaveletOptions
    {
        private WaveletType typeOfWavelet; //Type of wavelet(db, bior.
        private float minEnergy; //Minimum Energy in the bands to consider for texture defect detection.
    }

    public struct CooccurrenceOptions
    {
        private int level; //Level/size of matrix.
        private DisplacementVector displacement; //Displacemnet between pixels to accumulate the matrix.
    }

    public struct ParticleClassifierLocalThresholdOptions
    {
        private LocalThresholdMethod method; //Specifies the local thresholding method the function uses.
        private ParticleType particleType; //Specifies what kind of particles to look for.

        private uint windowWidth;
            //The width of the rectangular window around the pixel on which the function performs the local threshold.

        private uint windowHeight;
            //The height of the rectangular window around the pixel on which the function performs the local threshold.

        private double deviationWeight;
            //Specifies the k constant used in the Niblack local thresholding algorithm, which determines the weight applied to the variance calculation.
    }

    public struct RangeFloat
    {
        private float minValue; //The minimum value of the range.
        private float maxValue; //The maximum value of the range.
    }

    public struct ParticleClassifierAutoThresholdOptions
    {
        private ThresholdMethod method;
            //The method for binary thresholding, which specifies how to calculate the classes.

        private ParticleType particleType; //Specifies what kind of particles to look for.
        private RangeFloat limits; //The limits on the automatic threshold range.
    }

    public struct ParticleClassifierPreprocessingOptions2
    {
        private ParticleClassifierThresholdType thresholdType; //The type of threshold to perform on the image.
        private RangeFloat manualThresholdRange; //The range of pixels to keep if manually thresholding the image.

        private ParticleClassifierAutoThresholdOptions autoThresholdOptions;
            //The options used to auto threshold the image.

        private ParticleClassifierLocalThresholdOptions localThresholdOptions;
            //The options used to local threshold the image.

        private int rejectBorder; //Set this element to TRUE to reject border particles.
        private int numErosions; //The number of erosions to perform.
    }

    public struct MeasureParticlesReport
    {
        private IntPtr pixelMeasurements; //The measurements on the particles in the image, in pixel coordinates.

        private IntPtr calibratedMeasurements;
            //The measurements on the particles in the image, in real-world coordinates.

        private UIntPtr numParticles; //The number of particles on which measurements were taken.
        private UIntPtr numMeasurements; //The number of measurements taken.
    }

    public struct GeometricPatternMatch3
    {
        private PointFloat position; //The location of the origin of the template in the match.
        private float rotation; //The rotation of the match relative to the template image, in degrees.

        private float scale;
            //The size of the match relative to the size of the template image, expressed as a percentage.

        private float score; //The accuracy of the match.

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner; //An array of four points describing the rectangle surrounding the template image.

        private int inverse; //This element is TRUE if the match is an inverse of the template image.
        private float occlusion; //The percentage of the match that is occluded.

        private float templateMatchCurveScore;
            //The accuracy of the match obtained by comparing the template curves to the curves in the match region.

        private float matchTemplateCurveScore;
            //The accuracy of the match obtained by comparing the curves in the match region to the template curves.

        private float correlationScore;
            //The accuracy of the match obtained by comparing the template image to the match region using a correlation metric that compares the two regions as a function of their pixel values.

        private PointFloat calibratedPosition; //The location of the origin of the template in the match.
        private float calibratedRotation; //The rotation of the match relative to the template image, in degrees.

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] calibratedCorner;
            //An array of four points describing the rectangle surrounding the template image.
    }

    public struct MatchGeometricPatternAdvancedOptions3
    {
        private uint subpixelIterations;
            //Specifies the maximum number of incremental improvements used to refine matches with subpixel information.

        private double subpixelTolerance;
            //Specifies the maximum amount of change, in pixels, between consecutive incremental improvements in the match position before the function stops refining the match position.

        private uint initialMatchListLength; //Specifies the maximum size of the match list.

        private int targetTemplateCurveScore;
            //Set this element to TRUE to specify that the function should calculate the match curve to template curve score and return it for each match result.

        private int correlationScore;
            //Set this element to TRUE to specify that the function should calculate the correlation score and return it for each match result.

        private double minMatchSeparationDistance;
            //Specifies the minimum separation distance, in pixels, between the origins of two matches that have unique positions.

        private double minMatchSeparationAngle;
            //Specifies the minimum angular difference, in degrees, between two matches that have unique angles.

        private double minMatchSeparationScale;
            //Specifies the minimum difference in scale, expressed as a percentage, between two matches that have unique scales.

        private double maxMatchOverlap;
            //Specifies the maximum amount of overlap, expressed as a percentage, allowed between the bounding rectangles of two unique matches.

        private int coarseResult;
            //Specifies whether you want the function to spend less time accurately estimating the location of a match.

        private int enableCalibrationSupport;
            //Set this element to TRUE to specify the algorithm treat the inspection image as a calibrated image.

        private ContrastMode enableContrastReversal;
            //Use this element to specify the contrast of the matches to search for in the image.

        private GeometricMatchingSearchStrategy matchStrategy; //Specifies the aggressiveness of the search strategy.

        private uint refineMatchFactor;
            //Specifies the factor that is applied to the number of matches requested by the user to determine the number of matches that are refined at the initial matching stage.

        private uint subpixelMatchFactor;
            //Specifies the factor that is applied to the number of matches requested by the user to determine the number of matches that are evaluated at the final subpixel matching stage.
    }

    public struct ColorOptions
    {
        private ColorClassificationResolution colorClassificationResolution;
            //Specifies the color resolution of the classifier.

        private uint useLuminance; //Specifies if the luminance band is going to be used in the feature vector.
        private ColorMode colorMode; //Specifies the color mode of the classifier.
    }

    public struct SampleScore
    {
        private IntPtr className; //The name of the class.
        private float distance; //The distance from the item to this class.
        private uint index; //index of this sample.
    }

    public struct ClassifierReportAdvanced
    {
        private IntPtr bestClassName; //The name of the best class for the sample.
        private float classificationScore; //The similarity of the sample and the two closest classes in the classifier.
        private float identificationScore; //The similarity of the sample and the assigned class.
        private IntPtr allScores; //All classes and their scores.
        private int allScoresSize; //The number of entries in allScores.
        private IntPtr sampleScores; //All samples and their scores.
        private int sampleScoresSize; //The number of entries in sampleScores.
    }

    public struct LearnGeometricPatternAdvancedOptions2
    {
        private double minScaleFactor; //Specifies the minimum scale factor that the template is learned for.
        private double maxScaleFactor; //Specifies the maximum scale factor the template is learned for.
        private double minRotationAngleValue; //Specifies the minimum rotation angle the template is learned for.
        private double maxRotationAngleValue; //Specifies the maximum rotation angle the template is learned for.

        private uint imageSamplingFactor;
            //Specifies the factor that is used to subsample the template and the image for the initial matching phase.
    }

    public struct ParticleFilterOptions2
    {
        private int rejectMatches;
            //Set this parameter to TRUE to transfer only those particles that do not meet all the criteria.

        private int rejectBorder; //Set this element to TRUE to reject border particles.
        private int fillHoles; //Set this element to TRUE to fill holes in particles.

        private int connectivity8;
            //Set this parameter to TRUE to use connectivity-8 to determine whether particles are touching.
    }

    public struct FindEdgeOptions2
    {
        private RakeDirection direction; //The direction to search in the ROI.
        private int showSearchArea; //If TRUE, the function overlays the search area on the image.

        private int showSearchLines;
            //If TRUE, the function overlays the search lines used to locate the edges on the image.

        private int showEdgesFound; //If TRUE, the function overlays the locations of the edges found on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue searchLinesColor; //Specifies the RGB color value to use to overlay the search lines.
        private RGBValue searchEdgesColor; //Specifies the RGB color value to use to overlay the search edges.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the overlay group name to assign to the overlays.
        private EdgeOptions2 edgeOptions; //Specifies the edge detection options along a single search line.
    }

    public struct FindEdgeReport
    {
        private IntPtr straightEdges; //An array of straight edges detected.
        private uint numStraightEdges; //Indicates the number of straight edges found.
    }

    public struct FindTransformRectOptions2
    {
        private FindReferenceDirection direction;
            //Specifies the direction and orientation in which the function searches for the primary axis.

        private int showSearchArea; //If TRUE, the function overlays the search area on the image.

        private int showSearchLines;
            //If TRUE, the function overlays the search lines used to locate the edges on the image.

        private int showEdgesFound; //If TRUE, the function overlays the locations of the edges found on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue searchLinesColor; //Specifies the RGB color value to use to overlay the search lines.
        private RGBValue searchEdgesColor; //Specifies the RGB color value to use to overlay the search edges.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the overlay group name to assign to the overlays.
        private EdgeOptions2 edgeOptions; //Specifies the edge detection options along a single search line.
    }

    public struct FindTransformRectsOptions2
    {
        private FindReferenceDirection direction;
            //Specifies the direction and orientation in which the function searches for the primary axis.

        private int showSearchArea; //If TRUE, the function overlays the search area on the image.

        private int showSearchLines;
            //If TRUE, the function overlays the search lines used to locate the edges on the image.

        private int showEdgesFound; //If TRUE, the function overlays the locations of the edges found on the image.

        private int showResult;
            //If TRUE, the function overlays the hit lines to the object and the edge used to generate the hit line on the result image.

        private RGBValue searchAreaColor; //Specifies the RGB color value to use to overlay the search area.
        private RGBValue searchLinesColor; //Specifies the RGB color value to use to overlay the search lines.
        private RGBValue searchEdgesColor; //Specifies the RGB color value to use to overlay the search edges.
        private RGBValue resultColor; //Specifies the RGB color value to use to overlay the results.
        private IntPtr overlayGroupName; //Specifies the overlay group name to assign to the overlays.

        private EdgeOptions2 primaryEdgeOptions;
            //Specifies the parameters used to compute the edge gradient information and detect the edges for the primary ROI.

        private EdgeOptions2 secondaryEdgeOptions;
            //Specifies the parameters used to compute the edge gradient information and detect the edges for the secondary ROI.
    }

    public struct ReadTextReport3
    {
        private IntPtr readString; //The read string.
        private IntPtr characterReport; //An array of reports describing the properties of each identified character.
        private int numCharacterReports; //The number of identified characters.
        private IntPtr roiBoundingCharacters; //An array specifying the coordinates of the character bounding ROI.
    }

    public struct CharacterStatistics
    {
        private int left; //The left offset of the character bounding rectangles in the current ROI.
        private int top; //The top offset of the character bounding rectangles in the current ROI.
        private int width; //The width of each of the characters you trained in the current ROI.
        private int height; //The height of each trained character in the current ROI.
        private int characterSize; //The size of the character in pixels.
    }

    public struct CharReport3
    {
        private IntPtr character; //The character value.

        private int classificationScore;
            //The degree to which the assigned character class represents the object better than the other character classes in the character set.

        private int verificationScore;
            //The similarity of the character and the reference character for the character class.

        private int verified;
            //This element is TRUE if a reference character was found for the character class and FALSE if a reference character was not found.

        private int lowThreshold; //The minimum value of the threshold range used for this character.
        private int highThreshold; //The maximum value of the threshold range used for this character.
        private CharacterStatistics characterStats; //Describes the characters segmented in the ROI.
    }

    public struct ArcInfo2
    {
        private PointFloat center; //The center point of the arc.
        private double radius; //The radius of the arc.
        private double startAngle; //The starting angle of the arc, specified counter-clockwise from the x-axis.
        private double endAngle; //The ending angle of the arc, specified counter-clockwise from the x-axis.
    }

    public struct EdgeReport2
    {
        private IntPtr edges; //An array of edges detected.
        private uint numEdges; //Indicates the number of edges detected.

        private IntPtr gradientInfo;
            //An array that contains the calculated edge strengths along the user-defined search area.

        private uint numGradientInfo; //Indicates the number of elements contained in gradientInfo.

        private int calibrationValid;
            //Indicates if the calibration data corresponding to the location of the edges is correct.
    }

    public struct SearchArcInfo
    {
        private ArcInfo2 arcCoordinates; //Describes the arc used for edge detection.
        private EdgeReport2 edgeReport; //Describes the edges found in this search line.
    }

    public struct ConcentricRakeReport2
    {
        private IntPtr firstEdges; //The first edge point detected along each search line in the ROI.
        private uint numFirstEdges; //The number of points in the firstEdges array.
        private IntPtr lastEdges; //The last edge point detected along each search line in the ROI.
        private uint numLastEdges; //The number of points in the lastEdges array.
        private IntPtr searchArcs; //Contains the arcs used for edge detection and the edge information for each arc.
        private uint numSearchArcs; //The number of arcs in the searchArcs array.
    }

    public struct SpokeReport2
    {
        private IntPtr firstEdges; //The first edge point detected along each search line in the ROI.
        private uint numFirstEdges; //The number of points in the firstEdges array.
        private IntPtr lastEdges; //The last edge point detected along each search line in the ROI.
        private uint numLastEdges; //The number of points in the lastEdges array.
        private IntPtr searchLines; //The search lines used for edge detection.
        private uint numSearchLines; //The number of search lines used in the edge detection.
    }

    public struct EdgeInfo
    {
        private PointFloat position; //The location of the edge in the image.
        private PointFloat calibratedPosition; //The position of the edge in the image in real-world coordinates.
        private double distance; //The location of the edge from the first point along the boundary of the input ROI.

        private double calibratedDistance;
            //The location of the edge from the first point along the boundary of the input ROI in real-world coordinates.

        private double magnitude; //The intensity contrast at the edge.
        private double noisePeak; //The strength of the noise associated with the current edge.
        private int rising; //Indicates the polarity of the edge.
    }

    public struct SearchLineInfo
    {
        private LineFloat lineCoordinates; //The endpoints of the search line.
        private EdgeReport2 edgeReport; //Describes the edges found in this search line.
    }

    public struct RakeReport2
    {
        private IntPtr firstEdges; //The first edge point detected along each search line in the ROI.
        private uint numFirstEdges; //The number of points in the firstEdges array.
        private IntPtr lastEdges; //The last edge point detected along each search line in the ROI.
        private uint numLastEdges; //The number of points in the lastEdges array.
        private IntPtr searchLines; //The search lines used for edge detection.
        private uint numSearchLines; //The number of search lines used in the edge detection.
    }

    public struct TransformBehaviors
    {
        private GroupBehavior ShiftBehavior;
            //Specifies the behavior of an overlay group when a shift operation is applied to an image.

        private GroupBehavior ScaleBehavior;
            //Specifies the behavior of an overlay group when a scale operation is applied to an image.

        private GroupBehavior RotateBehavior;
            //Specifies the behavior of an overlay group when a rotate operation is applied to an image.

        private GroupBehavior SymmetryBehavior;
            //Specifies the behavior of an overlay group when a symmetry operation is applied to an image.
    }

    public struct QRCodeDataToken
    {
        private QRStreamMode mode; //Specifies the stream mode or the format of the data that is encoded in the QR code.
        private uint modeData; //Indicates specifiers used by the user to postprocess the data if it requires it.
        private IntPtr data; //Shows the encoded data in the QR code.
        private uint dataLength; //Specifies the length of the data found in the QR code.
    }

    public struct ParticleFilterOptions
    {
        private int rejectMatches;
            //Set this parameter to TRUE to transfer only those particles that do not meet all the criteria.

        private int rejectBorder; //Set this element to TRUE to reject border particles.

        private int connectivity8;
            //Set this parameter to TRUE to use connectivity-8 to determine whether particles are touching.
    }

    public struct StraightEdgeReport2
    {
        private IntPtr straightEdges; //Contains an array of found straight edges.
        private uint numStraightEdges; //Indicates the number of straight edges found.
        private IntPtr searchLines; //Contains an array of all search lines used in the detection.
        private uint numSearchLines; //The number of search lines used in the edge detection.
    }

    public struct StraightEdgeOptions
    {
        private uint numLines; //Specifies the number of straight edges to find.
        private StraightEdgeSearchMode searchMode; //Specifies the method used to find the straight edge.
        private double minScore; //Specifies the minimum score of a detected straight edge.
        private double maxScore; //Specifies the maximum score of a detected edge.
        private double orientation; //Specifies the angle at which the straight edge is expected to be found.

        private double angleRange;
            //Specifies the +/- range around the orientation within which the straight edge is expected to be found.

        private double angleTolerance; //Specifies the expected angular accuracy of the straight edge.
        private uint stepSize; //Specifies the gap in pixels between the search lines used with the rake-based methods.

        private double minSignalToNoiseRatio;
            //Specifies the minimum signal to noise ratio (SNR) of the edge points used to fit the straight edge.

        private double minCoverage;
            //Specifies the minimum number of points as a percentage of the number of search lines that need to be included in the detected straight edge.

        private uint houghIterations; //Specifies the number of iterations used in the Hough-based method.
    }

    public struct StraightEdge
    {
        private LineFloat straightEdgeCoordinates; //End points of the detected straight edge in pixel coordinates.

        private LineFloat calibratedStraightEdgeCoordinates;
            //End points of the detected straight edge in real-world coordinates.

        private double angle; //Angle of the found edge using the pixel coordinates.
        private double calibratedAngle; //Angle of the found edge using the real-world coordinates.
        private double score; //Describes the score of the detected edge.
        private double straightness; //The straightness value of the detected straight edge.

        private double averageSignalToNoiseRatio;
            //Describes the average signal to noise ratio (SNR) of the detected edge.

        private int calibrationValid; //Indicates if the calibration data for the straight edge is valid.
        private IntPtr usedEdges; //An array of edges that were used to determine this straight line.
        private uint numUsedEdges; //Indicates the number of edges in the usedEdges array.
    }

    public struct QRCodeSearchOptions
    {
        private QRRotationMode rotationMode; //Specifies the amount of QR code rotation the function should allow for.

        private uint skipLocation;
            //If set to TRUE, specifies that the function should assume that the QR code occupies the entire image (or the entire search region).

        private uint edgeThreshold;
            //The strength of the weakest edge the function uses to find the coarse location of the QR code in the image.

        private QRDemodulationMode demodulationMode; //The demodulation mode the function uses to locate the QR code.
        private QRCellSampleSize cellSampleSize; //The cell sample size the function uses to locate the QR code.
        private QRCellFilterMode cellFilterMode; //The cell filter mode the function uses to locate the QR code.
        private uint skewDegreesAllowed; //Specifies the amount of skew in the QR code the function should allow for.
    }

    public struct QRCodeSizeOptions
    {
        private uint minSize; //Specifies the minimum size (in pixels) of the QR code in the image.
        private uint maxSize; //Specifies the maximum size (in pixels) of the QR code in the image.
    }

    public struct QRCodeDescriptionOptions
    {
        private QRDimensions dimensions;
            //The number of rows and columns that are populated for the QR code, measured in cells.

        private QRPolarities polarity; //The polarity of the QR code.

        private QRMirrorMode mirror;
            //This element is TRUE if the QR code appears mirrored in the image and FALSE if the QR code appears normally in the image.

        private QRModelType modelType; //This option allows you to specify the type of QR code.
    }

    public struct QRCodeReport
    {
        private uint found;
            //This element is TRUE if the function located and decoded a QR code and FALSE if the function failed to locate and decode a QR code.

        private IntPtr data; //The data encoded in the QR code.
        private uint dataLength; //The length of the data array.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] boundingBox; //An array of four points describing the rectangle surrounding the QR code.
        private IntPtr tokenizedData; //Contains the data tokenized in exactly the way it was encoded in the code.
        private uint sizeOfTokenizedData; //Size of the tokenized data.
        private uint numErrorsCorrected; //The number of errors the function corrected when decoding the QR code.
        private uint dimensions; //The number of rows and columns that are populated for the QR code, measured in cells.
        private uint version; //The version of the QR code.
        private QRModelType modelType; //This option allows you to specify what type of QR code this is.
        private QRStreamMode streamMode; //The format of the data encoded in the stream.
        private QRPolarities matrixPolarity; //The polarity of the QR code.

        private uint mirrored;
            //This element is TRUE if the QR code appears mirrored in the image and FALSE if the QR code appears normally in the image.

        private uint positionInAppendStream;
            //Indicates what position the QR code is in with respect to the stream of data in all codes.

        private uint sizeOfAppendStream; //Specifies how many QR codes are part of a larger array of codes.
        private int firstEAN128ApplicationID; //The first EAN-128 Application ID encountered in the stream.
        private int firstECIDesignator; //The first Regional Language Designator encountered in the stream.

        private uint appendStreamIdentifier;
            //Specifies what stream the QR code is in relation to when the code is part of a larger array of codes.

        private uint minimumEdgeStrength;
            //The strength of the weakest edge the function used to find the coarse location of the QR code in the image.

        private QRDemodulationMode demodulationMode; //The demodulation mode the function used to locate the QR code.
        private QRCellSampleSize cellSampleSize; //The cell sample size the function used to locate the QR code.
        private QRCellFilterMode cellFilterMode; //The cell filter mode the function used to locate the QR code.
    }

    public struct AIMGradeReport
    {
        private AIMGrade overallGrade;
            //The overall letter grade, which is equal to the lowest of the other five letter grades.

        private AIMGrade decodingGrade;
            //The letter grade assigned to a Data Matrix barcode based on the success of the function in decoding the Data Matrix barcode.

        private AIMGrade symbolContrastGrade;
            //The letter grade assigned to a Data Matrix barcode based on the symbol contrast raw score.

        private float symbolContrast;
            //The symbol contrast raw score representing the percentage difference between the mean of the reflectance of the darkest 10 percent and lightest 10 percent of the Data Matrix barcode.

        private AIMGrade printGrowthGrade; //The print growth letter grade for the Data Matrix barcode.

        private float printGrowth;
            //The print growth raw score for the barcode, which is based on the extent to which dark or light markings appropriately fill their module boundaries.

        private AIMGrade axialNonuniformityGrade; //The axial nonuniformity grade for the Data Matrix barcode.

        private float axialNonuniformity;
            //The axial nonuniformity raw score for the barcode, which is based on how much the sampling point spacing differs from one axis to another.

        private AIMGrade unusedErrorCorrectionGrade;
            //The unused error correction letter grade for the Data Matrix barcode.

        private float unusedErrorCorrection;
            //The unused error correction raw score for the Data Matrix barcode, which is based on the extent to which regional or spot damage in the Data Matrix barcode has eroded the reading safety margin provided by the error correction.
    }

    public struct DataMatrixSizeOptions
    {
        private uint minSize; //Specifies the minimum size (in pixels) of the Data Matrix barcode in the image.
        private uint maxSize; //Specifies the maximum size (in pixels) of the Data Matrix barcode in the image.
        private uint quietZoneWidth; //Specifies the expected minimum size of the quiet zone, in pixels.
    }

    public struct DataMatrixDescriptionOptions
    {
        private float aspectRatio;
            //Specifies the ratio of the width of each Data Matrix barcode cell (in pixels) to the height of the Data Matrix barcode (in pixels).

        private uint rows; //Specifies the number of rows in the Data Matrix barcode.
        private uint columns; //Specifies the number of columns in the Data Matrix barcode.
        private int rectangle; //Set this element to TRUE to specify that the Data Matrix barcode is rectangular.
        private DataMatrixECC ecc; //Specifies the ECC used for this Data Matrix barcode.
        private DataMatrixPolarity polarity; //Specifies the data-to-background contrast for the Data Matrix barcode.

        private DataMatrixCellFillMode cellFill;
            //Specifies the fill percentage for a cell of the Data Matrix barcode that is in the "ON" state.

        private float minBorderIntegrity;
            //Specifies the minimum percentage of the border (locator pattern and timing pattern) the function should expect in the Data Matrix barcode.

        private DataMatrixMirrorMode mirrorMode;
            //Specifies if the Data Matrix barcode appears normally in the image or if the barcode appears mirrored in the image.
    }

    public struct DataMatrixSearchOptions
    {
        private DataMatrixRotationMode rotationMode;
            //Specifies the amount of Data Matrix barcode rotation the function should allow for.

        private int skipLocation;
            //If set to TRUE, specifies that the function should assume that the Data Matrix barcode occupies the entire image (or the entire search region).

        private uint edgeThreshold;
            //Specifies the minimum contrast a pixel must have in order to be considered part of a matrix cell edge.

        private DataMatrixDemodulationMode demodulationMode;
            //Specifies the mode the function should use to demodulate (determine which cells are on and which cells are off) the Data Matrix barcode.

        private DataMatrixCellSampleSize cellSampleSize;
            //Specifies the sample size, in pixels, the function should take to determine if each cell is on or off.

        private DataMatrixCellFilterMode cellFilterMode;
            //Specifies the mode the function uses to determine the pixel value for each cell.

        private uint skewDegreesAllowed;
            //Specifies the amount of skew in the Data Matrix barcode the function should allow for.

        private uint maxIterations;
            //Specifies the maximum number of iterations before the function stops looking for the Data Matrix barcode.

        private uint initialSearchVectorWidth;
            //Specifies the number of pixels the function should average together to determine the location of an edge.
    }

    public struct DataMatrixReport
    {
        private int found;
            //This element is TRUE if the function located and decoded a Data Matrix barcode and FALSE if the function failed to locate and decode a Data Matrix barcode.

        private int binary;
            //This element is TRUE if the Data Matrix barcode contains binary data and FALSE if the Data Matrix barcode contains text data.

        private IntPtr data; //The data encoded in the Data Matrix barcode.
        private uint dataLength; //The length of the data array.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] boundingBox;
            //An array of four points describing the rectangle surrounding the Data Matrix barcode.

        private uint numErrorsCorrected;
            //The number of errors the function corrected when decoding the Data Matrix barcode.

        private uint numErasuresCorrected;
            //The number of erasures the function corrected when decoding the Data Matrix barcode.

        private float aspectRatio;
            //Specifies the aspect ratio of the Data Matrix barcode in the image, which equals the ratio of the width of a Data Matrix barcode cell (in pixels) to the height of a Data Matrix barcode cell (in pixels).

        private uint rows; //The number of rows in the Data Matrix barcode.
        private uint columns; //The number of columns in the Data Matrix barcode.
        private DataMatrixECC ecc; //The Error Correction Code (ECC) used by the Data Matrix barcode.
        private DataMatrixPolarity polarity; //The polarity of the Data Matrix barcode.
        private DataMatrixCellFillMode cellFill; //The cell fill percentage of the Data Matrix barcode.

        private float borderIntegrity;
            //The percentage of the Data Matrix barcode border that appears correctly in the image.

        private int mirrored;
            //This element is TRUE if the Data Matrix barcode appears mirrored in the image and FALSE if the Data Matrix barcode appears normally in the image.

        private uint minimumEdgeStrength;
            //The strength of the weakest edge the function used to find the coarse location of the Data Matrix barcode in the image.

        private DataMatrixDemodulationMode demodulationMode;
            //The demodulation mode the function used to locate the Data Matrix barcode.

        private DataMatrixCellSampleSize cellSampleSize;
            //The cell sample size the function used to locate the Data Matrix barcode.

        private DataMatrixCellFilterMode cellFilterMode;
            //The cell filter mode the function used to locate the Data Matrix barcode.

        private uint iterations;
            //The number of iterations the function took in attempting to locate the Data Matrix barcode.
    }

    public struct JPEG2000FileAdvancedOptions
    {
        private WaveletTransformMode waveletMode; //Determines which wavelet transform to use when writing the file.

        private int useMultiComponentTransform;
            //Set this parameter to TRUE to use an additional transform on RGB images.

        private uint maxWaveletTransformLevel; //Specifies the maximum allowed level of wavelet transform.

        private float quantizationStepSize;
            //Specifies the absolute base quantization step size for derived quantization mode.
    }

    public struct MatchGeometricPatternAdvancedOptions2
    {
        private int minFeaturesUsed; //Specifies the minimum number of features the function uses when matching.
        private int maxFeaturesUsed; //Specifies the maximum number of features the function uses when matching.

        private int subpixelIterations;
            //Specifies the maximum number of incremental improvements used to refine matches with subpixel information.

        private double subpixelTolerance;
            //Specifies the maximum amount of change, in pixels, between consecutive incremental improvements in the match position before the function stops refining the match position.

        private int initialMatchListLength; //Specifies the maximum size of the match list.

        private float matchTemplateCurveScore;
            //Set this element to TRUE to specify that the function should calculate the match curve to template curve score and return it for each match result.

        private int correlationScore;
            //Set this element to TRUE to specify that the function should calculate the correlation score and return it for each match result.

        private double minMatchSeparationDistance;
            //Specifies the minimum separation distance, in pixels, between the origins of two matches that have unique positions.

        private double minMatchSeparationAngle;
            //Specifies the minimum angular difference, in degrees, between two matches that have unique angles.

        private double minMatchSeparationScale;
            //Specifies the minimum difference in scale, expressed as a percentage, between two matches that have unique scales.

        private double maxMatchOverlap;
            //Specifies the maximum amount of overlap, expressed as a percentage, allowed between the bounding rectangles of two unique matches.

        private int coarseResult;
            //Specifies whether you want the function to spend less time accurately estimating the location of a match.

        private int smoothContours;
            //Set this element to TRUE to specify smoothing be done on the contours of the inspection image before feature extraction.

        private int enableCalibrationSupport;
            //Set this element to TRUE to specify the algorithm treat the inspection image as a calibrated image.
    }

    public struct InspectionAlignment
    {
        private PointFloat position; //The location of the center of the golden template in the image under inspection.
        private float rotation; //The rotation of the golden template in the image under inspection, in degrees.

        private float scale;
            //The percentage of the size of the area under inspection compared to the size of the golden template.
    }

    public struct InspectionOptions
    {
        private RegistrationMethod registrationMethod;
            //Specifies how the function registers the golden template and the target image.

        private NormalizationMethod normalizationMethod;
            //Specifies how the function normalizes the golden template to the target image.

        private int edgeThicknessToIgnore; //Specifies desired thickness of edges to be ignored.

        private float brightThreshold;
            //Specifies the threshold for areas where the target image is brighter than the golden template.

        private float darkThreshold;
            //Specifies the threshold for areas where the target image is darker than the golden template.

        private int binary;
            //Specifies whether the function should return a binary image giving the location of defects, or a grayscale image giving the intensity of defects.
    }

    public struct CharReport2
    {
        private IntPtr character; //The character value.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner;
            //An array of four points that describes the rectangle that surrounds the character.

        private int lowThreshold; //The minimum value of the threshold range used for this character.
        private int highThreshold; //The maximum value of the threshold range used for this character.

        private int classificationScore;
            //The degree to which the assigned character class represents the object better than the other character classes in the character set.

        private int verificationScore;
            //The similarity of the character and the reference character for the character class.

        private int verified;
            //This element is TRUE if a reference character was found for the character class and FALSE if a reference character was not found.
    }

    public struct CharInfo2
    {
        private IntPtr charValue; //Retrieves the character value of the corresponding character in the character set.
        private IntPtr charImage; //The image you used to train this character.

        private IntPtr publicImage;
            //The public representation that NI Vision uses to match objects to this character.

        private int isReferenceChar;
            //This element is TRUE if the character is the reference character for the character class.
    }

    public struct ReadTextReport2
    {
        private IntPtr readString; //The read string.
        private IntPtr characterReport; //An array of reports describing the properties of each identified character.
        private int numCharacterReports; //The number of identified characters.
    }

    public struct EllipseFeature
    {
        private PointFloat position; //The location of the center of the ellipse.
        private double rotation; //The orientation of the semi-major axis of the ellipse with respect to the horizontal.
        private double minorRadius; //The length of the semi-minor axis of the ellipse.
        private double majorRadius; //The length of the semi-major axis of the ellipse.
    }

    public struct CircleFeature
    {
        private PointFloat position; //The location of the center of the circle.
        private double radius; //The radius of the circle.
    }

    public struct ConstCurveFeature
    {
        private PointFloat position; //The center of the circle that this constant curve lies upon.
        private double radius; //The radius of the circle that this constant curve lies upon.

        private double startAngle;
            //When traveling along the constant curve from one endpoint to the next in a counterclockwise manner, this is the angular component of the vector originating at the center of the constant curve and pointing towards the first endpoint of the constant curve.

        private double endAngle;
            //When traveling along the constant curve from one endpoint to the next in a counterclockwise manner, this is the angular component of the vector originating at the center of the constant curve and pointing towards the second endpoint of the constant curve.
    }

    public struct RectangleFeature
    {
        private PointFloat position; //The center of the rectangle.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner ; //The four corners of the rectangle.
        private double rotation; //The orientation of the rectangle with respect to the horizontal.
        private double width; //The width of the rectangle.
        private double height; //The height of the rectangle.
    }

    public struct LegFeature
    {
        private PointFloat position; //The location of the leg feature.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner ; //The four corners of the leg feature.
        private double rotation; //The orientation of the leg with respect to the horizontal.
        private double width; //The width of the leg.
        private double height; //The height of the leg.
    }

    public struct CornerFeature
    {
        private PointFloat position; //The location of the corner feature.
        private double rotation; //The angular component of the vector bisecting the corner from position.
        private double enclosedAngle; //The measure of the enclosed angle of the corner.
        private int isVirtual;
    }

    public struct LineFeature
    {
        private PointFloat startPoint; //The starting point of the line.
        private PointFloat endPoint; //The ending point of the line.
        private double length; //The length of the line measured in pixels from the start point to the end point.
        private double rotation; //The orientation of the line with respect to the horizontal.
    }

    public struct ParallelLinePairFeature
    {
        private PointFloat firstStartPoint; //The starting point of the first line of the pair.
        private PointFloat firstEndPoint; //The ending point of the first line of the pair.
        private PointFloat secondStartPoint; //The starting point of the second line of the pair.
        private PointFloat secondEndPoint; //The ending point of the second line of the pair.
        private double rotation; //The orientation of the feature with respect to the horizontal.
        private double distance; //The distance from the first line to the second line.
    }

    public struct PairOfParallelLinePairsFeature
    {
        private ParallelLinePairFeature firstParallelLinePair; //The first parallel line pair.
        private ParallelLinePairFeature secondParallelLinePair; //The second parallel line pair.
        private double rotation; //The orientation of the feature with respect to the horizontal.

        private double distance;
            //The distance from the midline of the first parallel line pair to the midline of the second parallel line pair.
    }

    public struct GeometricFeature
    {
        private IntPtr pointer;
    }

    /*
union GeometricFeature_union {
    CircleFeature* circle;                  //A pointer to a CircleFeature.
    EllipseFeature* ellipse;                 //A pointer to an EllipseFeature.
    ConstCurveFeature* constCurve;              //A pointer to a ConstCurveFeature.
    RectangleFeature* rectangle;               //A pointer to a RectangleFeature.
    LegFeature* leg;                     //A pointer to a LegFeature.
    CornerFeature* corner;                  //A pointer to a CornerFeature.
    ParallelLinePairFeature* parallelLinePair;        //A pointer to a ParallelLinePairFeature.
    PairOfParallelLinePairsFeature* pairOfParallelLinePairs; //A pointer to a PairOfParallelLinePairsFeature.
    LineFeature* line;                    //A pointer to a LineFeature.
    ClosedCurveFeature* closedCurve;             //A pointer to a ClosedCurveFeature.
}
*/

    public struct FeatureData
    {
        private FeatureType type; //An enumeration representing the type of the feature.
        private IntPtr contourPoints; //A set of points describing the contour of the feature.
        private int numContourPoints; //The number of points in the contourPoints array.
        private GeometricFeature feature; //The feature data specific to this type of feature.
    }

    public struct GeometricPatternMatch2
    {
        private PointFloat position; //The location of the origin of the template in the match.
        private float rotation; //The rotation of the match relative to the template image, in degrees.

        private float scale;
            //The size of the match relative to the size of the template image, expressed as a percentage.

        private float score; //The accuracy of the match.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner;
            //An array of four points describing the rectangle surrounding the template image.

        private int inverse; //This element is TRUE if the match is an inverse of the template image.
        private float occlusion; //The percentage of the match that is occluded.

        private float templateMatchCurveScore;
            //The accuracy of the match obtained by comparing the template curves to the curves in the match region.

        private float matchTemplateCurveScore;
            //The accuracy of the match obtained by comparing the curves in the match region to the template curves.

        private float correlationScore;
            //The accuracy of the match obtained by comparing the template image to the match region using a correlation metric that compares the two regions as a function of their pixel values.

        private String255 label;
            //The label corresponding to this match when the match is returned by imaqMatchMultipleGeometricPatterns().

        private IntPtr featureData; //The features used in this match.
        private int numFeatureData; //The size of the featureData array.
        private PointFloat calibratedPosition; //The location of the origin of the template in the match.
        private float calibratedRotation; //The rotation of the match relative to the template image, in degrees.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] calibratedCorner ;
            //An array of four points describing the rectangle surrounding the template image.
    }

    public struct ClosedCurveFeature
    {
        private PointFloat position; //The center of the closed curve feature.
        private double arcLength; //The arc length of the closed curve feature.
    }

    public struct LineMatch
    {
        private PointFloat startPoint; //The starting point of the matched line.
        private PointFloat endPoint; //The ending point of the matched line.
        private double length; //The length of the line measured in pixels from the start point to the end point.
        private double rotation; //The orientation of the matched line.
        private double score; //The score of the matched line.
    }

    public struct LineDescriptor
    {
        private double minLength; //Specifies the minimum length of a line the function will return.
        private double maxLength; //Specifies the maximum length of a line the function will return.
    }

    public struct RectangleDescriptor
    {
        private double minWidth; //Specifies the minimum width of a rectangle the algorithm will return.
        private double maxWidth; //Specifies the maximum width of a rectangle the algorithm will return.
        private double minHeight; //Specifies the minimum height of a rectangle the algorithm will return.
        private double maxHeight; //Specifies the maximum height of a rectangle the algorithm will return.
    }

    public struct RectangleMatch
    {
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner; //The corners of the matched rectangle.
        private double rotation; //The orientation of the matched rectangle.
        private double width; //The width of the matched rectangle.
        private double height; //The height of the matched rectangle.
        private double score; //The score of the matched rectangle.
    }

    public struct EllipseDescriptor
    {
        private double minMajorRadius;
            //Specifies the minimum length of the semi-major axis of an ellipse the function will return.

        private double maxMajorRadius;
            //Specifies the maximum length of the semi-major axis of an ellipse the function will return.

        private double minMinorRadius;
            //Specifies the minimum length of the semi-minor axis of an ellipse the function will return.

        private double maxMinorRadius;
            //Specifies the maximum length of the semi-minor axis of an ellipse the function will return.
    }

    public struct EllipseMatch
    {
        private PointFloat position; //The location of the center of the matched ellipse.
        private double rotation; //The orientation of the matched ellipse.
        private double majorRadius; //The length of the semi-major axis of the matched ellipse.
        private double minorRadius; //The length of the semi-minor axis of the matched ellipse.
        private double score; //The score of the matched ellipse.
    }

    public struct CircleMatch
    {
        private PointFloat position; //The location of the center of the matched circle.
        private double radius; //The radius of the matched circle.
        private double score; //The score of the matched circle.
    }

    public struct CircleDescriptor
    {
        private double minRadius; //Specifies the minimum radius of a circle the function will return.
        private double maxRadius; //Specifies the maximum radius of a circle the function will return.
    }

    public struct ShapeDetectionOptions
    {
        private uint mode; //Specifies the method used when looking for the shape in the image.

        private IntPtr angleRanges;
            //An array of angle ranges, in degrees, where each range specifies how much you expect the shape to be rotated in the image.

        private int numAngleRanges; //The size of the orientationRanges array.

        private RangeFloat scaleRange;
            //A range that specifies the sizes of the shapes you expect to be in the image, expressed as a ratio percentage representing the size of the pattern in the image divided by size of the original pattern multiplied by 100.

        private double minMatchScore;
    }

    public struct Curve
    {
        private IntPtr points; //The points on the curve.
        private uint numPoints; //The number of points in the curve.
        private int closed; //This element is TRUE if the curve is closed and FALSE if the curve is open.
        private double curveLength; //The length of the curve.
        private double minEdgeStrength; //The lowest edge strength detected on the curve.
        private double maxEdgeStrength; //The highest edge strength detected on the curve.
        private double averageEdgeStrength; //The average of all edge strengths detected on the curve.
    }

    public struct CurveOptions
    {
        private ExtractionMode extractionMode; //Specifies the method the function uses to identify curves in the image.
        private int threshold; //Specifies the minimum contrast a seed point must have in order to begin a curve.

        private EdgeFilterSize filterSize;
            //Specifies the width of the edge filter the function uses to identify curves in the image.

        private int minLength; //Specifies the length, in pixels, of the smallest curve the function will extract.

        private int rowStepSize;
            //Specifies the distance, in the y direction, between lines the function inspects for curve seed points.

        private int columnStepSize;
            //Specifies the distance, in the x direction, between columns the function inspects for curve seed points.

        private int maxEndPointGap;
            //Specifies the maximum gap, in pixels, between the endpoints of a curve that the function identifies as a closed curve.

        private int onlyClosed;
            //Set this element to TRUE to specify that the function should only identify closed curves in the image.

        private int subpixelAccuracy;
            //Set this element to TRUE to specify that the function identifies the location of curves with subpixel accuracy by interpolating between points to find the crossing of threshold.
    }

    public struct Barcode2DInfo
    {
        private Barcode2DType type; //The type of the 2D barcode.

        private int binary;
            //This element is TRUE if the 2D barcode contains binary data and FALSE if the 2D barcode contains text data.

        private IntPtr data; //The data encoded in the 2D barcode.
        private uint dataLength; //The length of the data array.

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] boundingBox;
            //An array of four points describing the rectangle surrounding the 2D barcode.

        private uint numErrorsCorrected; //The number of errors the function corrected when decoding the 2D barcode.
        private uint numErasuresCorrected; //The number of erasures the function corrected when decoding the 2D barcode.
        private uint rows; //The number of rows in the 2D barcode.
        private uint columns; //The number of columns in the 2D barcode.
    }

    public struct DataMatrixOptions
    {
        private Barcode2DSearchMode searchMode; //Specifies the mode the function uses to search for barcodes.
        private Barcode2DContrast contrast; //Specifies the contrast of the barcodes that the function searches for.

        private Barcode2DCellShape cellShape;
            //Specifies the shape of the barcode data cells, which affects how the function decodes the barcode.

        private Barcode2DShape barcodeShape; //Specifies the shape of the barcodes that the function searches for.

        private DataMatrixSubtype subtype;
            //Specifies the Data Matrix subtypes of the barcodes that the function searches for.
    }

    public struct ClassifierAccuracyReport
    {
        private int size; //The size of the arrays in this structure.
        private float accuracy; //The overall accuracy of the classifier, from 0 to 1000.
        private IntPtr classNames; //The names of the classes of this classifier.
        private IntPtr classAccuracy; //An array of size elements that contains accuracy information for each class.

        private IntPtr classPredictiveValue;
            //An array containing size elements that contains the predictive values of each class.

        private IntPtr classificationDistribution;
            //A two-dimensional array containing information about how the classifier classifies its samples.
    }

    public struct NearestNeighborClassResult
    {
        private IntPtr className; //The name of the class.
        private float standardDeviation; //The standard deviation of the members of this class.
        private int count; //The number of samples in this class.
    }

    public struct NearestNeighborTrainingReport
    {
        private IntPtr classDistancesTable; //The confidence in the training.
        private IntPtr allScores; //All classes and their scores.
        private int allScoresSize; //The number of entries in allScores.
    }

    public struct ParticleClassifierPreprocessingOptions
    {
        private int manualThreshold; //Set this element to TRUE to specify the threshold range manually.
        private RangeFloat manualThresholdRange; //If a manual threshold is being done, the range of pixels to keep.

        private ThresholdMethod autoThresholdMethod;
            //If an automatic threshold is being done, the method used to calculate the threshold range.

        private RangeFloat limits; //The limits on the automatic threshold range.
        private ParticleType particleType; //Specifies what kind of particles to look for.
        private int rejectBorder; //Set this element to TRUE to reject border particles.
        private int numErosions; //The number of erosions to perform.
    }

    public struct ClassifierSampleInfo
    {
        private IntPtr className; //The name of the class this sample is in.

        private IntPtr featureVector;
            //The feature vector of this sample, or NULL if this is not a custom classifier session.

        private int featureVectorSize; //The number of elements in the feature vector.
        private IntPtr thumbnail; //A thumbnail image of this sample, or NULL if no image was specified.
    }

    public struct ClassScore
    {
        private IntPtr className; //The name of the class.
        private float distance; //The distance from the item to this class.
    }

    public struct ClassifierReport
    {
        private IntPtr bestClassName; //The name of the best class for the sample.
        private float classificationScore; //The similarity of the sample and the two closest classes in the classifier.
        private float identificationScore; //The similarity of the sample and the assigned class.
        private IntPtr allScores; //All classes and their scores.
        private int allScoresSize; //The number of entries in allScores.
    }

    public struct NearestNeighborOptions
    {
        private NearestNeighborMethod method; //The method to use.
        private NearestNeighborMetric metric; //The metric to use.
        private int k; //The value of k, if the IMAQ_K_NEAREST_NEIGHBOR method is used.
    }

    public struct ParticleClassifierOptions
    {
        private float scaleDependence; //The relative importance of scale when classifying particles.
        private float mirrorDependence; //The relative importance of mirror symmetry when classifying particles.
    }

    public struct RGBU64Value
    {
        private ushort B; //The blue value of the color.
        private ushort G; //The green value of the color.
        private ushort R; //The red value of the color.

        private ushort alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    public struct GeometricPatternMatch
    {
        private PointFloat position; //The location of the origin of the template in the match.
        private float rotation; //The rotation of the match relative to the template image, in degrees.

        private float scale;
            //The size of the match relative to the size of the template image, expressed as a percentage.

        private float score; //The accuracy of the match.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner;
            //An array of four points describing the rectangle surrounding the template image.

        private int inverse; //This element is TRUE if the match is an inverse of the template image.
        private float occlusion; //The percentage of the match that is occluded.

        private float templateMatchCurveScore;
            //The accuracy of the match obtained by comparing the template curves to the curves in the match region.

        private float matchTemplateCurveScore;
            //The accuracy of the match obtained by comparing the curves in the match region to the template curves.

        private float correlationScore;
            //The accuracy of the match obtained by comparing the template image to the match region using a correlation metric that compares the two regions as a function of their pixel values.
    }

    public struct MatchGeometricPatternAdvancedOptions
    {
        private int minFeaturesUsed; //Specifies the minimum number of features the function uses when matching.
        private int maxFeaturesUsed; //Specifies the maximum number of features the function uses when matching.

        private int subpixelIterations;
            //Specifies the maximum number of incremental improvements used to refine matches with subpixel information.

        private double subpixelTolerance;
            //Specifies the maximum amount of change, in pixels, between consecutive incremental improvements in the match position before the function stops refining the match position.

        private int initialMatchListLength; //Specifies the maximum size of the match list.

        private int matchTemplateCurveScore;
            //Set this element to TRUE to specify that the function should calculate the match curve to template curve score and return it for each match result.

        private int correlationScore;
            //Set this element to TRUE to specify that the function should calculate the correlation score and return it for each match result.

        private double minMatchSeparationDistance;
            //Specifies the minimum separation distance, in pixels, between the origins of two matches that have unique positions.

        private double minMatchSeparationAngle;
            //Specifies the minimum angular difference, in degrees, between two matches that have unique angles.

        private double minMatchSeparationScale;
            //Specifies the minimum difference in scale, expressed as a percentage, between two matches that have unique scales.

        private double maxMatchOverlap;
            //Specifies the maximum amount of overlap, expressed as a percentage, allowed between the bounding rectangles of two unique matches.

        private int coarseResult;
            //Specifies whether you want the function to spend less time accurately estimating the location of a match.
    }

    public struct MatchGeometricPatternOptions
    {
        private uint mode;
            //Specifies the method imaqMatchGeometricPattern() uses when looking for the pattern in the image.

        private int subpixelAccuracy;
            //Set this element to TRUE to specify that the function should calculate match locations with subpixel accuracy.

        private IntPtr angleRanges;
            //An array of angle ranges, in degrees, where each range specifies how much you expect the template to be rotated in the image.

        private int numAngleRanges; //Number of angle ranges in the angleRanges array.

        private RangeFloat scaleRange;
            //A range that specifies the sizes of the pattern you expect to be in the image, expressed as a ratio percentage representing the size of the pattern in the image divided by size of the original pattern multiplied by 100.

        private RangeFloat occlusionRange;
            //A range that specifies the percentage of the pattern you expect to be occluded in the image.

        private int numMatchesRequested; //Number of valid matches expected.
        private float minMatchScore; //The minimum score a match can have for the function to consider the match valid.
    }

    public struct LearnGeometricPatternAdvancedOptions
    {
        private int minRectLength; //Specifies the minimum length for each side of a rectangular feature.
        private double minRectAspectRatio; //Specifies the minimum aspect ratio of a rectangular feature.
        private int minRadius; //Specifies the minimum radius for a circular feature.
        private int minLineLength; //Specifies the minimum length for a linear feature.
        private double minFeatureStrength; //Specifies the minimum strength for a feature.
        private int maxFeaturesUsed; //Specifies the maximum number of features the function uses when learning.

        private int maxPixelDistanceFromLine;
            //Specifies the maximum number of pixels between an edge pixel and a linear feature for the function to consider that edge pixel as part of the linear feature.
    }

    public struct FitEllipseOptions
    {
        private int rejectOutliers;
            //Whether to use every given point or only a subset of the points to fit the ellipse.

        private double minScore; //Specifies the required quality of the fitted ellipse.

        private double pixelRadius;
            //The acceptable distance, in pixels, that a point determined to belong to the ellipse can be from the circumference of the ellipse.

        private int maxIterations;
            //Specifies the number of refinement iterations you allow the function to perform on the initial subset of points.
    }

    public struct FitCircleOptions
    {
        private int rejectOutliers; //Whether to use every given point or only a subset of the points to fit the circle.
        private double minScore; //Specifies the required quality of the fitted circle.

        private double pixelRadius;
            //The acceptable distance, in pixels, that a point determined to belong to the circle can be from the circumference of the circle.

        private int maxIterations;
            //Specifies the number of refinement iterations you allow the function to perform on the initial subset of points.
    }

    public struct ConstructROIOptions2
    {
        private int windowNumber; //The window number of the image window.

        private IntPtr windowTitle;
            //Specifies the message string that the function displays in the title bar of the window.

        private PaletteType type; //The palette type to use.

        private IntPtr palette;
            //If type is IMAQ_PALETTE_USER, this array is the palette of colors to use with the window.

        private int numColors;
            //If type is IMAQ_PALETTE_USER, this element is the number of colors in the palette array.

        private uint maxContours; //The maximum number of contours the user will be able to select.
    }

    public struct HSLValue
    {
        private byte L; //The color luminance.
        private byte S; //The color saturation.
        private byte H; //The color hue.

        private byte alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    public struct HSVValue
    {
        private byte V; //The color value.
        private byte S; //The color saturation.
        private byte H; //The color hue.

        private byte alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    public struct HSIValue
    {
        private byte I; //The color intensity.
        private byte S; //The color saturation.
        private byte H; //The color hue.

        private byte alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    public struct CIELabValue
    {
        private double b; //The yellow/blue information of the color.
        private double a; //The red/green information of the color.
        private double L; //The color lightness.

        private byte alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    public struct CIEXYZValue
    {
        private double Z; //The Z color information.
        private double Y; //The color luminance.
        private double X; //The X color information.

        private byte alpha;
            //The alpha value of the color, which represents extra information about a color image, such as gamma correction.
    }

    /*

union Color2_union {
    RGBValue rgb;      //The information needed to describe a color in the RGB (Red, Green, and Blue) color space.
HSLValue hsl;      //The information needed to describe a color in the HSL (Hue, Saturation, and Luminance) color space.
HSVValue hsv;      //The information needed to describe a color in the HSI (Hue, Saturation, and Value) color space.
HSIValue hsi;      //The information needed to describe a color in the HSI (Hue, Saturation, and Intensity) color space.
CIELabValue cieLab;   //The information needed to describe a color in the CIE L*a*b* (L, a, b) color space.
CIEXYZValue cieXYZ;   //The information needed to describe a color in the CIE XYZ (X, Y, Z) color space.
int rawValue; //The integer value for the data in the color union.
} Color2;

    */

    public struct BestEllipse2
    {
        private PointFloat center; //The coordinate location of the center of the ellipse.
        private PointFloat majorAxisStart; //The coordinate location of the start of the major axis of the ellipse.
        private PointFloat majorAxisEnd; //The coordinate location of the end of the major axis of the ellipse.
        private PointFloat minorAxisStart; //The coordinate location of the start of the minor axis of the ellipse.
        private PointFloat minorAxisEnd; //The coordinate location of the end of the minor axis of the ellipse.
        private double area; //The area of the ellipse.
        private double perimeter; //The length of the perimeter of the ellipse.
        private double error; //Represents the least square error of the fitted ellipse to the entire set of points.

        private int valid;
            //This element is TRUE if the function achieved the minimum score within the number of allowed refinement iterations and FALSE if the function did not achieve the minimum score.

        private IntPtr pointsUsed;
            //An array of the indexes for the points array indicating which points the function used to fit the ellipse.

        private int numPointsUsed; //The number of points the function used to fit the ellipse.
    }

    public struct LearnPatternAdvancedOptions
    {
        private IntPtr shiftOptions;
            //Use this element to control the behavior of imaqLearnPattern2() during the shift-invariant learning phase.

        private IntPtr rotationOptions;
            //Use this element to control the behavior of imaqLearnPattern2()during the rotation-invariant learning phase.
    }

    public struct AVIInfo
    {
        private uint width; //The width of each frame.
        private uint height; //The height of each frame.
        private ImageType imageType; //The type of images this AVI contains.
        private uint numFrames; //The number of frames in the AVI.
        private uint framesPerSecond; //The number of frames per second this AVI should be shown at.
        private IntPtr filterName; //The name of the compression filter used to create this AVI.
        private int hasData; //Specifies whether this AVI has data attached to each frame or not.
        private uint maxDataSize; //If this AVI has data, the maximum size of the data in each frame.
    }

    public struct LearnPatternAdvancedShiftOptions
    {
        private int initialStepSize;
            //The largest number of image pixels to shift the sample across the inspection image during the initial phase of shift-invariant matching.

        private int initialSampleSize;
            //Specifies the number of template pixels that you want to include in a sample for the initial phase of shift-invariant matching.

        private double initialSampleSizeFactor;
            //Specifies the size of the sample for the initial phase of shift-invariant matching as a percent of the template size, in pixels.

        private int finalSampleSize;
            //Specifies the number of template pixels you want to add to initialSampleSize for the final phase of shift-invariant matching.

        private double finalSampleSizeFactor;
            //Specifies the size of the sample for the final phase of shift-invariant matching as a percent of the edge points in the template, in pixels.

        private int subpixelSampleSize;
            //Specifies the number of template pixels that you want to include in a sample for the subpixel phase of shift-invariant matching.

        private double subpixelSampleSizeFactor;
            //Specifies the size of the sample for the subpixel phase of shift-invariant matching as a percent of the template size, in pixels.
    }

    public struct LearnPatternAdvancedRotationOptions
    {
        private SearchStrategy searchStrategySupport;
            //Specifies the aggressiveness of the rotation search strategy available during the matching phase.

        private int initialStepSize;
            //The largest number of image pixels to shift the sample across the inspection image during the initial phase of matching.

        private int initialSampleSize;
            //Specifies the number of template pixels that you want to include in a sample for the initial phase of rotation-invariant matching.

        private double initialSampleSizeFactor;
            //Specifies the size of the sample for the initial phase of rotation-invariant matching as a percent of the template size, in pixels.

        private int initialAngularAccuracy;
            //Sets the angle accuracy, in degrees, to use during the initial phase of rotation-invariant matching.

        private int finalSampleSize;
            //Specifies the number of template pixels you want to add to initialSampleSize for the final phase of rotation-invariant matching.

        private double finalSampleSizeFactor;
            //Specifies the size of the sample for the final phase of rotation-invariant matching as a percent of the edge points in the template, in pixels.

        private int finalAngularAccuracy;
            //Sets the angle accuracy, in degrees, to use during the final phase of the rotation-invariant matching.

        private int subpixelSampleSize;
            //Specifies the number of template pixels that you want to include in a sample for the subpixel phase of rotation-invariant matching.

        private double subpixelSampleSizeFactor;
            //Specifies the size of the sample for the subpixel phase of rotation-invariant matching as a percent of the template size, in pixels.
    }

    public struct MatchPatternAdvancedOptions
    {
        private int subpixelIterations;
            //Defines the maximum number of incremental improvements used to refine matching using subpixel information.

        private double subpixelTolerance;
            //Defines the maximum amount of change, in pixels, between consecutive incremental improvements in the match position that you want to trigger the end of the refinement process.

        private int initialMatchListLength; //Specifies the maximum size of the match list.
        private int matchListReductionFactor; //Specifies the reduction of the match list as matches are refined.

        private int initialStepSize;
            //Specifies the number of pixels to shift the sample across the inspection image during the initial phase of shift-invariant matching.

        private SearchStrategy searchStrategy; //Specifies the aggressiveness of the rotation search strategy.

        private int intermediateAngularAccuracy;
            //Specifies the accuracy to use during the intermediate phase of rotation-invariant matching.
    }

    public struct ParticleFilterCriteria2
    {
        private MeasurementType parameter; //The morphological measurement that the function uses for filtering.
        private float lower; //The lower bound of the criteria range.
        private float upper; //The upper bound of the criteria range.
        private int calibrated; //Set this element to TRUE to take calibrated measurements.

        private int exclude;
            //Set this element to TRUE to indicate that a match occurs when the measurement is outside the criteria range.
    }

    public struct BestCircle2
    {
        private PointFloat center; //The coordinate location of the center of the circle.
        private double radius; //The radius of the circle.
        private double area; //The area of the circle.
        private double perimeter; //The length of the perimeter of the circle.
        private double error; //Represents the least square error of the fitted circle to the entire set of points.

        private int valid;
            //This element is TRUE if the function achieved the minimum score within the number of allowed refinement iterations and FALSE if the function did not achieve the minimum score.

        private IntPtr pointsUsed;
            //An array of the indexes for the points array indicating which points the function used to fit the circle.

        private int numPointsUsed; //The number of points the function used to fit the circle.
    }

    public struct OCRSpacingOptions
    {
        private int minCharSpacing;
            //The minimum number of pixels that must be between two characters for NI Vision to train or read the characters separately.

        private int minCharSize;
            //The minimum number of pixels required for an object to be a potentially identifiable character.

        private int maxCharSize;
            //The maximum number of pixels required for an object to be a potentially identifiable character.

        private int maxHorizontalElementSpacing;
            //The maximum horizontal spacing, in pixels, allowed between character elements to train or read the character elements as a single character.

        private int maxVerticalElementSpacing; //The maximum vertical element spacing in pixels.
        private int minBoundingRectWidth; //The minimum possible width, in pixels, for a character bounding rectangle.
        private int maxBoundingRectWidth; //The maximum possible width, in pixels, for a character bounding rectangle.
        private int minBoundingRectHeight; //The minimum possible height, in pixels, for a character bounding rectangle.
        private int maxBoundingRectHeight; //The maximum possible height, in pixels, for a character bounding rectangle.

        private int autoSplit;
            //Set this element to TRUE to automatically adjust the location of the character bounding rectangle when characters overlap vertically.
    }

    public struct OCRProcessingOptions
    {
        private ThresholdMode mode; //The thresholding mode.
        private int lowThreshold; //The low threshold value when you set mode to IMAQ_FIXED_RANGE.
        private int highThreshold; //The high threshold value when you set mode to IMAQ_FIXED_RANGE.
        private int blockCount; //The number of blocks for threshold calculation algorithms that require blocks.

        private int fastThreshold;
            //Set this element to TRUE to use a faster, less accurate threshold calculation algorithm.

        private int biModalCalculation;
            //Set this element to TRUE to calculate both the low and high threshold values when using the fast thresholding method.

        private int darkCharacters; //Set this element to TRUE to read or train dark characters on a light background.
        private int removeParticlesTouchingROI; //Set this element to TRUE to remove the particles touching the ROI.
        private int erosionCount; //The number of erosions to perform.
    }

    public struct ReadTextOptions
    {
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 255)]
        private String255[] validChars; //An array of strings that specifies the valid characters.
        private int numValidChars; //The number of strings in the validChars array that you have initialized.

        private char substitutionChar;
            //The character to substitute for objects that the function cannot match with any of the trained characters.

        private ReadStrategy readStrategy;
            //The read strategy, which determines how closely the function analyzes images in the reading process to match objects with trained characters.

        private int acceptanceLevel;
            //The minimum acceptance level at which an object is considered a trained character.

        private int aspectRatio; //The maximum aspect ratio variance percentage for valid characters.

        private ReadResolution readResolution;
            //The read resolution, which determines how much of the trained character data the function uses to match objects to trained characters.
    }

    public struct CharInfo
    {
        private IntPtr charValue; //Retrieves the character value of the corresponding character in the character set.
        private IntPtr charImage; //The image you used to train this character.

        private IntPtr publicImage;
            //The public representation that NI Vision uses to match objects to this character.
    }


    public struct Rect
    {
        private int top; //Location of the top edge of the rectangle.
        private int left; //Location of the left edge of the rectangle.
        private int height; //Height of the rectangle.
        private int width; //Width of the rectangle.
    }

    public struct CharReport
    {
        private IntPtr character; //The character value.

        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner;
            //An array of four points that describes the rectangle that surrounds the character.

        private int reserved; //This element is reserved.
        private int lowThreshold; //The minimum value of the threshold range used for this character.
        private int highThreshold; //The maximum value of the threshold range used for this character.
    }


    public struct ReadTextReport
    {
        private IntPtr readString; //The read string.
        private IntPtr characterReport; //An array of reports describing the properties of each identified character.
        private int numCharacterReports; //The number of identified characters.
    }

    public struct Point
    {
        private int x; //The x-coordinate of the point.
        private int y; //The y-coordinate of the point.
    }

    public struct Annulus
    {
        private Point center; //The coordinate location of the center of the annulus.
        private int innerRadius; //The public radius of the annulus.
        private int outerRadius; //The external radius of the annulus.
        private double startAngle; //The start angle, in degrees, of the annulus.
        private double endAngle; //The end angle, in degrees, of the annulus.
    }

    public struct EdgeLocationReport
    {
        private IntPtr edges; //The coordinate location of all edges detected by the search line.
        private int numEdges; //The number of points in the edges array.
    }

    public struct EdgeOptions
    {
        private uint threshold; //Specifies the threshold value for the contrast of the edge.

        private uint width;
            //The number of pixels that the function averages to find the contrast at either side of the edge.

        private uint steepness;
            //The span, in pixels, of the slope of the edge projected along the path specified by the input points.

        private InterpolationMethod subpixelType; //The method for interpolating.
        private uint subpixelDivisions; //The number of samples the function obtains from a pixel.
    }

    public struct EdgeReport
    {
        private float location; //The location of the edge from the first point in the points array.
        private float contrast; //The contrast at the edge.
        private PolarityType polarity; //The polarity of the edge.
        private float reserved; //This element is reserved.
        private PointFloat coordinate; //The coordinates of the edge.
    }

    public struct ExtremeReport
    {
        private double location; //The locations of the extreme.
        private double amplitude; //The amplitude of the extreme.
        private double secondDerivative; //The second derivative of the extreme.
    }

    public struct FitLineOptions
    {
        private float minScore; //Specifies the required quality of the fitted line.

        private float pixelRadius;
            //Specifies the neighborhood pixel relationship for the initial subset of points being used.

        private int numRefinements;
            //Specifies the number of refinement iterations you allow the function to perform on the initial subset of points.
    }

    public struct DisplayMapping
    {
        private MappingMethod method; //Describes the method for converting 16-bit pixels to 8-bit pixels.
        private int minimumValue; //When method is IMAQ_RANGE, minimumValue represents the value that is mapped to 0.
        private int maximumValue; //When method is IMAQ_RANGE, maximumValue represents the value that is mapped to 255.

        private int shiftCount;
            //When method is IMAQ_DOWNSHIFT, shiftCount represents the number of bits the function right-shifts the 16-bit pixel values.
    }

    public struct DetectExtremesOptions
    {
        private double threshold; //Defines which extremes are too small.

        private int width;
            //Specifies the number of consecutive data points the function uses in the quadratic least-squares fit.
    }

    public struct ImageInfo
    {
        private CalibrationUnit imageUnit;
            //If you set calibration information with imaqSetSimpleCalibrationInfo(), imageUnit is the calibration unit.

        private float stepX;
            //If you set calibration information with imaqSetCalibrationInfo(), stepX is the distance in the calibration unit between two pixels in the x direction.

        private float stepY;
            //If you set calibration information with imaqSetCalibrationInfo(), stepY is the distance in the calibration unit between two pixels in the y direction.

        private ImageType imageType; //The type of the image.
        private int xRes; //The number of columns in the image.
        private int yRes; //The number of rows in the image.

        private int xOffset;
            //If you set mask offset information with imaqSetMaskOffset(), xOffset is the offset of the mask origin in the x direction.

        private int yOffset;
            //If you set mask offset information with imaqSetMaskOffset(), yOffset is the offset of the mask origin in the y direction.

        private int border; //The number of border pixels around the image.
        private int pixelsPerLine; //The number of pixels stored for each line of the image.
        private IntPtr reserved0; //This element is reserved.
        private IntPtr reserved1; //This element is reserved.
        private IntPtr imageStart; //A pointer to pixel (0,0).
    }

    public struct LCDOptions
    {
        private int litSegments; //Set this parameter to TRUE if the segments are brighter than the background.
        private float threshold; //Determines whether a segment is ON or OFF.
        private int sign; //Indicates whether the function must read the sign of the indicator.
        private int decimalPoint; //Determines whether to look for a decimal separator after each digit.
    }

    public struct LCDReport
    {
        private IntPtr text; //A string of the characters of the LCD.
        private IntPtr segmentInfo; //An array of LCDSegment structures describing which segments of each digit are on.
        private int numCharacters; //The number of characters that the function reads.
        private int reserved; //This element is reserved.
    }

    public struct LCDSegments
    {
        /*
    ua:1;         //True if the a segment is on.
    ub:1;         //True if the b segment is on.
    uc:1;         //True if the c segment is on.
    ud:1;         //True if the d segment is on.
    ue:1;         //True if the e segment is on.
    uf:1;         //True if the f segment is on.
    ug:1;         //True if the g segment is on.
    ureserved:25; //This element is reserved.
    */
        private uint data;
    }

    public struct LearnCalibrationOptions
    {
        private CalibrationMode mode;
            //Specifies the type of algorithm you want to use to reduce distortion in your image.

        private ScalingMethod method; //Defines the scaling method correction functions use to correct the image.
        private CalibrationROI roi; //Specifies the ROI correction functions use when correcting an image.

        private int learnMap;
            //Set this element to TRUE if you want the function to calculate and store an error map during the learning process.

        private int learnTable;
            //Set this element to TRUE if you want the function to calculate and store the correction table.
    }

    public struct LearnColorPatternOptions
    {
        private LearningMode learnMode; //Specifies the invariance mode the function uses when learning the pattern.

        private ImageFeatureMode featureMode;
            //Specifies the features the function uses when learning the color pattern.

        private int threshold;
            //Specifies the saturation threshold the function uses to distinguish between two colors that have the same hue values.

        private ColorIgnoreMode ignoreMode;
            //Specifies whether the function excludes certain colors from the color features of the template image.

        private IntPtr colorsToIgnore;
            //An array of ColorInformation structures providing a set of colors to exclude from the color features of the template image.

        private int numColorsToIgnore; //The number of ColorInformation structures in the colorsToIgnore array.
    }

    public struct Line
    {
        private Point start; //The coordinate location of the start of the line.
        private Point end; //The coordinate location of the end of the line.
    }

    public struct LinearAverages
    {
        private IntPtr columnAverages; //An array containing the mean pixel value of each column.
        private int columnCount; //The number of elements in the columnAverages array.
        private IntPtr rowAverages; //An array containing the mean pixel value of each row.
        private int rowCount; //The number of elements in the rowAverages array.

        private IntPtr risingDiagAverages;
            //An array containing the mean pixel value of each diagonal running from the lower left to the upper right of the inspected area of the image.

        private int risingDiagCount; //The number of elements in the risingDiagAverages array.

        private IntPtr fallingDiagAverages;
            //An array containing the mean pixel value of each diagonal running from the upper left to the lower right of the inspected area of the image.

        private int fallingDiagCount; //The number of elements in the fallingDiagAverages array.
    }

    public struct LineProfile
    {
        private IntPtr profileData; //An array containing the value of each pixel in the line.
        private Rect boundingBox; //The bounding rectangle of the line.
        private float min; //The smallest pixel value in the line profile.
        private float max; //The largest pixel value in the line profile.
        private float mean; //The mean value of the pixels in the line profile.
        private float stdDev; //The standard deviation of the line profile.
        private int dataCount; //The size of the profileData array.
    }

    public struct MatchColorPatternOptions
    {
        private MatchingMode matchMode; //Specifies the method to use when looking for the color pattern in the image.

        private ImageFeatureMode featureMode;
            //Specifies the features to use when looking for the color pattern in the image.

        private int minContrast; //Specifies the minimum contrast expected in the image.

        private int subpixelAccuracy;
            //Set this parameter to TRUE to return areas in the image that match the pattern area with subpixel accuracy.

        private IntPtr angleRanges;
            //An array of angle ranges, in degrees, where each range specifies how much you expect the pattern to be rotated in the image.

        private int numRanges; //Number of angle ranges in the angleRanges array.

        private double colorWeight;
            //Determines the percent contribution of the color score to the final color pattern matching score.

        private ColorSensitivity sensitivity; //Specifies the sensitivity of the color information in the image.

        private SearchStrategy strategy;
            //Specifies how the color features of the image are used during the search phase.

        private int numMatchesRequested; //Number of valid matches expected.
        private float minMatchScore; //The minimum score a match can have for the function to consider the match valid.
    }

    public struct HistogramReport
    {
        private IntPtr histogram; //An array describing the number of pixels that fell into each class.
        private int histogramCount; //The number of elements in the histogram array.
        private float min; //The smallest pixel value that the function classified.
        private float max; //The largest pixel value that the function classified.
        private float start; //The smallest pixel value that fell into the first class.
        private float width; //The size of each class.
        private float mean; //The mean value of the pixels that the function classified.
        private float stdDev; //The standard deviation of the pixels that the function classified.
        private int numPixels; //The number of pixels that the function classified.
    }

    public struct ArcInfo
    {
        private Rect boundingBox; //The coordinate location of the bounding box of the arc.
        private double startAngle; //The counterclockwise angle from the x-axis in degrees to the start of the arc.
        private double endAngle; //The counterclockwise angle from the x-axis in degrees to the end of the arc.
    }

    public struct AxisReport
    {
        private PointFloat origin;
            //The origin of the coordinate system, which is the intersection of the two axes of the coordinate system.

        private PointFloat mainAxisEnd;
            //The end of the main axis, which is the result of the computation of the intersection of the main axis with the rectangular search area.

        private PointFloat secondaryAxisEnd;
            //The end of the secondary axis, which is the result of the computation of the intersection of the secondary axis with the rectangular search area.
    }

    public struct BarcodeInfo
    {
        private IntPtr outputString; //A string containing the decoded barcode data.
        private int size; //The size of the output string.
        private char outputChar1; //The contents of this character depend on the barcode type.
        private char outputChar2; //The contents of this character depend on the barcode type.

        private double confidenceLevel;
            //A quality measure of the decoded barcode ranging from 0 to 100, with 100 being the best.

        private BarcodeType type; //The type of barcode.
    }

    public struct BCGOptions
    {
        private float brightness; //Adjusts the brightness of the image.
        private float contrast; //Adjusts the contrast of the image.
        private float gamma; //Performs gamma correction.
    }

    public struct BestCircle
    {
        private PointFloat center; //The coordinate location of the center of the circle.
        private double radius; //The radius of the circle.
        private double area; //The area of the circle.
        private double perimeter; //The length of the perimeter of the circle.
        private double error; //Represents the least square error of the fitted circle to the entire set of points.
    }

    public struct BestEllipse
    {
        private PointFloat center; //The coordinate location of the center of the ellipse.
        private PointFloat majorAxisStart; //The coordinate location of the start of the major axis of the ellipse.
        private PointFloat majorAxisEnd; //The coordinate location of the end of the major axis of the ellipse.
        private PointFloat minorAxisStart; //The coordinate location of the start of the minor axis of the ellipse.
        private PointFloat minorAxisEnd; //The coordinate location of the end of the minor axis of the ellipse.
        private double area; //The area of the ellipse.
        private double perimeter; //The length of the perimeter of the ellipse.
    }

    public struct BestLine
    {
        private PointFloat start; //The coordinate location of the start of the line.
        private PointFloat end; //The coordinate location of the end of the line.
        private LineEquation equation; //Defines the three coefficients of the equation of the best fit line.

        private int valid;
            //This element is TRUE if the function achieved the minimum score within the number of allowed refinement iterations and FALSE if the function did not achieve the minimum score.

        private double error; //Represents the least square error of the fitted line to the entire set of points.

        private IntPtr pointsUsed;
            //An array of the indexes for the points array indicating which points the function used to fit the line.

        private int numPointsUsed; //The number of points the function used to fit the line.
    }

    public struct BrowserOptions
    {
        private int width; //The width to make the browser.
        private int height; //The height to make the browser image.
        private int imagesPerLine; //The number of images to place on a single line.
        private RGBValue backgroundColor; //The background color of the browser.
        private int frameSize; //Specifies the number of pixels with which to border each thumbnail.
        private BrowserFrameStyle style; //The style for the frame around each thumbnail.
        private float ratio; //Specifies the width to height ratio of each thumbnail.
        private RGBValue focusColor; //The color to use to display focused cells.
    }

    public struct CoordinateSystem
    {
        private PointFloat origin; //The origin of the coordinate system.

        private float angle;
            //The angle, in degrees, of the x-axis of the coordinate system relative to the image x-axis.

        private AxisOrientation axisOrientation; //The direction of the y-axis of the coordinate reference system.
    }

    public struct CalibrationInfo
    {
        private IntPtr errorMap; //The error map for the calibration.
        private int mapColumns; //The number of columns in the error map.
        private int mapRows; //The number of rows in the error map.
        private IntPtr userRoi; //Specifies the ROI the user provided when learning the calibration.

        private IntPtr calibrationRoi;
            //Specifies the ROI that corresponds to the region of the image where the calibration information is accurate.

        private LearnCalibrationOptions options;
            //Specifies the calibration options the user provided when learning the calibration.

        private GridDescriptor grid; //Specifies the scaling constants for the image.
        private CoordinateSystem system; //Specifies the coordinate system for the real world coordinates.

        private RangeFloat range;
            //The range of the grayscale the function used to represent the circles in the grid image.

        private float quality; //The quality score of the learning process, which is a value between 0-1000.
    }

    public struct CalibrationPoints
    {
        private IntPtr pixelCoordinates; //The array of pixel coordinates.
        private IntPtr realWorldCoordinates; //The array of corresponding real-world coordinates.
        private int numCoordinates; //The number of coordinates in both of the arrays.
    }

    public struct CaliperOptions
    {
        private TwoEdgePolarityType polarity; //Specifies the edge polarity of the edge pairs.
        private float separation; //The distance between edge pairs.
        private float separationDeviation; //Sets the range around the separation value.
    }

    public struct CaliperReport
    {
        private float edge1Contrast; //The contrast of the first edge.
        private PointFloat edge1Coord; //The coordinates of the first edge.
        private float edge2Contrast; //The contrast of the second edge.
        private PointFloat edge2Coord; //The coordinates of the second edge.
        private float separation; //The distance between the two edges.
        private float reserved; //This element is reserved.
    }

    public struct DrawTextOptions
    {
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 32)]
        private byte[] fontName; //The font name to use.
        private int fontSize; //The size of the font.
        private int bold; //Set this parameter to TRUE to bold text.
        private int italic; //Set this parameter to TRUE to italicize text.
        private int underline; //Set this parameter to TRUE to underline text.
        private int strikeout; //Set this parameter to TRUE to strikeout text.
        private TextAlignment textAlignment; //Sets the alignment of text.
        private FontColor fontColor; //Sets the font color.
    }

    public struct CircleReport
    {
        private Point center; //The coordinate point of the center of the circle.
        private int radius; //The radius of the circle, in pixels.
        private int area; //The area of the circle, in pixels.
    }

    public struct ClosedContour
    {
        private IntPtr points; //The points that make up the closed contour.
        private int numPoints; //The number of points in the array.
    }

    public struct ColorHistogramReport
    {
        private HistogramReport plane1; //The histogram report of the first color plane.
        private HistogramReport plane2; //The histogram report of the second plane.
        private HistogramReport plane3; //The histogram report of the third plane.
    }

    public struct ColorInformation
    {
        private int infoCount; //The size of the info array.
        private int saturation; //The saturation level the function uses to learn the color information.

        private IntPtr info;
            //An array of color information that represents the color spectrum analysis of a region of an image in a compact form.
    }

    public struct Complex
    {
        private float r; //The real part of the value.
        private float i; //The imaginary part of the value.
    }


    public struct ConcentricRakeReport
    {
        private IntPtr rakeArcs; //An array containing the location of each concentric arc line used for edge detection.
        private int numArcs; //The number of arc lines in the rakeArcs array.
        private IntPtr firstEdges; //The coordinate location of all edges detected as first edges.
        private int numFirstEdges; //The number of points in the first edges array.
        private IntPtr lastEdges; //The coordinate location of all edges detected as last edges.
        private int numLastEdges; //The number of points in the last edges array.

        private IntPtr allEdges;
            //An array of reports describing the location of the edges located by each concentric rake arc line.

        private IntPtr linesWithEdges;
            //An array of indices into the rakeArcs array indicating the concentric rake arc lines on which the function detected at least one edge.

        private int numLinesWithEdges;
            //The number of concentric rake arc lines along which the function detected edges.
    }

    public struct ConstructROIOptions
    {
        private int windowNumber; //The window number of the image window.

        private IntPtr windowTitle;
            //Specifies the message string that the function displays in the title bar of the window.

        private PaletteType type; //The palette type to use.

        private IntPtr palette;
            //If type is IMAQ_PALETTE_USER, this array is the palette of colors to use with the window.

        private int numColors;
            //If type is IMAQ_PALETTE_USER, this element is the number of colors in the palette array.
    }

    public struct ContourInfo
    {
        private ContourType type; //The contour type.
        private uint numPoints; //The number of points that make up the contour.
        private IntPtr points; //The points describing the contour.
        private RGBValue contourColor; //The contour color.
    }

    public struct ContourUnion
    {
        private IntPtr pointer;
    }

    /*
union ContourUnion_union {
    Point* point;           //Use this member when the contour is of type IMAQ_POINT.
Line* line;            //Use this member when the contour is of type IMAQ_LINE.
Rect* rect;            //Use this member when the contour is of type IMAQ_RECT.
Rect* ovalBoundingBox; //Use this member when the contour is of type IMAQ_OVAL.
ClosedContour* closedContour;   //Use this member when the contour is of type IMAQ_CLOSED_CONTOUR.
OpenContour* openContour;     //Use this member when the contour is of type IMAQ_OPEN_CONTOUR.
Annulus* annulus;         //Use this member when the contour is of type IMAQ_ANNULUS.
RotatedRect* rotatedRect;     //Use this member when the contour is of type IMAQ_ROTATED_RECT.
} ContourUnion;
*/

    public struct ContourInfo2
    {
        private ContourType type; //The contour type.
        private RGBValue color; //The contour color.
        private ContourUnion structure; //The information necessary to describe the contour in coordinate space.
    }

    public struct ContourPoint
    {
        private double x; //The x-coordinate value in the image.
        private double y; //The y-coordinate value in the image.
        private double curvature; //The change in slope at this edge point of the segment.

        private double xDisplacement;
            //The x displacement of the current edge pixel from a cubic spline fit of the current edge segment.

        private double yDisplacement;
            //The y displacement of the current edge pixel from a cubic spline fit of the current edge segment.
    }

    public struct CoordinateTransform
    {
        private Point initialOrigin; //The origin of the initial coordinate system.

        private float initialAngle;
            //The angle, in degrees, of the x-axis of the initial coordinate system relative to the image x-axis.

        private Point finalOrigin; //The origin of the final coordinate system.

        private float finalAngle;
            //The angle, in degrees, of the x-axis of the final coordinate system relative to the image x-axis.
    }

    public struct CoordinateTransform2
    {
        private CoordinateSystem referenceSystem; //Defines the coordinate system for input coordinates.

        private CoordinateSystem measurementSystem;
            //Defines the coordinate system in which the function should perform measurements.
    }

    public struct CannyOptions
    {
        private float sigma;
            //The sigma of the Gaussian smoothing filter that the function applies to the image before edge detection.

        private float upperThreshold;
            //The upper fraction of pixel values in the image from which the function chooses a seed or starting point of an edge segment.

        private float lowerThreshold;
            //The function multiplies this value by upperThreshold to determine the lower threshold for all the pixels in an edge segment.

        private int windowSize; //The window size of the Gaussian filter that the function applies to the image.
    }

    public struct Range
    {
        private int minValue; //The minimum value of the range.
        private int maxValue; //The maximum value of the range.
    }

    public struct UserPointSymbol
    {
        private int cols; //Number of columns in the symbol.
        private int rows; //Number of rows in the symbol.
        private IntPtr pixels; //The pixels of the symbol.
    }

    public struct View3DOptions
    {
        private int sizeReduction;
            //A divisor the function uses when determining the final height and width of the 3D image.

        private int maxHeight; //Defines the maximum height of a pixel from the image source drawn in 3D.
        private Direction3D direction; //Defines the 3D orientation.
        private float alpha; //Determines the angle between the horizontal and the baseline.
        private float beta; //Determines the angle between the horizontal and the second baseline.
        private int border; //Defines the border size.
        private int background; //Defines the background color.
        private Plane3D plane; //Indicates the view a function uses to show complex images.
    }

    public struct MatchPatternOptions
    {
        private MatchingMode mode; //Specifies the method to use when looking for the pattern in the image.
        private int minContrast; //Specifies the minimum contrast expected in the image.

        private int subpixelAccuracy;
            //Set this element to TRUE to return areas in the image that match the pattern area with subpixel accuracy.

        private IntPtr angleRanges;
            //An array of angle ranges, in degrees, where each range specifies how much you expect the pattern to be rotated in the image.

        private int numRanges; //Number of angle ranges in the angleRanges array.
        private int numMatchesRequested; //Number of valid matches expected.
        private int matchFactor; //Controls the number of potential matches that the function examines.
        private float minMatchScore; //The minimum score a match can have for the function to consider the match valid.
    }

    public struct TIFFFileOptions
    {
        private int rowsPerStrip; //Indicates the number of rows that the function writes per strip.
        private PhotometricMode photoInterp; //Designates which photometric interpretation to use.
        private TIFFCompressionType compressionType; //Indicates the type of compression to use on the TIFF file.
    }

    /*

union Color_union {
    RGBValue rgb;      //The information needed to describe a color in the RGB (Red, Green, and Blue) color space.
HSLValue hsl;      //The information needed to describe a color in the HSL (Hue, Saturation, and Luminance) color space.
HSVValue hsv;      //The information needed to describe a color in the HSI (Hue, Saturation, and Value) color space.
HSIValue hsi;      //The information needed to describe a color in the HSI (Hue, Saturation, and Intensity) color space.
int rawValue; //The integer value for the data in the color union.
} Color;

union PixelValue_union {
    float grayscale; //A grayscale pixel value.
RGBValue rgb;       //A RGB pixel value.
HSLValue hsl;       //A HSL pixel value.
Complex complex;   //A complex pixel value.
RGBU64Value rgbu64;    //An u64-bit RGB pixel value.
} PixelValue;
*/

    public struct OpenContour
    {
        private IntPtr points; //The points that make up the open contour.
        private int numPoints; //The number of points in the array.
    }

    public struct OverlayTextOptions
    {
        private IntPtr fontName; //The name of the font to use.
        private int fontSize; //The size of the font.
        private int bold; //Set this element to TRUE to bold the text.
        private int italic; //Set this element to TRUE to italicize the text.
        private int underline; //Set this element to TRUE to underline the text.
        private int strikeout; //Set this element to TRUE to strikeout the text.
        private TextAlignment horizontalTextAlignment; //Sets the alignment of the text.
        private VerticalTextAlignment verticalTextAlignment; //Sets the vertical alignment for the text.
        private RGBValue backgroundColor; //Sets the color for the text background pixels.
        private double angle; //The counterclockwise angle, in degrees, of the text relative to the x-axis.
    }

    public struct ParticleFilterCriteria
    {
        private MeasurementValue parameter; //The morphological measurement that the function uses for filtering.
        private float lower; //The lower bound of the criteria range.
        private float upper; //The upper bound of the criteria range.

        private int exclude;
            //Set this element to TRUE to indicate that a match occurs when the value is outside the criteria range.
    }

    public struct ParticleReport
    {
        private int area; //The number of pixels in the particle.

        private float calibratedArea;
            //The size of the particle, calibrated to the calibration information of the image.

        private float perimeter; //The length of the perimeter, calibrated to the calibration information of the image.
        private int numHoles; //The number of holes in the particle.
        private int areaOfHoles; //The total surface area, in pixels, of all the holes in a particle.

        private float perimeterOfHoles;
            //The length of the perimeter of all the holes in the particle calibrated to the calibration information of the image.

        private Rect boundingBox; //The smallest rectangle that encloses the particle.
        private float sigmaX; //The sum of the particle pixels on the x-axis.
        private float sigmaY; //The sum of the particle pixels on the y-axis.
        private float sigmaXX; //The sum of the particle pixels on the x-axis, squared.
        private float sigmaYY; //The sum of the particle pixels on the y-axis, squared.
        private float sigmaXY; //The sum of the particle pixels on the x-axis and y-axis.
        private int longestLength; //The length of the longest horizontal line segment.
        private Point longestPoint; //The location of the leftmost pixel of the longest segment in the particle.
        private int projectionX; //The length of the particle when projected onto the x-axis.
        private int projectionY; //The length of the particle when projected onto the y-axis.

        private int connect8;
            //This element is TRUE if the function used connectivity-8 to determine if particles are touching.
    }

    public struct PatternMatch
    {
        private PointFloat position; //The location of the center of the match.
        private float rotation; //The rotation of the match relative to the template image, in degrees.

        private float scale;
            //The size of the match relative to the size of the template image, expressed as a percentage.

        private float score; //The accuracy of the match.
        [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
        private PointFloat[] corner;
            //An array of four points describing the rectangle surrounding the template image.
    }

    public struct QuantifyData
    {
        private float mean; //The mean value of the pixel values.
        private float stdDev; //The standard deviation of the pixel values.
        private float min; //The smallest pixel value.
        private float max; //The largest pixel value.
        private float calibratedArea; //The area, calibrated to the calibration information of the image.
        private int pixelArea; //The area, in number of pixels.

        private float relativeSize;
            //The proportion, expressed as a percentage, of the associated region relative to the whole image.
    }

    public struct QuantifyReport
    {
        private QuantifyData global; //Statistical data of the whole image.

        private IntPtr regions;
            //An array of QuantifyData structures containing statistical data of each region of the image.

        private int regionCount; //The number of regions.
    }

    public struct RakeOptions
    {
        private int threshold; //Specifies the threshold value for the contrast of the edge.

        private int width;
            //The number of pixels that the function averages to find the contrast at either side of the edge.

        private int steepness;
            //The span, in pixels, of the slope of the edge projected along the path specified by the input points.

        private int subsamplingRatio; //Specifies the number of pixels that separate two consecutive search lines.
        private InterpolationMethod subpixelType; //The method for interpolating.
        private int subpixelDivisions; //The number of samples the function obtains from a pixel.
    }

    public struct RakeReport
    {
        private IntPtr rakeLines; //The coordinate location of each of the rake lines used by the function.
        private int numRakeLines; //The number of lines in the rakeLines array.
        private IntPtr firstEdges; //The coordinate location of all edges detected as first edges.
        private uint numFirstEdges; //The number of points in the firstEdges array.
        private IntPtr lastEdges; //The coordinate location of all edges detected as last edges.
        private uint numLastEdges; //The number of points in the lastEdges array.
        private IntPtr allEdges; //An array of reports describing the location of the edges located by each rake line.

        private IntPtr linesWithEdges;
            //An array of indices into the rakeLines array indicating the rake lines on which the function detected at least one edge.

        private int numLinesWithEdges; //The number of rake lines along which the function detected edges.
    }

    public struct TransformReport
    {
        private IntPtr points; //An array of transformed coordinates.

        private IntPtr validPoints;
            //An array of values that describe the validity of each of the coordinates according to the region of interest you calibrated using either imaqLearnCalibrationGrid() or imaqLearnCalibrationPoints().

        private int numPoints; //The length of both the points array and the validPoints array.
    }

    public struct ShapeReport
    {
        private Rect coordinates; //The bounding rectangle of the object.
        private Point centroid; //The coordinate location of the centroid of the object.
        private int size; //The size, in pixels, of the object.

        private double score;
            //A value ranging between 1 and 1,000 that specifies how similar the object in the image is to the template.
    }

    public struct MeterArc
    {
        private PointFloat needleBase; //The coordinate location of the base of the meter needle.
        private IntPtr arcCoordPoints; //An array of points describing the coordinate location of the meter arc.
        private int numOfArcCoordPoints; //The number of points in the arcCoordPoints array.
        private int needleColor; //This element is TRUE when the meter has a light-colored needle on a dark background.
    }

    public struct ThresholdData
    {
        private float rangeMin; //The lower boundary of the range to keep.
        private float rangeMax; //The upper boundary of the range to keep.
        private float newValue; //If useNewValue is TRUE, newValue is the replacement value for pixels within the range.

        private int useNewValue;
            //If TRUE, the function sets pixel values within [rangeMin, rangeMax] to the value specified in newValue.
    }

    public struct StructuringElement
    {
        private int matrixCols; //Number of columns in the matrix.
        private int matrixRows; //Number of rows in the matrix.
        private int hexa; //Set this element to TRUE if you specify a hexagonal structuring element in kernel.
        private IntPtr kernel; //The values of the structuring element.
    }

    public struct SpokeReport
    {
        private IntPtr spokeLines; //The coordinate location of each of the spoke lines used by the function.
        private int numSpokeLines; //The number of lines in the spokeLines array.
        private IntPtr firstEdges; //The coordinate location of all edges detected as first edges.
        private int numFirstEdges; //The number of points in the firstEdges array.
        private IntPtr lastEdges; //The coordinate location of all edges detected as last edges.
        private int numLastEdges; //The number of points in the lastEdges array.
        private IntPtr allEdges; //An array of reports describing the location of the edges located by each spoke line.

        private IntPtr linesWithEdges;
            //An array of indices into the spokeLines array indicating the rake lines on which the function detected at least one edge.

        private int numLinesWithEdges; //The number of spoke lines along which the function detects edges.
    }

    public struct SimpleEdgeOptions
    {
        private LevelType type; //Determines how the function evaluates the threshold and hysteresis values.
        private int threshold; //The pixel value at which an edge occurs.
        private int hysteresis; //A value that helps determine edges in noisy images.
        private EdgeProcess process; //Determines which edges the function looks for.

        private int subpixel;
            //Set this element to TRUE to find edges with subpixel accuracy by interpolating between points to find the crossing of the given threshold.
    }

    public struct SelectParticleCriteria
    {
        private MeasurementValue parameter; //The morphological measurement that the function uses for filtering.
        private float lower; //The lower boundary of the criteria range.
        private float upper; //The upper boundary of the criteria range.
    }

    public struct SegmentInfo
    {
        private int numberOfPoints; //The number of points in the segment.
        private int isOpen; //If TRUE, the contour is open.
        private double weight; //The significance of the edge in terms of the gray values that constitute the edge.
        private IntPtr points; //The points of the segment.
    }

    public struct RotationAngleRange
    {
        private float lower; //The lowest amount of rotation, in degrees, a valid pattern can have.
        private float upper; //The highest amount of rotation, in degrees, a valid pattern can have.
    }

    public struct RotatedRect
    {
        private int top; //Location of the top edge of the rectangle before rotation.
        private int left; //Location of the left edge of the rectangle before rotation.
        private int height; //Height of the rectangle.
        private int width; //Width of the rectangle.
        private double angle; //The rotation, in degrees, of the rectangle.
    }

    public struct ROIProfile
    {
        private LineProfile report;
            //Quantifying information about the points along the edge of each contour in the ROI.

        private IntPtr pixels; //An array of the points along the edge of each contour in the ROI.
    }

    public struct ToolWindowOptions
    {
        private int showSelectionTool; //If TRUE, the selection tool becomes visible.
        private int showZoomTool; //If TRUE, the zoom tool becomes visible.
        private int showPointTool; //If TRUE, the point tool becomes visible.
        private int showLineTool; //If TRUE, the line tool becomes visible.
        private int showRectangleTool; //If TRUE, the rectangle tool becomes visible.
        private int showOvalTool; //If TRUE, the oval tool becomes visible.
        private int showPolygonTool; //If TRUE, the polygon tool becomes visible.
        private int showClosedFreehandTool; //If TRUE, the closed freehand tool becomes visible.
        private int showPolyLineTool; //If TRUE, the polyline tool becomes visible.
        private int showFreehandTool; //If TRUE, the freehand tool becomes visible.
        private int showAnnulusTool; //If TRUE, the annulus becomes visible.
        private int showRotatedRectangleTool; //If TRUE, the rotated rectangle tool becomes visible.
        private int showPanTool; //If TRUE, the pan tool becomes visible.
        private int showZoomOutTool; //If TRUE, the zoom out tool becomes visible.
        private int reserved2; //This element is reserved and should be set to FALSE.
        private int reserved3; //This element is reserved and should be set to FALSE.
        private int reserved4; //This element is reserved and should be set to FALSE.
    }

    public struct SpokeOptions
    {
        private int threshold; //Specifies the threshold value for the contrast of the edge.

        private int width;
            //The number of pixels that the function averages to find the contrast at either side of the edge.

        private int steepness;
            //The span, in pixels, of the slope of the edge projected along the path specified by the input points.

        private double subsamplingRatio; //The angle, in degrees, between each radial search line in the spoke.
        private InterpolationMethod subpixelType; //The method for interpolating.
        private int subpixelDivisions; //The number of samples the function obtains from a pixel.
    }

}

