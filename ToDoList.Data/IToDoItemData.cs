using System.Collections.Generic;
using ToDoListLogic;

namespace ToDoList.Data
{
    public interface IToDoItemData
    {
        IEnumerable<ToDoItem> GetItemsByNameAndState(string name = null, bool state = false);
        void Add(ToDoItem newItem);
        void Update(ToDoItem updatedItem);
        int GetCountOfItems(bool state = false);
    }
}
