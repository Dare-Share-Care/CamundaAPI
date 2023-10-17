using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class TaskDto : BaseDto
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}