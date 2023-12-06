
namespace CleanArchitecture.Domain.Rentals;
public sealed record DateRange
{
    private DateRange()
    {

    }

    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }

    public int QuantityDays => End.DayNumber - Start.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new ApplicationException("End Date should be greater than Start Date");
        }

        return new DateRange
        {
            Start = start,
            End = end
        };

    }
}
