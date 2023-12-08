using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class UsersRepository : Repository<User>, IUserRepository
{
    public UsersRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}
