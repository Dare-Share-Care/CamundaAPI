using System.Text.Json.Serialization;

namespace Web.Models.Dtos;


public class BaseDto
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}