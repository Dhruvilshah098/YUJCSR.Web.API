using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Response
{
    public class SuccessResponse : APIResponse
    {
        public object Result { get; }

        public SuccessResponse(object result, string msg)
            : base(200)
        {
            Result = result;
            Message = msg;
            Status = true;
        }
        public SuccessResponse(object result, object resultList, string msg)
            : base(200)
        {
            Result = result;
            Message = msg;
            Status = true;
        }
    }
}
