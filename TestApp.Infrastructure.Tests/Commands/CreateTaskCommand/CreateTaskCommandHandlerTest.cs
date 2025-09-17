using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Moq;
using TestApp.Core.Entities;
using TestApp.Core.Enums;
using TestApp.Infrastructure.Commands.CreateTaskCommand;
using TestApp.Infrastructure.Repositories;
using Xunit;

namespace TestApp.Infrastructure.Tests.Commands.CreateTaskCommand;

[TestSubject(typeof(CreateTaskCommandHandler))]
public class CreateTaskCommandHandlerTest
{

    [Fact]
    public async Task Handle_ValidCommand_ReturnsTaskId()
    {
        // Arrange
        var repository = new Mock<ITaskRepository>();
        var handler = new CreateTaskCommandHandler(repository.Object);
        var command = new Infrastructure.Commands.CreateTaskCommand.CreateTaskCommand
        {
            Title = "Test Task",
            AuthorId = 1,
            Priority = Priority.Medium
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result > 0);
        repository.Verify(r => r.AddAsync(It.IsAny<TestTask>()), Times.Once);
    }
}