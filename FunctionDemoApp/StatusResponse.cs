using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Text.Json.Serialization;


namespace FunctionDemoApp;


internal sealed class StatusResponse
{
	[OpenApiProperty(Description = "The app function status", Nullable = false)]
	[JsonPropertyName("status")]
	public string Status { get; set; } = string.Empty;
}
