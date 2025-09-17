using MediatR;
using TestApp.Core.Enums;

namespace TestApp.Infrastructure.Queries.CreateTaskCommand;

public record CreateTaskCommand : IRequest<int>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public int AuthorId { get; init; }
    public int? AssigneeId { get; init; }
    public Priority Priority { get; init; }
}