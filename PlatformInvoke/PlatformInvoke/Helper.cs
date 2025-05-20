using System.Runtime.InteropServices;

namespace PlatformInvoke;

internal static class Helper
{
    public static void DisplayLastError()
    {
        // Get the last p/invoke error and display it.
        int error = Marshal.GetLastPInvokeError();
        Console.WriteLine($"Last p/invoke error: {error}");
    }
}
