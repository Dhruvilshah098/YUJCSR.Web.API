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
    public class CompanyBusinessManager : ICompanyBusinessManager
    {
        #region "Private variables"
        private readonly IRepositoryBase<CompanyModel> _baseRepository;
        private readonly ILogger<CompanyBusinessManager> _logger;
        private APIResponse _response;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string? _activityId;
        #endregion
        public CompanyBusinessManager(IRepositoryBase<CompanyModel> baseRepository,
                            ILogger<CompanyBusinessManager> logger, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activityId = _httpContextAccessor.HttpContext.Request.Headers["X-ActivityID"];
        }
        public async Task<APIResponse> CheckLogin(string userid, string password, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.UserName == userid && a.Password == password).FirstOrDefault();
                if (data != null)
                {
                    var cso = new CompanyRequest()
                    {
                        CompanyID = data.CompanyID,
                        CompanyName = data.CompanyName,
                        EmailID = data.EmailID,
                        ContactPerson = data.ContactPerson,
                        ContactDetails = data.ContactDetails
                    };
                    _response = new SuccessResponse(cso, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                    _response.Status = true;
                }
                else
                {
                    _response = new ErrorResponse(400, "Invalid Credential !!!", SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
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

        public async Task<APIResponse> GetCompanyList(string approval, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<CompanyModel>();
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

        public async Task<APIResponse> GetCompanyProfile(string companyId, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.CompanyID == companyId).FirstOrDefault();

                _response = new SuccessResponse(data, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetCompanyProfile ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }

        public async Task<APIResponse> OnBoardCompany(CompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var model = new CompanyModel
                {
                    ActiveStatus = request.ActiveStatus,
                    CreatedBy = request.CreatedBy,
                    ContactDetails = request.ContactDetails,
                    
                    ContactPerson = request.ContactPerson,
                    CompanyID = Guid.NewGuid().ToString(),
                    CompanyName = request.CompanyName,
                    CompanyCode = request.CompanyCode,
                    CommunicationRemarks = request.CommunicationRemarks,
                    EmailID = request.EmailID,                  
                    Website = request.Website,
                    ApprovalStatus = "pending",
                    UserName = request.UserName,
                    Password = request.Password,
                    State = request.State,
                    AnnualCSRFund = request.AnnualCSRFund,
                    AppointmentDate = request.AppointmentDate,
                    FieldOfWork = request.FieldOfWork,
                    Industry = request.Industry,
                    PastCSRActivities = request.PastCSRActivities,
                    YearAdded = request.YearAdded,
                    ModifiedBy = request.ModifiedBy
                };

                var response = _baseRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in OnBoardCompany ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }

        public async Task<APIResponse> UpdateCompanyProfile(string companyid, CompanyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.CompanyID == companyid).FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse(400, "No companyid is available ");
                }
                
                data.CompanyCode = string.IsNullOrEmpty(request?.CompanyCode) ? data.CompanyCode : request.CompanyCode;
                data.CompanyName = string.IsNullOrEmpty(request?.CompanyName) ? data.CompanyName : request.CompanyName;
                data.EmailID = string.IsNullOrEmpty(request?.EmailID) ? data.EmailID : request.EmailID;
                data.Website = string.IsNullOrEmpty(request?.Website) ? data.Website : request.Website;
                data.ContactDetails = string.IsNullOrEmpty(request?.ContactDetails) ? data.ContactDetails : request.ContactDetails;
                data.State = string.IsNullOrEmpty(request?.State) ? data.State : request.State;
                data.ContactPerson = string.IsNullOrEmpty(request?.ContactPerson) ? data.ContactPerson : request.ContactPerson;
                data.AnnualCSRFund = request?.AnnualCSRFund == null ? data.AnnualCSRFund : request.AnnualCSRFund;
                data.Industry = string.IsNullOrEmpty(request?.Industry) ? data.Industry : request.Industry;
                data.FieldOfWork = string.IsNullOrEmpty(request?.FieldOfWork) ? data.FieldOfWork : request.FieldOfWork;
                data.PastCSRActivities = string.IsNullOrEmpty(request?.PastCSRActivities) ? data.PastCSRActivities : request.PastCSRActivities;
                data.YearAdded = string.IsNullOrEmpty(request?.YearAdded ) ? data.YearAdded : request.YearAdded;
                data.AppointmentDate = request?.AppointmentDate == null ? data.AppointmentDate : request.AppointmentDate;
                data.UserName = string.IsNullOrEmpty(request?.UserName) ? data.UserName : request.UserName;
                data.Password = string.IsNullOrEmpty(request?.Password) ? data.Password : request.Password;
                data.ApprovalStatus = string.IsNullOrEmpty(request?.ApprovalStatus) ? data.ApprovalStatus : request.ApprovalStatus;
                data.CommunicationRemarks = string.IsNullOrEmpty(request?.CommunicationRemarks) ? data.CommunicationRemarks : request.CommunicationRemarks;
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.CreatedBy = string.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.Website;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                var response = _baseRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateCompanyProfile ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }
    }
}
