namespace WPIHal.Natives;

public static unsafe partial class HalI2C
{
    public static void InitializeI2C(I2CPort port, out HalStatus status)
    {
        status = HalStatus.Ok;
        InitializeI2CRefShim(port, ref status);
    }
}
