using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Request
{
    public class AdminUserRequest :BaseRequest
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public string? LoginID { get; set; }
        public string? Password { get; set; }
        public string? EmailID { get; set; }
        public string? MobileNumber { get; set; }
        public string? Designation { get; set; }
        public string? UserType { get; set; }
    }
}
