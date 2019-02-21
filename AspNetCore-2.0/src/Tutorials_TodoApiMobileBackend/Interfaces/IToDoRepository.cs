using System.Collections.Generic;
using Tutorials_TodoApiMobileBackend.Models;


namespace Tutorials_TodoApiMobileBackend.Interfaces
{
    public interface IToDoRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<ToDoItem> All { get; }
        ToDoItem Find(string id);
        void Insert(ToDoItem item);
        void Update(ToDoItem item);
        void Delete(string id);
    }
}
