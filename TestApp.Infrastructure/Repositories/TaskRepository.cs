using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entities;

namespace TestApp.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DataDbContext _context;

    public TaskRepository(DataDbContext context)
    {
        _context = context;
    }

    public async Task<TestTask> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .Include(t => t.Author)
            .Include(t => t.Assignee)
            .Include(t => t.Subtasks)
            .Include(t => t.RelatedTasks)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TestTask>> GetAllAsync()
    {
        return await _context.Tasks
            .Include(t => t.Author)
            .Include(t => t.Assignee)
            .ToListAsync();
    }

    public async Task AddAsync(TestTask task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public void Update(TestTask task)
    {
        _context.Tasks.Update(task);
    }

    public void Delete(TestTask task)
    {
        _context.Tasks.Remove(task);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}