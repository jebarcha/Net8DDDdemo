using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events;

public sealed record RentReservedDomainEvent(Guid RentId) : IDomainEvent;
