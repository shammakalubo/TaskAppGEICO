using System;
using System.Collections.Generic;

#nullable disable

namespace TaskAppGEICO.Models
{
    public partial class TaskTable
    {
        public int Tid { get; set; }
        public string Tname { get; set; }
        public string Description { get; set; }
        public DateTime? TdueDate { get; set; }
        public int? TpriorityId { get; set; }
        public int? TstatusId { get; set; }
    }
}
