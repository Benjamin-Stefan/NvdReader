using CommandLine;

namespace NvdReader.Console;

public class ConsoleOptions
{
    [Option("apiKey", HelpText = "The public rate limit (without an API key) is 5 requests in a rolling 30 second window; the rate limit with an API key is 50 requests in a rolling 30 second window. Show: https://nvd.nist.gov/developers/request-an-api-key")]
    public string? ApiKey { get; set; }

    [Option("connectionString", HelpText = "Connection to mongodb", Required = true)]
    public required string ConnectionString { get; set; }

    [Option("databaseName", HelpText = "Database name on mongodb", Required = true)]
    public required string DatabaseName { get; set; }
}