using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//testing
// Practice change for feature/test-branch
// Practice change for feature/test-branch-B
namespace TodoApp.Core.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
//testing