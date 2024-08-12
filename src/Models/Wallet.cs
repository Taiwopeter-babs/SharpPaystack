using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SharpPayStack.Shared;

namespace SharpPayStack.Models;

[Table("wallets")]
public class Wallet : BaseModel
{
    [Required]
    public decimal Balance { get; set; }

    [Required]
    public string? Currency { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [JsonIgnore]
    public Customer Customer { get; set; } = null!;
}