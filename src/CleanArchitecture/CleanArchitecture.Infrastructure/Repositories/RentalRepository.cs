using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class RentalRepository : Repository<Rent>, IRentRepository
{
    private static readonly RentStatus[] ActiveRentStatus =
    {
        RentStatus.Reserved,
        RentStatus.Confirmed,
        RentStatus.Completed
    };

    public RentalRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehiculo vehiculo,
        DateRange duration,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<Rent>()
        .AnyAsync(
            rent =>
                rent.VehiculoId == vehiculo.Id &&
                rent.Duration!.Start <= duration.End &&
                rent.Duration.End >= duration.Start &&
                ActiveRentStatus.Contains(rent.Status),
                cancellationToken
        );
    }
}
