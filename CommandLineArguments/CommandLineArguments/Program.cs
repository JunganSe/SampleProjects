namespace CommandLineArguments;

internal class Program
{
    static void Main(string[] args)
    {
        var arguments = BooleanArgumentsParser.Parse<SampleArguments>(args);
    }
}
