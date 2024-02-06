using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;

namespace YUJCSR.Business.Interface
{
    public interface ICompanyBusinessManager
    {
        Task<APIResponse> OnBoardCompany(CompanyRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateCompanyProfile(string companyid, CompanyRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetCompanyList(string approval, CancellationToken cancellationToken);
        Task<APIResponse> GetCompanyProfile(string companyId, CancellationToken cancellationToken);
        Task<APIResponse> CheckLogin(string userid, string password, CancellationToken cancellationToken);
    }
}
