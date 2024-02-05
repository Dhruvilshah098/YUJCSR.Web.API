using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ImpactMasterRequest : BaseRequest
    {
        public string? ImpactID { get; set; }
        public string? ImpactName { get; set; }
        public Int32? ImpactNumber { get; set; }
    }
}
