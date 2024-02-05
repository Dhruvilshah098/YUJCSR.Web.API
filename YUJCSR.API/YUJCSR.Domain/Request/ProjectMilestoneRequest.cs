using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectMilestoneRequest : BaseRequest
    {
        public string? MilestoneID { get; set; }
        public string? MilestoneName { get; set; }
        public string? Description { get; set; }
        public string? ProjectID { get; set; }
    }
}
