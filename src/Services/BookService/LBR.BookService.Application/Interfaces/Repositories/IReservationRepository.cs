using LBR.BookService.Domain.Entities;
using LBR.BookService.Domain.Enums;

namespace LBR.BookService.Application.Interfaces.Repositories;

/// <summary>
/// Repository interface for Reservation operations
/// </summary>
public interface IReservationRepository
{
    /// <summary>
    /// Gets a reservation by its ID
    /// </summary>
    Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all reservations for a specific user
    /// </summary>
    Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets active reservations for a specific user
    /// </summary>
    Task<IEnumerable<Reservation>> GetActiveReservationsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all reservations for a specific book
    /// </summary>
    Task<IEnumerable<Reservation>> GetByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets active reservations for a specific book
    /// </summary>
    Task<IEnumerable<Reservation>> GetActiveReservationsByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all reservations
    /// </summary>
    Task<IEnumerable<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets reservations by status
    /// </summary>
    Task<IEnumerable<Reservation>> GetByStatusAsync(ReservationStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets expired reservations that need to be marked as expired
    /// </summary>
    Task<IEnumerable<Reservation>> GetExpiredReservationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new reservation
    /// </summary>
    Task<Reservation> AddAsync(Reservation reservation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing reservation
    /// </summary>
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a reservation
    /// </summary>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user has an active reservation for a specific book
    /// </summary>
    Task<bool> HasActiveReservationAsync(Guid userId, Guid bookId, CancellationToken cancellationToken = default);
}
