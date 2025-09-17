using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entities;

namespace TestApp.Infrastructure;

/// <summary>
/// Контекст бд
/// </summary>
public class DataDbContext : DbContext
{
    public DbSet<TestTask> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskRelation> TaskRelations { get; set; }

    /// ctor
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestTask>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Status).IsRequired();
            entity.Property(t => t.Priority).IsRequired();

            entity.HasOne(t => t.Author)
                .WithMany()
                .HasForeignKey(t => t.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Assignee)
                .WithMany()
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.ParentTask)
                .WithMany(t => t.Subtasks)
                .HasForeignKey(t => t.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<TaskRelation>(entity =>
        {
            entity.HasKey(tr => tr.Id);
            entity.HasOne(tr => tr.FromTask)
                .WithMany(t => t.RelatedTasks)
                .HasForeignKey(tr => tr.FromTaskId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(tr => tr.ToTask)
                .WithMany()
                .HasForeignKey(tr => tr.ToTaskId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
        });
    }
}