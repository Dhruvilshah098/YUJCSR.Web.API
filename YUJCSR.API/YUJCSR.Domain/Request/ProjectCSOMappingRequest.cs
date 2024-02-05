using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectCSOMappingRequest :BaseRequest
    {
        public string? ProjectCSOMappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? CSOID { get; set; }
    }
}
