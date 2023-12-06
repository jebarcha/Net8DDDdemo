using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events;
public sealed record RentCancelledDomainEvent(Guid Id) : IDomainEvent;
