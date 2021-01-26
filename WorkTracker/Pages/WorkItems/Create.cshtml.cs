using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkTracker.DataAccess;
using WorkTracker.Models;

namespace WorkTracker.Pages.WorkItems
{
    public class CreateModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public CreateModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PriorityId"] = new SelectList(_context.Priorities, "PriorityId", "PriorityId");
        ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Description");
        ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return Page();
        }

        [BindProperty]
        public WorkItem WorkItem { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WorkItems.Add(WorkItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
