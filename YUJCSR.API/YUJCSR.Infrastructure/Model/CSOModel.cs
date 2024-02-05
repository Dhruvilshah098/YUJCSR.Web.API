using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class CSOModel : ModelBase
    {
        [Key]
        public string? CSOID { get; set; }
        public string? CSOName { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? OrgType { get; set; }
        public string? Website { get; set; }
        public string? Founder { get; set; }
        public string? ContactPerson { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Aim { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? Objective { get; set; }
        public string? CoreAreas { get; set; }
        public decimal? FundRaisedLastYear { get; set; }
        public string? OtherInfo { get; set; }
        public string? ApprovalStatus { get; set; }

        public string? username { get; set; }
        public string? password { get; set; }
    }
}
