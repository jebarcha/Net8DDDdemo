using FluentValidation;

namespace CleanArchitecture.Application.Rentals.ReservRent;

public class ReservRentCommandValidator : AbstractValidator<ReservRentCommand>
{
    public ReservRentCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VehiculoId).NotEmpty();
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}
