using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using ServiceLib;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


namespace FunctionDemoApp
{
	public class AzureFunctions
	{
		private readonly IDemoService _service;


		public AzureFunctions(IDemoService service)
		{
			_service = service;
		}

		[FunctionName("GetStatus")]
		public async Task<IActionResult> GetStatusAsync(
			 [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest request,
			 ILogger logger)
		{
			logger.LogInformation("C# HTTP trigger function processed a request.");

			await Task.Delay(100);

			return new OkObjectResult("{ \"status\": \"It's Working\" }");
		}


		[FunctionName("SendMessage")]
		public async Task<IActionResult> SendMessageAsync(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
			ILogger logger)
		{
			logger.LogInformation("SendMessage triggered via HTTP");

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			Dictionary<string, string> data = JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
			data.TryGetValue("message", out string? message);

			if (string.IsNullOrWhiteSpace(message) == false)
			{
				logger.LogInformation("Function received message '{Message}' for processing", $"{message}");
				await _service.ProcessMessageAsync(message);
			}
			else
			{
				logger.LogWarning("No 'message' property was found.");
				return new BadRequestResult();
			}

			return new NoContentResult();
		}


		[FunctionName("ProcessQueueMessage")]
		public async Task ProcessMessageAsync([QueueTrigger("%QueueName%")] string message, ILogger logger)
		{
			logger.LogInformation("Processing message from message queue");
			await _service.ProcessMessageAsync(message);
		}
	}
}
