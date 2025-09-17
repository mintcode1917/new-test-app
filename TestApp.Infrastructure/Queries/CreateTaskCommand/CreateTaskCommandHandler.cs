using MediatR;
using TestApp.Core.Entities;
using TestApp.Core.Enums;
using TestApp.Infrastructure.Repositories;

namespace TestApp.Infrastructure.Queries.CreateTaskCommand;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
{
    private readonly ITaskRepository _repository;
    
    public CreateTaskCommandHandler(ITaskRepository repository)
        => _repository = repository;

    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TestTask()
        {
            AuthorId = request.AuthorId,
            AssigneeId = request.AssigneeId,
            Priority = request.Priority,
            Status = Status.New
        };
        
        await _repository.AddAsync(task);
        return task.Id;
    }
}