using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectMilestoneModel : ModelBase
    {
        [Key]
        public string? MilestoneID { get; set; }
        public string? MilestoneName { get; set; }
        public string? Description { get; set; }
        public string? ProjectID { get; set; }
    }
}
