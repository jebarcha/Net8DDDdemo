using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

public sealed record SearchVehiculosQuery(
    DateOnly startDate,
    DateOnly endDate
) : IQuery<IReadOnlyList<VehiculoResponse>>;
