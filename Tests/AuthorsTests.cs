using System.Net;
using Application.Common.Dtos;
using Application.Common.Dtos.Author;
using Application.Common.MappingProfiles;
using Application.Common.Utils;
using Application.UseCases.AuthorCases.Commands.CreateAuthorCase;
using AutoFixture;
using AutoMapper;
using Azure.Storage.Blobs;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Presentation.Controllers;
using Xunit;

namespace Tests;

public class AuthorsTests
{
    private const string DatabaseName = "TestDb";

    private readonly LibraryDbContext _libraryDbContext;

    private readonly AuthorsController _authorsController;
    
    private readonly IFixture _fixture = new Fixture();
    
    public AuthorsTests()
    {
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        var options = new DbContextOptionsBuilder<LibraryDbContext>().UseInMemoryDatabase(DatabaseName).Options;
        _libraryDbContext = new LibraryDbContext(options);
        
        var blobServiceClient = new Mock<BlobServiceClient>();

        var services = new ServiceCollection();

        services
            .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
            .AddHttpContextAccessor()
            .AddAutoMapper(typeof(AuthorMappingProfile).Assembly)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAuthorHandler).Assembly))
            .AddDbContext<LibraryDbContext>(opt => opt.UseInMemoryDatabase(DatabaseName))
            .AddSingleton(blobServiceClient.Object)
            .AddScoped<IUnitOfWork, UnitOfWork>();
            
        var serviceProvider = services.BuildServiceProvider();
        
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var mediatR = serviceProvider.GetRequiredService<IMediator>();
        
        _authorsController = new AuthorsController(httpContextAccessor, mapper, mediatR);
    }
    
    [Fact]
    public async Task CreateAuthor_ValidData_ReturnOk()
    {
        // Arrange
        var createAuthorDto = _fixture.Create<CreateAuthorDto>();

        // Act
        var act = await _authorsController.CreateAuthor(createAuthorDto, default);

        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<ReadAuthorDto>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNull();
        result.Value.Should().BeEquivalentTo(createAuthorDto);
    }
    
    [Fact]
    public async Task DeleteAuthor_ValidId_ReturnNoContent()
    {
        // Arrange
        var authorEntity = CreateAuthorEntity();

        await _libraryDbContext.Authors.AddAsync(authorEntity);
        await _libraryDbContext.SaveChangesAsync();
        
        var deleteAuthorDto = _fixture.Build<DeleteAuthorDto>()
            .With(d => d.Id, authorEntity.Id)
            .Create();
        
        // Act
        var act = await _authorsController.DeleteAuthor(deleteAuthorDto, default);

        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<Unit>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNull();
        result.Value.Should().Be(Unit.Value);
    }
    
    [Fact]
    public async Task DeleteAuthor_InvalidId_ReturnNotFound()
    {
        // Arrange
        var deleteAuthorDto = _fixture.Create<DeleteAuthorDto>();

        // Act
        var act = await _authorsController.DeleteAuthor(deleteAuthorDto, default);

        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<Unit>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Value.Should().Be(Unit.Value);
        result.Errors.Should().NotBeNullOrEmpty();
        
        result.Errors.Should().BeEquivalentTo(ErrorMessages.AuthorIdNotFound);
    }
    
    [Fact]
    public async Task UpdateAuthor_ValidData_ReturnOk()
    {
        // Arrange
        var authorEntity = CreateAuthorEntity();

        await _libraryDbContext.Authors.AddAsync(authorEntity);
        await _libraryDbContext.SaveChangesAsync();
        
        var updateAuthorDto = _fixture.Build<UpdateAuthorDto>()
            .With(d => d.Id, authorEntity.Id)
            .Create();
        
        // Act
        var act = await _authorsController.UpdateAuthor(updateAuthorDto, default);

        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<ReadAuthorDto>>();
        
        CheckSuccessResult(result);
        
        result.Value.Should().BeEquivalentTo(updateAuthorDto);
    }
    
    [Fact]
    public  async Task UpdateAuthor_InvalidId_ReturnNotFound()
    {
        // Arrange
        var updateAuthorDto = _fixture.Create<UpdateAuthorDto>();

        // Act
        var act = await _authorsController.UpdateAuthor(updateAuthorDto, default);

        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<ReadAuthorDto>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeNullOrEmpty();
        
        result.Errors.Should().BeEquivalentTo(ErrorMessages.NotFoundError);
    }

    [Fact]
    public async Task GetAllAuthors_WithData_ReturnsOk()
    {
        // Arrange
        var authorEntities = _fixture.Build<Author>()
            .Without(a => a.Books)
            .CreateMany(3).ToList();
        
        await _libraryDbContext.Authors.AddRangeAsync(authorEntities);
        await _libraryDbContext.SaveChangesAsync();
        
        // Act
        var act = await _authorsController.GetAllAuthors(new PageInfoDto(), default);
        
        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<ReadAuthorsDto>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNull();
        
        result.Value.Authors.Should().BeEquivalentTo(authorEntities, options => 
            options.Excluding(a => a.Books));
        result.Value.TotalCount.Should().Be(authorEntities.Count);
    }
    
    [Fact]
    public async Task GetAllAuthors_WithoutData_ReturnsOkWithEmptyList()
    {
        // Arrange
        var pageInfoDto = _fixture.Build<PageInfoDto>()
            .With(x => x.PageSize, 3)
            .With(x => x.PageNumber, 1)
            .Create();
        
        // Act
        var act = await _authorsController.GetAllAuthors(pageInfoDto, default);
        
        // Assert
        act.Should().BeOfType<ObjectResult>();
        
        var result = act.As<ObjectResult>().Value.As<Result<ReadAuthorsDto>>();
        
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNull();
        
        result.Value.Authors.Should().BeEmpty();
        result.Value.TotalCount.Should().Be(0);
    }
    
    private Author CreateAuthorEntity() => _fixture.Build<Author>()
        .Without(a => a.Books)
        .Create();
    
    private static void CheckSuccessResult<T>(Result<T> result)
    {
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Value.Should().NotBeNull();
        result.Errors.Should().BeNull();
    }
}