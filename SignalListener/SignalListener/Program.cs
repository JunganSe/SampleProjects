namespace SignalListener;

internal class Program
{
    // Run the bat file to send a signal to the listener.
    static void Main(string[] args)
    {
        var listener = new SampleListener();
        var callback = new Action(() =>
        {
            Console.WriteLine("Signal received!");
        });

        listener.Start("SampleListener.SampleCommand", callback);

        Console.ReadKey();
    }
}
