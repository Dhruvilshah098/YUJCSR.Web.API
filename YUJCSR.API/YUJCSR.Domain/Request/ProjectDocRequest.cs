using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class ProjectDocRequest :BaseRequest
    {
        public string? DocID { get; set; }
        public string? ProjectID { get; set; }
        public string? DcoTypeID { get; set; }
        public string? OriginalFileName { get; set; }
        public string? SystemFileName { get; set; }
        public string? FileExtension { get; set; }
        public string? FilePath { get; set; }
    }
}
