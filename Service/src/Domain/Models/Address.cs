using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

/// <summary>
/// Represents a physical address associated with a user.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the unique identifier for the address.
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the user identifier that owns this address.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the first line of the street address.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Street1 { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the second line of the street address (optional).
    /// </summary>
    [MaxLength(200)]
    public string? Street2 { get; set; }

    /// <summary>
    /// Gets or sets the city name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the state or province.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal or ZIP code.
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country name or code.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of address (Home, Work, Business).
    /// </summary>
    [Required]
    public AddressType Type { get; set; } = AddressType.HOME;

    /// <summary>
    /// Gets or sets whether this is the primary address for the user.
    /// </summary>
    public bool IsPrimary { get; set; } = false;

    /// <summary>
    /// Gets or sets when this address was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets when this address was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the user who owns this address.
    /// </summary>
    public User User { get; set; } = null!;

    /// <summary>
    /// Gets the full address as a formatted string.
    /// </summary>
    public string FullAddress
    {
        get
        {
            var parts = new List<string> { Street1 };

            if (!string.IsNullOrWhiteSpace(Street2))
                parts.Add(Street2);

            parts.Add($"{City}, {State} {PostalCode}");
            parts.Add(Country);

            return string.Join(", ", parts);
        }
    }
}
