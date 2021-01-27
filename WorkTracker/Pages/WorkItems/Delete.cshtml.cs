using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkTracker.DataAccess;
using WorkTracker.Models;

namespace WorkTracker.Pages.WorkItems
{
    public class DeleteModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public DeleteModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkItem WorkItem { get; set; }
        public int ProjId { get; set; } 

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkItem = await _context.WorkItems.FindAsync(id);

            if (WorkItem != null)
            {
                ProjId = WorkItem.ProjectId;
                _context.WorkItems.Remove(WorkItem);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Work item deleted!";
            }

            return RedirectToPage("./Index", new { id = ProjId });
        }
    }
}
