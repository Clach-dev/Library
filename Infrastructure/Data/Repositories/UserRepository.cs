using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class UserRepository(LibraryDbContext context) : BaseRepository<User>(context), IUserRepository
{
}