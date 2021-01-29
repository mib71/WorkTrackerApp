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
        [BindProperty(SupportsGet = true)]
        public int ProjId { get; set; }
        public string ProjName { get; set; }

        public async Task OnGetAsync(int id)
        {
            ProjId = id;
            WorkItem = await _context.WorkItems
                .Include(w => w.Project)
                .Where(w => w.ProjectId == id)
                .Include(w => w.Priority)
                .Include(w => w.Status).OrderBy(w => w.Status.StatusOrderBy).ToListAsync();
                        
            ProjName = await _context.Projects.Where(p => p.ProjectId == id).Select(p => p.Name).FirstOrDefaultAsync();            
        }

        public async Task OnPost(int id, string status, string priority)
        {            
            if (status == null) status = "all";
            if (priority == null) priority = "all";

            WorkItem = await _context.WorkItems.Include(w => w.Project)
                    .Where(w => w.ProjectId == id)
                    .Include(w => w.Priority)
                    .Include(w => w.Status).OrderBy(w => w.Status.StatusOrderBy).ToListAsync();

            ProjName = await _context.Projects.Where(p => p.ProjectId == id).Select(p => p.Name).FirstOrDefaultAsync();

            if (status != "all")
            {
                WorkItem = WorkItem.Where(q => q.Status.StatusAlias == status).OrderBy(w => w.Status.StatusOrderBy).ToList();
            }

            if (priority != "all")
            {
                WorkItem = WorkItem.Where(q => q.Priority.PriorityAlias == priority).OrderBy(w => w.Status.StatusOrderBy).ToList();
            }
        }
    }
}
