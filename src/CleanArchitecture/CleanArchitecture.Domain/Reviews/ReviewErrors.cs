using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotElegible = new(
        "Review.NotEligible",
        "This review and rate is not elegible because is not completed yet."
    );
}
