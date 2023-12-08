using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Rentals.ReservRent.GetRental;

internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<RentResponse>> Handle(
        GetRentalQuery request,
        CancellationToken cancellationToken
    )
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """ 
            SELECT
                id AS Id,
                vehiculo_id AS VehiculoId,
                user_id AS UserId,
                status AS Status,
                price_rent AS PriceRent,
                currency_type_rent AS CurrencyTypeRent,
                price_manteinance AS PriceManteinance,
                currency_type_maintenance AS CurrencyTypeMaintenance,
                price_accessory AS PriceAccessory,
                currency_type_accessory AS CurrencyTypeAccessory,
                total AS Total,
                currency_type_total AS CurrencyTypeTotal,
                duration_start AS DurationStart,
                duration_end AS DurationEnd,
                created_date AS CreatedDate
            FROM rentals WHERE id=@RentId
        """;

        var rent = await connection.QueryFirstOrDefaultAsync<RentResponse>(
            sql,
            new
            {
                request.RentId
            }
        );

        return rent!;
    }
}
