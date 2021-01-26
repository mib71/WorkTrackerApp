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

            //ProjName = WorkItem.Where(w => w.ProjectId == id).FirstOrDefault().ToString();
            //ProjName = WorkItem.FirstOrDefault(p => p.ProjectId == id).ToString();
            ProjName = WorkItem.Select(p => p.Project.Name).FirstOrDefault().ToString();                       
        }
    }
}
