using System.Text.Json.Serialization;

namespace SharpPayStack.Models;


public record PaystackCreateVirtualAccountDto
{
    [JsonPropertyName("customer")]
    public required string Customer { get; init; }

    [JsonPropertyName("preferred_bank")]
    public required string PreferredBank { get; init; }

}

public record VirtualAccountBank
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }
}

public record VirtualAccountResponse : PaystackResponseDto
{
    [JsonPropertyName("account_name")]
    public required string AccountName { get; init; }

    [JsonPropertyName("account_number")]
    public required string AccountNumber { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    public required VirtualAccountBank Bank { get; init; }
}