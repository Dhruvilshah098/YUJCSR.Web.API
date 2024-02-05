using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectBudgetRequest : BaseRequest
    {
        public string? BudgetID { get; set; }
        public string? ProjectID { get; set; }
        public string? Milestone { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
    }
}
