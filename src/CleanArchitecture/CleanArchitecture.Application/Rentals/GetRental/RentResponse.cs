namespace CleanArchitecture.Application.Rentals.ReservRent.GetRental;

public sealed class RentResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid VehiculoId { get; init; }
    public int Status { get; init; }
    public decimal PriceRent { get; init; }
    public string? CurrencyTypeRent { get; init; }
    public decimal PriceManteinance { get; init; }
    public string? CurrencyTypeMaintenance { get; init; }
    public decimal PriceAccessory { get; init; }
    public string? CurrencyTypeAccessory { get; init; }
    public decimal Total { get; init; }
    public string? CurrencyTypeTotal { get; init; }
    public DateOnly DurationStart { get; init; }
    public DateOnly DurationEnd { get; init; }
    public DateTime CreatedDate { get; init; }

}
