using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Rentals;

public class PriceService
{
    public PriceDetail CalculatePrice(Vehiculo vehiculo, DateRange period)
    {
        var currencyType = vehiculo.Price!.CurrencyType;
        var priceByPeriod = new Currency(
            period.QuantityDays * vehiculo.Price.Amount,
            currencyType);

        decimal percentageChange = 0;

        foreach (var accesory in vehiculo.Accesories)
        {
            percentageChange += accesory switch
            {
                Accessory.AppleCar or Accessory.AndrioidCar => 0.05m,
                Accessory.AC => 0.01m,
                Accessory.Maps => 0.01m,
                _ => 0
            };
        }

        var accesoryCharges = Currency.Zero(currencyType);

        if (percentageChange > 0)
        {
            accesoryCharges = new Currency(
                priceByPeriod.Amount * percentageChange,
                currencyType);
        }

        var total = Currency.Zero();
        total += priceByPeriod;

        if (!vehiculo!.Manteinance!.IsZero())
        {
            total += vehiculo.Manteinance;
        }

        total += accesoryCharges;

        return new PriceDetail(
            priceByPeriod,
            vehiculo.Manteinance,
            accesoryCharges,
            total
        );
    }
}
