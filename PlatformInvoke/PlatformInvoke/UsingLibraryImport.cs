using System.Runtime.InteropServices;

namespace PlatformInvoke;

// In .net 7 and later, the LibraryImport attribute is used to declare
// a method that will be imported from a native library at runtime.
// This is part of the new "Library Import" feature in .NET 7 and later,
// which allows for a more modern and type-safe way to call
// native code compared to the traditional DllImport attribute.
internal partial class UsingLibraryImport
{
    // When using LibraryImport, "<AllowUnsafeBlocks>true</AllowUnsafeBlocks>" must be set in the project file.
    // The compiler should suggest this when LibraryImport is first used.
    //
    // When using LibraryImport, the compiler may give this error:
    // CS8795 "Partial method (...) must have an implementation part because it has accessibility modifiers."
    // This will go away when the project is built, since the compiler
    // will generate the implementation part when this attribute is used.
    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16, SetLastError = true)]
    private static partial int MessageBoxW(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    public void ShowMessageBox()
    {
        int hResult = MessageBoxW(IntPtr.Zero, "Example text using LibraryImport", "Example caption", 0);
        Console.WriteLine($"hResult: {hResult}");
        Helper.DisplayLastError();
    }
}
