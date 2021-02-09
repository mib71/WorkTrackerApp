using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkTracker.Models
{
    public class Status
    {
        public string StatusId { get; set; }
        public string StatusAlias { get; set; }
        public int StatusOrderBy { get; set; }
    }
}
