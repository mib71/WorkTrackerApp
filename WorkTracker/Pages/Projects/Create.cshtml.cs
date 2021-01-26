using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkTracker.DataAccess;
using WorkTracker.Models;

namespace WorkTracker.Pages.Projects
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
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Project created!";

            return RedirectToPage("/Index");
        }
    }
}
