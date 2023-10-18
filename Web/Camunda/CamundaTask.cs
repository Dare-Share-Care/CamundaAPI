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
        var url = "http://localhost:8080/engine-rest/task";
        var response = await _httpClient.GetAsync(url);
        var result = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<TaskDto>>(result);

        return tasks ?? throw new InvalidOperationException();
    }

    public async Task<List<ExternalTaskDto>> GetExternalTasks()
    {
        var url = "http://localhost:8080/engine-rest/external-task";
        var response = await _httpClient.GetAsync(url);
        var result = await response.Content.ReadAsStringAsync();
        var tasks = JsonSerializer.Deserialize<List<ExternalTaskDto>>(result);
        
        return tasks ?? throw new InvalidOperationException();
    }

    
    // public async Task<string> CompleteExternalTask(string id, CompleteTaskDto dto)
    // {
    //     var url = $"http://localhost:8080/engine-rest/external-task/{id}/complete";
    //     var dtoJson = JsonSerializer.Serialize(dto);
    //     var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
    //     var response = await _httpClient.PostAsync(url, content);
    //     return response.IsSuccessStatusCode ? "Task completed successfully." : "Task failed to complete.";
    // }
    
    public async Task<string> CompleteExternalTask(string id,  CompleteExternalTaskDto dto)
    {
        
        //Lock task worker
        await FetchAndLockExternalTask(dto.Topic);
        
        var url = $"http://localhost:8080/engine-rest/external-task/{id}/complete";
        var dtoJson = JsonSerializer.Serialize(dto);
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        
        //Unlock task worker
        await UnlockExternalTask(id);
        
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }

    private async Task FetchAndLockExternalTask(TopicDto topicDto)
    {
        var dto = new FetchAndLockDto()
        {
            WorkerId = "C#Worker",
            MaxTasks = 1,
            Topics = new List<TopicDto>()
        };
        
        dto.Topics.Add(topicDto);
        
        var url = "http://localhost:8080/engine-rest/external-task/fetchAndLock";
        var dtoJson = JsonSerializer.Serialize(dto);
        var content = new StringContent(dtoJson, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        await response.Content.ReadAsStringAsync();
    }

    private async Task UnlockExternalTask(string id)
    {
        var url = $"http://localhost:8080/engine-rest/external-task/{id}/unlock";
        var response = await _httpClient.PostAsync(url, null);
        await response.Content.ReadAsStringAsync();
    }
}