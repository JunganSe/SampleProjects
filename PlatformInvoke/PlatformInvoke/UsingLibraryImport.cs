using System.Runtime.InteropServices;

namespace PlatformInvoke;

internal partial class UsingLibraryImport
{
    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    private static partial int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    public void ShowMessageBox()
    {
        int hResult = MessageBoxW(IntPtr.Zero, "Example text using LibraryImport", "Example caption", 0);
        Console.WriteLine($"hResult: {hResult}");
        Helper.DisplayLastError();
    }
}
