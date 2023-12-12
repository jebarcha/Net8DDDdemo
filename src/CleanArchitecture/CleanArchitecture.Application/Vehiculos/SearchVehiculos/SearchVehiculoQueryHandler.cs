using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

internal sealed class SearchVehiculoQueryHandler
: IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
{
    private static readonly int[] ActiveRentStatuses =
    {
        (int)RentStatus.Reserved,
        (int)RentStatus.Confirmed,
        (int)RentStatus.Completed
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiculoQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
        SearchVehiculosQuery request,
        CancellationToken cancellationToken
    )
    {
        if (request.startDate > request.endDate)
        {
            return new List<VehiculoResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                a.id as Id,
                a.model as Model,
                a.vin as Vin,
                a.price_amount as Price,
                a.price_currency_type as PriceCurrencyType,
                a.address_country as Country,
                a.address_department as Department,
                a.address_state as State,
                a.address_city as City,
                a.address_street as Street

            FROM vehiculos AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM rentals AS b
                WHERE
                    b.vehiculo_id = a.id AND
                    b.duration_start <= @EndDate AND
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveRentStatuses)
            )
        """;

        var vehiculos = await connection
            .QueryAsync<VehiculoResponse, AddressResponse, VehiculoResponse>
            (
                sql,
                (vehiculo, address) =>
                {
                    vehiculo.Addresss = address;
                    return vehiculo;
                },
                new
                {
                    StartDate = request.startDate,
                    EndDate = request.endDate,
                    ActiveRentStatuses
                },
                splitOn: "Country"
            );

        return vehiculos.ToList();
    }
}
