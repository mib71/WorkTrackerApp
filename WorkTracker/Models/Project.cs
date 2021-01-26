using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Pleas enter project name")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pleas enter project description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pleas enter date")]
        public DateTime? Created { get; set; }

        public ICollection<WorkItem> WorkItems { get; set; }
    }
}
