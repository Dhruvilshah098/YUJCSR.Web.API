using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectBudgetModel : ModelBase
    {
        [Key]
        public string? BudgetID { get; set; }
        public string? ProjectID { get; set; }
        public string? Milestone { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
    }
}
