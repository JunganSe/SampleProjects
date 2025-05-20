using System.Runtime.InteropServices;

namespace PlatformInvoke;

internal class UsingDllImport
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    public void ShowMessageBox()
    {
        int hResult = MessageBox(IntPtr.Zero, "Using DllImport", "Example caption", 0);
        Console.WriteLine($"hResult: {hResult}");

        Helper.DisplayLastError();
    }
}
