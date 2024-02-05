using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Business.Interface;
using YUJCSR.Common.Constants;
using YUJCSR.Domain.Response;
using YUJCSR.Infrastructure.Model;
using YUJCSR.Infrastructure.Repositories.Interface;

namespace YUJCSR.Business.Implementation
{
    public class UNSDGBusinessManager : IUNSDGBusinessManager
    {
        #region "Private variables"
        private readonly IRepositoryBase<UNSDGModel> _baseRepository;
        private readonly ILogger<AdminUserBusinessManager> _logger;
        private APIResponse _response;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string? _activityId;
        #endregion
        public UNSDGBusinessManager(IRepositoryBase<UNSDGModel> baseRepository,
                            ILogger<AdminUserBusinessManager> logger, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activityId = _httpContextAccessor.HttpContext.Request.Headers["X-ActivityID"];
        }
        public async Task<APIResponse> GetUSDGoals(CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var data = _baseRepository.FindAll().OrderBy(a => a.UNSDGNumber);

                _response = new SuccessResponse(data, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetUSDGoals ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }
    }
}
