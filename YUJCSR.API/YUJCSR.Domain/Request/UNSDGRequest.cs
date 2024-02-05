using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class UNSDGRequest :BaseRequest
    {
        public string? UNSDGID { get; set; }
        public string? UNSDGName { get; set; }
        public Int32? UNSDGNumber { get; set; }
        public string? PhotoPath { get; set; }
    }
}
