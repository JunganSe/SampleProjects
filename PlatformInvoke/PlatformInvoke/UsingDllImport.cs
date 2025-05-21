using System.Runtime.InteropServices;

namespace PlatformInvoke;

internal class UsingDllImport
{
    // When using DllImport, the method is resolved at runtime.
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    public void ShowMessageBox()
    {
        Console.WriteLine("Using DllImport to open a message box...");
        int hResult = MessageBox(IntPtr.Zero, "Example text using DllImport", "Example caption", 0);
        Console.WriteLine($"MessageBox hResult: {hResult}");
        Helper.DisplayLastError();
    }
}
