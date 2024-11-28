using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            TaskItem = await _context.Tasks.FindAsync(id);

            if (TaskItem == null)
            {
                return NotFound();
            }

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Ensure the Id is valid and the task exists in the database
            var taskToUpdate = await _context.Tasks.FindAsync(TaskItem.Id);

            if (taskToUpdate == null)
            {
                return NotFound();
            }

            // Update the task properties with the form values
            taskToUpdate.Title = TaskItem.Title;
            taskToUpdate.Description = TaskItem.Description;
            taskToUpdate.DueDate = TaskItem.DueDate;
            taskToUpdate.IsComplete = TaskItem.IsComplete;

            // Mark the entity as modified and save changes
            _context.Tasks.Update(taskToUpdate);  // Explicitly tell EF to track this task and mark it for update
            //_logger.LogInformation($"Updating task with Id = {TaskItem.Id}. IsComplete = {TaskItem.IsComplete}");

            await _context.SaveChangesAsync();  // Save changes to the database

            return RedirectToPage("./Index");
        }





    }
}
