using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Rentals;

public sealed class Rent : Entity
{
    private Rent()
    {

    }

    private Rent(
        Guid id,
        Guid vehiculoId,
        Guid userId,
        DateRange? duration,
        Currency priceByPeriod,
        Currency manteinance,
        Currency accesories,
        Currency total,
        RentStatus status,
        DateTime creationDate
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        UserId = userId;
        Duration = duration;
        PriceByPeriod = priceByPeriod;
        Manteinance = manteinance;
        Accesories = accesories;
        Total = total;
        Status = status;
        CreationDate = creationDate;
    }
    public Guid VehiculoId { get; private set; }

    public Guid UserId { get; private set; }
    public Currency? PriceByPeriod { get; private set; }
    public Currency? Manteinance { get; private set; }
    public Currency? Accesories { get; private set; }
    public Currency? Total { get; private set; }

    public RentStatus Status { get; private set; }

    public DateRange? Duration { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? ConfirmationDate { get; private set; }
    public DateTime? RejectedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    public DateTime? CancellationDate { get; private set; }

    public static Rent Reservation(
        Vehiculo vehiculo,
        Guid userId,
        DateRange? duration,
        DateTime creationDate,
        PriceService priceService
    )
    {
        var priceDetail = priceService.CalculatePrice(
            vehiculo,
            duration!
        );

        var rent = new Rent(
            Guid.NewGuid(),
            vehiculo.Id,
            userId,
            duration,
            priceDetail.PricebyPeriod,
            priceDetail.Manteinance,
            priceDetail.Accesories,
            priceDetail.Total,
            RentStatus.Reserved,
            creationDate
        );

        rent.RaiseDomainEvent(new RentReservedDomainEvent(rent.Id!));

        vehiculo.DateLastRent = creationDate;

        return rent;
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != RentStatus.Reserved)
        {
            return Result.Failure(RentErrors.NotReserved);
        }

        Status = RentStatus.Confirmed;
        ConfirmationDate = utcNow;

        RaiseDomainEvent(new RentConfirmedDomainEvent(Id));
        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != RentStatus.Reserved)
        {
            return Result.Failure(RentErrors.NotReserved);
        }

        Status = RentStatus.Rejected;
        RejectedDate = utcNow;

        RaiseDomainEvent(new RentRejectedDomainEvent(Id));
        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);
        if (currentDate > Duration!.Start)
        {
            return Result.Failure(RentErrors.AlreadyStarted);
        }

        Status = RentStatus.Cancelled;
        CancellationDate = utcNow;

        RaiseDomainEvent(new RentCancelledDomainEvent(Id));
        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if (Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);
        }

        Status = RentStatus.Completed;
        CompletedDate = utcNow;

        RaiseDomainEvent(new RentCompletedDomainEvent(Id));
        return Result.Success();
    }

}
