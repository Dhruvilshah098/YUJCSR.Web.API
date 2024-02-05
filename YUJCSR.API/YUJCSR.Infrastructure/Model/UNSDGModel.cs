using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class UNSDGModel : ModelBase
    {
        [Key]
        public string? UNSDGID { get; set; }
        public string? UNSDGName { get; set; }
        public Int32? UNSDGNumber { get; set; }
        public string? PhotoPath { get; set; }
    }
}
