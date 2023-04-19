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
	.Build();

await host.RunAsync();
