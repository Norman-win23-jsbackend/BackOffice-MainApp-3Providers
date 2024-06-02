using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Norman.Common.Services.Interfaces;
using Nour.Common.Services.Repositories;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("host.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config["CosmosDbConnectionString"];

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<CosmosClient>((serviceProvider) =>
        {
            return new CosmosClient(connectionString);
        });
        services.AddSingleton<ICosmosDBInterface, CosmosDBRepository>();
    })
    .Build();
host.Run();
