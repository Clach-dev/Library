using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class BookRepository(LibraryDbContext context) : BaseRepository<Book>(context), IBookRepository
{
}