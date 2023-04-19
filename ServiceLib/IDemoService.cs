namespace ServiceLib;


public interface IDemoService
{
	Task ProcessMessageAsync(string message);
}
