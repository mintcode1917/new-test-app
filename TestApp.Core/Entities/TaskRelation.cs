namespace TestApp.Core.Entities;

public class TaskRelation
{
    public int Id { get; set; }
    
    public int FromTaskId { get; set; }
    
    public TestTask FromTask { get; set; }
    
    public int ToTaskId { get; set; }
    
    public TestTask ToTask { get; set; }
}