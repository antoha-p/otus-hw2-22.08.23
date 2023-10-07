using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class Customer : BaseEntity
{
    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }
}