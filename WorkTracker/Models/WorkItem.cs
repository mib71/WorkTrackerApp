using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Models
{
    public class WorkItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pleas enter title")]
        [MaxLength(40)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Pleas enter date")]
        public DateTime? Created { get; set; }
        public DateTime? Completed { get; set; }

        [Required(ErrorMessage = "Pleas select status")]
        public string StatusId { get; set; }
        public Status Status { get; set; }

        [Required(ErrorMessage = "Pleas select priority")]
        public string PriorityId { get; set; }
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "Pleas select project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
