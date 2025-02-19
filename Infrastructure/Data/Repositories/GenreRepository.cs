using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class GenreRepository(LibraryDbContext context) : BaseRepository<Genre>(context), IGenreRepository
{
}