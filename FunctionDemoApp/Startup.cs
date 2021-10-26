using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ServiceLib;


[assembly: FunctionsStartup(typeof(FunctionDemoApp.Startup))]


namespace FunctionDemoApp
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder.Services.AddCustomServices();
		}
	}
}
