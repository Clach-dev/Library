using System.Net;
using Application.Common.MappingProfiles;
using Application.UseCases.AuthorCases.Commands.CreateAuthorCase;
using Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;
using Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests;

public class AuthorsTests
{
    private readonly IFixture _fixture;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AuthorsTests()
    {
        _fixture = new Fixture();
        
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<AuthorMappingProfile>(); });
        _mapper = config.CreateMapper();
        
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }
    
    [Fact]
    public async Task CreateAuthorHandler_GivenValidData_ShouldReturnCreatedAuthor()
    {
        // Arrange
        var createAuthorCommand = _fixture.Create<CreateAuthorCommand>();
        _unitOfWorkMock.Setup(uow => uow.Authors.CreateAsync(It.IsAny<Author>(), default))
            .Returns(Task.CompletedTask);
        
        var handler = new CreateAuthorHandler(_unitOfWorkMock.Object, _mapper);

        // Act
        var result = await handler.Handle(createAuthorCommand, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var dto = result.Value;
        dto.Should().NotBeNull();
        dto.Id.Should().NotBeEmpty();
        dto.LastName.Should().Be(createAuthorCommand.LastName);
        dto.FirstName.Should().Be(createAuthorCommand.FirstName);
        dto.MiddleName.Should().Be(createAuthorCommand.MiddleName);
        dto.Description.Should().Be(createAuthorCommand.Description);
        
        result.Errors.Should().Equal([string.Empty]);
        
        _unitOfWorkMock.Verify(uow => uow.Authors.CreateAsync(It.IsAny<Author>(), default), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdateAuthorHandler_GivenValidData_ShouldReturnUpdatedAuthor()
    {
        // Arrange
        var updateAuthorCommand = _fixture.Create<UpdateAuthorCommand>();
        var authorEntity = _mapper.Map<Author>(updateAuthorCommand);
        authorEntity.Id = updateAuthorCommand.Id;
        
        _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(authorEntity);
        
        var handler = new UpdateAuthorHandler(_unitOfWorkMock.Object, _mapper);

        // Act
        var result = await handler.Handle(updateAuthorCommand, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var dto = result.Value;
        dto.Should().NotBeNull();
        dto.Id.Should().Be(updateAuthorCommand.Id);
        dto.LastName.Should().Be(updateAuthorCommand.LastName);
        dto.FirstName.Should().Be(updateAuthorCommand.FirstName);
        dto.MiddleName.Should().Be(updateAuthorCommand.MiddleName);
        dto.Description.Should().Be(updateAuthorCommand.Description);
        
        result.Errors.Should().Equal([string.Empty]);

        _unitOfWorkMock.Verify(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task UpdateAuthorHandler_GivenInvalidAuthorId_ShouldReturnNotFoundError()
    {
        // Arrange
        var updateAuthorCommand = _fixture.Create<UpdateAuthorCommand>();
        
        _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((Author)null!);
        
        var handler = new UpdateAuthorHandler(_unitOfWorkMock.Object, _mapper);

        // Act
        var result = await handler.Handle(updateAuthorCommand, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeNull();
        
        _unitOfWorkMock.Verify(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Never);
    }
    
    [Fact]
    public async Task DeleteAuthorHandler_GivenValidData_ShouldReturnNoContentResult()
    {
        // Arrange
        var deleteAuthorCommand = _fixture.Create<DeleteAuthorCommand>();
        
        var authorEntity = _fixture
            .Build<Author>()
            .With(entity => entity.Id, deleteAuthorCommand.Id)
            .With(entity => entity.LastName, _fixture.Create<string>())
            .With(entity => entity.FirstName, _fixture.Create<string>())
            .With(entity => entity.MiddleName, _fixture.Create<string>())
            .With(entity => entity.Description, _fixture.Create<string>())
            .Without(entity => entity.Books)
            .Create();
        
        _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync(authorEntity);
        
        var handler = new DeleteAuthorHandler(_unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(deleteAuthorCommand, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        result.Value.Should().Be(default);
        result.Errors.Should().Equal([string.Empty]);
        
        _unitOfWorkMock.Verify(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAuthorHandler_GivenInvalidAuthorId_ShouldReturnNotFoundError()
    {
        // Arrange
        var deleteAuthorCommand = _fixture.Create<DeleteAuthorCommand>();
        
        _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default))
            .ReturnsAsync((Author)null!);
        
        var handler = new DeleteAuthorHandler(_unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(deleteAuthorCommand, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Value.Should().BeNull();
        result.Errors.Should().NotBeNull();
        
        _unitOfWorkMock.Verify(uow => uow.Authors.GetByIdAsync(It.IsAny<Guid>(), default), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Never);
    }
    
    // [Fact]
    // public async Task GetAllAuthorsHandler_GivenValidData_ShouldReturnAllAuthors()
    // {
    //     // Arrange
    //     var getAllAuthorsQuery = _fixture.Create<GetAllAuthorsQuery>();
    //     
    //     _unitOfWorkMock.Setup(uow => uow.Authors.CreateAsync(It.IsAny<Author>(), default))
    //         .Returns(Task.CompletedTask);
    //     
    //     var handler = new GetAllAuthorsHandler(_unitOfWorkMock.Object, _mapper);
    //
    //     // Act
    //     var result = await handler.Handle(getAllAuthorsQuery.PageInfoDto, default);
    //
    //     // Assert
    //     result.Should().NotBeNull();
    //     result.IsSuccess.Should().BeTrue();
    //     result.StatusCode.Should().Be(HttpStatusCode.Created);
    //     
    //     var dto = result.Value;
    //     dto.Should().NotBeNull();
    //     dto.Id.Should().NotBeEmpty();
    //     dto.LastName.Should().Be(createAuthorCommand.LastName);
    //     dto.FirstName.Should().Be(createAuthorCommand.FirstName);
    //     dto.MiddleName.Should().Be(createAuthorCommand.MiddleName);
    //     dto.Description.Should().Be(createAuthorCommand.Description);
    //     
    //     result.Errors.Should().Equal([string.Empty]);
    //     
    //     _unitOfWorkMock.Verify(uow => uow.Authors.CreateAsync(It.IsAny<Author>(), default), Times.Once);
    //     _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(default), Times.Once);
    // }
}