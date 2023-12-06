namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public sealed class VehiculoResponse
{
    public Guid Id { get; init; }
    public string? Model { get; init; }
    public string? Vin { get; init; }
    public decimal Price { get; init; }
    public string? CurrencyType { get; init; }

    public AddressResponse? Addresss { get; set; }
}
