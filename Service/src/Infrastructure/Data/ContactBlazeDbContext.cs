using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext for the Contact Blaze application.
/// Manages database connections and entity configurations.
/// </summary>
public class ContactBlazeDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactBlazeDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public ContactBlazeDbContext(DbContextOptions<ContactBlazeDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Users entity set.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Gets or sets the PhoneNumbers entity set.
    /// </summary>
    public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Addresses entity set.
    /// </summary>
    public DbSet<Address> Addresses { get; set; } = null!;

    /// <summary>
    /// Configures the entity models and their relationships.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedNever(); // GUID will be generated in code
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.Email).IsUnique(); // Ensure unique emails
            entity.Property(u => u.CreatedAt).IsRequired();
            entity.Property(u => u.IsActive).IsRequired();

            // Configure relationships
            entity.HasMany(u => u.PhoneNumbers)
                  .WithOne(p => p.User)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.Addresses)
                  .WithOne(a => a.User)
                  .HasForeignKey(a => a.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure PhoneNumber entity
        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedNever();
            entity.Property(p => p.Number).IsRequired().HasMaxLength(20);
            entity.Property(p => p.CreatedAt).IsRequired();

            // Configure SmartEnum conversion
            entity.Property(p => p.Type)
                  .HasConversion(
                      v => v.Value,
                      v => PhoneNumberType.FromValue(v))
                  .IsRequired();
        });

        // Configure Address entity
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).ValueGeneratedNever();
            entity.Property(a => a.Street1).IsRequired().HasMaxLength(200);
            entity.Property(a => a.Street2).HasMaxLength(200);
            entity.Property(a => a.City).IsRequired().HasMaxLength(100);
            entity.Property(a => a.State).IsRequired().HasMaxLength(100);
            entity.Property(a => a.PostalCode).IsRequired().HasMaxLength(20);
            entity.Property(a => a.Country).IsRequired().HasMaxLength(100);
            entity.Property(a => a.CreatedAt).IsRequired();

            // Configure SmartEnum conversion
            entity.Property(a => a.Type)
                  .HasConversion(
                      v => v.Value,
                      v => AddressType.FromValue(v))
                  .IsRequired();
        });
    }
}
