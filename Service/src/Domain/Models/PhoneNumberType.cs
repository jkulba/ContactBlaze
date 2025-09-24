using Ardalis.SmartEnum;

namespace Domain.Models;

/// <summary>
/// Represents the different types of phone numbers that can be associated with a user.
/// This SmartEnum provides type safety and extensibility compared to traditional enums.
/// </summary>
public class PhoneNumberType : SmartEnum<PhoneNumberType>
{
    /// <summary>
    /// Represents a mobile/cellular phone number.
    /// </summary>
    public static readonly PhoneNumberType MOBILE = new(nameof(MOBILE), "Mobile", 1);

    /// <summary>
    /// Represents an office/work phone number.
    /// </summary>
    public static readonly PhoneNumberType OFFICE = new(nameof(OFFICE), "Office", 2);

    /// <summary>
    /// Represents a home/residential phone number.
    /// </summary>
    public static readonly PhoneNumberType HOME = new(nameof(HOME), "Home", 3);

    /// <summary>
    /// Gets the user-friendly display name for the phone number type.
    /// </summary>
    /// <value>A human-readable string representing the phone number type.</value>
    public string DisplayName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumberType"/> class.
    /// </summary>
    /// <param name="name">The internal name of the phone number type.</param>
    /// <param name="displayName">The user-friendly display name.</param>
    /// <param name="value">The numeric value associated with the type.</param>
    protected PhoneNumberType(string name, string displayName, int value) : base(name, value)
    {
        DisplayName = displayName;
    }
}
