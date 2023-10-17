using System.Text;
using System.Text.Json;
using Web.Models.Dtos;

namespace Web.Services;

public class CamundaProcess
{
    private readonly HttpClient _httpClient;
    
    public CamundaProcess()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<string> StartCamundaProcess(string processKey)
    {
        var url = $"http://localhost:8080/engine-rest/process-definition/key/{processKey}/start";
        var dto = new ProcessDto();
        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
    
        var response = await _httpClient.PostAsync(url, content);
    
        return response.IsSuccessStatusCode ? "Process started successfully." : "Process failed to start.";
    }
    
    public async Task<List<ProcessDto>> GetProcesses()
    {
        const string url = $"http://localhost:8080/engine-rest/process-instance";
        var response = await _httpClient.GetAsync(url);
        var result = await response.Content.ReadAsStringAsync();
        var processes = JsonSerializer.Deserialize<List<ProcessDto>>(result);
        return processes ?? throw new InvalidOperationException();
    }
}