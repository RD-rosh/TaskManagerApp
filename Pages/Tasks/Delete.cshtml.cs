using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;


namespace TaskManagerApp.Pages.Tasks
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        // GET: /Tasks/Delete/{id}
        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _context.Tasks.FindAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        // POST: /Tasks/Delete/{id}
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Retrieve the TaskItem to delete
            TaskItem = await _context.Tasks.FindAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            // Remove the TaskItem from the context
            _context.Tasks.Remove(TaskItem);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the index page after deletion
            return RedirectToPage("./Index");
        }
    }
}
