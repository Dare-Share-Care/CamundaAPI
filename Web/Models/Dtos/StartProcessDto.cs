using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class StartProcessDto
{
    [JsonPropertyName("processKey")]
    public string? ProcessKey { get; set; }
}