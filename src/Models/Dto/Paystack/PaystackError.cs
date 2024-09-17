using System.Text.Json.Serialization;

namespace SharpPayStack.Models;

public record PaystackError
{

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("message")]
    public required string[] Message { get; init; }
}