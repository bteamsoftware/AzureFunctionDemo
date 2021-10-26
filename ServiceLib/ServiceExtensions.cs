using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ServiceLib
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddCustomServices(this IServiceCollection services)
		{
			services.AddOptions<ServiceOptions>()
					.Configure<IConfiguration>((settings, configuration) =>
					{
						configuration.GetSection(ServiceOptions.Section).Bind(settings);
					});
			services.AddTransient<IDemoService, DemoService>();

			return services;
		}
	}
}
