using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUJCSR.Domain.Response
{
    public class ErrorResponse : APIResponse
    {
        public string _errorCode;
        public string _errorMsg;
        public string _errorType;
        public ErrorResponse(int errorCode, string errorMsg, string errorType) :base(errorCode)
        {
            _errorCode = errorCode.ToString();
            _errorMsg = errorMsg;
            _errorType = errorType;
            Message = errorMsg;
        }
    }
}
