using System.Collections.Generic;
using System.Linq;
using TodoApi.Config;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoRepository : ITodoRepository
    {
        private List<TodoItem> _TodoList;
        private IMyDatabaseSettings settings;

        public TodoRepository(IMyDatabaseSettings config)
        {
            this.settings = config;
            InitializeData();
        }

        public IEnumerable<TodoItem> All
        {
            get { return _TodoList; }
        }

        public bool DoesItemExist(int Id)
        {
            return _TodoList.Any(item => item.Id == Id);
        }

        public TodoItem Find(int Id)
        {
            return _TodoList.FirstOrDefault(item => item.Id == Id);
        }

        public void Insert(TodoItem item)
        {
            _TodoList.Add(item);
        }

        public void Update(TodoItem item)
        {
            var TodoItem = this.Find(item.Id);
            var index = _TodoList.IndexOf(TodoItem);
            _TodoList.RemoveAt(index);
            _TodoList.Insert(index, item);
        }

        public void Delete(int Id)
        {
            _TodoList.Remove(this.Find(Id));
        }

        private void InitializeData()
        {
            _TodoList = new List<TodoItem>();

            var TodoItem1 = new TodoItem
            {
                Id = 123,
                Name = this.settings.BooksCollectionName,
                Secret = nameof(this.settings.BooksCollectionName),
                IsComplete = true
            };

            var TodoItem2 = new TodoItem
            {
                Id = 456,
                Name = this.settings.ConnectionString,
                Secret = nameof(this.settings.ConnectionString),
                IsComplete = false
            };

            var TodoItem3 = new TodoItem
            {
                Id = 789,
                Name = this.settings.DatabaseName,
                Secret = nameof(this.settings.DatabaseName),
                IsComplete = false,
            };

            _TodoList.Add(TodoItem1);
            _TodoList.Add(TodoItem2);
            _TodoList.Add(TodoItem3);
        }
    }
}