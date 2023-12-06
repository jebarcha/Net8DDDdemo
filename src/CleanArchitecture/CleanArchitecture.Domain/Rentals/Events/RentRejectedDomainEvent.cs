using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events;
public sealed record RentRejectedDomainEvent(Guid Id) : IDomainEvent;
