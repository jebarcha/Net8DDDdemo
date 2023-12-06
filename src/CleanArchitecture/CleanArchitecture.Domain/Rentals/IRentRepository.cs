using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Rentals;

public interface IRentRepository
{
    Task<Rent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duration,
        CancellationToken cancellationToken = default
    );

    void Add(Rent rent);

}
