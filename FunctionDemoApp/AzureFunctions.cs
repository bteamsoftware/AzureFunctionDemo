using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using ServiceLib;
using System.Net;
using System.Text.Json;


namespace FunctionDemoApp;


public class AzureFunctions
{
	private readonly IDemoService _service;
	private readonly ILogger<AzureFunctions> _logger;


	public AzureFunctions(IDemoService service, ILogger<AzureFunctions> logger)
	{
		_service = service;
		_logger = logger;
	}


	[OpenApiOperation("ListStatus", tags: new[] { "Status" }, Visibility = OpenApiVisibilityType.Important,
		Summary = "Get Status", Description = "Obtains the function app status")]
	[OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(StatusResponse),
		Summary = "Successful", Description = "The status was obtained")]
	[Function("GetStatus")]
	public async Task<HttpResponseData> GetStatusAsync(
		 [HttpTrigger(AuthorizationLevel.Function, "get", Route = "status")] HttpRequestData request)
	{
		_logger.LogInformation("C# HTTP trigger function processed a request.");

		await Task.Delay(100);

		HttpResponseData response = request.CreateResponse(HttpStatusCode.OK);
		await response.WriteAsJsonAsync(new StatusResponse() { Status = "It is Working!" });

		return response;
	}


	[OpenApiOperation("SendMessage", tags: new[] { "Message" }, Visibility = OpenApiVisibilityType.Important,
		Summary = "Send Message", Description = "Sends new message")]
	[OpenApiRequestBody("application/json", typeof(SendMessageRequest), Required = true)]
	[OpenApiResponseWithoutBody(HttpStatusCode.NoContent, Summary = "Sent", Description = "The message was sent")]
	[OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Summary = "Bad Request", Description = "The message request did not have a message property")]
	[Function("SendMessage")]
	public async Task<HttpResponseData> SendMessageAsync(
		[HttpTrigger(AuthorizationLevel.Function, "post", Route = "message")] HttpRequestData request)
	{
		_logger.LogInformation("SendMessage triggered via HTTP");

		string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
		Dictionary<string, string>? data = JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
		string? message = null;
		data?.TryGetValue("message", out message);

		if (string.IsNullOrWhiteSpace(message) == false)
		{
			_logger.LogInformation("Function received message '{Message}' for processing", $"{message}");
			await _service.ProcessMessageAsync(message);
		}
		else
		{
			_logger.LogWarning("No 'message' property was found.");
			return request.CreateResponse(HttpStatusCode.BadRequest);
		}

		return request.CreateResponse(HttpStatusCode.NoContent);
	}


	[Function("ProcessQueueMessage")]
	public async Task ProcessMessageAsync([QueueTrigger("%QueueName%")] string message)
	{
		_logger.LogInformation("Processing message from message queue");
		await _service.ProcessMessageAsync(message);
	}
}
