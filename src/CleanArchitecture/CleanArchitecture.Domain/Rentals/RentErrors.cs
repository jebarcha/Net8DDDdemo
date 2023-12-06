using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals;

public static class RentErrors
{
    public static Error NotFound = new Error(
        "Rent.Found",
        "Rent id was not found"
    );

    public static Error Overlap = new Error(
        "Rent.Overlap",
        "Rent is being taken from 2 or more clients at the same time in the same date"
    );

    public static Error NotReserved = new Error(
        "Rent.NotReserved",
        "Rent is not reserved"
    );

    public static Error NotConfirmed = new Error(
       "Rent.NotConfirmed",
       "Rent is not confirmed"
   );

    public static Error AlreadyStarted = new Error(
       "Rent.AlreadyStarted",
       "Rent already started"
   );
}
