using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace ServiceLib;


public class DemoService : IDemoService
{
	private readonly ServiceOptions _options;
	private readonly ILogger<DemoService> _logger;


	public DemoService(IOptions<ServiceOptions> options, ILogger<DemoService> logger)
	{
		_options = options.Value;
		_logger = logger;
	}


	public async Task ProcessMessageAsync(string message)
	{
		_logger.LogInformation("Processing message '{Message}' in the demo service", message);
		
		string server = _options.UseHttps ? $"https://{_options.Url}" : $"http://{_options.Url}";
		_logger.LogInformation("Contacting server {URL} for processing", server);

		await Task.Delay(100);
	}
}
