using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Text.Json.Serialization;


namespace FunctionDemoApp;


internal sealed class SendMessageRequest
{
	[OpenApiProperty(Description = "The message to send", Nullable = false)]
	[JsonPropertyName("message")]
	public string Message { get; set; } = string.Empty;
}
