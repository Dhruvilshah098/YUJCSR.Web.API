using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class CSODocModel :ModelBase
    {
        [Key]
        public string? DocID { get; set; }
        public string? CSOID { get; set; }
        public string? DcoTypeID { get; set; }
        public string? OriginalFileName { get; set; }
        public string? SystemFileName { get; set; }
        public string? FileExtension { get; set; }
    }
}
