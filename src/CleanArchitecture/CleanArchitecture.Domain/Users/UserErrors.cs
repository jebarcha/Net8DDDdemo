using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "User Id does not exist"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Credentials are incorrect"
    );


}
