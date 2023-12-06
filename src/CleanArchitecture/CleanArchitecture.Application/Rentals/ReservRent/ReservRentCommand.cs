using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.ReservRent;

public record ReservRentCommand(
    Guid vehiculoId,
    Guid userId,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<Guid>;
