using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class AuthorRepository(LibraryDbContext context) : BaseRepository<Author>(context), IAuthorRepository
{
}