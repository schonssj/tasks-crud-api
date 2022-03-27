using System.Data.Entity;

namespace TasksControl.Models
{
    public class TasksContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}