using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class FetchAndLockDto 
{
    [JsonPropertyName("workerId")]
    public string WorkerId { get; set; }
    [JsonPropertyName("maxTasks")]
    public int MaxTasks { get; set; }
    [JsonPropertyName("topics")]
    public List<TopicDto> Topics { get; set; }
}   