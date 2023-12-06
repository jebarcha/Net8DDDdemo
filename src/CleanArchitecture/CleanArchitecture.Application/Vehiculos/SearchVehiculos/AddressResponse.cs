namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public sealed class AddressResponse
{
    public string? Country { get; init; }
    public string? Department { get; init; }
    public string? State { get; init; } //provincia
    public string? Street { get; init; }
}
