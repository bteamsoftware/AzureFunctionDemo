using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;


namespace FunctionDemoApp;


internal sealed class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
	public override OpenApiInfo Info { get; set; } = new()
	{
		Title = "Sample Azure Function API",
		Description = "Show how to implement an API with Azure Functions",
		Version = "1.0.0",
		Contact = new OpenApiContact()
		{
			Name = "Joe Schmoe",
			Email = "joe.schmoe@contoso.com",
			Url = new Uri("https://www.contoso.com")
		},
		License = new OpenApiLicense()
		{
			Name = "MIT",
			Url = new Uri("http://opensource.org/licenses/MIT")
		}
	};


	public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}
