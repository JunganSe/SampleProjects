namespace PlatformInvoke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dllImport = new UsingDllImport();
            dllImport.ShowMessageBox();
        }
    }
}
