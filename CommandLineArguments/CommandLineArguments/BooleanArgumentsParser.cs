namespace CommandLineArguments;

internal static class BooleanArgumentsParser
{
    private const string _prefix = "-";

    // Uses reflection to get the properties of the type T,
    // and check if they are found in the provided arguments (optionally preceeded by a prefix).
    // If a property is found, it is set to true, otherwise it is set to false.
    // Also logs a summary of the parsed arguments.
    public static T Parse<T>(string[] arguments) where T : new()
    {
        Console.WriteLine("Parsing arguments...");

        var output = new T();
        var properties = typeof(T)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool));
        foreach (var property in properties)
        {
            bool value = arguments.Any(arg => string.Equals(arg, $"{_prefix}{property.Name}", StringComparison.OrdinalIgnoreCase));
            property.SetValue(output, value);
        }

        string summary = GetSummary(output);
        Console.WriteLine($"Parsed arguments: {summary}");

        return output;
    }

    public static string GetSummary<T>(T arguments)
    {
        var propertySummaries = typeof(T)
            .GetProperties()
            .Where(p => p.PropertyType == typeof(bool))
            .Select(p => $"{p.Name}: {p.GetValue(arguments)}");
        return string.Join(", ", propertySummaries);
    }
}
