using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository implementation for User entity operations.
/// Provides data access operations for Users using Entity Framework Core.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ContactBlazeDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserRepository(ContactBlazeDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.PhoneNumbers)
            .Include(u => u.Addresses)
            .AsNoTracking()
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User?> GetWithDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.PhoneNumbers)
            .Include(u => u.Addresses)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .AsNoTracking()
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = null;

        await _context.Users.AddAsync(user, cancellationToken);
        return user;
    }

    /// <inheritdoc />
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        return await Task.FromResult(user);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync([id], cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FindAsync([id], cancellationToken);
        if (user == null)
            return false;

        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
