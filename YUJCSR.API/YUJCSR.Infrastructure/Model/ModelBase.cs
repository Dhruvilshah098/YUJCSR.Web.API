using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ModelBase
    {
        
        public ModelBase()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
        public bool ActiveStatus { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; internal set; }
        public DateTime? ModifiedDate { get; internal set; }
    }
}
