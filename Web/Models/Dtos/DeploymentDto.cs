using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class DeploymentDto : BaseDto
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("source")] public string? Source { get; set; }
}