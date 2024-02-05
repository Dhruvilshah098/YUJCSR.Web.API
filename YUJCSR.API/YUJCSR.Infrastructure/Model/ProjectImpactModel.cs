using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectImpactModel : ModelBase
    {
        [Key]
        public string? ImpactMappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? ImpactID { get; set; }
        public string? Stage1 { get; set; }
        public string? Stage2 { get; set; }
        public string? Stage3 { get; set; }
        public string? Stage4 { get; set; }


    }
}
