using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;

namespace YUJCSR.Business.Interface
{
    public interface IAdminUserBusinessManager
    {
        Task<APIResponse> SaveAdminUser(AdminUserRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetAdminUsers( CancellationToken cancellationToken);
    }
}


