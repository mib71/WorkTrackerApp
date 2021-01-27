using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkTracker.DataAccess;
using WorkTracker.Models;

namespace WorkTracker.Pages.WorkItems
{
    public class EditModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public EditModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkItem WorkItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkItem = await _context.WorkItems
                .Include(w => w.Priority)
                .Include(w => w.Project)
                .Include(w => w.Status).FirstOrDefaultAsync(m => m.Id == id);

            if (WorkItem == null)
            {
                return NotFound();
            }
           ViewData["PriorityId"] = new SelectList(_context.Priorities, "PriorityId", "PriorityId");
           ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
           ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WorkItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkItemExists(WorkItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            TempData["Success"] = "Workitem updated!";
            return RedirectToPage("./Index", new { id = WorkItem.ProjectId });
        }

        private bool WorkItemExists(int id)
        {
            return _context.WorkItems.Any(e => e.Id == id);
        }
    }
}
