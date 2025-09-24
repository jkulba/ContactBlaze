using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class PhoneNumber
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Number { get; set; } = string.Empty;

    [Required]
    public PhoneNumberType Type { get; set; } = PhoneNumberType.MOBILE;

    public bool IsPrimary { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public User User { get; set; } = null!;
}
