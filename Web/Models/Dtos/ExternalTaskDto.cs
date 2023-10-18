using System.Text.Json.Serialization;

namespace Web.Models.Dtos;

public class ExternalTaskDto : BaseDto
{
    [JsonPropertyName("topicName")]
    public string? TopicName { get; set; }
}