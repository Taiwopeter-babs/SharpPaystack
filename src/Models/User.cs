using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using SharpPayStack.Shared;

namespace SharpPayStack.Models;


public class User : IdentityUser
{
    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [JsonIgnore]
    public string? Password { get; set; }
    public required Role Role { get; set; }
}