using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpPayStack.Shared;

namespace SharpPayStack.Models;

[Table("bankDetails")]
public class BankDetails : BaseModel
{
    [Required]
    public string? BankName { get; set; }

    [Required]
    public string? AccountNumber { get; set; }


    [Required]
    public string? AccountName { get; set; }

    [Required]
    public int CustomerId { get; set; }


    [Required]
    public Customer Customer { get; set; } = null!;
}