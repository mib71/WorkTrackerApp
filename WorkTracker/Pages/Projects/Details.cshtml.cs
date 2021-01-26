﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkTracker.DataAccess;
using WorkTracker.Models;

namespace WorkTracker.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public DetailsModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
