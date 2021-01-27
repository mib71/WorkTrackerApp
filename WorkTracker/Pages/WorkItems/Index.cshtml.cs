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
                .Include(w => w.Status).ToListAsync();
                        
            ProjName = await _context.Projects.Where(p => p.ProjectId == id).Select(p => p.Name).FirstOrDefaultAsync();            
        }

        public void OnPost(int id, string status)
        {
            if (status == "all")
            {
                WorkItem = _context.WorkItems.Include(w => w.Project)
                    .Where(w => w.ProjectId == id)
                    .Include(w => w.Priority)
                    .Include(w => w.Status).ToList();
            }
            else if (status == "inp")
            {
                WorkItem = _context.WorkItems.Include(w => w.Project)
                    .Where(w => w.ProjectId == id)
                    .Include(w => w.Priority)
                    .Include(w => w.Status).Where(w => w.StatusId == "In Progress").ToList();
            }
            else if (status == "don")
            {
                WorkItem = _context.WorkItems.Include(w => w.Project)
                    .Where(w => w.ProjectId == id)
                    .Include(w => w.Priority)
                    .Include(w => w.Status).Where(w => w.StatusId == "Done").ToList();
            }
            else if (status == "tod")
            {
                WorkItem = _context.WorkItems.Include(w => w.Project)
                    .Where(w => w.ProjectId == id)
                    .Include(w => w.Priority)
                    .Include(w => w.Status).Where(w => w.StatusId == "To Do").ToList();
            }

        }
    }
}
