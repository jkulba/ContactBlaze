using Domain.Models;

namespace Application.Interfaces;

/// <summary>
/// Repository interface for User entity operations.
/// Defines the contract for User data access operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets all users asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A collection of all users.</returns>
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The user if found, otherwise null.</returns>
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The user if found, otherwise null.</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets users with their related phone numbers and addresses asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A collection of users with their related data.</returns>
    Task<IEnumerable<User>> GetWithDetailsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a user with their related phone numbers and addresses by ID asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The user with related data if found, otherwise null.</returns>
    Task<User?> GetWithDetailsByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active users asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A collection of active users.</returns>
    Task<IEnumerable<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The added user.</returns>
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing user asynchronously.
    /// </summary>
    /// <param name="user">The user to update.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The updated user.</returns>
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a user by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>True if the user was deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs a soft delete by setting the user as inactive asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user to deactivate.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>True if the user was deactivated, false if not found.</returns>
    Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user with the given email exists asynchronously.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>True if a user with the email exists, otherwise false.</returns>
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all pending changes to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
