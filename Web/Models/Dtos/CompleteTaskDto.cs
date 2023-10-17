using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class CompleteTaskDto
{
    [JsonPropertyName("variables")]
    public Dictionary<string, Dictionary<string, object>>? Variables { get; set; }
}