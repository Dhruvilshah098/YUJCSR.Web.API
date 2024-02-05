using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Business.Interface;
using YUJCSR.Common.Constants;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;
using YUJCSR.Infrastructure.Model;
using YUJCSR.Infrastructure.Repositories.Interface;

namespace YUJCSR.Business.Implementation
{
    public class AdminUserBusinessManager : IAdminUserBusinessManager
    {
        #region "Private variables"
        private readonly IRepositoryBase<AdminUserModel> _baseRepository;
        private readonly ILogger<AdminUserBusinessManager> _logger;
        private APIResponse _response;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string? _activityId;
        #endregion
        public AdminUserBusinessManager(IRepositoryBase<AdminUserModel> baseRepository,
                            ILogger<AdminUserBusinessManager> logger, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activityId = _httpContextAccessor.HttpContext.Request.Headers["X-ActivityID"];
        }
        public async Task<APIResponse> SaveAdminUser(AdminUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"
                
                #endregion
                var model = new AdminUserModel
                {
                    ActiveStatus = request.ActiveStatus,                    
                    CreatedBy = request.CreatedBy,       
                    UserID = Guid.NewGuid().ToString(),
                    Designation = request.Designation,
                    EmailID = request.EmailID,
                    LoginID = request.LoginID,
                    MobileNumber = request.MobileNumber,
                    ModifiedBy = request.ModifiedBy,
                    Password = request.Password,
                    UserName = request.UserName,
                    UserType = request.UserType
                };

                var response = _baseRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveExam ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }

        public async Task<APIResponse> GetAdminUsers( CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var data = _baseRepository.FindAll();

                _response = new SuccessResponse(data, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveExam ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }
    }
}
