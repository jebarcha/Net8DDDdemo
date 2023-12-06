using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals;

public record PriceDetail(
    Currency PricebyPeriod,
    Currency Manteinance,
    Currency Accesories,
    Currency Total
);
