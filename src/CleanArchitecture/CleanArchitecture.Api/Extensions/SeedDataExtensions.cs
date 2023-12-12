using System.Reflection.Metadata;
using Bogus;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Vehiculos;
using Dapper;

namespace CleanArchitecture.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> vehiculos = new();

        for (int i = 0; i < 100; i++)
        {
            vehiculos.Add(new
            {
                Id = Guid.NewGuid(),
                Vin = faker.Vehicle.Vin(),
                Model = faker.Vehicle.Model(),
                Country = faker.Address.Country(),
                Department = faker.Address.State(),
                State = faker.Address.Country(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                PriceAmount = faker.Random.Decimal(1000, 20000),
                PriceCurrencyType = "USD",
                PriceManteinance = faker.Random.Decimal(100, 200),
                ManteinanceCurrencyType = "USD",
                Accesories = new List<int> { (int)Accessory.Wifi, (int)Accessory.AppleCar },
                DateLastRent = DateTime.MinValue
            });
        }

        const string sql = """
            INSERT INTO public.vehiculos
            (id, vin, model, address_country, address_department, address_state, address_city, address_street, price_amount, price_currency_type, manteinance_amount, manteinance_currency_type, accesories, date_last_rent)
            values(@id, @Vin, @Model, @Country, @Department, @State, @City, @Street, @PriceAmount, @PriceCurrencyType, @PriceManteinance, @ManteinanceCurrencyType, @Accesories, @DateLastRent)
        """;

        connection.Execute(sql, vehiculos);

    }

}
