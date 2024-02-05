using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Response;

namespace YUJCSR.Business.Interface
{
    public interface IUNSDGBusinessManager
    {
        Task<APIResponse> GetUSDGoals(CancellationToken cancellationToken);
    }
}
