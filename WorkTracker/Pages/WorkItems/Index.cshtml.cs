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
    public class IndexModel : PageModel
    {
        private readonly WorkTracker.DataAccess.AppDbContext _context;

        public IndexModel(WorkTracker.DataAccess.AppDbContext context)
        {
            _context = context;
        }

        public IList<WorkItem> WorkItem { get;set; }

        public async Task OnGetAsync()
        {
            WorkItem = await _context.WorkItems
                .Include(w => w.Priority)
                .Include(w => w.Project)
                .Include(w => w.Status).ToListAsync();
        }
    }
}
