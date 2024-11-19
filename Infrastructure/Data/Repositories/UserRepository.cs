using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
}