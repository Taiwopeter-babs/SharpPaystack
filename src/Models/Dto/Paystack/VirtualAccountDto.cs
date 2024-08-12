using Newtonsoft.Json;

namespace SharpPayStack.Models;


public record PaystackCreateVirtualAccountDto
{
    [JsonProperty("customer")]
    public required string Customer { get; init; }

    [JsonProperty("preferred_bank")]
    public required string PreferredBank { get; init; }

}

public record VirtualAccountBank
{
    [JsonProperty("name")]
    public required string Name { get; init; }

    [JsonProperty("id")]
    public required int Id { get; init; }
}

public record VirtualAccountResponse : PaystackResponseDto
{
    [JsonProperty("account_name")]
    public required string AccountName { get; init; }

    [JsonProperty("account_number")]
    public required string AccountNumber { get; init; }

    [JsonProperty("id")]
    public required int Id { get; init; }

    public required VirtualAccountBank Bank { get; init; }
}