using Ardalis.SmartEnum;

namespace Domain.Models;

/// <summary>
/// Represents the different types of addresses that can be associated with a user.
/// This SmartEnum provides type safety and extensibility compared to traditional enums.
/// </summary>
public class AddressType : SmartEnum<AddressType>
{
    /// <summary>
    /// Represents a home/residential address.
    /// </summary>
    public static readonly AddressType HOME = new(nameof(HOME), "Home", 1);

    /// <summary>
    /// Represents a work/employment address.
    /// </summary>
    public static readonly AddressType WORK = new(nameof(WORK), "Work", 2);

    /// <summary>
    /// Represents a business/commercial address.
    /// </summary>
    public static readonly AddressType BUSINESS = new(nameof(BUSINESS), "Business", 3);

    /// <summary>
    /// Gets the user-friendly display name for the address type.
    /// </summary>
    /// <value>A human-readable string representing the address type.</value>
    public string DisplayName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressType"/> class.
    /// </summary>
    /// <param name="name">The internal name of the address type.</param>
    /// <param name="displayName">The user-friendly display name.</param>
    /// <param name="value">The numeric value associated with the type.</param>
    protected AddressType(string name, string displayName, int value) : base(name, value)
    {
        DisplayName = displayName;
    }
}
