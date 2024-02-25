namespace WPILib;

public class PrintCommand(string msg) : InstantCommand(() => Console.WriteLine(msg))
{
}
