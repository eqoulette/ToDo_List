namespace ToDoList.Data
{
    using System.Data.Entity;
    using ToDoListLogic;

    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext()
            : base("name=ToDoListDbContext")
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}