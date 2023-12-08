using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {
        builder.ToTable("rentals");
        builder.HasKey(rent => rent.Id);

        builder.OwnsOne(rent => rent.PriceByPeriod, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Manteinance, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Accesories, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Total, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Duration);

        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(rent => rent.VehiculoId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rent => rent.UserId);


    }
}
