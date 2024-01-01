using Microsoft.FeatureManagement;
using MongoDB.Driver;
using NvdReader.Api.BackgroundTasks;
using NvdReader.Api.Endpoints;
using NvdReader.Application.Interactors;
using NvdReader.Application.Interfaces;
using NvdReader.Application.Services;
using NvdReader.Domain.Options;
using NvdReader.Infrastructure.ExternalClients.Nvd;
using NvdReader.Infrastructure.Interfaces;
using NvdReader.Infrastructure.Persistence;
using NvdReader.Infrastructure.Repository.DataSources;
using NvdReader.Infrastructure.Repository.Products;
using NvdReader.Infrastructure.Repository.Vulnerabilities;
using NvdReader.Shared.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services));

// asp.net core features
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureFlags"));
builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisCache");
});
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policyBuilder => policyBuilder.Cache());
    options.AddBasePolicy(policyBuilder => policyBuilder.Expire(TimeSpan.FromMinutes(10)));
});

// database
var mongoClient = new MongoClient(builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value);
var mongoDb = mongoClient.GetDatabase(builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value);

builder.Services.AddSingleton(mongoDb);
builder.Services.AddSingleton<IMongodbContext, MongodbContext>();

// options
builder.Services.AddSingleton(_ => new ApiClientOptions
{
    ApiKey = builder.Configuration.GetSection("NvdApi:ApiKey").Value ?? string.Empty
});

// httpclient
builder.Services.AddHttpClient<INvdApiClient, NvdApiClient>()
    .AddPolicyHandler(HttpPolicyExtensionHelpers.GetRetryPolicy());

// repository
builder.Services.AddScoped<ICveRepository, CveRepository>();
builder.Services.AddScoped<ISourceRepository, SourceRepository>();
builder.Services.AddScoped<ICveHistoryRepository, CveHistoryRepository>();
builder.Services.AddScoped<ICpeRepository, CpeRepository>();
builder.Services.AddScoped<ICpeMatchRepository, CpeMatchRepository>();

// syncService
builder.Services.AddScoped<INvdCveSyncService, NvdCveSyncService>();
builder.Services.AddScoped<INvdSourceSyncService, NvdSourceSyncService>();
builder.Services.AddScoped<INvdCveHistorySyncService, NvdCveHistorySyncService>();
builder.Services.AddScoped<INvdCpeSyncService, NvdCpeSyncService>();
builder.Services.AddScoped<INvdCpeMatchSyncService, NvdCpeMatchSyncService>();

// services
builder.Services.AddScoped<ICveService, CveService>();
builder.Services.AddScoped<ISourceService, SourceService>();
builder.Services.AddScoped<ICpeService, CpeService>();

// interactor
builder.Services.AddTransient<NvdCveInteractor>();
builder.Services.AddTransient<NvdSourceInteractor>();
builder.Services.AddTransient<NvdCveHistoryInteractor>();
builder.Services.AddTransient<NvdCpeInteractor>();
builder.Services.AddTransient<NvdCpeMatchInteractor>();

// background tasks
builder.Services.AddHostedService<NvdBackgroundTask>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseOutputCache();

// endpoints
app.UseCpeEndpoints();
app.UseCveEndpoints();
app.UseSourceEndpoints();

app.Run();
