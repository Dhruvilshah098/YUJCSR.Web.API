using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Response;

namespace YUJCSR.Common.Interface
{
    public interface IHttpHelper
    {
        IActionResult HandleResponse(APIResponse response);
    }
}
