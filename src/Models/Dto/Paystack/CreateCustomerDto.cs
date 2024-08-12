using System.Text.Json.Serialization;

namespace SharpPayStack.Models;


public record PaystackResponseDto
{
    [JsonPropertyName("status")]
    public bool Status { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}

public record PaystackCreateCustomerDto
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
}

public record PaystackCustomerData
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("integration")]
    public int Integration { get; init; }

    [JsonPropertyName("domain")]
    public string? Domain { get; init; }

    [JsonPropertyName("customer_code")]
    public string? CustomerCode { get; init; }

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("identified")]
    public bool Identified { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}

public record CreateCustomerResponseDto : PaystackResponseDto
{
    [JsonPropertyName("data")]
    public required PaystackCustomerData Data { get; init; }
}