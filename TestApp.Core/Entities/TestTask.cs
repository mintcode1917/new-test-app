using TestApp.Core.Enums;

namespace TestApp.Core.Entities;

public class TestTask
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public Priority Priority { get; set; }
    
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    public int? AssigneeId { get; set; }
    public User Assignee { get; set; }
    
    public int? ParentTaskId { get; set; }
    public TestTask ParentTask { get; set; }
    
    public List<TestTask> Subtasks { get; set; } = new();
    public List<TaskRelation> RelatedTasks { get; set; } = new();
}