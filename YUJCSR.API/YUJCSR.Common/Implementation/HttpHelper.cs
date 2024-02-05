using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Common.Interface;
using YUJCSR.Domain.Response;

namespace YUJCSR.Common.Implementation
{
    public class HttpHelper : IHttpHelper
    {
        public IActionResult HandleResponse(APIResponse response)
        {
            switch (response.StatusCode)
            {
                case 200:
                    return new OkObjectResult(response);
                case 400:
                    return new BadRequestObjectResult(response);
                case 500:
                    return new InternalServerErrorObjectResult(response);
                default:
                    return new InternalServerErrorObjectResult(response);
            }
        }

       
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = 500;
        }
    }
}