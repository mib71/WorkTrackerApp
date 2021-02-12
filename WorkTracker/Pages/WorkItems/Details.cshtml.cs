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
    public class DetailsModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public DetailsModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
