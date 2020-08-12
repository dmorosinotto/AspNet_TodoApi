using System.Collections.Generic;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface ITodoRepository
    {
        bool DoesItemExist(int id);
        IEnumerable<TodoItem> All { get; }
        TodoItem Find(int id);
        void Insert(TodoItem item);
        void Update(TodoItem item);
        void Delete(int id);
    }
}