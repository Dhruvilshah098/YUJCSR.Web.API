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
    public class ProjectBusinessManager : IProjectBusinessManager
    {

        #region "Private variables"
        private readonly IRepositoryBase<ProjectModel> _baseRepository;
        private readonly IRepositoryBase<ProjectBudgetModel> _budgetRepository;
        private readonly IRepositoryBase<ProjectMilestoneModel> _milestoneRepository;
        private readonly IRepositoryBase<ProjectCSOMappingModel> _csoMappingRepository;
        private readonly IRepositoryBase<ProjectUNSDGMappingModel> _unsdgMappingRepository;
        private readonly IRepositoryBase<ProjectImpactModel> _impactMappingRepository;
        private readonly IRepositoryBase<ProjectDocModel> _projectDocRepository;

        private readonly ILogger<ProjectBusinessManager> _logger;
        private APIResponse _response;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string photoUploadPath = string.Empty;
        private readonly string? _activityId;
        #endregion
        public ProjectBusinessManager(IRepositoryBase<ProjectModel> baseRepository, IRepositoryBase<ProjectBudgetModel> budgetRepository,
                     IRepositoryBase<ProjectMilestoneModel> milestoneRepository, IRepositoryBase<ProjectCSOMappingModel> csoMappingRepository,
                     IRepositoryBase<ProjectImpactModel> impactMappingRepository, IRepositoryBase<ProjectDocModel> projectDocRepository,
                            IRepositoryBase<ProjectUNSDGMappingModel> unsdgMappingRepository, ILogger<ProjectBusinessManager> logger, IHttpContextAccessor httpContextAccessor)
        {
            _baseRepository = baseRepository;
            _budgetRepository = budgetRepository;
            _milestoneRepository = milestoneRepository;
            _csoMappingRepository = csoMappingRepository;
            _unsdgMappingRepository = unsdgMappingRepository;
            _impactMappingRepository = impactMappingRepository;
            _projectDocRepository = projectDocRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _activityId = _httpContextAccessor.HttpContext.Request.Headers["X-ActivityID"];
            photoUploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PROJECT_DOCS");
        }
        public async Task<APIResponse> GetProjectById(string projectId, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.ProjectID == projectId).FirstOrDefault();
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
        public async Task<List<ProjectRequest>> GetProjectList(CancellationToken cancellationToken)
        {
            var data = new List<ProjectRequest>();
            try
            {
                var list = new List<ProjectModel>();
                list = _baseRepository.FindAll().ToList();
                foreach (var item in list)
                {
                    data.Add(new ProjectRequest
                    {
                        ActiveStatus = item.ActiveStatus,
                        Amenities = item.Amenities,
                        AreOfInterest = item.AreOfInterest,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        DevelopmentGoal = item.DevelopmentGoal,
                        DurationInMonths = item.DurationInMonths,
                        ExpectedOutcome = item.ExpectedOutcome,
                        Experts = item.Experts,
                        Location = item.Location,
                        ModifiedBy = item.ModifiedBy,
                        ModifiedDate = item.ModifiedDate,
                        ProjectDescription = item.ProjectDescription,
                        ProjectID = item.ProjectID,
                        RefID = item.RefID,
                        Title = item.Title,
                        TotalBudget = item.TotalBudget

                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveExam ", ex);

            }
            return data;
        }
        public async Task<APIResponse> SaveProject(ProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var model = new ProjectModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    Amenities = request.Amenities,
                    AreOfInterest = request.AreOfInterest,
                    DevelopmentGoal = request.DevelopmentGoal,
                    DurationInMonths = request.DurationInMonths,
                    ExpectedOutcome = request.ExpectedOutcome,
                    Experts = request.Experts,
                    Location = request.Location,
                    ModifiedBy = request.ModifiedBy,
                    ProjectDescription = request.ProjectDescription,
                    ProjectID = Guid.NewGuid().ToString(),
                    RefID = request.RefID,
                    Title = request.Title,
                    TotalBudget = request.TotalBudget,
                    ProjectType = request.ProjectType
                };

                var response = _baseRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProject ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }
        public async Task<APIResponse> UpdateProject(string projectId, ProjectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _baseRepository.FindByCondition(a => a.ProjectID == projectId).FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse(400, "No Project is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.Amenities = string.IsNullOrEmpty(request?.Amenities) ? data.Amenities : request.Amenities;
                data.AreOfInterest = string.IsNullOrEmpty(request?.AreOfInterest) ? data.AreOfInterest : request.AreOfInterest;
                data.DevelopmentGoal = string.IsNullOrEmpty(request?.DevelopmentGoal) ? data.DevelopmentGoal : request.DevelopmentGoal;
                data.DurationInMonths = request?.DurationInMonths == null ? data.DurationInMonths : request.DurationInMonths;
                data.ExpectedOutcome = request?.ExpectedOutcome == null ? data.ExpectedOutcome : request.ExpectedOutcome;
                data.Experts = string.IsNullOrEmpty(request?.Experts) ? data.Experts : request.Experts;
                data.Location = string.IsNullOrEmpty(request?.Location) ? data.Location : request.Location;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;

                data.ProjectDescription = string.IsNullOrEmpty(request?.ProjectDescription) ? data.ProjectDescription : request.ProjectDescription;
                data.RefID = string.IsNullOrEmpty(request?.RefID) ? data.RefID : request.RefID;
                data.Title = string.IsNullOrEmpty(request?.Title) ? data.Title : request.Title;
                data.TotalBudget = request?.TotalBudget == null ? data.TotalBudget : request.TotalBudget;

                var response = _baseRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProject ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }
       
        #region "ProjectBudget"
        public async Task<APIResponse> SaveProjectBudget(string projectid, ProjectBudgetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                #region "Validation"

                #endregion
                var model = new ProjectBudgetModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    Amount = request.Amount,
                    BudgetID = Guid.NewGuid().ToString(),
                    Description = request.Description,
                    Milestone = request.Milestone,
                    ModifiedBy = request.ModifiedBy,
                    ProjectID = projectid
                };

                var response = _budgetRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProject ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }

            return _response;
        }
        public async Task<APIResponse> UpdateProjectBudget(string budgetId, ProjectBudgetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _budgetRepository.FindByCondition(a => a.BudgetID == budgetId).FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse(400, "No Project Budget is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.ProjectID = String.IsNullOrEmpty(request?.ProjectID) ? data.ProjectID : request.ProjectID;
                data.Milestone = String.IsNullOrEmpty(request?.Milestone) ? data.Milestone : request.Milestone;
                data.Amount = request?.Amount == null ? data.Amount : request.Amount;
                data.Description = String.IsNullOrEmpty(request?.Description) ? data.Description : request.Description;
                data.CreatedBy = String.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.CreatedBy;
                data.ModifiedBy = String.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;

                var response = _budgetRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;

            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProjectBudget ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectBudgetList(string projectid, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<ProjectBudgetModel>();
                #region "Validation"

                #endregion
                list = _budgetRepository.FindByCondition(a => a.ProjectID == projectid).ToList();

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

        public async Task<APIResponse> GetProjectBudgetById(string budgetId, CancellationToken cancellationToken)
        {
            try
            {
                var data = _budgetRepository.FindByCondition(a => a.BudgetID == budgetId).FirstOrDefault();
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
        #endregion
        #region "ProjectMilestone"
        public async Task<APIResponse> SaveProjectMilestone(string projectid, ProjectMilestoneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new ProjectMilestoneModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    Description = request.Description,
                    MilestoneID = Guid.NewGuid().ToString(),
                    MilestoneName = request.MilestoneName,
                    ModifiedBy = request.ModifiedBy,
                    ProjectID = projectid
                };
                var response = _milestoneRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProjectMilestone ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> UpdateProjectMilestone(string milestoneid, ProjectMilestoneRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _milestoneRepository.FindByCondition(a => a.MilestoneID == milestoneid).FirstOrDefault();

                if (data == null)
                {
                    return new APIResponse(400, "No Milestone is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                data.ProjectID = string.IsNullOrEmpty(request?.ProjectID) ? data.ProjectID : request.ProjectID;
                data.MilestoneName = string.IsNullOrEmpty(request?.MilestoneName) ? data.MilestoneName : request.MilestoneName;
                data.Description = string.IsNullOrEmpty(request?.Description) ? data.Description : request.Description;
                data.CreatedBy = string.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.CreatedBy;
                var response = _milestoneRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProject ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectMilestoneList(string projectid, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<ProjectMilestoneModel>();
                list = _milestoneRepository.FindAll().ToList();
                _response = new SuccessResponse(list, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetProjectMilestoneList ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectMilestoneById(string milestoneid, CancellationToken cancellationToken)
        {
            try
            {
                var data = _milestoneRepository.FindByCondition(a => a.MilestoneID == milestoneid).FirstOrDefault();

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
        #endregion
        #region "ProjectCSOMapping"
        public async Task<APIResponse> SaveProjectCSOMapping(string projectid, ProjectCSOMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new ProjectCSOMappingModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    ProjectCSOMappingID = Guid.NewGuid().ToString(),
                    CSOID = request.CSOID,
                    ModifiedBy = request.ModifiedBy,
                    ProjectID = projectid
                };
                var response = _csoMappingRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProjectCSOMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> UpdateProjectCSOMapping(string mappingid, ProjectCSOMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _csoMappingRepository.FindByCondition(a => a.ProjectCSOMappingID == mappingid).FirstOrDefault();
                if (data == null)
                {
                    return new APIResponse(400, "No Milestone is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                data.ProjectID = string.IsNullOrEmpty(request?.ProjectID) ? data.ProjectID : request.ProjectID;
                data.CSOID = string.IsNullOrEmpty(request?.CSOID) ? data.CSOID : request.CSOID;
                data.CreatedBy = string.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.CreatedBy;
                var response = _csoMappingRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProjectCSOMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectCSOMappingList(string projectid, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<ProjectCSOMappingModel>();
                list = _csoMappingRepository.FindAll().ToList();
                _response = new SuccessResponse(list, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetProjectMilestoneList ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectCSOMappingById(string mappingid, CancellationToken cancellationToken)
        {
            try
            {
                var data = _csoMappingRepository.FindByCondition(a => a.ProjectCSOMappingID == mappingid).FirstOrDefault();

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

        public async Task<APIResponse> UploadProjectPhoto(string milestoneid, string projectid, string doctypeid, IFormFile photo, CancellationToken cancellationToken)
        {
            try
            {
                #region "Data validation"
                var milestone = _milestoneRepository.FindByCondition(a => a.MilestoneID == milestoneid).FirstOrDefault();
                if (milestone == null)
                {
                    return new APIResponse(400, "Milestone not valid");
                }
                #endregion
                #region "Photo Upload DB"


                #endregion
                #region "Physical Save"
                if (photo != null && photo.Length > 0)
                {
                    #region "Check and Create Project path"
                    //Folder structure  - PROJECT_DOCS -> ProjectID- > Milestones -> MilestoneID -> fileName
                    string milestonesPath = Path.Combine(photoUploadPath, "MILESTONES");
                    string projectPath = Path.Combine(milestonesPath, projectid);
                    if (!Directory.Exists(projectPath))
                    {
                        Directory.CreateDirectory(projectPath);
                    }
                    string milestonePath = Path.Combine(projectPath, milestoneid);
                    if (!Directory.Exists(milestonePath))
                    {
                        Directory.CreateDirectory(milestonePath);
                    }
                    #endregion

                    string photoExtension = Path.GetExtension(photo.FileName);
                    string SystemFileName = Guid.NewGuid().ToString() + photoExtension;
                    string originalFileName = photo.FileName;

                    string fullPath = Path.Combine(milestonePath, SystemFileName);
                    using (var newstream = new FileStream(fullPath, FileMode.Create))
                    {
                        photo.CopyTo(newstream);
                    }
                    #region "DB Save"
                    _projectDocRepository.Save(new ProjectDocModel
                    {
                        ActiveStatus = true,
                        CreatedBy = "system",
                        DcoTypeID = doctypeid,
                        DocID = Guid.NewGuid().ToString(),
                        FileExtension = photoExtension,
                        ModifiedBy = "system",
                        OriginalFileName = originalFileName,
                        ProjectID = projectid,
                        SystemFileName = SystemFileName
                    });
                    #endregion

                }
                #endregion

                _response = new SuccessResponse("Photo uploaded", SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {


            }
            return _response;
        }



        #endregion
        #region "Project UNSDG "
        public async Task<APIResponse> SaveProjectUNSGDMapping(string projectid, ProjectUNSDGMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new ProjectUNSDGMappingModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    MappingID = Guid.NewGuid().ToString(),
                    UNSGDID = request.UNSGDID,
                    ModifiedBy = request.ModifiedBy,
                    ProjectID = projectid
                };
                var response = _unsdgMappingRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProjectCSOMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> UpdateProjectUNSGDMapping(string mappingid, ProjectUNSDGMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _unsdgMappingRepository.FindByCondition(a => a.MappingID == mappingid).FirstOrDefault();
                if (data == null)
                {
                    return new APIResponse(400, "No UNSDG Mapping is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                data.ProjectID = string.IsNullOrEmpty(request?.ProjectID) ? data.ProjectID : request.ProjectID;
                data.UNSGDID = string.IsNullOrEmpty(request?.UNSGDID) ? data.UNSGDID : request.UNSGDID;
                data.CreatedBy = string.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.CreatedBy;
                var response = _unsdgMappingRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProjectUNSGDMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectUNSGDMappingList(string projectid, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<ProjectUNSDGMappingModel>();
                list = _unsdgMappingRepository.FindByCondition(a => a.ProjectID == projectid).ToList();
                _response = new SuccessResponse(list, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetProjectUNSGDMappingList ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }
        #endregion
        #region "Project Imapct Mapping"
        public async Task<APIResponse> SaveProjectImapctMapping(string projectid, ProjectImapctMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var model = new ProjectImpactModel
                {
                    ActiveStatus = true,
                    CreatedBy = request.CreatedBy,
                    ImpactMappingID = Guid.NewGuid().ToString(),
                    ImpactID = request.ImpactID,
                    ModifiedBy = request.ModifiedBy,
                    ProjectID = projectid,
                    Stage1 = request.Stage1,
                    Stage2 = request.Stage2,
                    Stage3 = request.Stage3,
                    Stage4 = request.Stage4
                };
                var response = _impactMappingRepository.Save(model);
                _response = response.Status ? new SuccessResponse(model, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in SaveProjectCSOMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> UpdateProjectImapctMapping(string mappingid, ProjectImapctMappingRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _impactMappingRepository.FindByCondition(a => a.ImpactMappingID == mappingid).FirstOrDefault();
                if (data == null)
                {
                    return new APIResponse(400, "No Milestone is available ");
                }
                data.ActiveStatus = request?.ActiveStatus == null ? data.ActiveStatus : request.ActiveStatus;
                data.ModifiedBy = string.IsNullOrEmpty(request?.ModifiedBy) ? data.ModifiedBy : request.ModifiedBy;
                data.ProjectID = string.IsNullOrEmpty(request?.ProjectID) ? data.ProjectID : request.ProjectID;
                data.ImpactID = string.IsNullOrEmpty(request?.ImpactID) ? data.ImpactID : request.ImpactID;
                data.Stage1 = string.IsNullOrEmpty(request?.Stage1) ? data.Stage1 : request.Stage1;
                data.Stage2 = string.IsNullOrEmpty(request?.Stage2) ? data.Stage2 : request.Stage2;
                data.Stage3 = string.IsNullOrEmpty(request?.Stage3) ? data.Stage3 : request.Stage3;
                data.Stage4 = string.IsNullOrEmpty(request?.Stage4) ? data.Stage4 : request.Stage4;
                data.CreatedBy = string.IsNullOrEmpty(request?.CreatedBy) ? data.CreatedBy : request.CreatedBy;
                var response = _impactMappingRepository.Update(data);
                _response = response.Status ? new SuccessResponse(data, SuccessMessage.Exam_Save) : new ErrorResponse(Convert.ToInt32(response.Error._errorCode), response.Error._errorMsg, response.Error._errorType);
                _response.Status = response.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in UpdateProjectImapctMapping ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectImapctMappingList(string projectid, CancellationToken cancellationToken)
        {
            try
            {
                var list = new List<ProjectImpactModel>();
                list = _impactMappingRepository.FindAll().ToList();
                _response = new SuccessResponse(list, SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(" : Error in GetProjectMilestoneList ", ex);
                _response = new ErrorResponse(500, ErrorMessage.Save_Message, ErrorType.ServerError);
            }
            return _response;
        }

        public async Task<APIResponse> GetProjectImpactMappingById(string mappingId, CancellationToken cancellationToken)
        {
            try
            {
                var data = _impactMappingRepository.FindByCondition(a => a.ImpactMappingID == mappingId).FirstOrDefault();

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

        public async Task<APIResponse> UploadProjectDocument(string projectid, string doctypeid, IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                #region "Data validation"
                var milestone = _baseRepository.FindByCondition(a => a.ProjectID == projectid).FirstOrDefault();
                if (milestone == null)
                {
                    return new APIResponse(400, "projectid not valid");
                }
                #endregion
                #region "Photo Upload DB"


                #endregion
                #region "Physical Save"
                if (file != null && file.Length > 0)
                {
                    #region "Check and Create Project path"
                    //Folder structure  - PROJECT_DOCS -> ProjectID -> fileName
                    string projectPath = Path.Combine(photoUploadPath, projectid);
                    if (!Directory.Exists(projectPath))
                    {
                        Directory.CreateDirectory(projectPath);
                    }                   
                    #endregion

                    string photoExtension = Path.GetExtension(file.FileName);
                    string SystemFileName = Guid.NewGuid().ToString() + photoExtension;
                    string originalFileName = file.FileName;

                    string fullPath = Path.Combine(projectPath, SystemFileName);
                    string absPath = Path.Combine("PROJECT_DOCS", projectid,SystemFileName);
                    using (var newstream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(newstream);
                    }
                    #region "DB Save"
                    _projectDocRepository.Save(new ProjectDocModel
                    {
                        ActiveStatus = true,
                        CreatedBy = "system",
                        DcoTypeID = doctypeid,
                        DocID = Guid.NewGuid().ToString(),
                        FileExtension = photoExtension,
                        ModifiedBy = "system",
                        OriginalFileName = originalFileName,
                        ProjectID = projectid,
                        SystemFileName = SystemFileName,
                        FilePath = absPath
                    }); ;
                    #endregion

                }
                #endregion

                _response = new SuccessResponse("Photo uploaded", SuccessMessage.Record_Found_Message);//: new ErrorResponse(500, "internal server error", "");
                _response.Status = true;
            }
            catch (Exception ex)
            {


            }
            return _response;
        }
        #endregion


    }
}
