using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ImpactMasterModel :ModelBase
    {
        [Key]
        public string? ImpactID { get; set; }
        public string? ImpactName { get; set; }
        public Int32? ImpactNumber { get; set; }
    }
}
