using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectUNSDGMappingRequest : BaseRequest
    {
        public string? MappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? UNSGDID { get; set; }
    }
}
