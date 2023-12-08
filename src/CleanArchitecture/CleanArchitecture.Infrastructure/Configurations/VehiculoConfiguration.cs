using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("vehiculos");
        builder.HasKey(vehiculo => vehiculo.Id);

        builder.OwnsOne(vehiculo => vehiculo.Address);

        builder.Property(vehiculo => vehiculo.Model)
            .HasMaxLength(200)
            .HasConversion(model => model!.Value, value => new Model(value));

        builder.Property(vehiculo => vehiculo.Vin)
            .HasMaxLength(500)
            .HasConversion(vin => vin!.Value, value => new Vin(value));

        builder.OwnsOne(vehiculo => vehiculo.Price, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(vehiculo => vehiculo.Manteinance, priceBuilder =>
        {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.Property<uint>("Version").IsRowVersion();

    }
}
