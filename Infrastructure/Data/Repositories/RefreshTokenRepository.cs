using Application.Common.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class RefreshTokenRepository(LibraryDbContext context) : BaseRepository<RefreshToken>(context), IRefreshTokenRepository
{
}