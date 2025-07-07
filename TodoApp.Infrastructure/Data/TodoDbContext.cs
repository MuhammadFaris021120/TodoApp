using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    }
}
