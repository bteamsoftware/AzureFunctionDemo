using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ServiceLib;
using System.Threading.Tasks;


namespace FunctionDemoApp;


public class Program
{
	public static async Task Main()
	{
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
	}
}
