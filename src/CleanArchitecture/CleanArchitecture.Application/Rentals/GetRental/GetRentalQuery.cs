using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rentals.ReservRent.GetRental;


public sealed record GetRentalQuery(Guid RentId) : IQuery<RentResponse>;
