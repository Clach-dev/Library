using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class BookRepository(AppDbContext context) : BaseRepository<Book>(context), IBookRepository
{
}