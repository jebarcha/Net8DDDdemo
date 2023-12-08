using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Application.Rentals.ReservRent;

internal sealed class ReservRentCommandHandler
: ICommandHandler<ReservRentCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehiculoRepository _vehiculoRepository;
    private readonly IRentRepository _rentRepository;
    private readonly PriceService _priceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ReservRentCommandHandler(
        IUserRepository userRepository,
        IVehiculoRepository vehiculoRepository,
        IRentRepository rentRepository,
        PriceService priceService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider
    )
    {
        _userRepository = userRepository;
        _vehiculoRepository = vehiculoRepository;
        _rentRepository = rentRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        ReservRentCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehiculo = await _vehiculoRepository.GetByIdAsync(request.VehiculoId, cancellationToken);
        if (vehiculo is null)
        {
            return Result.Failure<Guid>(VehiculoErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _rentRepository.IsOverlappingAsync(vehiculo, duration, cancellationToken))
        {
            return Result.Failure<Guid>(RentErrors.Overlap);
        }

        try
        {
            var rent = Rent.Reservation(
                vehiculo,
                user.Id,
                duration,
                _dateTimeProvider.currentTime,
                _priceService
            );

            _rentRepository.Add(rent);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return rent.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(RentErrors.Overlap);
        }
    }
}
