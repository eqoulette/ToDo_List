using System.Collections.Generic;
using System.Linq;
using ToDoListLogic;

namespace ToDoList.Data
{
    public class SqlToDoItemData : IToDoItemData
    {
        private readonly ToDoListDbContext db;

        public SqlToDoItemData(ToDoListDbContext db)
        {
            this.db = db;
        }

        public void Add(ToDoItem newItem)
        {
            db.ToDoItems.Add(newItem);
            db.SaveChanges();
        }

        public int GetCountOfItems(bool state = false)
        {
            var query = (from i in db.ToDoItems
                        where i.IsCompleted == state
                        select i).Count();

            return query;
        }

        public IEnumerable<ToDoItem> GetItemsByNameAndState(string name = null, bool state = false)
        {
            var query = from i in db.ToDoItems
                        where (string.IsNullOrEmpty(name) || i.ItemName.Contains(name)) &&
                        i.IsCompleted == state
                        orderby i.DueDate.HasValue descending, 
                                i.DueDate                       
                        select i;

            return query;
        }

        public void Update(ToDoItem updatedItem)
        {
            var result = db.ToDoItems.SingleOrDefault(i => i.Id == updatedItem.Id);
            
            if (result != null)
            {
                if (result != null)
                {
                    result.ItemName = updatedItem.ItemName;
                    result.DueDate = updatedItem.DueDate;
                    result.Priority = updatedItem.Priority;
                    result.IsCompleted = updatedItem.IsCompleted;
                }
            }

            db.SaveChanges();
        }
    }
}
