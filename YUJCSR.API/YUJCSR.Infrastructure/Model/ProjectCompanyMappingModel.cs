using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectCompanyMappingModel :ModelBase
    {
        [Key]
        public string? ProjectCompanyMappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? CompanyID { get; set; }
        public string? ApprovalStatus { get; set; }
    }
}
