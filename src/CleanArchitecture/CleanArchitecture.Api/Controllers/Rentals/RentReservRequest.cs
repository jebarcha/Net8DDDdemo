namespace CleanArchitecture.Api.Controllers.Rentals;

public sealed record RentReservRequest(
    Guid VehiculoId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate

);
