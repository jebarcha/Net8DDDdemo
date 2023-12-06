using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(
        Guid id,
        Guid vehiculoId,
        Guid rentId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime? creationDate
    ) : base(id)
    {
        VehiculoId = vehiculoId;
        RentId = rentId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreationDate = creationDate;
    }

    public Guid VehiculoId { get; private set; }
    public Guid RentId { get; private set; }
    public Guid UserId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime? CreationDate { get; private set; }

    public static Result<Review> Create(
        Rent rent,
        Rating rating,
        Comment comment,
        DateTime? creationDate
    )
    {
        if (rent.Status != RentStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotElegible);
        }

        var review = new Review(
            Guid.NewGuid(),
            rent.VehiculoId,
            rent.Id,
            rent.UserId,
            rating,
            comment,
            creationDate
        );

        review.RaiseDomainEvent(new ReviewCreateDomainEvent(review.Id));

        return review;
    }
}
