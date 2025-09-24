using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation property for phone numbers
    public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

    // Computed property for display purposes
    public string FullName => $"{FirstName} {LastName}".Trim();

    // Convenience properties
    public PhoneNumber? PrimaryPhoneNumber => PhoneNumbers.FirstOrDefault(p => p.IsPrimary);
    public PhoneNumber? MobilePhone => PhoneNumbers.FirstOrDefault(p => p.Type == PhoneNumberType.MOBILE);
}
