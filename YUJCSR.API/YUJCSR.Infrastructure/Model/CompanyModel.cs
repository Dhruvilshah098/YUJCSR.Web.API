using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class CompanyModel : ModelBase
    {
        [Key]
        public string? CompanyID { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailID { get; set; }
        public string? Website { get; set; }
        public string? ContactDetails { get; set; }

        public string? State { get; set; }
        public string? ContactPerson { get; set; }
        public decimal? AnnualCSRFund { get; set; }
        public string? Industry { get; set; }
        public string? FieldOfWork { get; set; }
        public string? PastCSRActivities { get; set; }


        public string? YearAdded { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? CommunicationRemarks { get; set; }
    }
}
