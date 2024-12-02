﻿using Application.Common.Interfaces.IRepositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class BookRepository(LibraryDbContext context) : BaseRepository<Book>(context), IBookRepository
{
}