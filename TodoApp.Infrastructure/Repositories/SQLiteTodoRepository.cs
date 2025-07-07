using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    public class SQLiteTodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public SQLiteTodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem?> UpdateAsync(TodoItem item)
        {
            var existing = await _context.TodoItems.FindAsync(item.Id);
            if (existing == null) return null;

            existing.Title = item.Title;
            existing.IsCompleted = item.IsCompleted;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return false;

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
