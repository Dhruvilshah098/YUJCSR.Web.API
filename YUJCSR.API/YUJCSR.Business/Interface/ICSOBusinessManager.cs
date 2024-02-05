using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;

namespace YUJCSR.Business.Interface
{
    public interface ICSOBusinessManager
    {
        Task<APIResponse> OnBoardCSO(CSORequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateCSOProfile(string csoid, CSORequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetCSOList(string approval ,CancellationToken cancellationToken);
        Task<APIResponse> GetCSOProfile(string csoId, CancellationToken cancellationToken);
        Task<APIResponse> CheckLogin(string userid,string password, CancellationToken cancellationToken);
        //Task<APIResponse> UploadDocument(string csoId, CancellationToken cancellationToken);
    }
}
