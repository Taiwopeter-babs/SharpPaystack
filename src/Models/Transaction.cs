using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharpPayStack.Shared;

namespace SharpPayStack.Models;

[Table("transactions")]
public class Transaction : BaseModel
{
    [Required]
    public int Amount { get; set; }

    [Required]
    public TransactionStatus Status { get; set; }

    public string? CustomerCode { get; set; }

    public string? CustomerName { get; set; }

    [Required]
    public string? Reference { get; set; }

    public TransactionType? TransactionType { get; set; }

    public string? Description { get; set; }
}