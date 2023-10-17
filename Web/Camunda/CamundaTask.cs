using System.Text;
using System.Text.Json;
using Web.Models.Dtos;

namespace Web.Camunda;

public class CamundaTask
{
    private readonly HttpClient _httpClient;

    public CamundaTask()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<string> CompleteTask(string id, CompleteTaskDto dto)
    {
        var url = $"http://localhost:8080/engine-rest/task/{id}/complete";
        var dtoJson = JsonSerializer.Serialize(dto);
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        return response.IsSuccessStatusCode ? "Task completed successfully." : "Task failed to complete.";
    }
    
    public async Task<List<TaskDto>> GetTasks()
    {
        var url = $"http://localhost:8080/engine-rest/task";
        var response = await _httpClient.GetAsync(url);
        var result = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskDto>>(result);

        return tasks ?? throw new InvalidOperationException();
    }
}