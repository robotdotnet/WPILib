namespace CodeHelpers.Test;

public static class TestHelpers
{
    public static string NormalizeLineEndings(this string input)
    {
        //Normalize to fully \n
        input = input.Replace("\r\n", "\n");
        if (OperatingSystem.IsWindows())
        {
            // If windows, go to /r/n
            input = input.Replace("\n", "\r\n");
        }
        return input;
    }
}
