using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events;
public sealed record RentCompletedDomainEvent(Guid Id) : IDomainEvent;
