using System.Text.Json.Serialization;

namespace SharpPayStack.Models;

public record BasePaystackTransferDto
{

    [JsonPropertyName("source")]
    public required string Source { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("amount")]
    public required decimal Amount { get; set; }
}

public record PaystackTransferDto : BasePaystackTransferDto
{
    [JsonPropertyName("recipient")]
    public string? Recipient { get; init; }
}

public record PaystackTransferData : BasePaystackTransferDto
{
    [JsonPropertyName("integration")]
    public int Integration { get; init; }

    [JsonPropertyName("domain")]
    public string? Domain { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("recipient")]
    public int Recipient { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("transfer_code")]
    public string? TransferCode { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}


public record PaystackTransferResponse : PaystackResponseDto
{
    public required PaystackTransferData Data { get; init; }
}