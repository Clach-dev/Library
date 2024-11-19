using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class AuthorRepository(AppDbContext context) : BaseRepository<Author>(context), IAuthorRepository
{
}