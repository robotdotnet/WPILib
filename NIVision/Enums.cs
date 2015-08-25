using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIVision
{
    public enum PointSymbol 
    {
        IMAQ_POINT_AS_PIXEL = 0,
        IMAQ_POINT_AS_CROSS = 1,
        IMAQ_POINT_USER_DEFINED = 2,
        IMAQ_POINT_SYMBOL_SIZE_GUARD = -1,
    }

    public enum MeasurementValue 
    {
        IMAQ_AREA = 0,
        IMAQ_AREA_CALIBRATED = 1,
        IMAQ_NUM_HOLES = 2,
        IMAQ_AREA_OF_HOLES = 3,
        IMAQ_TOTAL_AREA = 4,
        IMAQ_IMAGE_AREA = 5,
        IMAQ_PARTICLE_TO_IMAGE = 6,
        IMAQ_PARTICLE_TO_TOTAL = 7,
        IMAQ_CENTER_MASS_X = 8,
        IMAQ_CENTER_MASS_Y = 9,
        IMAQ_LEFT_COLUMN = 10,
        IMAQ_TOP_ROW = 11,
        IMAQ_RIGHT_COLUMN = 12,
        IMAQ_BOTTOM_ROW = 13,
        IMAQ_WIDTH = 14,
        IMAQ_HEIGHT = 15,
        IMAQ_MAX_SEGMENT_LENGTH = 16,
        IMAQ_MAX_SEGMENT_LEFT_COLUMN = 17,
        IMAQ_MAX_SEGMENT_TOP_ROW = 18,
        IMAQ_PERIMETER = 19,
        IMAQ_PERIMETER_OF_HOLES = 20,
        IMAQ_SIGMA_X = 21,
        IMAQ_SIGMA_Y = 22,
        IMAQ_SIGMA_XX = 23,
        IMAQ_SIGMA_YY = 24,
        IMAQ_SIGMA_XY = 25,
        IMAQ_PROJ_X = 26,
        IMAQ_PROJ_Y = 27,
        IMAQ_INERTIA_XX = 28,
        IMAQ_INERTIA_YY = 29,
        IMAQ_INERTIA_XY = 30,
        IMAQ_MEAN_H = 31,
        IMAQ_MEAN_V = 32,
        IMAQ_MAX_INTERCEPT = 33,
        IMAQ_MEAN_INTERCEPT = 34,
        IMAQ_ORIENTATION = 35,
        IMAQ_EQUIV_ELLIPSE_MINOR = 36,
        IMAQ_ELLIPSE_MAJOR = 37,
        IMAQ_ELLIPSE_MINOR = 38,
        IMAQ_ELLIPSE_RATIO = 39,
        IMAQ_RECT_LONG_SIDE = 40,
        IMAQ_RECT_SHORT_SIDE = 41,
        IMAQ_RECT_RATIO = 42,
        IMAQ_ELONGATION = 43,
        IMAQ_COMPACTNESS = 44,
        IMAQ_HEYWOOD = 45,
        IMAQ_TYPE_FACTOR = 46,
        IMAQ_HYDRAULIC = 47,
        IMAQ_WADDLE_DISK = 48,
        IMAQ_DIAGONAL = 49,
        IMAQ_MEASUREMENT_VALUE_SIZE_GUARD = -1,
    }

    public enum ScalingMode 
    {
        IMAQ_SCALE_LARGER = 0,
        IMAQ_SCALE_SMALLER = 1,
        IMAQ_SCALING_MODE_SIZE_GUARD = -1,
    }

    public enum ScalingMethod 
    {
        IMAQ_SCALE_TO_PRESERVE_AREA = 0,
        IMAQ_SCALE_TO_FIT = 1,
        IMAQ_SCALING_METHOD_SIZE_GUARD = -1,
    }

    public enum ReferenceMode 
    {
        IMAQ_COORD_X_Y = 0,
        IMAQ_COORD_ORIGIN_X = 1,
        IMAQ_REFERENCE_MODE_SIZE_GUARD = -1,
    }

    public enum RectOrientation 
    {
        IMAQ_BASE_INSIDE = 0,
        IMAQ_BASE_OUTSIDE = 1,
        IMAQ_TEXT_ORIENTATION_SIZE_GUARD = -1,
    }

    public enum ShapeMode 
    {
        IMAQ_SHAPE_RECT = 1,
        IMAQ_SHAPE_OVAL = 2,
        IMAQ_SHAPE_MODE_SIZE_GUARD = -1,
    }

    public enum PolarityType : long
    {
        IMAQ_EDGE_RISING = 1,
        IMAQ_EDGE_FALLING = -1,
        IMAQ_POLARITY_TYPE_SIZE_GUARD = 4294967295,
    }

    public enum SizeType 
    {
        IMAQ_KEEP_LARGE = 0,
        IMAQ_KEEP_SMALL = 1,
        IMAQ_SIZE_TYPE_SIZE_GUARD = -1,
    }

    public enum Plane3D 
    {
        IMAQ_3D_REAL = 0,
        IMAQ_3D_IMAGINARY = 1,
        IMAQ_3D_MAGNITUDE = 2,
        IMAQ_3D_PHASE = 3,
        IMAQ_PLANE_3D_SIZE_GUARD = -1,
    }

    public enum PhotometricMode 
    {
        IMAQ_WHITE_IS_ZERO = 0,
        IMAQ_BLACK_IS_ZERO = 1,
        IMAQ_PHOTOMETRIC_MODE_SIZE_GUARD = -1,
    }

    public enum ParticleInfoMode 
    {
        IMAQ_BASIC_INFO = 0,
        IMAQ_ALL_INFO = 1,
        IMAQ_PARTICLE_INFO_MODE_SIZE_GUARD = -1,
    }

    public enum OutlineMethod 
    {
        IMAQ_EDGE_DIFFERENCE = 0,
        IMAQ_EDGE_GRADIENT = 1,
        IMAQ_EDGE_PREWITT = 2,
        IMAQ_EDGE_ROBERTS = 3,
        IMAQ_EDGE_SIGMA = 4,
        IMAQ_EDGE_SOBEL = 5,
        IMAQ_OUTLINE_METHOD_SIZE_GUARD = -1,
    }

    public enum MorphologyMethod 
    {
        IMAQ_AUTOM = 0,
        IMAQ_CLOSE = 1,
        IMAQ_DILATE = 2,
        IMAQ_ERODE = 3,
        IMAQ_GRADIENT = 4,
        IMAQ_GRADIENTOUT = 5,
        IMAQ_GRADIENTIN = 6,
        IMAQ_HITMISS = 7,
        IMAQ_OPEN = 8,
        IMAQ_PCLOSE = 9,
        IMAQ_POPEN = 10,
        IMAQ_THICK = 11,
        IMAQ_THIN = 12,
        IMAQ_MORPHOLOGY_METHOD_SIZE_GUARD = -1,
    }

    public enum MeterArcMode 
    {
        IMAQ_METER_ARC_ROI = 0,
        IMAQ_METER_ARC_POINTS = 1,
        IMAQ_METER_ARC_MODE_SIZE_GUARD = -1,
    }

    public enum RakeDirection 
    {
        IMAQ_LEFT_TO_RIGHT = 0,
        IMAQ_RIGHT_TO_LEFT = 1,
        IMAQ_TOP_TO_BOTTOM = 2,
        IMAQ_BOTTOM_TO_TOP = 3,
        IMAQ_RAKE_DIRECTION_SIZE_GUARD = -1,
    }

    public enum TruncateMode 
    {
        IMAQ_TRUNCATE_LOW = 0,
        IMAQ_TRUNCATE_HIGH = 1,
        IMAQ_TRUNCATE_MODE_SIZE_GUARD = -1,
    }

    public enum AttenuateMode 
    {
        IMAQ_ATTENUATE_LOW = 0,
        IMAQ_ATTENUATE_HIGH = 1,
        IMAQ_ATTENUATE_MODE_SIZE_GUARD = -1,
    }

    public enum WindowThreadPolicy 
    {
        IMAQ_CALLING_THREAD = 0,
        IMAQ_SEPARATE_THREAD = 1,
        IMAQ_WINDOW_THREAD_POLICY_SIZE_GUARD = -1,
    }

    public enum WindowOptions 
    {
        IMAQ_WIND_RESIZABLE = 1,
        IMAQ_WIND_TITLEBAR = 2,
        IMAQ_WIND_CLOSEABLE = 4,
        IMAQ_WIND_TOPMOST = 8,
        IMAQ_WINDOW_OPTIONS_SIZE_GUARD = -1,
    }

    public enum WindowEventType 
    {
        IMAQ_NO_EVENT = 0,
        IMAQ_CLICK_EVENT = 1,
        IMAQ_DRAW_EVENT = 2,
        IMAQ_MOVE_EVENT = 3,
        IMAQ_SIZE_EVENT = 4,
        IMAQ_SCROLL_EVENT = 5,
        IMAQ_ACTIVATE_EVENT = 6,
        IMAQ_CLOSE_EVENT = 7,
        IMAQ_DOUBLE_CLICK_EVENT = 8,
        IMAQ_WINDOW_EVENT_TYPE_SIZE_GUARD = -1,
    }

    public enum VisionInfoType 
    {
        IMAQ_ANY_VISION_INFO = 0,
        IMAQ_PATTERN_MATCHING_INFO = 1,
        IMAQ_CALIBRATION_INFO = 2,
        IMAQ_OVERLAY_INFO = 3,
        IMAQ_VISION_INFO_TYPE_SIZE_GUARD = -1,
    }

    public enum SearchStrategy 
    {
        IMAQ_CONSERVATIVE = 1,
        IMAQ_BALANCED = 2,
        IMAQ_AGGRESSIVE = 3,
        IMAQ_VERY_AGGRESSIVE = 4,
        IMAQ_SEARCH_STRATEGY_SIZE_GUARD = -1,
    }

    public enum TwoEdgePolarityType 
    {
        IMAQ_NONE = 0,
        IMAQ_RISING_FALLING = 1,
        IMAQ_FALLING_RISING = 2,
        IMAQ_RISING_RISING = 3,
        IMAQ_FALLING_FALLING = 4,
        IMAQ_TWO_EDGE_POLARITY_TYPE_SIZE_GUARD = -1,
    }

    public enum ObjectType 
    {
        IMAQ_BRIGHT_OBJECTS = 0,
        IMAQ_DARK_OBJECTS = 1,
        IMAQ_OBJECT_TYPE_SIZE_GUARD = -1,
    }

    public enum Tool : long
    {
        IMAQ_NO_TOOL = -1,
        IMAQ_SELECTION_TOOL = 0,
        IMAQ_POINT_TOOL = 1,
        IMAQ_LINE_TOOL = 2,
        IMAQ_RECTANGLE_TOOL = 3,
        IMAQ_OVAL_TOOL = 4,
        IMAQ_POLYGON_TOOL = 5,
        IMAQ_CLOSED_FREEHAND_TOOL = 6,
        IMAQ_ANNULUS_TOOL = 7,
        IMAQ_ZOOM_TOOL = 8,
        IMAQ_PAN_TOOL = 9,
        IMAQ_POLYLINE_TOOL = 10,
        IMAQ_FREEHAND_TOOL = 11,
        IMAQ_ROTATED_RECT_TOOL = 12,
        IMAQ_ZOOM_OUT_TOOL = 13,
        IMAQ_TOOL_SIZE_GUARD = 4294967295,
    }

    public enum TIFFCompressionType 
    {
        IMAQ_NO_COMPRESSION = 0,
        IMAQ_JPEG = 1,
        IMAQ_RUN_LENGTH = 2,
        IMAQ_ZIP = 3,
        IMAQ_TIFF_COMPRESSION_TYPE_SIZE_GUARD = -1,
    }

    public enum ThresholdMethod 
    {
        IMAQ_THRESH_CLUSTERING = 0,
        IMAQ_THRESH_ENTROPY = 1,
        IMAQ_THRESH_METRIC = 2,
        IMAQ_THRESH_MOMENTS = 3,
        IMAQ_THRESH_INTERCLASS = 4,
        IMAQ_THRESHOLD_METHOD_SIZE_GUARD = -1,
    }

    public enum TextAlignment 
    {
        IMAQ_LEFT = 0,
        IMAQ_CENTER = 1,
        IMAQ_RIGHT = 2,
        IMAQ_TEXT_ALIGNMENT_SIZE_GUARD = -1,
    }

    public enum SpokeDirection 
    {
        IMAQ_OUTSIDE_TO_INSIDE = 0,
        IMAQ_INSIDE_TO_OUTSIDE = 1,
        IMAQ_SPOKE_DIRECTION_SIZE_GUARD = -1,
    }

    public enum SkeletonMethod 
    {
        IMAQ_SKELETON_L = 0,
        IMAQ_SKELETON_M = 1,
        IMAQ_SKELETON_INVERSE = 2,
        IMAQ_SKELETON_METHOD_SIZE_GUARD = -1,
    }

    public enum VerticalTextAlignment 
    {
        IMAQ_BOTTOM = 0,
        IMAQ_TOP = 1,
        IMAQ_BASELINE = 2,
        IMAQ_VERTICAL_TEXT_ALIGNMENT_SIZE_GUARD = -1,
    }

    public enum CalibrationROI 
    {
        IMAQ_FULL_IMAGE = 0,
        IMAQ_CALIBRATION_ROI = 1,
        IMAQ_USER_ROI = 2,
        IMAQ_CALIBRATION_AND_USER_ROI = 3,
        IMAQ_CALIBRATION_OR_USER_ROI = 4,
        IMAQ_CALIBRATION_ROI_SIZE_GUARD = -1,
    }

    public enum ContourType 
    {
        IMAQ_EMPTY_CONTOUR = 0,
        IMAQ_POINT = 1,
        IMAQ_LINE = 2,
        IMAQ_RECT = 3,
        IMAQ_OVAL = 4,
        IMAQ_CLOSED_CONTOUR = 5,
        IMAQ_OPEN_CONTOUR = 6,
        IMAQ_ANNULUS = 7,
        IMAQ_ROTATED_RECT = 8,
        IMAQ_CONTOUR_TYPE_SIZE_GUARD = -1,
    }

    public enum MathTransformMethod 
    {
        IMAQ_TRANSFORM_LINEAR = 0,
        IMAQ_TRANSFORM_LOG = 1,
        IMAQ_TRANSFORM_EXP = 2,
        IMAQ_TRANSFORM_SQR = 3,
        IMAQ_TRANSFORM_SQRT = 4,
        IMAQ_TRANSFORM_POWX = 5,
        IMAQ_TRANSFORM_POW1X = 6,
        IMAQ_MATH_TRANSFORM_METHOD_SIZE_GUARD = -1,
    }

    public enum ComplexPlane 
    {
        IMAQ_REAL = 0,
        IMAQ_IMAGINARY = 1,
        IMAQ_MAGNITUDE = 2,
        IMAQ_PHASE = 3,
        IMAQ_COMPLEX_PLANE_SIZE_GUARD = -1,
    }

    public enum PaletteType 
    {
        IMAQ_PALETTE_GRAY = 0,
        IMAQ_PALETTE_BINARY = 1,
        IMAQ_PALETTE_GRADIENT = 2,
        IMAQ_PALETTE_RAINBOW = 3,
        IMAQ_PALETTE_TEMPERATURE = 4,
        IMAQ_PALETTE_USER = 5,
        IMAQ_PALETTE_TYPE_SIZE_GUARD = -1,
    }

    public enum ColorSensitivity 
    {
        IMAQ_SENSITIVITY_LOW = 0,
        IMAQ_SENSITIVITY_MED = 1,
        IMAQ_SENSITIVITY_HIGH = 2,
        IMAQ_COLOR_SENSITIVITY_SIZE_GUARD = -1,
    }

    public enum ColorMode 
    {
        IMAQ_RGB = 0,
        IMAQ_HSL = 1,
        IMAQ_HSV = 2,
        IMAQ_HSI = 3,
        IMAQ_CIE = 4,
        IMAQ_CIEXYZ = 5,
        IMAQ_COLOR_MODE_SIZE_GUARD = -1,
    }

    public enum DetectionMode 
    {
        IMAQ_DETECT_PEAKS = 0,
        IMAQ_DETECT_VALLEYS = 1,
        IMAQ_DETECTION_MODE_SIZE_GUARD = -1,
    }

    public enum CalibrationUnit 
    {
        IMAQ_UNDEFINED = 0,
        IMAQ_ANGSTROM = 1,
        IMAQ_MICROMETER = 2,
        IMAQ_MILLIMETER = 3,
        IMAQ_CENTIMETER = 4,
        IMAQ_METER = 5,
        IMAQ_KILOMETER = 6,
        IMAQ_MICROINCH = 7,
        IMAQ_INCH = 8,
        IMAQ_FOOT = 9,
        IMAQ_NAUTICMILE = 10,
        IMAQ_GROUNDMILE = 11,
        IMAQ_STEP = 12,
        IMAQ_CALIBRATION_UNIT_SIZE_GUARD = -1,
    }

    public enum ConcentricRakeDirection 
    {
        IMAQ_COUNTER_CLOCKWISE = 0,
        IMAQ_CLOCKWISE = 1,
        IMAQ_CONCENTRIC_RAKE_DIRECTION_SIZE_GUARD = -1,
    }

    public enum CalibrationMode 
    {
        IMAQ_PERSPECTIVE = 0,
        IMAQ_NONLINEAR = 1,
        IMAQ_SIMPLE_CALIBRATION = 2,
        IMAQ_CORRECTED_IMAGE = 3,
        IMAQ_CALIBRATION_MODE_SIZE_GUARD = -1,
    }

    public enum BrowserLocation 
    {
        IMAQ_INSERT_FIRST_FREE = 0,
        IMAQ_INSERT_END = 1,
        IMAQ_BROWSER_LOCATION_SIZE_GUARD = -1,
    }

    public enum BrowserFrameStyle 
    {
        IMAQ_RAISED_FRAME = 0,
        IMAQ_BEVELLED_FRAME = 1,
        IMAQ_OUTLINE_FRAME = 2,
        IMAQ_HIDDEN_FRAME = 3,
        IMAQ_STEP_FRAME = 4,
        IMAQ_RAISED_OUTLINE_FRAME = 5,
        IMAQ_BROWSER_FRAME_STYLE_SIZE_GUARD = -1,
    }

    public enum BorderMethod 
    {
        IMAQ_BORDER_MIRROR = 0,
        IMAQ_BORDER_COPY = 1,
        IMAQ_BORDER_CLEAR = 2,
        IMAQ_BORDER_METHOD_SIZE_GUARD = -1,
    }

    public enum BarcodeType : long
    {
        IMAQ_INVALID = -1,
        IMAQ_CODABAR = 1,
        IMAQ_CODE39 = 2,
        IMAQ_CODE93 = 4,
        IMAQ_CODE128 = 8,
        IMAQ_EAN8 = 16,
        IMAQ_EAN13 = 32,
        IMAQ_I2_OF_5 = 64,
        IMAQ_MSI = 128,
        IMAQ_UPCA = 256,
        IMAQ_PHARMACODE = 512,
        IMAQ_RSS_LIMITED = 1024,
        IMAQ_BARCODE_TYPE_SIZE_GUARD = 4294967295,
    }

    public enum AxisOrientation 
    {
        IMAQ_DIRECT = 0,
        IMAQ_INDIRECT = 1,
        IMAQ_AXIS_ORIENTATION_SIZE_GUARD = -1,
    }

    public enum ColorIgnoreMode 
    {
        IMAQ_IGNORE_NONE = 0,
        IMAQ_IGNORE_BLACK = 1,
        IMAQ_IGNORE_WHITE = 2,
        IMAQ_IGNORE_BLACK_AND_WHITE = 3,
        IMAQ_BLACK_WHITE_IGNORE_MODE_SIZE_GUARD = -1,
    }

    public enum LevelType 
    {
        IMAQ_ABSOLUTE = 0,
        IMAQ_RELATIVE = 1,
        IMAQ_LEVEL_TYPE_SIZE_GUARD = -1,
    }

    public enum MatchingMode 
    {
        IMAQ_MATCH_SHIFT_INVARIANT = 1,
        IMAQ_MATCH_ROTATION_INVARIANT = 2,
        IMAQ_MATCHING_MODE_SIZE_GUARD = -1,
    }

    public enum MappingMethod 
    {
        IMAQ_FULL_DYNAMIC = 0,
        IMAQ_DOWNSHIFT = 1,
        IMAQ_RANGE = 2,
        IMAQ_90_PCT_DYNAMIC = 3,
        IMAQ_PERCENT_RANGE = 4,
        IMAQ_DEFAULT_MAPPING = 10,
        IMAQ_MOST_SIGNIFICANT = 11,
        IMAQ_FULL_DYNAMIC_ALWAYS = 12,
        IMAQ_DOWNSHIFT_ALWAYS = 13,
        IMAQ_RANGE_ALWAYS = 14,
        IMAQ_90_PCT_DYNAMIC_ALWAYS = 15,
        IMAQ_PERCENT_RANGE_ALWAYS = 16,
        IMAQ_MAPPING_METHOD_SIZE_GUARD = -1,
    }

    public enum ComparisonFunction 
    {
        IMAQ_CLEAR_LESS = 0,
        IMAQ_CLEAR_LESS_OR_EQUAL = 1,
        IMAQ_CLEAR_EQUAL = 2,
        IMAQ_CLEAR_GREATER_OR_EQUAL = 3,
        IMAQ_CLEAR_GREATER = 4,
        IMAQ_COMPARE_FUNCTION_SIZE_GUARD = -1,
    }

    public enum LineGaugeMethod 
    {
        IMAQ_EDGE_TO_EDGE = 0,
        IMAQ_EDGE_TO_POINT = 1,
        IMAQ_POINT_TO_EDGE = 2,
        IMAQ_POINT_TO_POINT = 3,
        IMAQ_LINE_GAUGE_METHOD_SIZE_GUARD = -1,
    }

    public enum Direction3D 
    {
        IMAQ_3D_NW = 0,
        IMAQ_3D_SW = 1,
        IMAQ_3D_SE = 2,
        IMAQ_3D_NE = 3,
        IMAQ_DIRECTION_3D_SIZE_GUARD = -1,
    }

    public enum LearningMode 
    {
        IMAQ_LEARN_ALL = 0,
        IMAQ_LEARN_SHIFT_INFORMATION = 1,
        IMAQ_LEARN_ROTATION_INFORMATION = 2,
        IMAQ_LEARNING_MODE_SIZE_GUARD = -1,
    }

    public enum KernelFamily 
    {
        IMAQ_GRADIENT_FAMILY = 0,
        IMAQ_LAPLACIAN_FAMILY = 1,
        IMAQ_SMOOTHING_FAMILY = 2,
        IMAQ_GAUSSIAN_FAMILY = 3,
        IMAQ_KERNEL_FAMILY_SIZE_GUARD = -1,
    }

    public enum InterpolationMethod 
    {
        IMAQ_ZERO_ORDER = 0,
        IMAQ_BILINEAR = 1,
        IMAQ_QUADRATIC = 2,
        IMAQ_CUBIC_SPLINE = 3,
        IMAQ_BILINEAR_FIXED = 4,
        IMAQ_INTERPOLATION_METHOD_SIZE_GUARD = -1,
    }

    public enum ImageType 
    {
        IMAQ_IMAGE_U8 = 0,
        IMAQ_IMAGE_U16 = 7,
        IMAQ_IMAGE_I16 = 1,
        IMAQ_IMAGE_SGL = 2,
        IMAQ_IMAGE_COMPLEX = 3,
        IMAQ_IMAGE_RGB = 4,
        IMAQ_IMAGE_HSL = 5,
        IMAQ_IMAGE_RGB_U64 = 6,
        IMAQ_IMAGE_TYPE_SIZE_GUARD = -1,
    }

    public enum ImageFeatureMode 
    {
        IMAQ_COLOR_AND_SHAPE_FEATURES = 0,
        IMAQ_COLOR_FEATURES = 1,
        IMAQ_SHAPE_FEATURES = 2,
        IMAQ_FEATURE_MODE_SIZE_GUARD = -1,
    }

    public enum FontColor 
    {
        IMAQ_WHITE = 0,
        IMAQ_BLACK = 1,
        IMAQ_INVERT = 2,
        IMAQ_BLACK_ON_WHITE = 3,
        IMAQ_WHITE_ON_BLACK = 4,
        IMAQ_FONT_COLOR_SIZE_GUARD = -1,
    }

    public enum FlipAxis 
    {
        IMAQ_HORIZONTAL_AXIS = 0,
        IMAQ_VERTICAL_AXIS = 1,
        IMAQ_CENTER_AXIS = 2,
        IMAQ_DIAG_L_TO_R_AXIS = 3,
        IMAQ_DIAG_R_TO_L_AXIS = 4,
        IMAQ_FLIP_AXIS_SIZE_GUARD = -1,
    }

    public enum EdgeProcess 
    {
        IMAQ_FIRST = 0,
        IMAQ_FIRST_AND_LAST = 1,
        IMAQ_ALL = 2,
        IMAQ_BEST = 3,
        IMAQ_EDGE_PROCESS_SIZE_GUARD = -1,
    }

    public enum DrawMode 
    {
        IMAQ_DRAW_VALUE = 0,
        IMAQ_DRAW_INVERT = 2,
        IMAQ_PAINT_VALUE = 1,
        IMAQ_PAINT_INVERT = 3,
        IMAQ_HIGHLIGHT_VALUE = 4,
        IMAQ_DRAW_MODE_SIZE_GUARD = -1,
    }

    public enum NearestNeighborMetric 
    {
        IMAQ_METRIC_MAXIMUM = 0,
        IMAQ_METRIC_SUM = 1,
        IMAQ_METRIC_EUCLIDEAN = 2,
        IMAQ_NEAREST_NEIGHBOR_METRIC_SIZE_GUARD = -1,
    }

    public enum ReadResolution 
    {
        IMAQ_LOW_RESOLUTION = 0,
        IMAQ_MEDIUM_RESOLUTION = 1,
        IMAQ_HIGH_RESOLUTION = 2,
        IMAQ_READ_RESOLUTION_SIZE_GUARD = -1,
    }

    public enum ThresholdMode 
    {
        IMAQ_FIXED_RANGE = 0,
        IMAQ_COMPUTED_UNIFORM = 1,
        IMAQ_COMPUTED_LINEAR = 2,
        IMAQ_COMPUTED_NONLINEAR = 3,
        IMAQ_THRESHOLD_MODE_SIZE_GUARD = -1,
    }

    public enum ReadStrategy 
    {
        IMAQ_READ_AGGRESSIVE = 0,
        IMAQ_READ_CONSERVATIVE = 1,
        IMAQ_READ_STRATEGY_SIZE_GUARD = -1,
    }

    public enum MeasurementType 
    {
        IMAQ_MT_CENTER_OF_MASS_X = 0,
        IMAQ_MT_CENTER_OF_MASS_Y = 1,
        IMAQ_MT_FIRST_PIXEL_X = 2,
        IMAQ_MT_FIRST_PIXEL_Y = 3,
        IMAQ_MT_BOUNDING_RECT_LEFT = 4,
        IMAQ_MT_BOUNDING_RECT_TOP = 5,
        IMAQ_MT_BOUNDING_RECT_RIGHT = 6,
        IMAQ_MT_BOUNDING_RECT_BOTTOM = 7,
        IMAQ_MT_MAX_FERET_DIAMETER_START_X = 8,
        IMAQ_MT_MAX_FERET_DIAMETER_START_Y = 9,
        IMAQ_MT_MAX_FERET_DIAMETER_END_X = 10,
        IMAQ_MT_MAX_FERET_DIAMETER_END_Y = 11,
        IMAQ_MT_MAX_HORIZ_SEGMENT_LENGTH_LEFT = 12,
        IMAQ_MT_MAX_HORIZ_SEGMENT_LENGTH_RIGHT = 13,
        IMAQ_MT_MAX_HORIZ_SEGMENT_LENGTH_ROW = 14,
        IMAQ_MT_BOUNDING_RECT_WIDTH = 16,
        IMAQ_MT_BOUNDING_RECT_HEIGHT = 17,
        IMAQ_MT_BOUNDING_RECT_DIAGONAL = 18,
        IMAQ_MT_PERIMETER = 19,
        IMAQ_MT_CONVEX_HULL_PERIMETER = 20,
        IMAQ_MT_HOLES_PERIMETER = 21,
        IMAQ_MT_MAX_FERET_DIAMETER = 22,
        IMAQ_MT_EQUIVALENT_ELLIPSE_MAJOR_AXIS = 23,
        IMAQ_MT_EQUIVALENT_ELLIPSE_MINOR_AXIS = 24,
        IMAQ_MT_EQUIVALENT_ELLIPSE_MINOR_AXIS_FERET = 25,
        IMAQ_MT_EQUIVALENT_RECT_LONG_SIDE = 26,
        IMAQ_MT_EQUIVALENT_RECT_SHORT_SIDE = 27,
        IMAQ_MT_EQUIVALENT_RECT_DIAGONAL = 28,
        IMAQ_MT_EQUIVALENT_RECT_SHORT_SIDE_FERET = 29,
        IMAQ_MT_AVERAGE_HORIZ_SEGMENT_LENGTH = 30,
        IMAQ_MT_AVERAGE_VERT_SEGMENT_LENGTH = 31,
        IMAQ_MT_HYDRAULIC_RADIUS = 32,
        IMAQ_MT_WADDEL_DISK_DIAMETER = 33,
        IMAQ_MT_AREA = 35,
        IMAQ_MT_HOLES_AREA = 36,
        IMAQ_MT_PARTICLE_AND_HOLES_AREA = 37,
        IMAQ_MT_CONVEX_HULL_AREA = 38,
        IMAQ_MT_IMAGE_AREA = 39,
        IMAQ_MT_NUMBER_OF_HOLES = 41,
        IMAQ_MT_NUMBER_OF_HORIZ_SEGMENTS = 42,
        IMAQ_MT_NUMBER_OF_VERT_SEGMENTS = 43,
        IMAQ_MT_ORIENTATION = 45,
        IMAQ_MT_MAX_FERET_DIAMETER_ORIENTATION = 46,
        IMAQ_MT_AREA_BY_IMAGE_AREA = 48,
        IMAQ_MT_AREA_BY_PARTICLE_AND_HOLES_AREA = 49,
        IMAQ_MT_RATIO_OF_EQUIVALENT_ELLIPSE_AXES = 50,
        IMAQ_MT_RATIO_OF_EQUIVALENT_RECT_SIDES = 51,
        IMAQ_MT_ELONGATION_FACTOR = 53,
        IMAQ_MT_COMPACTNESS_FACTOR = 54,
        IMAQ_MT_HEYWOOD_CIRCULARITY_FACTOR = 55,
        IMAQ_MT_TYPE_FACTOR = 56,
        IMAQ_MT_SUM_X = 58,
        IMAQ_MT_SUM_Y = 59,
        IMAQ_MT_SUM_XX = 60,
        IMAQ_MT_SUM_XY = 61,
        IMAQ_MT_SUM_YY = 62,
        IMAQ_MT_SUM_XXX = 63,
        IMAQ_MT_SUM_XXY = 64,
        IMAQ_MT_SUM_XYY = 65,
        IMAQ_MT_SUM_YYY = 66,
        IMAQ_MT_MOMENT_OF_INERTIA_XX = 68,
        IMAQ_MT_MOMENT_OF_INERTIA_XY = 69,
        IMAQ_MT_MOMENT_OF_INERTIA_YY = 70,
        IMAQ_MT_MOMENT_OF_INERTIA_XXX = 71,
        IMAQ_MT_MOMENT_OF_INERTIA_XXY = 72,
        IMAQ_MT_MOMENT_OF_INERTIA_XYY = 73,
        IMAQ_MT_MOMENT_OF_INERTIA_YYY = 74,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_XX = 75,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_XY = 76,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_YY = 77,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_XXX = 78,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_XXY = 79,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_XYY = 80,
        IMAQ_MT_NORM_MOMENT_OF_INERTIA_YYY = 81,
        IMAQ_MT_HU_MOMENT_1 = 82,
        IMAQ_MT_HU_MOMENT_2 = 83,
        IMAQ_MT_HU_MOMENT_3 = 84,
        IMAQ_MT_HU_MOMENT_4 = 85,
        IMAQ_MT_HU_MOMENT_5 = 86,
        IMAQ_MT_HU_MOMENT_6 = 87,
        IMAQ_MT_HU_MOMENT_7 = 88,
        IMAQ_MEASUREMENT_TYPE_SIZE_GUARD = -1,
    }

    public enum GeometricMatchingMode 
    {
        IMAQ_GEOMETRIC_MATCH_SHIFT_INVARIANT = 0,
        IMAQ_GEOMETRIC_MATCH_ROTATION_INVARIANT = 1,
        IMAQ_GEOMETRIC_MATCH_SCALE_INVARIANT = 2,
        IMAQ_GEOMETRIC_MATCH_OCCLUSION_INVARIANT = 4,
        IMAQ_GEOMETRIC_MATCHING_MODE_SIZE_GUARD = -1,
    }

    public enum ButtonLabel 
    {
        IMAQ_BUTTON_OK = 0,
        IMAQ_BUTTON_SAVE = 1,
        IMAQ_BUTTON_SELECT = 2,
        IMAQ_BUTTON_LOAD = 3,
        IMAQ_BUTTON_LABEL_SIZE_GUARD = -1,
    }

    public enum NearestNeighborMethod 
    {
        IMAQ_MINIMUM_MEAN_DISTANCE = 0,
        IMAQ_K_NEAREST_NEIGHBOR = 1,
        IMAQ_NEAREST_PROTOTYPE = 2,
        IMAQ_NEAREST_NEIGHBOR_METHOD_SIZE_GUARD = -1,
    }

    public enum QRMirrorMode : long
    {
        IMAQ_QR_MIRROR_MODE_AUTO_DETECT = -2,
        IMAQ_QR_MIRROR_MODE_MIRRORED = 1,
        IMAQ_QR_MIRROR_MODE_NORMAL = 0,
        IMAQ_QR_MIRROR_MODE_SIZE_GUARD = 4294967295,
    }

    public enum ColumnProcessingMode 
    {
        IMAQ_AVERAGE_COLUMNS = 0,
        IMAQ_MEDIAN_COLUMNS = 1,
        IMAQ_COLUMN_PROCESSING_MODE_SIZE_GUARD = -1,
    }

    public enum FindReferenceDirection 
    {
        IMAQ_LEFT_TO_RIGHT_DIRECT = 0,
        IMAQ_LEFT_TO_RIGHT_INDIRECT = 1,
        IMAQ_TOP_TO_BOTTOM_DIRECT = 2,
        IMAQ_TOP_TO_BOTTOM_INDIRECT = 3,
        IMAQ_RIGHT_TO_LEFT_DIRECT = 4,
        IMAQ_RIGHT_TO_LEFT_INDIRECT = 5,
        IMAQ_BOTTOM_TO_TOP_DIRECT = 6,
        IMAQ_BOTTOM_TO_TOP_INDIRECT = 7,
        IMAQ_FIND_COORD_SYS_DIR_SIZE_GUARD = -1,
    }

    public enum MulticoreOperation 
    {
        IMAQ_GET_CORES = 0,
        IMAQ_SET_CORES = 1,
        IMAQ_USE_MAX_AVAILABLE = 2,
        IMAQ_MULTICORE_OPERATION_SIZE_GUARD = -1,
    }

    public enum GroupBehavior 
    {
        IMAQ_GROUP_CLEAR = 0,
        IMAQ_GROUP_KEEP = 1,
        IMAQ_GROUP_TRANSFORM = 2,
        IMAQ_GROUP_BEHAVIOR_SIZE_GUARD = -1,
    }

    public enum QRDimensions 
    {
        IMAQ_QR_DIMENSIONS_AUTO_DETECT = 0,
        IMAQ_QR_DIMENSIONS_11x11 = 11,
        IMAQ_QR_DIMENSIONS_13x13 = 13,
        IMAQ_QR_DIMENSIONS_15x15 = 15,
        IMAQ_QR_DIMENSIONS_17x17 = 17,
        IMAQ_QR_DIMENSIONS_21x21 = 21,
        IMAQ_QR_DIMENSIONS_25x25 = 25,
        IMAQ_QR_DIMENSIONS_29x29 = 29,
        IMAQ_QR_DIMENSIONS_33x33 = 33,
        IMAQ_QR_DIMENSIONS_37x37 = 37,
        IMAQ_QR_DIMENSIONS_41x41 = 41,
        IMAQ_QR_DIMENSIONS_45x45 = 45,
        IMAQ_QR_DIMENSIONS_49x49 = 49,
        IMAQ_QR_DIMENSIONS_53x53 = 53,
        IMAQ_QR_DIMENSIONS_57x57 = 57,
        IMAQ_QR_DIMENSIONS_61x61 = 61,
        IMAQ_QR_DIMENSIONS_65x65 = 65,
        IMAQ_QR_DIMENSIONS_69x69 = 69,
        IMAQ_QR_DIMENSIONS_73x73 = 73,
        IMAQ_QR_DIMENSIONS_77x77 = 77,
        IMAQ_QR_DIMENSIONS_81x81 = 81,
        IMAQ_QR_DIMENSIONS_85x85 = 85,
        IMAQ_QR_DIMENSIONS_89x89 = 89,
        IMAQ_QR_DIMENSIONS_93x93 = 93,
        IMAQ_QR_DIMENSIONS_97x97 = 97,
        IMAQ_QR_DIMENSIONS_101x101 = 101,
        IMAQ_QR_DIMENSIONS_105x105 = 105,
        IMAQ_QR_DIMENSIONS_109x109 = 109,
        IMAQ_QR_DIMENSIONS_113x113 = 113,
        IMAQ_QR_DIMENSIONS_117x117 = 117,
        IMAQ_QR_DIMENSIONS_121x121 = 121,
        IMAQ_QR_DIMENSIONS_125x125 = 125,
        IMAQ_QR_DIMENSIONS_129x129 = 129,
        IMAQ_QR_DIMENSIONS_133x133 = 133,
        IMAQ_QR_DIMENSIONS_137x137 = 137,
        IMAQ_QR_DIMENSIONS_141x141 = 141,
        IMAQ_QR_DIMENSIONS_145x145 = 145,
        IMAQ_QR_DIMENSIONS_149x149 = 149,
        IMAQ_QR_DIMENSIONS_153x153 = 153,
        IMAQ_QR_DIMENSIONS_157x157 = 157,
        IMAQ_QR_DIMENSIONS_161x161 = 161,
        IMAQ_QR_DIMENSIONS_165x165 = 165,
        IMAQ_QR_DIMENSIONS_169x169 = 169,
        IMAQ_QR_DIMENSIONS_173x173 = 173,
        IMAQ_QR_DIMENSIONS_177x177 = 177,
        IMAQ_QR_DIMENSIONS_SIZE_GUARD = -1,
    }

    public enum QRCellFilterMode : long
    {
        IMAQ_QR_CELL_FILTER_MODE_AUTO_DETECT = -2,
        IMAQ_QR_CELL_FILTER_MODE_AVERAGE = 0,
        IMAQ_QR_CELL_FILTER_MODE_MEDIAN = 1,
        IMAQ_QR_CELL_FILTER_MODE_CENTRAL_AVERAGE = 2,
        IMAQ_QR_CELL_FILTER_MODE_HIGH_AVERAGE = 3,
        IMAQ_QR_CELL_FILTER_MODE_LOW_AVERAGE = 4,
        IMAQ_QR_CELL_FILTER_MODE_VERY_HIGH_AVERAGE = 5,
        IMAQ_QR_CELL_FILTER_MODE_VERY_LOW_AVERAGE = 6,
        IMAQ_QR_CELL_FILTER_MODE_ALL = 8,
        IMAQ_QR_CELL_FILTER_MODE_SIZE_GUARD = 4294967295,
    }

    public enum RoundingMode 
    {
        IMAQ_ROUNDING_MODE_OPTIMIZE = 0,
        IMAQ_ROUNDING_MODE_TRUNCATE = 1,
        IMAQ_ROUNDING_MODE_SIZE_GUARD = -1,
    }

    public enum QRDemodulationMode : long
    {
        IMAQ_QR_DEMODULATION_MODE_AUTO_DETECT = -2,
        IMAQ_QR_DEMODULATION_MODE_HISTOGRAM = 0,
        IMAQ_QR_DEMODULATION_MODE_LOCAL_CONTRAST = 1,
        IMAQ_QR_DEMODULATION_MODE_COMBINED = 2,
        IMAQ_QR_DEMODULATION_MODE_ALL = 3,
        IMAQ_QR_DEMODULATION_MODE_SIZE_GUARD = 4294967295,
    }

    public enum ContrastMode 
    {
        IMAQ_ORIGINAL_CONTRAST = 0,
        IMAQ_REVERSED_CONTRAST = 1,
        IMAQ_BOTH_CONTRASTS = 2,
    }

    public enum QRPolarities : long
    {
        IMAQ_QR_POLARITY_AUTO_DETECT = -2,
        IMAQ_QR_POLARITY_BLACK_ON_WHITE = 0,
        IMAQ_QR_POLARITY_WHITE_ON_BLACK = 1,
        IMAQ_QR_POLARITY_MODE_SIZE_GUARD = 4294967295,
    }

    public enum QRRotationMode 
    {
        IMAQ_QR_ROTATION_MODE_UNLIMITED = 0,
        IMAQ_QR_ROTATION_MODE_0_DEGREES = 1,
        IMAQ_QR_ROTATION_MODE_90_DEGREES = 2,
        IMAQ_QR_ROTATION_MODE_180_DEGREES = 3,
        IMAQ_QR_ROTATION_MODE_270_DEGREES = 4,
        IMAQ_QR_ROTATION_MODE_SIZE_GUARD = -1,
    }

    public enum QRGradingMode 
    {
        IMAQ_QR_NO_GRADING = 0,
        IMAQ_QR_GRADING_MODE_SIZE_GUARD = -1,
    }

    public enum StraightEdgeSearchMode 
    {
        IMAQ_USE_FIRST_RAKE_EDGES = 0,
        IMAQ_USE_BEST_RAKE_EDGES = 1,
        IMAQ_USE_BEST_HOUGH_LINE = 2,
        IMAQ_USE_FIRST_PROJECTION_EDGE = 3,
        IMAQ_USE_BEST_PROJECTION_EDGE = 4,
        IMAQ_STRAIGHT_EDGE_SEARCH_SIZE_GUARD = -1,
    }

    public enum SearchDirection 
    {
        IMAQ_SEARCH_DIRECTION_LEFT_TO_RIGHT = 0,
        IMAQ_SEARCH_DIRECTION_RIGHT_TO_LEFT = 1,
        IMAQ_SEARCH_DIRECTION_TOP_TO_BOTTOM = 2,
        IMAQ_SEARCH_DIRECTION_BOTTOM_TO_TOP = 3,
        IMAQ_SEARCH_DIRECTION_SIZE_GUARD = -1,
    }

    public enum QRStreamMode 
    {
        IMAQ_QR_MODE_NUMERIC = 0,
        IMAQ_QR_MODE_ALPHANUMERIC = 1,
        IMAQ_QR_MODE_RAW_BYTE = 2,
        IMAQ_QR_MODE_EAN128_TOKEN = 3,
        IMAQ_QR_MODE_EAN128_DATA = 4,
        IMAQ_QR_MODE_ECI = 5,
        IMAQ_QR_MODE_KANJI = 6,
        IMAQ_QR_MODE_SIZE_GUARD = -1,
    }

    public enum ParticleClassifierType 
    {
        IMAQ_PARTICLE_LARGEST = 0,
        IMAQ_PARTICLE_ALL = 1,
        IMAQ_PARTICLE_CLASSIFIER_TYPE_SIZE_GUARD = -1,
    }

    public enum QRCellSampleSize : long
    {
        IMAQ_QR_CELL_SAMPLE_SIZE_AUTO_DETECT = -2,
        IMAQ_QR_CELL_SAMPLE_SIZE1X1 = 1,
        IMAQ_QR_CELL_SAMPLE_SIZE2X2 = 2,
        IMAQ_QR_CELL_SAMPLE_SIZE3X3 = 3,
        IMAQ_QR_CELL_SAMPLE_SIZE4X4 = 4,
        IMAQ_QR_CELL_SAMPLE_SIZE5X5 = 5,
        IMAQ_QR_CELL_SAMPLE_SIZE6X6 = 6,
        IMAQ_QR_CELL_SAMPLE_SIZE7X7 = 7,
        IMAQ_QR_CELL_SAMPLE_TYPE_SIZE_GUARD = 4294967295,
    }

    public enum RakeProcessType 
    {
        IMAQ_GET_FIRST_EDGES = 0,
        IMAQ_GET_FIRST_AND_LAST_EDGES = 1,
        IMAQ_GET_ALL_EDGES = 2,
        IMAQ_GET_BEST_EDGES = 3,
        IMAQ_RAKE_PROCESS_TYPE_SIZE_GUARD = -1,
    }

    public enum GeometricSetupDataItem 
    {
        IMAQ_CURVE_EXTRACTION_MODE = 0,
        IMAQ_CURVE_EDGE_THRSHOLD = 1,
        IMAQ_CURVE_EDGE_FILTER = 2,
        IMAQ_MINIMUM_CURVE_LENGTH = 3,
        IMAQ_CURVE_ROW_SEARCH_STEP_SIZE = 4,
        IMAQ_CURVE_COL_SEARCH_STEP_SIZE = 5,
        IMAQ_CURVE_MAX_END_POINT_GAP = 6,
        IMAQ_EXTRACT_CLOSED_CURVES = 7,
        IMAQ_ENABLE_SUBPIXEL_CURVE_EXTRACTION = 8,
        IMAQ_ENABLE_CORRELATION_SCORE = 9,
        IMAQ_ENABLE_SUBPIXEL_ACCURACY = 10,
        IMAQ_SUBPIXEL_ITERATIONS = 11,
        IMAQ_SUBPIXEL_TOLERANCE = 12,
        IMAQ_INITIAL_MATCH_LIST_LENGTH = 13,
        IMAQ_ENABLE_TARGET_TEMPLATE_CURVESCORE = 14,
        IMAQ_MINIMUM_MATCH_SEPARATION_DISTANCE = 15,
        IMAQ_MINIMUM_MATCH_SEPARATION_ANGLE = 16,
        IMAQ_MINIMUM_MATCH_SEPARATION_SCALE = 17,
        IMAQ_MAXIMUM_MATCH_OVERLAP = 18,
        IMAQ_ENABLE_COARSE_RESULT = 19,
        IMAQ_ENABLE_CALIBRATION_SUPPORT = 20,
        IMAQ_ENABLE_CONTRAST_REVERSAL = 21,
        IMAQ_SEARCH_STRATEGY = 22,
        IMAQ_REFINEMENT_MATCH_FACTOR = 23,
        IMAQ_SUBPIXEL_MATCH_FACTOR = 24,
        IMAQ_MAX_REFINEMENT_ITERATIONS = 25,
    }

    public enum DistortionModel : int
    {
        IMAQ_POLYNOMIAL_MODEL = 0,
        IMAQ_DIVISION_MODEL = 1,
        IMAQ_NO_DISTORTION_MODEL = -1,
    }

    public enum CalibrationThumbnailType 
    {
        IMAQ_CAMARA_MODEL_TYPE = 0,
        IMAQ_PERSPECTIVE_TYPE = 1,
        IMAQ_MICRO_PLANE_TYPE = 2,
        IMAQ_CALIBRATION_THUMBNAIL_TYPE_SIZE_GUARD = -1,
    }

    public enum SettingType 
    {
        IMAQ_ROTATION_ANGLE_RANGE = 0,
        IMAQ_SCALE_RANGE = 1,
        IMAQ_OCCLUSION_RANGE = 2,
        IMAQ_SETTING_TYPE_SIZE_GUARD = -1,
    }

    public enum SegmentationDistanceLevel 
    {
        IMAQ_SEGMENTATION_LEVEL_CONSERVATIVE = 0,
        IMAQ_SEGMENTATION_LEVEL_AGGRESSIVE = 1,
        IMAQ_SEGMENTATION_LEVEL_SIZE_GUARD = -1,
    }

    public enum ExtractContourSelection 
    {
        IMAQ_CLOSEST = 0,
        IMAQ_LONGEST = 1,
        IMAQ_STRONGEST = 2,
        IMAQ_EXTRACT_CONTOUR_SELECTION_SIZE_GUARD = -1,
    }

    public enum FindTransformMode 
    {
        IMAQ_FIND_REFERENCE = 0,
        IMAQ_UPDATE_TRANSFORM = 1,
        IMAQ_FIND_TRANSFORM_MODE_SIZE_GUARD = -1,
    }

    public enum ExtractContourDirection 
    {
        IMAQ_RECT_LEFT_RIGHT = 0,
        IMAQ_RECT_RIGHT_LEFT = 1,
        IMAQ_RECT_TOP_BOTTOM = 2,
        IMAQ_RECT_BOTTOM_TOP = 3,
        IMAQ_ANNULUS_INNER_OUTER = 4,
        IMAQ_ANNULUS_OUTER_INNER = 5,
        IMAQ_ANNULUS_START_STOP = 6,
        IMAQ_ANNULUS_STOP_START = 7,
        IMAQ_EXTRACT_CONTOUR_DIRECTION_SIZE_GUARD = -1,
    }

    public enum EdgePolaritySearchMode 
    {
        IMAQ_SEARCH_FOR_ALL_EDGES = 0,
        IMAQ_SEARCH_FOR_RISING_EDGES = 1,
        IMAQ_SEARCH_FOR_FALLING_EDGES = 2,
        IMAQ_EDGE_POLARITY_MODE_SIZE_GUARD = -1,
    }

    public enum Connectivity 
    {
        IMAQ_FOUR_CONNECTED = 0,
        IMAQ_EIGHT_CONNECTED = 1,
        IMAQ_CONNECTIVITY_SIZE_GUARD = -1,
    }

    public enum MorphologyReconstructOperation 
    {
        IMAQ_DILATE_RECONSTRUCT = 0,
        IMAQ_ERODE_RECONSTRUCT = 1,
        IMAQ_MORPHOLOGY_RECONSTRUCT_OPERATION_SIZE_GUARD = -1,
    }

    public enum WaveletType 
    {
        IMAQ_DB02 = 0,
        IMAQ_DB03 = 1,
        IMAQ_DB04 = 2,
        IMAQ_DB05 = 3,
        IMAQ_DB06 = 4,
        IMAQ_DB07 = 5,
        IMAQ_DB08 = 6,
        IMAQ_DB09 = 7,
        IMAQ_DB10 = 8,
        IMAQ_DB11 = 9,
        IMAQ_DB12 = 10,
        IMAQ_DB13 = 11,
        IMAQ_DB14 = 12,
        IMAQ_HAAR = 13,
        IMAQ_BIOR1_3 = 14,
        IMAQ_BIOR1_5 = 15,
        IMAQ_BIOR2_2 = 16,
        IMAQ_BIOR2_4 = 17,
        IMAQ_BIOR2_6 = 18,
        IMAQ_BIOR2_8 = 19,
        IMAQ_BIOR3_1 = 20,
        IMAQ_BIOR3_3 = 21,
        IMAQ_BIOR3_5 = 22,
        IMAQ_BIOR3_7 = 23,
        IMAQ_BIOR3_9 = 24,
        IMAQ_BIOR4_4 = 25,
        IMAQ_COIF1 = 26,
        IMAQ_COIF2 = 27,
        IMAQ_COIF3 = 28,
        IMAQ_COIF4 = 29,
        IMAQ_COIF5 = 30,
        IMAQ_SYM2 = 31,
        IMAQ_SYM3 = 32,
        IMAQ_SYM4 = 33,
        IMAQ_SYM5 = 34,
        IMAQ_SYM6 = 35,
        IMAQ_SYM7 = 36,
        IMAQ_SYM8 = 37,
        IMAQ_BIOR5_5 = 38,
        IMAQ_BIOR6_8 = 39,
        IMAQ_WAVE_TYPE_SIZE_GUARD = -1,
    }

    public enum ParticleClassifierThresholdType 
    {
        IMAQ_THRESHOLD_MANUAL = 0,
        IMAQ_THRESHOLD_AUTO = 1,
        IMAQ_THRESHOLD_LOCAL = 2,
    }

    public enum MeasureParticlesCalibrationMode 
    {
        IMAQ_CALIBRATION_MODE_PIXEL = 0,
        IMAQ_CALIBRATION_MODE_CALIBRATED = 1,
        IMAQ_CALIBRATION_MODE_BOTH = 2,
        IMAQ_MEASURE_PARTICLES_CALIBRATION_MODE_SIZE_GUARD = -1,
    }

    public enum GeometricMatchingSearchStrategy 
    {
        IMAQ_GEOMETRIC_MATCHING_CONSERVATIVE = 0,
        IMAQ_GEOMETRIC_MATCHING_BALANCED = 1,
        IMAQ_GEOMETRIC_MATCHING_AGGRESSIVE = 2,
        IMAQ_GEOMETRIC_MATCHING_SEARCH_STRATEGY_SIZE_GUARD = -1,
    }

    public enum ColorClassificationResolution 
    {
        IMAQ_CLASSIFIER_LOW_RESOLUTION = 0,
        IMAQ_CLASSIFIER_MEDIUM_RESOLUTION = 1,
        IMAQ_CLASSIFIER_HIGH_RESOLUTION = 2,
        IMAQ_CLASSIFIER_RESOLUTION_SIZE_GUARD = -1,
    }

    public enum ConnectionConstraintType 
    {
        IMAQ_DISTANCE_CONSTRAINT = 0,
        IMAQ_ANGLE_CONSTRAINT = 1,
        IMAQ_CONNECTIVITY_CONSTRAINT = 2,
        IMAQ_GRADIENT_CONSTRAINT = 3,
        IMAQ_NUM_CONNECTION_CONSTRAINT_TYPES = 4,
        IMAQ_CONNECTION_CONSTRAINT_SIZE_GUARD = -1,
    }

    public enum Barcode2DContrast 
    {
        IMAQ_ALL_BARCODE_2D_CONTRASTS = 0,
        IMAQ_BLACK_ON_WHITE_BARCODE_2D = 1,
        IMAQ_WHITE_ON_BLACK_BARCODE_2D = 2,
        IMAQ_BARCODE_2D_CONTRAST_SIZE_GUARD = -1,
    }

    public enum QRModelType 
    {
        IMAQ_QR_MODELTYPE_AUTO_DETECT = 0,
        IMAQ_QR_MODELTYPE_MICRO = 1,
        IMAQ_QR_MODELTYPE_MODEL1 = 2,
        IMAQ_QR_MODELTYPE_MODEL2 = 3,
        IMAQ_QR_MODEL_TYPE_SIZE_GUARD = -1,
    }

    public enum WindowBackgroundFillStyle 
    {
        IMAQ_FILL_STYLE_SOLID = 0,
        IMAQ_FILL_STYLE_HATCH = 2,
        IMAQ_FILL_STYLE_DEFAULT = 3,
        IMAQ_FILL_STYLE_SIZE_GUARD = -1,
    }

    public enum ExtractionMode 
    {
        IMAQ_NORMAL_IMAGE = 0,
        IMAQ_UNIFORM_REGIONS = 1,
        IMAQ_EXTRACTION_MODE_SIZE_GUARD = -1,
    }

    public enum EdgeFilterSize 
    {
        IMAQ_FINE = 0,
        IMAQ_NORMAL = 1,
        IMAQ_CONTOUR_TRACING = 2,
        IMAQ_EDGE_FILTER_SIZE_SIZE_GUARD = -1,
    }

    public enum Barcode2DSearchMode 
    {
        IMAQ_SEARCH_MULTIPLE = 0,
        IMAQ_SEARCH_SINGLE_CONSERVATIVE = 1,
        IMAQ_SEARCH_SINGLE_AGGRESSIVE = 2,
        IMAQ_BARCODE_2D_SEARCH_MODE_SIZE_GUARD = -1,
    }

    public enum DataMatrixSubtype 
    {
        IMAQ_ALL_DATA_MATRIX_SUBTYPES = 0,
        IMAQ_DATA_MATRIX_SUBTYPES_ECC_000_ECC_140 = 1,
        IMAQ_DATA_MATRIX_SUBTYPE_ECC_200 = 2,
        IMAQ_DATA_MATRIX_SUBTYPE_SIZE_GUARD = -1,
    }

    public enum FeatureType 
    {
        IMAQ_NOT_FOUND_FEATURE = 0,
        IMAQ_CIRCLE_FEATURE = 1,
        IMAQ_ELLIPSE_FEATURE = 2,
        IMAQ_CONST_CURVE_FEATURE = 3,
        IMAQ_RECTANGLE_FEATURE = 4,
        IMAQ_LEG_FEATURE = 5,
        IMAQ_CORNER_FEATURE = 6,
        IMAQ_PARALLEL_LINE_PAIR_FEATURE = 7,
        IMAQ_PAIR_OF_PARALLEL_LINE_PAIRS_FEATURE = 8,
        IMAQ_LINE_FEATURE = 9,
        IMAQ_CLOSED_CURVE_FEATURE = 10,
        IMAQ_FEATURE_TYPE_SIZE_GUARD = -1,
    }

    public enum Barcode2DCellShape 
    {
        IMAQ_SQUARE_CELLS = 0,
        IMAQ_ROUND_CELLS = 1,
        IMAQ_BARCODE_2D_CELL_SHAPE_SIZE_GUARD = -1,
    }

    public enum LocalThresholdMethod 
    {
        IMAQ_NIBLACK = 0,
        IMAQ_BACKGROUND_CORRECTION = 1,
        IMAQ_LOCAL_THRESHOLD_METHOD_SIZE_GUARD = -1,
    }

    public enum Barcode2DType 
    {
        IMAQ_PDF417 = 0,
        IMAQ_DATA_MATRIX_ECC_000 = 1,
        IMAQ_DATA_MATRIX_ECC_050 = 2,
        IMAQ_DATA_MATRIX_ECC_080 = 3,
        IMAQ_DATA_MATRIX_ECC_100 = 4,
        IMAQ_DATA_MATRIX_ECC_140 = 5,
        IMAQ_DATA_MATRIX_ECC_200 = 6,
        IMAQ_BARCODE_2D_TYPE_SIZE_GUARD = -1,
    }

    public enum ClassifierEngineType 
    {
        IMAQ_ENGINE_NONE = 0,
        IMAQ_ENGINE_NEAREST_NEIGHBOR = 1,
        IMAQ_ENGINE_SUPPORT_VECTOR_MACHINE = 2,
        IMAQ_CLASSIFIER_ENGINE_TYPE_SIZE_GUARD = -1,
    }

    public enum ClassifierType 
    {
        IMAQ_CLASSIFIER_CUSTOM = 0,
        IMAQ_CLASSIFIER_PARTICLE = 1,
        IMAQ_CLASSIFIER_COLOR = 2,
        IMAQ_CLASSIFIER_TEXTURE = 3,
        IMAQ_CLASSIFIER_TYPE_SIZE_GUARD = -1,
    }

    public enum ParticleType 
    {
        IMAQ_PARTICLE_BRIGHT = 0,
        IMAQ_PARTICLE_DARK = 1,
        IMAQ_PARTICLE_TYPE_SIZE_GUARD = -1,
    }

    public enum VisionInfoType2 
    {
        IMAQ_VISIONINFO_CALIBRATION = 1,
        IMAQ_VISIONINFO_OVERLAY = 2,
        IMAQ_VISIONINFO_GRAYTEMPLATE = 4,
        IMAQ_VISIONINFO_COLORTEMPLATE = 8,
        IMAQ_VISIONINFO_GEOMETRICTEMPLATE = 16,
        IMAQ_VISIONINFO_CUSTOMDATA = 32,
        IMAQ_VISIONINFO_GOLDENTEMPLATE = 64,
        IMAQ_VISIONINFO_GEOMETRICTEMPLATE2 = 128,
        IMAQ_VISIONINFO_ALL = -1,
    }

    public enum ReadClassifierFileMode 
    {
        IMAQ_CLASSIFIER_READ_ALL = 0,
        IMAQ_CLASSIFIER_READ_SAMPLES = 1,
        IMAQ_CLASSIFIER_READ_PROPERTIES = 2,
        IMAQ_READ_CLASSIFIER_FILE_MODES_SIZE_GUARD = -1,
    }

    public enum WriteClassifierFileMode 
    {
        IMAQ_CLASSIFIER_WRITE_ALL = 0,
        IMAQ_CLASSIFIER_WRITE_CLASSIFY_ONLY = 1,
        IMAQ_WRITE_CLASSIFIER_FILE_MODES_SIZE_GUARD = -1,
    }

    public enum Barcode2DShape 
    {
        IMAQ_SQUARE_BARCODE_2D = 0,
        IMAQ_RECTANGULAR_BARCODE_2D = 1,
        IMAQ_BARCODE_2D_SHAPE_SIZE_GUARD = -1,
    }

    public enum DataMatrixRotationMode 
    {
        IMAQ_UNLIMITED_ROTATION = 0,
        IMAQ_0_DEGREES = 1,
        IMAQ_90_DEGREES = 2,
        IMAQ_180_DEGREES = 3,
        IMAQ_270_DEGREES = 4,
        IMAQ_DATA_MATRIX_ROTATION_MODE_SIZE_GUARD = -1,
    }

    public enum AIMGrade 
    {
        IMAQ_AIM_GRADE_F = 0,
        IMAQ_AIM_GRADE_D = 1,
        IMAQ_AIM_GRADE_C = 2,
        IMAQ_AIM_GRADE_B = 3,
        IMAQ_AIM_GRADE_A = 4,
        IMAQ_DATA_MATRIX_AIM_GRADE_SIZE_GUARD = -1,
    }

    public enum DataMatrixCellFillMode : long
    {
        IMAQ_AUTO_DETECT_CELL_FILL_MODE = -2,
        IMAQ_LOW_FILL = 0,
        IMAQ_NORMAL_FILL = 1,
        IMAQ_DATA_MATRIX_CELL_FILL_MODE_SIZE_GUARD = 4294967295,
    }

    public enum DataMatrixDemodulationMode : long
    {
        IMAQ_AUTO_DETECT_DEMODULATION_MODE = -2,
        IMAQ_HISTOGRAM = 0,
        IMAQ_LOCAL_CONTRAST = 1,
        IMAQ_COMBINED = 2,
        IMAQ_ALL_DEMODULATION_MODES = 3,
        IMAQ_DATA_MATRIX_DEMODULATION_MODE_SIZE_GUARD = 4294967295,
    }

    public enum DataMatrixECC : long
    {
        IMAQ_AUTO_DETECT_ECC = -2,
        IMAQ_ECC_000 = 0,
        IMAQ_ECC_050 = 50,
        IMAQ_ECC_080 = 80,
        IMAQ_ECC_100 = 100,
        IMAQ_ECC_140 = 140,
        IMAQ_ECC_000_140 = 190,
        IMAQ_ECC_200 = 200,
        IMAQ_DATA_MATRIX_ECC_SIZE_GUARD = 4294967295,
    }

    public enum DataMatrixPolarity : long
    {
        IMAQ_AUTO_DETECT_POLARITY = -2,
        IMAQ_BLACK_DATA_ON_WHITE_BACKGROUND = 0,
        IMAQ_WHITE_DATA_ON_BLACK_BACKGROUND = 1,
        IMAQ_DATA_MATRIX_POLARITY_SIZE_GUARD = 4294967295,
    }

    public enum DataMatrixCellFilterMode : long
    {
        IMAQ_AUTO_DETECT_CELL_FILTER_MODE = -2,
        IMAQ_AVERAGE_FILTER = 0,
        IMAQ_MEDIAN_FILTER = 1,
        IMAQ_CENTRAL_AVERAGE_FILTER = 2,
        IMAQ_HIGH_AVERAGE_FILTER = 3,
        IMAQ_LOW_AVERAGE_FILTER = 4,
        IMAQ_VERY_HIGH_AVERAGE_FILTER = 5,
        IMAQ_VERY_LOW_AVERAGE_FILTER = 6,
        IMAQ_ALL_CELL_FILTERS = 8,
        IMAQ_DATA_MATRIX_CELL_FILTER_MODE_SIZE_GUARD = 4294967295,
    }

    public enum WindowBackgroundHatchStyle 
    {
        IMAQ_HATCH_STYLE_HORIZONTAL = 0,
        IMAQ_HATCH_STYLE_VERTICAL = 1,
        IMAQ_HATCH_STYLE_FORWARD_DIAGONAL = 2,
        IMAQ_HATCH_STYLE_BACKWARD_DIAGONAL = 3,
        IMAQ_HATCH_STYLE_CROSS = 4,
        IMAQ_HATCH_STYLE_CROSS_HATCH = 5,
        IMAQ_HATCH_STYLE_SIZE_GUARD = -1,
    }

    public enum DataMatrixMirrorMode : long
    {
        IMAQ_AUTO_DETECT_MIRROR = -2,
        IMAQ_APPEARS_NORMAL = 0,
        IMAQ_APPEARS_MIRRORED = 1,
        IMAQ_DATA_MATRIX_MIRROR_MODE_SIZE_GUARD = 4294967295,
    }

    public enum CalibrationMode2 
    {
        IMAQ_PERSPECTIVE_MODE = 0,
        IMAQ_MICROPLANE_MODE = 1,
        IMAQ_SIMPLE_CALIBRATION_MODE = 2,
        IMAQ_CORRECTED_IMAGE_MODE = 3,
        IMAQ_NO_CALIBRATION_MODE = 4,
        IMAQ_CALIBRATION_MODE2_SIZE_GUARD = -1,
    }

    public enum DataMatrixGradingMode 
    {
        IMAQ_NO_GRADING = 0,
        IMAQ_PREPARE_FOR_AIM = 1,
        IMAQ_DATA_MATRIX_GRADING_MODE_SIZE_GUARD = -1,
    }

    public enum WaveletTransformMode 
    {
        IMAQ_WAVELET_TRANSFORM_INTEGER = 0,
        IMAQ_WAVELET_TRANSFORM_FLOATING_POINT = 1,
        IMAQ_WAVELET_TRANSFORM_MODE_SIZE_GUARD = -1,
    }

    public enum NormalizationMethod 
    {
        IMAQ_NORMALIZATION_NONE = 0,
        IMAQ_NORMALIZATION_HISTOGRAM_MATCHING = 1,
        IMAQ_NORMALIZATION_AVERAGE_MATCHING = 2,
        IMAQ_NORMALIZATION_SIZE_GUARD = -1,
    }

    public enum RegistrationMethod 
    {
        IMAQ_REGISTRATION_NONE = 0,
        IMAQ_REGISTRATION_PERSPECTIVE = 1,
        IMAQ_REGISTRATION_SIZE_GUARD = -1,
    }

    public enum LinearAveragesMode 
    {
        IMAQ_COLUMN_AVERAGES = 1,
        IMAQ_ROW_AVERAGES = 2,
        IMAQ_RISING_DIAGONAL_AVERAGES = 4,
        IMAQ_FALLING_DIAGONAL_AVERAGES = 8,
        IMAQ_ALL_LINEAR_AVERAGES = 15,
        IMAQ_LINEAR_AVERAGES_MODE_SIZE_GUARD = -1,
    }

    public enum CompressionType 
    {
        IMAQ_COMPRESSION_NONE = 0,
        IMAQ_COMPRESSION_JPEG = 1,
        IMAQ_COMPRESSION_PACKED_BINARY = 2,
        IMAQ_COMPRESSION_TYPE_SIZE_GUARD = -1,
    }

    public enum FlattenType 
    {
        IMAQ_FLATTEN_IMAGE = 0,
        IMAQ_FLATTEN_IMAGE_AND_VISION_INFO = 1,
        IMAQ_FLATTEN_TYPE_SIZE_GUARD = -1,
    }

    public enum DataMatrixCellSampleSize : long
    {
        IMAQ_AUTO_DETECT_CELL_SAMPLE_SIZE = -2,
        IMAQ_1x1 = 1,
        IMAQ_2x2 = 2,
        IMAQ_3x3 = 3,
        IMAQ_4x4 = 4,
        IMAQ_5x5 = 5,
        IMAQ_6x6 = 6,
        IMAQ_7x7 = 7,
        IMAQ_DATA_MATRIX_CELL_SAMPLE_SIZE_SIZE_GUARD = 4294967295,
    }
}
