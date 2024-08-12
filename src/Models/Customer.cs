using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SharpPayStack.Shared;

namespace SharpPayStack.Models;

[Table("customers")]
public class Customer : BaseModel
{
    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    [JsonIgnore]
    public string? Password { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;

    [Required]
    public bool IsBlacklisted { get; set; } = false;

    public BankDetails? BankDetails { get; set; }
    public Wallet? Wallet { get; set; }
}