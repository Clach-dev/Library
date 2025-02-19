using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class UserRepository(LibraryDbContext context) : BaseRepository<User>(context), IUserRepository
{
}