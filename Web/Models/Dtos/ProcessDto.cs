using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class ProcessDto : BaseDto
{
    [JsonPropertyName("ended")]
    public bool? Ended { get; set; }
}