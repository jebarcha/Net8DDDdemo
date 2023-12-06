using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events;
public sealed record RentConfirmedDomainEvent(Guid Id) : IDomainEvent;
