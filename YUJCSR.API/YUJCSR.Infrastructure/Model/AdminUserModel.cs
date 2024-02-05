using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class AdminUserModel : ModelBase
    {
        [Key]
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
