using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Infrastructure.Model
{
    public class ProjectUNSDGMappingModel : ModelBase
    {

        [Key]
        public string? MappingID { get; set; }
        public string? ProjectID { get; set; }
        public string? UNSGDID { get; set; }
    }
}
