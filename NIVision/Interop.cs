using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NIVision
{
    public class Interop
    {
        private const string libraryPath = "libcl.dll";

        [DllImport(libraryPath, EntryPoint = "imaqAnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAnd(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqCompare", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCompare(IntPtr dest, IntPtr source, IntPtr compareImage, ComparisonFunction compare);

        [DllImport(libraryPath, EntryPoint = "imaqLogicalDifference", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLogicalDifference(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqNand", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqNand(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqNor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqNor(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqOr", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOr(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqXnor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqXnor(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqXor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqXor(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqCountParticles", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCountParticles(IntPtr image, int connectivity8, ref int numParticles);

        [DllImport(libraryPath, EntryPoint = "imaqMeasureParticle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMeasureParticle(IntPtr image, int particleNumber, int calibrated, MeasurementType measurement, ref double value);

        [DllImport(libraryPath, EntryPoint = "imaqMeasureParticles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMeasureParticles(IntPtr image, MeasureParticlesCalibrationMode calibrationMode, ref MeasurementType measurements, int numMeasurements);

        [DllImport(libraryPath, EntryPoint = "imaqParticleFilter4", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqParticleFilter4(IntPtr dest, IntPtr source, ref ParticleFilterCriteria2 criteria, int criteriaCount, ref ParticleFilterOptions2 options, ref ROI roi, ref int numParticles);

        [DllImport(libraryPath, EntryPoint = "imaqConvexHull", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConvexHull(IntPtr dest, IntPtr source, int connectivity8);

        [DllImport(libraryPath, EntryPoint = "imaqDanielssonDistance", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDanielssonDistance(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqFillHoles", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFillHoles(IntPtr dest, IntPtr source, int connectivity8);

        [DllImport(libraryPath, EntryPoint = "imaqFindCircles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFindCircles(IntPtr dest, IntPtr source, float minRadius, float maxRadius, ref int numCircles);

        [DllImport(libraryPath, EntryPoint = "imaqLabel2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLabel2(IntPtr dest, IntPtr source, int connectivity8, ref int particleCount);

        [DllImport(libraryPath, EntryPoint = "imaqMorphology", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMorphology(IntPtr dest, IntPtr source, MorphologyMethod method, ref StructuringElement structuringElement);

        [DllImport(libraryPath, EntryPoint = "imaqRejectBorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRejectBorder(IntPtr dest, IntPtr source, int connectivity8);

        [DllImport(libraryPath, EntryPoint = "imaqSegmentation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSegmentation(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqSeparation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSeparation(IntPtr dest, IntPtr source, int erosions, ref StructuringElement structuringElement);

        [DllImport(libraryPath, EntryPoint = "imaqSimpleDistance", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSimpleDistance(IntPtr dest, IntPtr source, ref StructuringElement structuringElement);

        [DllImport(libraryPath, EntryPoint = "imaqSizeFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSizeFilter(IntPtr dest, IntPtr source, int connectivity8, int erosions, SizeType keepSize, ref StructuringElement structuringElement);

        [DllImport(libraryPath, EntryPoint = "imaqSkeleton", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSkeleton(IntPtr dest, IntPtr source, SkeletonMethod method);

        [DllImport(libraryPath, EntryPoint = "imaqCopyFromRing", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCopyFromRing(ulong sessionID, IntPtr image, int imageToCopy, ref int imageNumber, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqEasyAcquire", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEasyAcquire([MarshalAs(UnmanagedType.LPStr)] string interfaceName);

        [DllImport(libraryPath, EntryPoint = "imaqExtractFromRing", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqExtractFromRing(ulong sessionID, int imageToExtract, ref int imageNumber);

        [DllImport(libraryPath, EntryPoint = "imaqGrab", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGrab(ulong sessionID, IntPtr image, int immediate);

        [DllImport(libraryPath, EntryPoint = "imaqReleaseImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReleaseImage(ulong sessionID);

        [DllImport(libraryPath, EntryPoint = "imaqSetupGrab", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetupGrab(ulong sessionID, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqSetupRing", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetupRing(ulong sessionID, ref IntPtr images, int numImages, int skipCount, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqSetupSequence", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetupSequence(ulong sessionID, ref IntPtr images, int numImages, int skipCount, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqSnap", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSnap(ulong sessionID, IntPtr image, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqStartAcquisition", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqStartAcquisition(ulong sessionID);

        [DllImport(libraryPath, EntryPoint = "imaqStopAcquisition", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqStopAcquisition(ulong sessionID);

        [DllImport(libraryPath, EntryPoint = "imaqAbsoluteDifference", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAbsoluteDifference(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqAdd", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAdd(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqAverage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAverage(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqDivide2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDivide2(IntPtr dest, IntPtr sourceA, IntPtr sourceB, RoundingMode roundingMode);

        [DllImport(libraryPath, EntryPoint = "imaqMax", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMax(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqMin", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMin(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqModulo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqModulo(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqMulDiv", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMulDiv(IntPtr dest, IntPtr sourceA, IntPtr sourceB, float value);

        [DllImport(libraryPath, EntryPoint = "imaqMultiply", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMultiply(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqSubtract", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSubtract(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqCaliperTool", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCaliperTool(IntPtr image, ref Point points, int numPoints, ref EdgeOptions edgeOptions, ref CaliperOptions caliperOptions, ref int numEdgePairs);

        [DllImport(libraryPath, EntryPoint = "imaqConcentricRake2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqConcentricRake2(IntPtr image, ref ROI roi, ConcentricRakeDirection direction, EdgeProcess process, int stepSize, ref EdgeOptions2 edgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqDetectExtremes", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqDetectExtremes(ref double pixels, int numPixels, DetectionMode mode, ref DetectExtremesOptions options, ref int numExtremes);

        [DllImport(libraryPath, EntryPoint = "imaqDetectRotation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDetectRotation(IntPtr referenceImage, IntPtr testImage, PointFloat referenceCenter, PointFloat testCenter, int radius, float precision, ref double angle);

        [DllImport(libraryPath, EntryPoint = "imaqEdgeTool4", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEdgeTool4(IntPtr image, ref ROI roi, EdgeProcess processType, ref EdgeOptions2 edgeOptions, uint reverseDirection);

        [DllImport(libraryPath, EntryPoint = "imaqFindEdge2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFindEdge2(IntPtr image, ref ROI roi, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref FindEdgeOptions2 findEdgeOptions, ref StraightEdgeOptions straightEdgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqFindTransformRect2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFindTransformRect2(IntPtr image, ref ROI roi, FindTransformMode mode, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref FindTransformRectOptions2 findTransformOptions, ref StraightEdgeOptions straightEdgeOptions, ref AxisReport axisReport);

        [DllImport(libraryPath, EntryPoint = "imaqFindTransformRects2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFindTransformRects2(IntPtr image, ref ROI primaryROI, ref ROI secondaryROI, FindTransformMode mode, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref FindTransformRectsOptions2 findTransformOptions, ref StraightEdgeOptions primaryStraightEdgeOptions, ref StraightEdgeOptions secondaryStraightEdgeOptions, ref AxisReport axisReport);

        [DllImport(libraryPath, EntryPoint = "imaqLineGaugeTool2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLineGaugeTool2(IntPtr image, Point start, Point end, LineGaugeMethod method, ref EdgeOptions edgeOptions, ref CoordinateTransform2 transform, ref float distance);

        [DllImport(libraryPath, EntryPoint = "imaqRake2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqRake2(IntPtr image, ref ROI roi, RakeDirection direction, EdgeProcess process, int stepSize, ref EdgeOptions2 edgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqSimpleEdge", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSimpleEdge(IntPtr image, ref Point points, int numPoints, ref SimpleEdgeOptions options, ref int numEdges);

        [DllImport(libraryPath, EntryPoint = "imaqSpoke2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSpoke2(IntPtr image, ref ROI roi, SpokeDirection direction, EdgeProcess process, int stepSize, ref EdgeOptions2 edgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqStraightEdge", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqStraightEdge(IntPtr image, ref ROI roi, SearchDirection searchDirection, ref EdgeOptions2 edgeOptions, ref StraightEdgeOptions straightEdgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqStraightEdge2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqStraightEdge2(IntPtr image, ref ROI roi, SearchDirection searchDirection, ref EdgeOptions2 edgeOptions, ref StraightEdgeOptions straightEdgeOptions, uint optimizedMode);

        [DllImport(libraryPath, EntryPoint = "imaqCannyEdgeFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCannyEdgeFilter(IntPtr dest, IntPtr source, ref CannyOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqConvolve2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConvolve2(IntPtr dest, IntPtr source, ref float kernel, int matrixRows, int matrixCols, float normalize, IntPtr mask, RoundingMode roundingMode);

        [DllImport(libraryPath, EntryPoint = "imaqCorrelate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCorrelate(IntPtr dest, IntPtr source, IntPtr templateImage, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqEdgeFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqEdgeFilter(IntPtr dest, IntPtr source, OutlineMethod method, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqLowPass", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLowPass(IntPtr dest, IntPtr source, int width, int height, float tolerance, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqMedianFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMedianFilter(IntPtr dest, IntPtr source, int width, int height, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqNthOrderFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqNthOrderFilter(IntPtr dest, IntPtr source, int width, int height, int n, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqDrawLineOnImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDrawLineOnImage(IntPtr dest, IntPtr source, DrawMode mode, Point start, Point end, float newPixelValue);

        [DllImport(libraryPath, EntryPoint = "imaqDrawShapeOnImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDrawShapeOnImage(IntPtr dest, IntPtr source, Rect rect, DrawMode mode, ShapeMode shape, float newPixelValue);

        [DllImport(libraryPath, EntryPoint = "imaqDrawTextOnImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDrawTextOnImage(IntPtr dest, IntPtr source, Point coord, [MarshalAs(UnmanagedType.LPStr)] string text, ref DrawTextOptions options, ref int fontNameUsed);

        [DllImport(libraryPath, EntryPoint = "imaqInterlaceCombine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqInterlaceCombine(IntPtr frame, IntPtr odd, IntPtr even);

        [DllImport(libraryPath, EntryPoint = "imaqInterlaceSeparate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqInterlaceSeparate(IntPtr frame, IntPtr odd, IntPtr even);

        [DllImport(libraryPath, EntryPoint = "imaqEnumerateCustomKeys", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEnumerateCustomKeys(IntPtr image, ref uint size);

        [DllImport(libraryPath, EntryPoint = "imaqGetBitDepth", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetBitDepth(IntPtr image, ref uint bitDepth);

        [DllImport(libraryPath, EntryPoint = "imaqGetBytesPerPixel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetBytesPerPixel(IntPtr image, ref int byteCount);

        [DllImport(libraryPath, EntryPoint = "imaqGetImageInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetImageInfo(IntPtr image, ref ImageInfo info);

        [DllImport(libraryPath, EntryPoint = "imaqGetImageSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetImageSize(IntPtr image, ref int width, ref int height);

        [DllImport(libraryPath, EntryPoint = "imaqGetImageType", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetImageType(IntPtr image, ref ImageType type);

        [DllImport(libraryPath, EntryPoint = "imaqGetMaskOffset", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetMaskOffset(IntPtr image, ref Point offset);

        [DllImport(libraryPath, EntryPoint = "imaqGetPixelAddress", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetPixelAddress(IntPtr image, Point pixel);

        [DllImport(libraryPath, EntryPoint = "imaqGetVisionInfoTypes", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetVisionInfoTypes(IntPtr image, ref uint present);

        [DllImport(libraryPath, EntryPoint = "imaqIsImageEmpty", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqIsImageEmpty(IntPtr image, ref int empty);

        [DllImport(libraryPath, EntryPoint = "imaqReadCustomData", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadCustomData(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string key, ref uint size);

        [DllImport(libraryPath, EntryPoint = "imaqRemoveCustomData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRemoveCustomData(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string key);

        [DllImport(libraryPath, EntryPoint = "imaqRemoveVisionInfo2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRemoveVisionInfo2(IntPtr image, uint info);

        [DllImport(libraryPath, EntryPoint = "imaqSetBitDepth", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetBitDepth(IntPtr image, uint bitDepth);

        [DllImport(libraryPath, EntryPoint = "imaqSetImageSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetImageSize(IntPtr image, int width, int height);

        [DllImport(libraryPath, EntryPoint = "imaqSetMaskOffset", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetMaskOffset(IntPtr image, Point offset);

        [DllImport(libraryPath, EntryPoint = "imaqWriteCustomData", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteCustomData(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr data, uint size);

        [DllImport(libraryPath, EntryPoint = "imaqAreToolsContextSensitive", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAreToolsContextSensitive(ref int sensitive);

        [DllImport(libraryPath, EntryPoint = "imaqCloseWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCloseWindow(int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqDisplayImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDisplayImage(IntPtr image, int windowNumber, int resize);

        [DllImport(libraryPath, EntryPoint = "imaqGetLastKey", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetLastKey(IntPtr keyPressed, ref int windowNumber, ref int modifiers);

        [DllImport(libraryPath, EntryPoint = "imaqGetSystemWindowHandle", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetSystemWindowHandle(int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowCenterPos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowCenterPos(int windowNumber, ref Point centerPosition);

        [DllImport(libraryPath, EntryPoint = "imaqSetToolContextSensitivity", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetToolContextSensitivity(int sensitive);

        [DllImport(libraryPath, EntryPoint = "imaqShowWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqShowWindow(int windowNumber, int visible);

        [DllImport(libraryPath, EntryPoint = "imaqCast", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCast(IntPtr dest, IntPtr source, ImageType type, ref float lookup, int shift);

        [DllImport(libraryPath, EntryPoint = "imaqCopyRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCopyRect(IntPtr dest, IntPtr source, Rect rect, Point destLoc);

        [DllImport(libraryPath, EntryPoint = "imaqDuplicate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDuplicate(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqFlatten", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFlatten(IntPtr image, FlattenType type, CompressionType compression, int quality, ref uint size);

        [DllImport(libraryPath, EntryPoint = "imaqFlip", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFlip(IntPtr dest, IntPtr source, FlipAxis axis);

        [DllImport(libraryPath, EntryPoint = "imaqMask", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMask(IntPtr dest, IntPtr source, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqResample", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqResample(IntPtr dest, IntPtr source, int newWidth, int newHeight, InterpolationMethod method, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqScale", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqScale(IntPtr dest, IntPtr source, int xScale, int yScale, ScalingMode scaleMode, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqTranspose", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqTranspose(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqUnflatten", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqUnflatten(IntPtr image, IntPtr data, uint size);

        [DllImport(libraryPath, EntryPoint = "imaqUnwrapImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqUnwrapImage(IntPtr dest, IntPtr source, Annulus annulus, RectOrientation orientation, InterpolationMethod method);

        [DllImport(libraryPath, EntryPoint = "imaqView3D", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqView3D(IntPtr dest, IntPtr source, ref View3DOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqCloseAVI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCloseAVI(int session);

        [DllImport(libraryPath, EntryPoint = "imaqCreateAVI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCreateAVI([MarshalAs(UnmanagedType.LPStr)] string fileName, [MarshalAs(UnmanagedType.LPStr)] string compressionFilter, int quality, uint framesPerSecond, uint maxDataSize);

        [DllImport(libraryPath, EntryPoint = "imaqGetAVIInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetAVIInfo(int session, ref AVIInfo info);

        [DllImport(libraryPath, EntryPoint = "imaqGetFileInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetFileInfo([MarshalAs(UnmanagedType.LPStr)] string fileName, ref CalibrationUnit calibrationUnit, ref float calibrationX, ref float calibrationY, ref int width, ref int height, ref ImageType imageType);

        [DllImport(libraryPath, EntryPoint = "imaqGetFilterNames", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetFilterNames(ref int numFilters);

        [DllImport(libraryPath, EntryPoint = "imaqLoadImagePopup", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLoadImagePopup([MarshalAs(UnmanagedType.LPStr)] string defaultDirectory, [MarshalAs(UnmanagedType.LPStr)] string defaultFileSpec, [MarshalAs(UnmanagedType.LPStr)] string fileTypeList, [MarshalAs(UnmanagedType.LPStr)] string title, int allowMultiplePaths, ButtonLabel buttonLabel, int restrictDirectory, int restrictExtension, int allowCancel, int allowMakeDirectory, ref int cancelled, ref int numPaths);

        [DllImport(libraryPath, EntryPoint = "imaqOpenAVI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOpenAVI([MarshalAs(UnmanagedType.LPStr)] string fileName);

        [DllImport(libraryPath, EntryPoint = "imaqReadAVIFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReadAVIFrame(IntPtr image, int session, uint frameNum, IntPtr data, ref uint dataSize);

        [DllImport(libraryPath, EntryPoint = "imaqReadFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReadFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, ref RGBValue colorTable, ref int numColors);

        [DllImport(libraryPath, EntryPoint = "imaqReadVisionFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReadVisionFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, ref RGBValue colorTable, ref int numColors);

        [DllImport(libraryPath, EntryPoint = "imaqWriteAVIFrame", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteAVIFrame(IntPtr image, int session, IntPtr data, uint dataLength);

        [DllImport(libraryPath, EntryPoint = "imaqWriteBMPFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteBMPFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, int compress, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqWriteFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqWriteJPEGFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteJPEGFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, uint quality, IntPtr colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqWriteJPEG2000File", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteJPEG2000File(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, int lossless, float compressionRatio, ref JPEG2000FileAdvancedOptions advancedOptions, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqWritePNGFile2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWritePNGFile2(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, uint compressionSpeed, ref RGBValue colorTable, int useBitDepth);

        [DllImport(libraryPath, EntryPoint = "imaqWriteTIFFFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteTIFFFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, ref TIFFFileOptions options, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqWriteVisionFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteVisionFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqBuildCoordinateSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqBuildCoordinateSystem(ref Point points, ReferenceMode mode, AxisOrientation orientation, ref CoordinateSystem system);

        [DllImport(libraryPath, EntryPoint = "imaqFitCircle2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFitCircle2(ref PointFloat points, int numPoints, ref FitCircleOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqFitEllipse2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFitEllipse2(ref PointFloat points, int numPoints, ref FitEllipseOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqFitLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFitLine(ref PointFloat points, int numPoints, ref FitLineOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqGetAngle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetAngle(PointFloat start1, PointFloat end1, PointFloat start2, PointFloat end2, ref float angle);

        [DllImport(libraryPath, EntryPoint = "imaqGetBisectingLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetBisectingLine(PointFloat start1, PointFloat end1, PointFloat start2, PointFloat end2, ref PointFloat bisectStart, ref PointFloat bisectEnd);

        [DllImport(libraryPath, EntryPoint = "imaqGetDistance", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetDistance(PointFloat point1, PointFloat point2, ref float distance);

        [DllImport(libraryPath, EntryPoint = "imaqGetIntersection", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetIntersection(PointFloat start1, PointFloat end1, PointFloat start2, PointFloat end2, ref PointFloat intersection);

        [DllImport(libraryPath, EntryPoint = "imaqGetMidLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetMidLine(PointFloat refLineStart, PointFloat refLineEnd, PointFloat point, ref PointFloat midLineStart, ref PointFloat midLineEnd);

        [DllImport(libraryPath, EntryPoint = "imaqGetPerpendicularLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetPerpendicularLine(PointFloat refLineStart, PointFloat refLineEnd, PointFloat point, ref PointFloat perpLineStart, ref PointFloat perpLineEnd, ref double distance);

        [DllImport(libraryPath, EntryPoint = "imaqGetPointsOnContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetPointsOnContour(IntPtr image, ref int numSegments);

        [DllImport(libraryPath, EntryPoint = "imaqGetPointsOnLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetPointsOnLine(Point start, Point end, ref int numPoints);

        [DllImport(libraryPath, EntryPoint = "imaqGetPolygonArea", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetPolygonArea(ref PointFloat points, int numPoints, ref float area);

        [DllImport(libraryPath, EntryPoint = "imaqInterpolatePoints", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqInterpolatePoints(IntPtr image, ref Point points, int numPoints, InterpolationMethod method, int subpixel, ref int interpCount);

        [DllImport(libraryPath, EntryPoint = "imaqClipboardToImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqClipboardToImage(IntPtr dest, ref RGBValue palette);

        [DllImport(libraryPath, EntryPoint = "imaqImageToClipboard", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqImageToClipboard(IntPtr image, ref RGBValue palette);

        [DllImport(libraryPath, EntryPoint = "imaqFillBorder", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFillBorder(IntPtr image, BorderMethod method);

        [DllImport(libraryPath, EntryPoint = "imaqGetBorderSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetBorderSize(IntPtr image, ref int borderSize);

        [DllImport(libraryPath, EntryPoint = "imaqSetBorderSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetBorderSize(IntPtr image, int size);

        [DllImport(libraryPath, EntryPoint = "imaqArrayToImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqArrayToImage(IntPtr image, IntPtr array, int numCols, int numRows);

        [DllImport(libraryPath, EntryPoint = "imaqCreateImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateImage(ImageType type, int borderSize);

        [DllImport(libraryPath, EntryPoint = "imaqImageToArray", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqImageToArray(IntPtr image, Rect rect, ref int columns, ref int rows);

        [DllImport(libraryPath, EntryPoint = "imaqColorBCGTransform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqColorBCGTransform(IntPtr dest, IntPtr source, ref BCGOptions redOptions, ref BCGOptions greenOptions, ref BCGOptions blueOptions, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqColorEqualize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqColorEqualize(IntPtr dest, IntPtr source, int colorEqualization);

        [DllImport(libraryPath, EntryPoint = "imaqColorHistogram2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqColorHistogram2(IntPtr image, int numClasses, ColorMode mode, ref CIEXYZValue whiteReference, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqColorLookup", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqColorLookup(IntPtr dest, IntPtr source, ColorMode mode, IntPtr mask, ref short plane1, ref short plane2, ref short plane3);

        [DllImport(libraryPath, EntryPoint = "imaqColorThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqColorThreshold(IntPtr dest, IntPtr source, int replaceValue, ColorMode mode, ref Range plane1Range, ref Range plane2Range, ref Range plane3Range);

        [DllImport(libraryPath, EntryPoint = "imaqSupervisedColorSegmentation", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSupervisedColorSegmentation(ref IntPtr session, IntPtr labelImage, IntPtr srcImage, ref ROI roi, ref ROILabel labelIn, uint numLabelIn, int maxDistance, int minIdentificationScore, ref ColorSegmenationOptions segmentOptions);

        [DllImport(libraryPath, EntryPoint = "imaqGetColorSegmentationMaxDistance", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetColorSegmentationMaxDistance(ref IntPtr session, ref ColorSegmenationOptions segmentOptions, SegmentationDistanceLevel distLevel, ref int maxDistance);

        [DllImport(libraryPath, EntryPoint = "imaqBCGTransform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqBCGTransform(IntPtr dest, IntPtr source, ref BCGOptions options, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqEqualize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqEqualize(IntPtr dest, IntPtr source, float min, float max, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqInverse", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqInverse(IntPtr dest, IntPtr source, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqMathTransform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMathTransform(IntPtr dest, IntPtr source, MathTransformMethod method, float rangeMin, float rangeMax, float power, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqWatershedTransform", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWatershedTransform(IntPtr dest, IntPtr source, int connectivity8, ref int zoneCount);

        [DllImport(libraryPath, EntryPoint = "imaqLookup2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLookup2(IntPtr dest, IntPtr source, ref int table, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqAreScrollbarsVisible", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAreScrollbarsVisible(int windowNumber, ref int visible);

        [DllImport(libraryPath, EntryPoint = "imaqBringWindowToTop", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqBringWindowToTop(int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqGetMousePos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetMousePos(ref Point position, ref int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowBackground", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowBackground(int windowNumber, ref WindowBackgroundFillStyle fillStyle, ref WindowBackgroundHatchStyle hatchStyle, ref RGBValue fillColor, ref RGBValue backgroundColor);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowDisplayMapping", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowDisplayMapping(int windowNum, ref DisplayMapping mapping);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowGrid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowGrid(int windowNumber, ref int xResolution, ref int yResolution);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowHandle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowHandle(ref int handle);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowPos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowPos(int windowNumber, ref Point position);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowSize(int windowNumber, ref int width, ref int height);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetWindowTitle(int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowZoom2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowZoom2(int windowNumber, ref float xZoom, ref float yZoom);

        [DllImport(libraryPath, EntryPoint = "imaqIsWindowNonTearing", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqIsWindowNonTearing(int windowNumber, ref int nonTearing);

        [DllImport(libraryPath, EntryPoint = "imaqIsWindowVisible", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqIsWindowVisible(int windowNumber, ref int visible);

        [DllImport(libraryPath, EntryPoint = "imaqMoveWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMoveWindow(int windowNumber, Point position);

        [DllImport(libraryPath, EntryPoint = "imaqSetupWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetupWindow(int windowNumber, int configuration);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowBackground", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowBackground(int windowNumber, WindowBackgroundFillStyle fillStyle, WindowBackgroundHatchStyle hatchStyle, ref RGBValue fillColor, ref RGBValue backgroundColor);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowDisplayMapping", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowDisplayMapping(int windowNumber, ref DisplayMapping mapping);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowGrid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowGrid(int windowNumber, int xResolution, int yResolution);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowMaxContourCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowMaxContourCount(int windowNumber, uint maxContourCount);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowNonTearing", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowNonTearing(int windowNumber, int nonTearing);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowPalette", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowPalette(int windowNumber, PaletteType type, ref RGBValue palette, int numColors);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowSize", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowSize(int windowNumber, int width, int height);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowThreadPolicy", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowThreadPolicy(WindowThreadPolicy policy);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowTitle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowTitle(int windowNumber, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowZoomToFit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowZoomToFit(int windowNumber, int zoomToFit);

        [DllImport(libraryPath, EntryPoint = "imaqShowScrollbars", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqShowScrollbars(int windowNumber, int visible);

        [DllImport(libraryPath, EntryPoint = "imaqZoomWindow2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqZoomWindow2(int windowNumber, float xZoom, float yZoom, Point center);

        [DllImport(libraryPath, EntryPoint = "imaqGetKernel", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetKernel(KernelFamily family, int size, int number);

        [DllImport(libraryPath, EntryPoint = "imaqMakeAnnulus", CallingConvention = CallingConvention.Cdecl)]
        public static extern Annulus imaqMakeAnnulus(Point center, int innerRadius, int referRadius, double startAngle, double endAngle);

        [DllImport(libraryPath, EntryPoint = "imaqMakePoint", CallingConvention = CallingConvention.Cdecl)]
        public static extern Point imaqMakePoint(int xCoordinate, int yCoordinate);

        [DllImport(libraryPath, EntryPoint = "imaqMakePointFloat", CallingConvention = CallingConvention.Cdecl)]
        public static extern PointFloat imaqMakePointFloat(float xCoordinate, float yCoordinate);

        [DllImport(libraryPath, EntryPoint = "imaqMakeRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern Rect imaqMakeRect(int top, int left, int height, int width);

        [DllImport(libraryPath, EntryPoint = "imaqMakeRectFromRotatedRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern Rect imaqMakeRectFromRotatedRect(RotatedRect rotatedRect);

        [DllImport(libraryPath, EntryPoint = "imaqMakeRotatedRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern RotatedRect imaqMakeRotatedRect(int top, int left, int height, int width, double angle);

        [DllImport(libraryPath, EntryPoint = "imaqMakeRotatedRectFromRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern RotatedRect imaqMakeRotatedRectFromRect(Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqMulticoreOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMulticoreOptions(MulticoreOperation operation, ref uint customNumCores);

        [DllImport(libraryPath, EntryPoint = "imaqCloseToolWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCloseToolWindow();

        [DllImport(libraryPath, EntryPoint = "imaqGetCurrentTool", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetCurrentTool(ref Tool currentTool);

        [DllImport(libraryPath, EntryPoint = "imaqGetLastEvent", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetLastEvent(ref WindowEventType type, ref int windowNumber, ref Tool tool, ref Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqGetToolWindowHandle", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetToolWindowHandle();

        [DllImport(libraryPath, EntryPoint = "imaqGetToolWindowPos", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetToolWindowPos(ref Point position);

        [DllImport(libraryPath, EntryPoint = "imaqIsToolWindowVisible", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqIsToolWindowVisible(ref int visible);

        [DllImport(libraryPath, EntryPoint = "imaqMoveToolWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMoveToolWindow(Point position);

        [DllImport(libraryPath, EntryPoint = "imaqSetCurrentTool", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetCurrentTool(Tool currentTool);

        [DllImport(libraryPath, EntryPoint = "imaqSetToolColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetToolColor(ref RGBValue color);

        [DllImport(libraryPath, EntryPoint = "imaqSetupToolWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetupToolWindow(int showCoordinates, int maxIconsPerLine, ref ToolWindowOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqShowToolWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqShowToolWindow(int visible);

        [DllImport(libraryPath, EntryPoint = "imaqGetMeterArc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetMeterArc(int lightNeedle, MeterArcMode mode, ref ROI roi, PointFloat basePoint, PointFloat start, PointFloat end);

        [DllImport(libraryPath, EntryPoint = "imaqReadMeter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReadMeter(IntPtr image, ref MeterArc arcInfo, ref double percentage, ref PointFloat endOfNeedle);

        [DllImport(libraryPath, EntryPoint = "imaqCopyCalibrationInfo2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCopyCalibrationInfo2(IntPtr dest, IntPtr source, Point offset);

        [DllImport(libraryPath, EntryPoint = "imaqGetCalibrationInfo2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetCalibrationInfo2(IntPtr image);

        [DllImport(libraryPath, EntryPoint = "imaqGetCalibrationInfo3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetCalibrationInfo3(IntPtr image, uint isGetErrorMap);

        [DllImport(libraryPath, EntryPoint = "imaqLearnCalibrationGrid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnCalibrationGrid(IntPtr image, ref ROI roi, ref LearnCalibrationOptions options, ref GridDescriptor grid, ref CoordinateSystem system, ref RangeFloat range, ref float quality);

        [DllImport(libraryPath, EntryPoint = "imaqLearnCalibrationPoints", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnCalibrationPoints(IntPtr image, ref CalibrationPoints points, ref ROI roi, ref LearnCalibrationOptions options, ref GridDescriptor grid, ref CoordinateSystem system, ref float quality);

        [DllImport(libraryPath, EntryPoint = "imaqSetCoordinateSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetCoordinateSystem(IntPtr image, ref CoordinateSystem system);

        [DllImport(libraryPath, EntryPoint = "imaqSetSimpleCalibration", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetSimpleCalibration(IntPtr image, ScalingMethod method, int learnTable, ref GridDescriptor grid, ref CoordinateSystem system);

        [DllImport(libraryPath, EntryPoint = "imaqTransformPixelToRealWorld", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqTransformPixelToRealWorld(IntPtr image, ref PointFloat pixelCoordinates, int numCoordinates);

        [DllImport(libraryPath, EntryPoint = "imaqTransformRealWorldToPixel", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqTransformRealWorldToPixel(IntPtr image, ref PointFloat realWorldCoordinates, int numCoordinates);

        [DllImport(libraryPath, EntryPoint = "imaqSetSimpleCalibration2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetSimpleCalibration2(IntPtr image, ref GridDescriptor gridDescriptor);

        [DllImport(libraryPath, EntryPoint = "imaqCalibrationSetAxisInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCalibrationSetAxisInfo(IntPtr image, ref CoordinateSystem axisInfo);

        [DllImport(libraryPath, EntryPoint = "imaqCalibrationGetThumbnailImage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCalibrationGetThumbnailImage(IntPtr templateImage, IntPtr image, CalibrationThumbnailType type, uint index);

        [DllImport(libraryPath, EntryPoint = "imaqCalibrationGetCalibrationInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCalibrationGetCalibrationInfo(IntPtr image, uint isGetErrorMap);

        [DllImport(libraryPath, EntryPoint = "imaqCalibrationGetCameraParameters", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCalibrationGetCameraParameters(IntPtr templateImage);

        [DllImport(libraryPath, EntryPoint = "imaqCalibrationCompactInformation", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCalibrationCompactInformation(IntPtr image);

        [DllImport(libraryPath, EntryPoint = "imaqArrayToComplexPlane", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqArrayToComplexPlane(IntPtr dest, IntPtr source, ref float newPixels, ComplexPlane plane);

        [DllImport(libraryPath, EntryPoint = "imaqComplexPlaneToArray", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqComplexPlaneToArray(IntPtr image, ComplexPlane plane, Rect rect, ref int rows, ref int columns);

        [DllImport(libraryPath, EntryPoint = "imaqExtractColorPlanes", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqExtractColorPlanes(IntPtr image, ColorMode mode, IntPtr plane1, IntPtr plane2, IntPtr plane3);

        [DllImport(libraryPath, EntryPoint = "imaqExtractComplexPlane", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqExtractComplexPlane(IntPtr dest, IntPtr source, ComplexPlane plane);

        [DllImport(libraryPath, EntryPoint = "imaqGetLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetLine(IntPtr image, Point start, Point end, ref int numPoints);

        [DllImport(libraryPath, EntryPoint = "imaqReplaceColorPlanes", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReplaceColorPlanes(IntPtr dest, IntPtr source, ColorMode mode, IntPtr plane1, IntPtr plane2, IntPtr plane3);

        [DllImport(libraryPath, EntryPoint = "imaqReplaceComplexPlane", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReplaceComplexPlane(IntPtr dest, IntPtr source, IntPtr newValues, ComplexPlane plane);

        [DllImport(libraryPath, EntryPoint = "imaqSetLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetLine(IntPtr image, IntPtr array, int arraySize, Point start, Point end);

        [DllImport(libraryPath, EntryPoint = "imaqLearnColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLearnColor(IntPtr image, ref ROI roi, ColorSensitivity sensitivity, int saturation);

        [DllImport(libraryPath, EntryPoint = "imaqMatchColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchColor(IntPtr image, ref ColorInformation info, ref ROI roi, ref int numScores);

        [DllImport(libraryPath, EntryPoint = "imaqAttenuate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAttenuate(IntPtr dest, IntPtr source, AttenuateMode highlow);

        [DllImport(libraryPath, EntryPoint = "imaqConjugate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConjugate(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqFFT", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFFT(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqFlipFrequencies", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFlipFrequencies(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqInverseFFT", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqInverseFFT(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqTruncate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqTruncate(IntPtr dest, IntPtr source, TruncateMode highlow, float ratioToKeep);

        [DllImport(libraryPath, EntryPoint = "imaqGradeDataMatrixBarcodeAIM", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGradeDataMatrixBarcodeAIM(IntPtr image, ref AIMGradeReport report);

        [DllImport(libraryPath, EntryPoint = "imaqReadBarcode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadBarcode(IntPtr image, BarcodeType type, ref ROI roi, int validate);

        [DllImport(libraryPath, EntryPoint = "imaqReadDataMatrixBarcode2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadDataMatrixBarcode2(IntPtr image, ref ROI roi, DataMatrixGradingMode prepareForGrading, ref DataMatrixDescriptionOptions descriptionOptions, ref DataMatrixSizeOptions sizeOptions, ref DataMatrixSearchOptions searchOptions);

        [DllImport(libraryPath, EntryPoint = "imaqReadPDF417Barcode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadPDF417Barcode(IntPtr image, ref ROI roi, Barcode2DSearchMode searchMode, ref uint numBarcodes);

        [DllImport(libraryPath, EntryPoint = "imaqReadQRCode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadQRCode(IntPtr image, ref ROI roi, QRGradingMode reserved, ref QRCodeDescriptionOptions descriptionOptions, ref QRCodeSizeOptions sizeOptions, ref QRCodeSearchOptions searchOptions);

        [DllImport(libraryPath, EntryPoint = "imaqFindLCDSegments", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFindLCDSegments(ref ROI roi, IntPtr image, ref LCDOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqReadLCD", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadLCD(IntPtr image, ref ROI roi, ref LCDOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqMatchShape", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchShape(IntPtr dest, IntPtr source, IntPtr templateImage, int scaleInvariant, int connectivity8, double tolerance, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqAddAnnulusContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddAnnulusContour(ref ROI roi, Annulus annulus);

        [DllImport(libraryPath, EntryPoint = "imaqAddClosedContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddClosedContour(ref ROI roi, ref Point points, int numPoints);

        [DllImport(libraryPath, EntryPoint = "imaqAddLineContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddLineContour(ref ROI roi, Point start, Point end);

        [DllImport(libraryPath, EntryPoint = "imaqAddOpenContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddOpenContour(ref ROI roi, ref Point points, int numPoints);

        [DllImport(libraryPath, EntryPoint = "imaqAddOvalContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddOvalContour(ref ROI roi, Rect boundingBox);

        [DllImport(libraryPath, EntryPoint = "imaqAddPointContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddPointContour(ref ROI roi, Point point);

        [DllImport(libraryPath, EntryPoint = "imaqAddRectContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddRectContour(ref ROI roi, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqAddRotatedRectContour2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddRotatedRectContour2(ref ROI roi, RotatedRect rect);

        [DllImport(libraryPath, EntryPoint = "imaqCopyContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCopyContour(ref ROI destRoi, ref ROI sourceRoi, int id);

        [DllImport(libraryPath, EntryPoint = "imaqGetContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetContour(ref ROI roi, uint index);

        [DllImport(libraryPath, EntryPoint = "imaqGetContourColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetContourColor(ref ROI roi, int id, ref RGBValue contourColor);

        [DllImport(libraryPath, EntryPoint = "imaqGetContourCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetContourCount(ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqGetContourInfo2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetContourInfo2(ref ROI roi, int id);

        [DllImport(libraryPath, EntryPoint = "imaqMoveContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMoveContour(ref ROI roi, int id, int deltaX, int deltaY);

        [DllImport(libraryPath, EntryPoint = "imaqRemoveContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRemoveContour(ref ROI roi, int id);

        [DllImport(libraryPath, EntryPoint = "imaqSetContourColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetContourColor(ref ROI roi, int id, ref RGBValue color);

        [DllImport(libraryPath, EntryPoint = "imaqConstructROI2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConstructROI2(IntPtr image, ref ROI roi, Tool initialTool, ref ToolWindowOptions tools, ref ConstructROIOptions2 options, ref int okay);

        [DllImport(libraryPath, EntryPoint = "imaqCreateROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateROI();

        [DllImport(libraryPath, EntryPoint = "imaqGetROIBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetROIBoundingBox(ref ROI roi, ref Rect boundingBox);

        [DllImport(libraryPath, EntryPoint = "imaqGetROIColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetROIColor(ref ROI roi, ref RGBValue roiColor);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetWindowROI(int windowNumber);

        [DllImport(libraryPath, EntryPoint = "imaqSetROIColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetROIColor(ref ROI roi, ref RGBValue color);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowROI(int windowNumber, ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqCentroid", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCentroid(IntPtr image, ref PointFloat centroid, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqExtractCurves", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqExtractCurves(IntPtr image, ref ROI roi, ref CurveOptions curveOptions, ref uint numCurves);

        [DllImport(libraryPath, EntryPoint = "imaqHistogram", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqHistogram(IntPtr image, int numClasses, float min, float max, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqLinearAverages2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLinearAverages2(IntPtr image, LinearAveragesMode mode, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqLineProfile", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLineProfile(IntPtr image, Point start, Point end);

        [DllImport(libraryPath, EntryPoint = "imaqQuantify", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqQuantify(IntPtr image, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqClearError", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqClearError();

        [DllImport(libraryPath, EntryPoint = "imaqGetErrorText", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetErrorText(int errorCode);

        [DllImport(libraryPath, EntryPoint = "imaqGetLastError", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetLastError();

        [DllImport(libraryPath, EntryPoint = "imaqGetLastErrorFunc", CallingConvention = CallingConvention.Cdecl)]
        public static extern string imaqGetLastErrorFunc();

        [DllImport(libraryPath, EntryPoint = "imaqSetError", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetError(int errorCode, [MarshalAs(UnmanagedType.LPStr)] string function);

        [DllImport(libraryPath, EntryPoint = "imaqAutoThreshold2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqAutoThreshold2(IntPtr dest, IntPtr source, int numClasses, ThresholdMethod method, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqLocalThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLocalThreshold(IntPtr dest, IntPtr source, uint windowWidth, uint windowHeight, LocalThresholdMethod method, double deviationWeight, ObjectType type, float replaceValue);

        [DllImport(libraryPath, EntryPoint = "imaqMagicWand", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMagicWand(IntPtr dest, IntPtr source, Point coord, float tolerance, int connectivity8, float replaceValue);

        [DllImport(libraryPath, EntryPoint = "imaqMultithreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMultithreshold(IntPtr dest, IntPtr source, ref ThresholdData ranges, int numRanges);

        [DllImport(libraryPath, EntryPoint = "imaqThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqThreshold(IntPtr dest, IntPtr source, float rangeMin, float rangeMax, int useNewValue, float newValue);

        [DllImport(libraryPath, EntryPoint = "imaqDispose", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDispose(IntPtr imaqObject);

        [DllImport(libraryPath, EntryPoint = "imaqDetectCircles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqDetectCircles(IntPtr image, ref CircleDescriptor circleDescriptor, ref CurveOptions curveOptions, ref ShapeDetectionOptions shapeDetectionOptions, ref ROI roi, ref int numMatchesReturned);

        [DllImport(libraryPath, EntryPoint = "imaqDetectEllipses", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqDetectEllipses(IntPtr image, ref EllipseDescriptor ellipseDescriptor, ref CurveOptions curveOptions, ref ShapeDetectionOptions shapeDetectionOptions, ref ROI roi, ref int numMatchesReturned);

        [DllImport(libraryPath, EntryPoint = "imaqDetectLines", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqDetectLines(IntPtr image, ref LineDescriptor lineDescriptor, ref CurveOptions curveOptions, ref ShapeDetectionOptions shapeDetectionOptions, ref ROI roi, ref int numMatchesReturned);

        [DllImport(libraryPath, EntryPoint = "imaqDetectRectangles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqDetectRectangles(IntPtr image, ref RectangleDescriptor rectangleDescriptor, ref CurveOptions curveOptions, ref ShapeDetectionOptions shapeDetectionOptions, ref ROI roi, ref int numMatchesReturned);

        [DllImport(libraryPath, EntryPoint = "imaqGetGeometricFeaturesFromCurves", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetGeometricFeaturesFromCurves(ref Curve curves, uint numCurves, ref FeatureType featureTypes, uint numFeatureTypes, ref uint numFeatures);

        [DllImport(libraryPath, EntryPoint = "imaqGetGeometricTemplateFeatureInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetGeometricTemplateFeatureInfo(IntPtr pattern, ref uint numFeatures);

        [DllImport(libraryPath, EntryPoint = "imaqLearnColorPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnColorPattern(IntPtr image, ref LearnColorPatternOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqLearnGeometricPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnGeometricPattern(IntPtr image, PointFloat originOffset, ref CurveOptions curveOptions, ref LearnGeometricPatternAdvancedOptions advancedLearnOptions, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqLearnIntPtrs", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLearnIntPtrs(ref IntPtr patterns, uint numberOfPatterns, ref String255 labels);

        [DllImport(libraryPath, EntryPoint = "imaqLearnPattern3", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnPattern3(IntPtr image, LearningMode learningMode, ref LearnPatternAdvancedOptions advancedOptions, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqMatchColorPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchColorPattern(IntPtr image, IntPtr pattern, ref MatchColorPatternOptions options, Rect searchRect, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqMatchGeometricPattern2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchGeometricPattern2(IntPtr image, IntPtr pattern, ref CurveOptions curveOptions, ref MatchGeometricPatternOptions matchOptions, ref MatchGeometricPatternAdvancedOptions2 advancedMatchOptions, ref ROI roi, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqMatchIntPtrs", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchIntPtrs(IntPtr image, ref IntPtr multiplePattern, ref ROI roi, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqReadIntPtrFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadIntPtrFile([MarshalAs(UnmanagedType.LPStr)] string fileName, String255 description);

        [DllImport(libraryPath, EntryPoint = "imaqRefineMatches", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqRefineMatches(IntPtr image, IntPtr pattern, ref PatternMatch candidatesIn, int numCandidatesIn, ref MatchPatternOptions options, ref MatchPatternAdvancedOptions advancedOptions, ref int numCandidatesref);

        [DllImport(libraryPath, EntryPoint = "imaqSetIntPtrsOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetIntPtrsOptions(ref IntPtr multiplePattern, [MarshalAs(UnmanagedType.LPStr)] string label, ref CurveOptions curveOptions, ref MatchGeometricPatternOptions matchOptions, ref MatchGeometricPatternAdvancedOptions2 advancedMatchOptions);

        [DllImport(libraryPath, EntryPoint = "imaqWriteIntPtrFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteIntPtrFile(ref IntPtr multiplePattern, [MarshalAs(UnmanagedType.LPStr)] string fileName, [MarshalAs(UnmanagedType.LPStr)] string description);

        [DllImport(libraryPath, EntryPoint = "imaqMatchGeometricPattern3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchGeometricPattern3(IntPtr image, IntPtr pattern, ref CurveOptions curveOptions, ref MatchGeometricPatternOptions matchOptions, ref MatchGeometricPatternAdvancedOptions3 advancedMatchOptions, ref ROI roi, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqLearnGeometricPattern2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnGeometricPattern2(IntPtr image, PointFloat originOffset, double angleOffset, ref CurveOptions curveOptions, ref LearnGeometricPatternAdvancedOptions2 advancedLearnOptions, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqMatchPattern3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchPattern3(IntPtr image, IntPtr pattern, ref MatchPatternOptions options, ref MatchPatternAdvancedOptions advancedOptions, ref ROI roi, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqClearOverlay", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqClearOverlay(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqCopyOverlay", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCopyOverlay(IntPtr dest, IntPtr source, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqGetOverlayProperties", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetOverlayProperties(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string group, ref TransformBehaviors transformBehaviors);

        [DllImport(libraryPath, EntryPoint = "imaqMergeOverlay", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMergeOverlay(IntPtr dest, IntPtr source, ref RGBValue palette, uint numColors, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayArc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayArc(IntPtr image, ref ArcInfo arc, ref RGBValue color, DrawMode drawMode, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayBitmap", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayBitmap(IntPtr image, Point destLoc, ref RGBValue bitmap, uint numCols, uint numRows, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayClosedContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayClosedContour(IntPtr image, ref Point points, int numPoints, ref RGBValue color, DrawMode drawMode, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayLine(IntPtr image, Point start, Point end, ref RGBValue color, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayMetafile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayMetafile(IntPtr image, IntPtr metafile, Rect rect, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayOpenContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayOpenContour(IntPtr image, ref Point points, int numPoints, ref RGBValue color, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayOval", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayOval(IntPtr image, Rect boundingBox, ref RGBValue color, DrawMode drawMode, IntPtr group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayPoints", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayPoints(IntPtr image, ref Point points, int numPoints, ref RGBValue colors, int numColors, PointSymbol symbol, ref UserPointSymbol userSymbol, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayRect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayRect(IntPtr image, Rect rect, ref RGBValue color, DrawMode drawMode, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayROI(IntPtr image, ref ROI roi, PointSymbol symbol, ref UserPointSymbol userSymbol, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqOverlayText", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqOverlayText(IntPtr image, Point origin, [MarshalAs(UnmanagedType.LPStr)] string text, ref RGBValue color, ref OverlayTextOptions options, [MarshalAs(UnmanagedType.LPStr)] string group);

        [DllImport(libraryPath, EntryPoint = "imaqSetOverlayProperties", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetOverlayProperties(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string group, ref TransformBehaviors transformBehaviors);

        [DllImport(libraryPath, EntryPoint = "imaqCreateCharSet", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateCharSet();

        [DllImport(libraryPath, EntryPoint = "imaqDeleteChar", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDeleteChar(ref CharSet set, int index);

        [DllImport(libraryPath, EntryPoint = "imaqGetCharCount", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetCharCount(ref CharSet set);

        [DllImport(libraryPath, EntryPoint = "imaqGetCharInfo2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetCharInfo2(ref CharSet set, int index);

        [DllImport(libraryPath, EntryPoint = "imaqReadOCRFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqReadOCRFile([MarshalAs(UnmanagedType.LPStr)] string fileName, ref CharSet set, String255 setDescription, ref ReadTextOptions readOptions, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqReadText3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadText3(IntPtr image, ref CharSet set, ref ROI roi, ref ReadTextOptions readOptions, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqRenameChar", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRenameChar(ref CharSet set, int index, [MarshalAs(UnmanagedType.LPStr)] string newCharValue);

        [DllImport(libraryPath, EntryPoint = "imaqSetReferenceChar", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetReferenceChar(ref CharSet set, int index, int isReferenceChar);

        [DllImport(libraryPath, EntryPoint = "imaqTrainChars", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqTrainChars(IntPtr image, ref CharSet set, int index, [MarshalAs(UnmanagedType.LPStr)] string charValue, ref ROI roi, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqVerifyPatterns", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqVerifyPatterns(IntPtr image, ref CharSet set, ref String255 expectedPatterns, int patternCount, ref ROI roi, ref int numScores);

        [DllImport(libraryPath, EntryPoint = "imaqVerifyText", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqVerifyText(IntPtr image, ref CharSet set, [MarshalAs(UnmanagedType.LPStr)] string expectedString, ref ROI roi, ref int numScores);

        [DllImport(libraryPath, EntryPoint = "imaqWriteOCRFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteOCRFile([MarshalAs(UnmanagedType.LPStr)] string fileName, ref CharSet set, [MarshalAs(UnmanagedType.LPStr)] string setDescription, ref ReadTextOptions readOptions, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqExtractContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqExtractContour(IntPtr image, ref ROI roi, ExtractContourDirection direction, ref CurveParameters curveParams, ref ConnectionConstraint connectionConstraintParams, uint numOfConstraints, ExtractContourSelection selection, IntPtr contourImage);

        [DllImport(libraryPath, EntryPoint = "imaqContourOverlay", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqContourOverlay(IntPtr image, IntPtr contourImage, ref ContourOverlaySettings pointsSettings, ref ContourOverlaySettings eqnSettings, [MarshalAs(UnmanagedType.LPStr)] string groupName);

        [DllImport(libraryPath, EntryPoint = "imaqContourComputeCurvature", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourComputeCurvature(IntPtr contourImage, uint kernel);

        [DllImport(libraryPath, EntryPoint = "imaqContourClassifyCurvature", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourClassifyCurvature(IntPtr contourImage, uint kernel, ref RangeLabel curvatureClasses, uint numCurvatureClasses);

        [DllImport(libraryPath, EntryPoint = "imaqContourComputeDistances", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourComputeDistances(IntPtr targetImage, IntPtr templateImage, ref SetupMatchPatternData matchSetupData, uint smoothingKernel);

        [DllImport(libraryPath, EntryPoint = "imaqContourClassifyDistances", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourClassifyDistances(IntPtr targetImage, IntPtr templateImage, ref SetupMatchPatternData matchSetupData, uint smoothingKernel, ref RangeLabel distanceRanges, uint numDistanceRanges);

        [DllImport(libraryPath, EntryPoint = "imaqContourInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourInfo(IntPtr contourImage);

        [DllImport(libraryPath, EntryPoint = "imaqContourSetupMatchPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourSetupMatchPattern(ref MatchMode matchMode, uint enableSubPixelAccuracy, ref CurveParameters curveParams, uint useLearnCurveParameters, ref RangeSettingDouble rangeSettings, uint numRangeSettings);

        [DllImport(libraryPath, EntryPoint = "imaqContourAdvancedSetupMatchPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqContourAdvancedSetupMatchPattern(ref SetupMatchPatternData matchSetupData, ref GeometricAdvancedSetupDataOption geometricOptions, uint numGeometricOptions);

        [DllImport(libraryPath, EntryPoint = "imaqContourFitLine", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourFitLine(IntPtr image, double pixelRadius);

        [DllImport(libraryPath, EntryPoint = "imaqContourFitCircle", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourFitCircle(IntPtr image, double pixelRadius, int rejectrefliers);

        [DllImport(libraryPath, EntryPoint = "imaqContourFitEllipse", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourFitEllipse(IntPtr image, double pixelRadius, int rejectrefliers);

        [DllImport(libraryPath, EntryPoint = "imaqContourFitSpline", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourFitSpline(IntPtr image, int degree, int numberOfControlPoints);

        [DllImport(libraryPath, EntryPoint = "imaqContourFitPolynomial", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqContourFitPolynomial(IntPtr image, int order);

        [DllImport(libraryPath, EntryPoint = "imaqFindCircularEdge2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFindCircularEdge2(IntPtr image, ref ROI roi, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref FindCircularEdgeOptions edgeOptions, ref CircleFitOptions circleFitOptions);

        [DllImport(libraryPath, EntryPoint = "imaqFindConcentricEdge2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqFindConcentricEdge2(IntPtr image, ref ROI roi, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref FindConcentricEdgeOptions edgeOptions, ref ConcentricEdgeFitOptions concentricEdgeFitOptions);

        [DllImport(libraryPath, EntryPoint = "imaqGrayMorphologyReconstruct", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGrayMorphologyReconstruct(IntPtr dstImage, IntPtr srcImage, IntPtr markerImage, ref PointFloat points, int numOfPoints, MorphologyReconstructOperation operation, ref StructuringElement structuringElement, ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqMorphologyReconstruct", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqMorphologyReconstruct(IntPtr dstImage, IntPtr srcImage, IntPtr markerImage, ref PointFloat points, int numOfPoints, MorphologyReconstructOperation operation, Connectivity connectivity, ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqDetectTextureDefect", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDetectTextureDefect(ref IntPtr session, IntPtr destImage, IntPtr srcImage, ref ROI roi, int initialStepSize, int finalStepSize, byte defectPixelValue, double minClassificationScore);

        [DllImport(libraryPath, EntryPoint = "imaqClassificationTextureDefectOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqClassificationTextureDefectOptions(ref IntPtr session, ref WindowSize windowOptions, ref WaveletOptions waveletOptions, ref IntPtr bandsUsed, ref int numBandsUsed, ref CooccurrenceOptions cooccurrenceOptions, byte setOperation);

        [DllImport(libraryPath, EntryPoint = "imaqCooccurrenceMatrix", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCooccurrenceMatrix(IntPtr srcImage, ref ROI roi, int levelPixel, ref DisplacementVector displacementVec, IntPtr featureOptionArray, uint featureOptionArraySize, ref IntPtr cooccurrenceMatrixArray, ref int coocurrenceMatrixRows, ref int coocurrenceMatrixCols, ref IntPtr featureVectorArray, ref int featureVectorArraySize);

        [DllImport(libraryPath, EntryPoint = "imaqExtractTextureFeatures", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqExtractTextureFeatures(IntPtr srcImage, ref ROI roi, ref WindowSize windowOptions, ref WaveletOptions waveletOptions, IntPtr waveletBands, uint numWaveletBands, ref CooccurrenceOptions cooccurrenceOptions, byte useWindow);

        [DllImport(libraryPath, EntryPoint = "imaqExtractWaveletBands", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqExtractWaveletBands(IntPtr srcImage, ref WaveletOptions waveletOptions, IntPtr waveletBands, uint numWaveletBands);

        [DllImport(libraryPath, EntryPoint = "imaqMaskToROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMaskToROI(IntPtr mask, ref int withinLimit);

        [DllImport(libraryPath, EntryPoint = "imaqROIProfile", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqROIProfile(IntPtr image, ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqROIToMask", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqROIToMask(IntPtr mask, ref ROI roi, int fillValue, IntPtr imageModel, ref int inSpace);

        [DllImport(libraryPath, EntryPoint = "imaqTransformROI2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqTransformROI2(ref ROI roi, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem);

        [DllImport(libraryPath, EntryPoint = "imaqLabelToROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLabelToROI(IntPtr image, ref uint labelsIn, uint numLabelsIn, int maxNumVectors, int isExternelEdges);

        [DllImport(libraryPath, EntryPoint = "imaqGrayMorphology", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGrayMorphology(IntPtr dest, IntPtr source, MorphologyMethod method, ref StructuringElement structuringElement);

        [DllImport(libraryPath, EntryPoint = "imaqAddClassifierSample", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddClassifierSample(IntPtr image, ref IntPtr session, ref ROI roi, [MarshalAs(UnmanagedType.LPStr)] string sampleClass, ref double featureVector, uint vectorSize);

        [DllImport(libraryPath, EntryPoint = "imaqAdvanceClassify", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqAdvanceClassify(IntPtr image, ref IntPtr session, ref ROI roi, ref double featureVector, uint vectorSize);

        [DllImport(libraryPath, EntryPoint = "imaqClassify", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqClassify(IntPtr image, ref IntPtr session, ref ROI roi, ref double featureVector, uint vectorSize);

        [DllImport(libraryPath, EntryPoint = "imaqCreateClassifier", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateClassifier(ClassifierType type);

        [DllImport(libraryPath, EntryPoint = "imaqDeleteClassifierSample", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDeleteClassifierSample(ref IntPtr session, int index);

        [DllImport(libraryPath, EntryPoint = "imaqGetClassifierAccuracy", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetClassifierAccuracy(ref IntPtr session);

        [DllImport(libraryPath, EntryPoint = "imaqGetClassifierSampleInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetClassifierSampleInfo(ref IntPtr session, int index, ref int numSamples);

        [DllImport(libraryPath, EntryPoint = "imaqGetColorClassifierOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetColorClassifierOptions(ref IntPtr session, ref ColorOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqGetNearestNeighborOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetNearestNeighborOptions(ref IntPtr session, ref NearestNeighborOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqGetParticleClassifierOptions2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetParticleClassifierOptions2(ref IntPtr session, ref ParticleClassifierPreprocessingOptions2 preprocessingOptions, ref ParticleClassifierOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqReadClassifierFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadClassifierFile(ref IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string fileName, ReadClassifierFileMode mode, ref ClassifierType type, ref ClassifierEngineType engine, String255 description);

        [DllImport(libraryPath, EntryPoint = "imaqRelabelClassifierSample", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqRelabelClassifierSample(ref IntPtr session, int index, [MarshalAs(UnmanagedType.LPStr)] string newClass);

        [DllImport(libraryPath, EntryPoint = "imaqSetParticleClassifierOptions2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetParticleClassifierOptions2(ref IntPtr session, ref ParticleClassifierPreprocessingOptions2 preprocessingOptions, ref ParticleClassifierOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqSetColorClassifierOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetColorClassifierOptions(ref IntPtr session, ref ColorOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqTrainNearestNeighborClassifier", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqTrainNearestNeighborClassifier(ref IntPtr session, ref NearestNeighborOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqWriteClassifierFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWriteClassifierFile(ref IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string fileName, WriteClassifierFileMode mode, String255 description);

        [DllImport(libraryPath, EntryPoint = "imaqClampMax2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqClampMax2(IntPtr image, ref ROI roi, ref CoordinateSystem baseSystem, ref CoordinateSystem newSystem, ref CurveOptions curveSettings, ref ClampSettings clampSettings, ref ClampOverlaySettings clampOverlaySettings);

        [DllImport(libraryPath, EntryPoint = "imaqCompareGoldenTemplate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCompareGoldenTemplate(IntPtr image, IntPtr goldenTemplate, IntPtr brightDefects, IntPtr darkDefects, ref InspectionAlignment alignment, ref InspectionOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqLearnGoldenTemplate", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnGoldenTemplate(IntPtr goldenTemplate, PointFloat originOffset, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqWritePNGFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqWritePNGFile(IntPtr image, [MarshalAs(UnmanagedType.LPStr)] string fileName, uint compressionSpeed, ref RGBValue colorTable);

        [DllImport(libraryPath, EntryPoint = "imaqSelectParticles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSelectParticles(IntPtr image, ref ParticleReport reports, int reportCount, int rejectBorder, ref SelectParticleCriteria criteria, int criteriaCount, ref int selectedCount);

        [DllImport(libraryPath, EntryPoint = "imaqParticleFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqParticleFilter(IntPtr dest, IntPtr source, ref ParticleFilterCriteria criteria, int criteriaCount, int rejectMatches, int connectivity8);

        [DllImport(libraryPath, EntryPoint = "imaqGetParticleInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetParticleInfo(IntPtr image, int connectivity8, ParticleInfoMode mode, ref int reportCount);

        [DllImport(libraryPath, EntryPoint = "imaqCalcCoeff", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCalcCoeff(IntPtr image, ref ParticleReport report, MeasurementValue parameter, ref float coefficient);

        [DllImport(libraryPath, EntryPoint = "imaqEdgeTool", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEdgeTool(IntPtr image, ref Point points, int numPoints, ref EdgeOptions options, ref int numEdges);

        [DllImport(libraryPath, EntryPoint = "imaqCircles", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCircles(IntPtr dest, IntPtr source, float minRadius, float maxRadius, ref int numCircles);

        [DllImport(libraryPath, EntryPoint = "imaqLabel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLabel(IntPtr dest, IntPtr source, int connectivity8, ref int particleCount);

        [DllImport(libraryPath, EntryPoint = "imaqFitEllipse", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFitEllipse(ref PointFloat points, int numPoints, ref BestEllipse ellipse);

        [DllImport(libraryPath, EntryPoint = "imaqFitCircle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqFitCircle(ref PointFloat points, int numPoints, ref BestCircle circle);

        [DllImport(libraryPath, EntryPoint = "imaqMatchPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchPattern(IntPtr image, IntPtr pattern, ref MatchPatternOptions options, Rect searchRect, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqConvex", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConvex(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqIsVisionInfoPresent", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqIsVisionInfoPresent(IntPtr image, VisionInfoType type, ref int present);

        [DllImport(libraryPath, EntryPoint = "imaqLineGaugeTool", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLineGaugeTool(IntPtr image, Point start, Point end, LineGaugeMethod method, ref EdgeOptions edgeOptions, ref CoordinateTransform reference, ref float distance);

        [DllImport(libraryPath, EntryPoint = "imaqBestCircle", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqBestCircle(ref PointFloat points, int numPoints, ref PointFloat center, ref double radius);

        [DllImport(libraryPath, EntryPoint = "imaqSavePattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSavePattern(IntPtr pattern, [MarshalAs(UnmanagedType.LPStr)] string fileName);

        [DllImport(libraryPath, EntryPoint = "imaqLoadPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLoadPattern(IntPtr pattern, [MarshalAs(UnmanagedType.LPStr)] string fileName);

        [DllImport(libraryPath, EntryPoint = "imaqTransformROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqTransformROI(ref ROI roi, Point originStart, float angleStart, Point originFinal, float angleFinal);

        [DllImport(libraryPath, EntryPoint = "imaqCoordinateReference", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCoordinateReference(ref Point points, ReferenceMode mode, ref Point origin, ref float angle);

        [DllImport(libraryPath, EntryPoint = "imaqGetContourInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetContourInfo(ref ROI roi, int id);

        [DllImport(libraryPath, EntryPoint = "imaqSetWindowOverlay", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetWindowOverlay(int windowNumber, ref IntPtr overlay);

        [DllImport(libraryPath, EntryPoint = "imaqCreateOverlayFromROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateOverlayFromROI(ref ROI roi);

        [DllImport(libraryPath, EntryPoint = "imaqCreateOverlayFromMetafile", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqCreateOverlayFromMetafile(IntPtr metafile);

        [DllImport(libraryPath, EntryPoint = "imaqSetCalibrationInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetCalibrationInfo(IntPtr image, CalibrationUnit unit, float xDistance, float yDistance);

        [DllImport(libraryPath, EntryPoint = "imaqGetCalibrationInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetCalibrationInfo(IntPtr image, ref CalibrationUnit unit, ref float xDistance, ref float yDistance);

        [DllImport(libraryPath, EntryPoint = "imaqConstructROI", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConstructROI(IntPtr image, ref ROI roi, Tool initialTool, ref ToolWindowOptions tools, ref ConstructROIOptions options, ref int okay);

        [DllImport(libraryPath, EntryPoint = "imaqGetParticleClassifierOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetParticleClassifierOptions(ref IntPtr session, ref ParticleClassifierPreprocessingOptions preprocessingOptions, ref ParticleClassifierOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqZoomWindow", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqZoomWindow(int windowNumber, int xZoom, int yZoom, Point center);

        [DllImport(libraryPath, EntryPoint = "imaqGetWindowZoom", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqGetWindowZoom(int windowNumber, ref int xZoom, ref int yZoom);

        [DllImport(libraryPath, EntryPoint = "imaqParticleFilter3", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqParticleFilter3(IntPtr dest, IntPtr source, ref ParticleFilterCriteria2 criteria, int criteriaCount, ref ParticleFilterOptions options, ref ROI roi, ref int numParticles);

        [DllImport(libraryPath, EntryPoint = "imaqReadText2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadText2(IntPtr image, ref CharSet set, ref ROI roi, ref ReadTextOptions readOptions, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqLearnPattern2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnPattern2(IntPtr image, LearningMode learningMode, ref LearnPatternAdvancedOptions advancedOptions);

        [DllImport(libraryPath, EntryPoint = "imaqConvolve", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqConvolve(IntPtr dest, IntPtr source, ref float kernel, int matrixRows, int matrixCols, float normalize, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqDivide", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqDivide(IntPtr dest, IntPtr sourceA, IntPtr sourceB);

        [DllImport(libraryPath, EntryPoint = "imaqEdgeTool3", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEdgeTool3(IntPtr image, ref ROI roi, EdgeProcess processType, ref EdgeOptions2 edgeOptions);

        [DllImport(libraryPath, EntryPoint = "imaqConcentricRake", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqConcentricRake(IntPtr image, ref ROI roi, ConcentricRakeDirection direction, EdgeProcess process, ref RakeOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqSpoke", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqSpoke(IntPtr image, ref ROI roi, SpokeDirection direction, EdgeProcess process, ref SpokeOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqLearnPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLearnPattern(IntPtr image, LearningMode learningMode);

        [DllImport(libraryPath, EntryPoint = "imaqLookup", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqLookup(IntPtr dest, IntPtr source, ref short table, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqMatchPattern2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchPattern2(IntPtr image, IntPtr pattern, ref MatchPatternOptions options, ref MatchPatternAdvancedOptions advancedOptions, Rect searchRect, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqSetParticleClassifierOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqSetParticleClassifierOptions(ref IntPtr session, ref ParticleClassifierPreprocessingOptions preprocessingOptions, ref ParticleClassifierOptions options);

        [DllImport(libraryPath, EntryPoint = "imaqCopyCalibrationInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqCopyCalibrationInfo(IntPtr dest, IntPtr source);

        [DllImport(libraryPath, EntryPoint = "imaqParticleFilter2", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqParticleFilter2(IntPtr dest, IntPtr source, ref ParticleFilterCriteria2 criteria, int criteriaCount, int rejectMatches, int connectivity8, ref int numParticles);

        [DllImport(libraryPath, EntryPoint = "imaqEdgeTool2", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqEdgeTool2(IntPtr image, ref Point points, int numPoints, EdgeProcess process, ref EdgeOptions options, ref int numEdges);

        [DllImport(libraryPath, EntryPoint = "imaqAddRotatedRectContour", CallingConvention = CallingConvention.Cdecl)]
        public static extern int imaqAddRotatedRectContour(ref ROI roi, RotatedRect rect);

        [DllImport(libraryPath, EntryPoint = "imaqReadDataMatrixBarcode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadDataMatrixBarcode(IntPtr image, ref ROI roi, ref DataMatrixOptions options, ref uint numBarcodes);

        [DllImport(libraryPath, EntryPoint = "imaqLinearAverages", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqLinearAverages(IntPtr image, Rect rect);

        [DllImport(libraryPath, EntryPoint = "imaqMatchGeometricPattern", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqMatchGeometricPattern(IntPtr image, IntPtr pattern, ref CurveOptions curveOptions, ref MatchGeometricPatternOptions matchOptions, ref MatchGeometricPatternAdvancedOptions advancedMatchOptions, ref ROI roi, ref int numMatches);

        [DllImport(libraryPath, EntryPoint = "imaqGetCharInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqGetCharInfo(ref CharSet set, int index);

        [DllImport(libraryPath, EntryPoint = "imaqReadText", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqReadText(IntPtr image, ref CharSet set, ref ROI roi, ref ReadTextOptions readOptions, ref OCRProcessingOptions processingOptions, ref OCRSpacingOptions spacingOptions);

        [DllImport(libraryPath, EntryPoint = "imaqAutoThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqAutoThreshold(IntPtr dest, IntPtr source, int numClasses, ThresholdMethod method);

        [DllImport(libraryPath, EntryPoint = "imaqColorHistogram", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqColorHistogram(IntPtr image, int numClasses, ColorMode mode, IntPtr mask);

        [DllImport(libraryPath, EntryPoint = "imaqRake", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr imaqRake(IntPtr image, ref ROI roi, RakeDirection direction, EdgeProcess process, ref RakeOptions options);

        [DllImport(libraryPath, EntryPoint = "Priv_ReadJPEGString_C", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Priv_ReadJPEGString_C(IntPtr image, ref IntPtr jpegString, uint stringLength);
    }
}
