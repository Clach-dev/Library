using Domain.Entities;
using Domain.Interfaces.IRepositories;

namespace Infrastructure.Data.Repositories;

public class ReviewRepository(LibraryDbContext context) : BaseRepository<Review>(context), IReviewRepository
{
}