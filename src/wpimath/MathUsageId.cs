namespace WPIMath;

public enum MathUsageId
{
    /** DifferentialDriveKinematics. */
    KinematicsDifferentialDrive,

    /** MecanumDriveKinematics. */
    KinematicsMecanumDrive,

    /** SwerveDriveKinematics. */
    KinematicsSwerveDrive,

    /** TrapezoidProfile. */
    TrajectoryTrapezoidProfile,

    /** LinearFilter. */
    FilterLinear,

    /** DifferentialDriveOdometry. */
    OdometryDifferentialDrive,

    /** SwerveDriveOdometry. */
    OdometrySwerveDrive,

    /** MecanumDriveOdometry. */
    OdometryMecanumDrive,

    /** PIDController. */
    ControllerPIDController2,

    /** ProfiledPIDController. */
    ControllerProfiledPIDController,
}
