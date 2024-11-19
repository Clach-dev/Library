using Application.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class ReservationRepository(AppDbContext context) : BaseRepository<Reservation>(context), IReservationRepository
{
}