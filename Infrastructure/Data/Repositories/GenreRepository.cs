using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class GenreRepository(LibraryDbContext context) : BaseRepository<Genre>(context), IGenreRepository
{
}