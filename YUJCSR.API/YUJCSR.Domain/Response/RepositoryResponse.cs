using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Response
{
    public class RepositoryResponse :BaseResponse
    {
        public object data { get; set; }
        public object dataList { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
