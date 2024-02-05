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
    public class CSOBusinessManager : ICSOBusinessManager
    {
        #region "Private variables"
        private readonly IRepositoryBase<CSOModel> _baseRepository;
        private readonly ILogger<CSOBusinessManager> _logger;
        private APIResponse _response;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string? _activityId;
        #endregion
        public CSOBusinessManager(IRepositoryBase<CSOModel> baseRepository,
                            ILogger<CSOBusinessManager> logger, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activityId = _httpContextAccessor.HttpContext.Request.Headers["X-ActivityID"];
        }
        public async Task<APIResponse> GetCSOList(string approval, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<CSOModel>();
                #region "Validation"

                #endregion
                switch (approval)
                {
                    case "all":
                        list = _baseRepository.FindAll().ToList();
                        break;
                    case "pending":
                        list = _baseRepository.FindByCondition(a => a.ApprovalStatus == "pending").ToList();
                        break;
                    case "approved":
                        list = _baseRepository.FindByCondition(a => a.ApprovalStatus == "approved").ToList();
                        break;
                    default:
                        break;
                }


                _response = new SuccessResponse(list, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveExam ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }

        public async Task<APIResponse> OnBoardCSO(CSORequest request, CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var model = new CSOModel
                {
                    ActiveStatus = request.ActiveStatus,
                    CreatedBy = request.CreatedBy,
                    Address = request.Address,
                    Aim = request.Aim,
                    ContactPerson = request.ContactPerson,
                    CoreAreas = request.CoreAreas,
                    CSOID = Guid.NewGuid().ToString(),
                    CSOName = request.CSOName,
                    Description = request.Description,
                    EmailId = request.EmailId,
                    Founder = request.Founder,
                    FundRaisedLastYear = request.FundRaisedLastYear,
                    Mission = request.Mission,
                    ModifiedBy = request.ModifiedBy,
                    Objective = request.Objective,
                    OrgType = request.OrgType,
                    OtherInfo = request.OtherInfo,
                    PhoneNumber = request.PhoneNumber,
                    Vision = request.Vision,
                    Website = request.Website,
                    ApprovalStatus = "pending",
                    username = request.username,
                    password = request.password
                };

                var response = _baseRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in OnBoardCSO ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }

        public async Task<APIResponse> UpdateCSOProfile(string csoid, CSORequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.CSOID == csoid).FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse(400, "No CSOId is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.Address = string.IsNullOrEmpty(request?.Address) ? data.Address : request.Address;
                data.Aim = string.IsNullOrEmpty(request?.Aim) ? data.Aim : request.Aim;
                data.ContactPerson = string.IsNullOrEmpty(request?.ContactPerson) ? data.ContactPerson : request.ContactPerson;
                data.CoreAreas = string.IsNullOrEmpty(request?.CoreAreas) ? data.CoreAreas : request.CoreAreas;
                data.CSOName = string.IsNullOrEmpty(request?.CSOName) ? data.CSOName : request.CSOName;
                data.Description = string.IsNullOrEmpty(request?.Description) ? data.Description : request.Description;
                data.EmailId = string.IsNullOrEmpty(request?.EmailId) ? data.EmailId : request.EmailId;
                data.Founder = string.IsNullOrEmpty(request?.Founder) ? data.Founder : request.Founder;
                data.FundRaisedLastYear = request?.FundRaisedLastYear == null ? data.FundRaisedLastYear : request.FundRaisedLastYear;
                data.Mission = string.IsNullOrEmpty(request?.Mission) ? data.Mission : request.Mission;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                data.Objective = string.IsNullOrEmpty(request?.Objective) ? data.Objective : request.Objective;
                data.OrgType = string.IsNullOrEmpty(request?.OrgType) ? data.OrgType : request.OrgType;
                data.OtherInfo = string.IsNullOrEmpty(request?.OtherInfo) ? data.OtherInfo : request.OtherInfo;
                data.PhoneNumber = string.IsNullOrEmpty(request?.PhoneNumber) ? data.PhoneNumber : request.PhoneNumber;
                data.Vision = string.IsNullOrEmpty(request?.Vision) ? data.Vision : request.Vision;
                data.Website = string.IsNullOrEmpty(request?.Website) ? data.Website : request.Website;

                var response = _baseRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateCSOProfile ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetCSOProfile(string csoId, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.CSOID == csoId).FirstOrDefault();

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

        public async Task<APIResponse> CheckLogin(string userid, string password, CancellationToken cancellationToken)
        {

            try
            {
                var data = _baseRepository.FindByCondition(a => a.username == userid && a.password == password).FirstOrDefault();
                if (data !=null)
                {
                    var cso = new CSORequest()
                    {
                        CSOID = data.CSOID,
                        CSOName = data.CSOName,
                        EmailId = data.EmailId,
                        PhoneNumber = data.PhoneNumber
                    };
                    _response = new SuccessResponse(cso, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                    _response.Status = true;
                }
                else
                {
                    _response = new ErrorResponse (400,"Invalid Credential !!!", SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                    _response.Status = false;
                }
                

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in CheckLogin ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }
    }
}
