using System.Net.Http.Headers;
using System.Text;

namespace Web.Services;

public class DeploymentService
{
    private readonly HttpClient _httpClient;

    public DeploymentService()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<string> Deploy(string deploymentName)
    {
        const string url = "http://localhost:8080/engine-rest/deployment/create";

        var file = await File.ReadAllBytesAsync(GetFilePath());
        var multipartFormDataContent = new MultipartFormDataContent();
        var byteArrayContent = new ByteArrayContent(file);
        
        byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        multipartFormDataContent.Add(byteArrayContent, "data", "CustomerComplains.bpmn");
        multipartFormDataContent.Add(new StringContent(deploymentName), "deployment-name");
        multipartFormDataContent.Add(new StringContent(".NET Application"), "deployment-source");

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Authorization",
            "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("demo:demo")));
        
        var response = await _httpClient.PostAsync(url, multipartFormDataContent);
        
        return response.IsSuccessStatusCode ? "Deployment started successfully." : "Deployment failed.";
    }
    
    
    private string GetFilePath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var path = Path.Combine(currentDirectory, "Resources", "CustomerComplains.bpmn");
        return path;
    }
}