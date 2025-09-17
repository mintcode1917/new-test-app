using TestApp.Core.Entities;

namespace TestApp.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<TestTask> GetByIdAsync(int id);

    Task<IEnumerable<TestTask>> GetAllAsync();

    Task AddAsync(TestTask task);

    void Update(TestTask task);

    void Delete(TestTask task);

    Task SaveChangesAsync();
}