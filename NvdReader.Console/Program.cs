using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using NvdReader.Application.Interactors;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Services;
using NvdReader.Console;
using NvdReader.Domain.Options;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Infrastructure.Persistence;
using NvdReader.Infrastructure.Repository.DataSources;
using NvdReader.Infrastructure.Repository.Products;
using NvdReader.Infrastructure.Repository.Vulnerabilities;
using NvdReader.Shared.Extensions;
using Serilog;

// arguments parse
var parserResult = Parser.Default.ParseArguments<ConsoleOptions>(args);
if (parserResult.Errors.Any())
{
    return;
}

var parserResultValue = parserResult.Value ?? throw new ArgumentException("Parameter are not parsed");

// services registration
var builder = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    {
        // database
        var mongoClient = new MongoClient(parserResultValue.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(parserResultValue.DatabaseName);

        services.AddSingleton(mongoDb);
        services.AddSingleton<IMongodbContext, MongodbContext>();
        services.AddSingleton(_ => new ApiClientOptions
        {
            ApiKey = parserResultValue.ApiKey ?? string.Empty
        });

        // httpclient
        services.AddHttpClient<INvdApiClient, NvdApiClient>()
            .AddPolicyHandler(HttpPolicyExtensionHelpers.GetRetryPolicy());

        // repository
        services.AddScoped<ICveRepository, CveRepository>();
        services.AddScoped<ISourceRepository, SourceRepository>();
        services.AddScoped<ICveHistoryRepository, CveHistoryRepository>();
        services.AddScoped<ICpeRepository, CpeRepository>();
        services.AddScoped<ICpeMatchRepository, CpeMatchRepository>();

        // syncService
        services.AddScoped<INvdCveSyncService, NvdCveSyncService>();
        services.AddScoped<INvdSourceSyncService, NvdSourceSyncService>();
        services.AddScoped<INvdCveHistorySyncService, NvdCveHistorySyncService>();
        services.AddScoped<INvdCpeSyncService, NvdCpeSyncService>();
        services.AddScoped<INvdCpeMatchSyncService, NvdCpeMatchSyncService>();

        // interactor
        services.AddSingleton<INvdInteractor, NvdCveInteractor>();
        services.AddSingleton<INvdInteractor, NvdSourceInteractor>();
        services.AddSingleton<INvdInteractor, NvdCveHistoryInteractor>();
        services.AddSingleton<INvdInteractor, NvdCpeInteractor>();
        services.AddSingleton<INvdInteractor, NvdCpeMatchInteractor>();
    })
    .UseSerilog((_, services, configuration) => configuration
        //.ReadFrom.Configuration(context.Configuration)
        .MinimumLevel.Debug()
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.log", rollingInterval: RollingInterval.Day)
    )
    .Build();

// start
var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    cancellationTokenSource.CancelAsync();
    e.Cancel = true;
};

var nvdSourceInteractor = ActivatorUtilities.CreateInstance<NvdSourceInteractor>(builder.Services);
await nvdSourceInteractor.HandelAsync(cancellationTokenSource.Token);

var nvdCveInteractor = ActivatorUtilities.CreateInstance<NvdCveInteractor>(builder.Services);
await nvdCveInteractor.HandelAsync(cancellationTokenSource.Token);

var nvdCveHistoryInteractor = ActivatorUtilities.CreateInstance<NvdCveHistoryInteractor>(builder.Services);
await nvdCveHistoryInteractor.HandelAsync(cancellationTokenSource.Token);

var nvdCpeInteractor = ActivatorUtilities.CreateInstance<NvdCpeInteractor>(builder.Services);
await nvdCpeInteractor.HandelAsync(cancellationTokenSource.Token);

var nvdCpeMatchInteractor = ActivatorUtilities.CreateInstance<NvdCpeMatchInteractor>(builder.Services);
await nvdCpeMatchInteractor.HandelAsync(cancellationTokenSource.Token);

Log.ForContext<Program>().Information("Done!");
Log.CloseAndFlush();
