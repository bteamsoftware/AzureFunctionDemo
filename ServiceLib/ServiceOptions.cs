namespace ServiceLib;


public class ServiceOptions
{
	public static readonly string Section = "Service";


	public ServiceOptions()
	{
		Url = string.Empty;
	}


	public string Url { get; set; }


	public bool UseHttps { get; set; }
}
