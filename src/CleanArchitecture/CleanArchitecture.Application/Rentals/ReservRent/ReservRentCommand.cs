using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.ReservRent;

public record ReservRentCommand(
    Guid VehiculoId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<Guid>;
