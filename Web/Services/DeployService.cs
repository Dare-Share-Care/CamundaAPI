namespace Web.Services;

public class DeployService
{
    private readonly HttpClient _httpClient;

    public DeployService()
    {
        _httpClient = new HttpClient();
    }
}