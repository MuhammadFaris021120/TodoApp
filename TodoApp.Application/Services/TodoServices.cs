using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
// Practice change for feature/test-branch
// Practice change for feature/test-branch
namespace TodoApp.Application.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repo;

        public TodoService(ITodoRepository repo)
        {
            _repo = repo;
        }

        public Task<List<TodoItem>> GetAllAsync() => _repo.GetAllAsync();
        public Task<TodoItem?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<TodoItem> AddAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("Title cannot be empty");
            return _repo.AddAsync(item);
        }

        public Task<TodoItem?> UpdateAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("Title cannot be empty");
            return _repo.UpdateAsync(item);
        }
        // Practice change for feature/test-branch
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
        // Practice change for feature/test-branch
    }
}

