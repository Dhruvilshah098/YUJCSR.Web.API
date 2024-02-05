using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectModel :ModelBase
    {
        [Key]
        public string? ProjectID { get; set; }
        public string? RefID { get; set; }
        public string? Title { get; set; }
        public string? AreOfInterest { get; set; }
        public string? DevelopmentGoal { get; set; }
        public string? ProjectDescription { get; set; }
        public string? Location { get; set; }
        public decimal? TotalBudget { get; set; }
        public decimal? DurationInMonths { get; set; }
        public string? ExpectedOutcome { get; set; }
        public string? Amenities { get; set; }
        public string? Experts { get; set; }
        public string? ProjectType { get; set; }
}
}
