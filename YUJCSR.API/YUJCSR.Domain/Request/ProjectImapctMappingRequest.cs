using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectImapctMappingRequest :BaseRequest
    {
        public string? ImapctMappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? ImpactID { get; set; }
        public string? Stage1 { get; set; }
        public string? Stage2 { get; set; }
        public string? Stage3 { get; set; }
        public string? Stage4 { get; set; }

    }
}
