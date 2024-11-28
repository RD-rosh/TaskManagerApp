using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TaskItem> Tasks { get; set; }

        public async Task OnGetAsync()
        {
            Tasks = await _context.Tasks
            .OrderBy(t => t.DueDate)  // Sort tasks by DueDate in ascending order
            .ToListAsync();
        }
    }
}
