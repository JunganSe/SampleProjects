namespace CommandLineArguments;

internal class Program
{
    // For debugging, the file "Properties/launchSettings.json" is used to provide 'args' to the Program.Main() method.
    // This file can be generated from the project properties --> debug section.

    // TODO: Handle different types of arguments, not just boolean.

    static void Main(string[] args)
    {
        var arguments = BooleanArgumentsParser.Parse<SampleArguments>(args);
    }
}
