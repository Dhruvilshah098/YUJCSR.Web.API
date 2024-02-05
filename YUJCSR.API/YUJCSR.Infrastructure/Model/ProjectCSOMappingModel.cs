using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectCSOMappingModel : ModelBase
    {
        [Key]
        public string? ProjectCSOMappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? CSOID { get; set; }
    }
}
