using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceLib;


IHost host = new HostBuilder()
	.ConfigureAppConfiguration(config => {
		config.AddEnvironmentVariables();
	})
	.ConfigureFunctionsWorkerDefaults()
	.ConfigureServices(services =>
	{
		services.AddCustomServices();
	})
	.ConfigureOpenApi()
	.Build();

await host.RunAsync();
