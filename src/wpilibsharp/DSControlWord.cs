using WPIHal;

namespace WPILib;

public struct DSControlWord
{
    private ControlWord m_controlWord;

    public DSControlWord()
    {
        Refresh();
    }

    public void Refresh()
    {
        m_controlWord = DriverStation.GetControlWordFromCache();
    }

    /**
 * Gets a value indicating whether the Driver Station requires the robot to be enabled.
 *
 * @return True if the robot is enabled, false otherwise.
 */
    public readonly bool IsEnabled => m_controlWord.Enabled && m_controlWord.DsAttached;

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be disabled.
     *
     * @return True if the robot should be disabled, false otherwise.
     */
    public readonly bool IsDisabled => !IsEnabled;

    /**
     * Gets a value indicating whether the Robot is e-stopped.
     *
     * @return True if the robot is e-stopped, false otherwise.
     */
    public readonly bool IsEStopped => m_controlWord.EStop;

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be running in
     * autonomous mode.
     *
     * @return True if autonomous mode should be enabled, false otherwise.
     */
    public readonly bool IsAutonomous => m_controlWord.Autonomous;

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be running in
     * autonomous mode and enabled.
     *
     * @return True if autonomous should be set and the robot should be enabled.
     */
    public readonly bool IsAutonomousEnabled => m_controlWord.Autonomous
        && m_controlWord.Enabled
        && m_controlWord.DsAttached;

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be running in
     * operator-controlled mode.
     *
     * @return True if operator-controlled mode should be enabled, false otherwise.
     */
    public readonly bool IsTeleop => !(IsAutonomous || IsTest);

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be running in
     * operator-controller mode and enabled.
     *
     * @return True if operator-controlled mode should be set and the robot should be enabled.
     */
    public readonly bool IsTeleopEnabled => !m_controlWord.Autonomous
        && !m_controlWord.Test
        && m_controlWord.Enabled
        && m_controlWord.DsAttached;

    /**
     * Gets a value indicating whether the Driver Station requires the robot to be running in test
     * mode.
     *
     * @return True if test mode should be enabled, false otherwise.
     */
    public readonly bool IsTest => m_controlWord.Test;

    /**
     * Gets a value indicating whether the Driver Station is attached.
     *
     * @return True if Driver Station is attached, false otherwise.
     */
    public readonly bool IsDSAttached => m_controlWord.DsAttached;

    /**
     * Gets if the driver station attached to a Field Management System.
     *
     * @return true if the robot is competing on a field being controlled by a Field Management System
     */
    public readonly bool IsFMSAttached => m_controlWord.FmsAttached;
}
