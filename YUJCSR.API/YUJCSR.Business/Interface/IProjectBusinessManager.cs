using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;

namespace YUJCSR.Business.Interface
{
    public interface IProjectBusinessManager
    {
        Task<APIResponse> SaveProject(ProjectRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProject(string projectId, ProjectRequest request, CancellationToken cancellationToken);
        Task<List<ProjectRequest>> GetProjectList( CancellationToken cancellationToken);
        Task<APIResponse> GetProjectById(string projectId, CancellationToken cancellationToken);
        #region "ProjectBudget"
        Task<APIResponse> SaveProjectBudget(string projectid, ProjectBudgetRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProjectBudget(string budgetId, ProjectBudgetRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectBudgetList(string projectid, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectBudgetById(string budgetId, CancellationToken cancellationToken);
        #endregion
        #region "ProjectMilestone"
        Task<APIResponse> SaveProjectMilestone(string projectid, ProjectMilestoneRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProjectMilestone(string milestoneid, ProjectMilestoneRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectMilestoneList(string projectid, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectMilestoneById(string milestoneid, CancellationToken cancellationToken);
        #endregion
        #region "ProjectCSOMapping"
        Task<APIResponse> SaveProjectCSOMapping(string projectid, ProjectCSOMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProjectCSOMapping(string mappingid, ProjectCSOMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectCSOMappingList(string projectid, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectCSOMappingById(string milestoneid, CancellationToken cancellationToken);
        #endregion
        #region "ProjectUNSGDMapping"
        Task<APIResponse> SaveProjectUNSGDMapping(string projectid, ProjectUNSDGMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProjectUNSGDMapping(string mappingid, ProjectUNSDGMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectUNSGDMappingList(string projectid, CancellationToken cancellationToken);
        #endregion
        #region "ProjectCSOMapping"
        Task<APIResponse> SaveProjectImapctMapping(string projectid, ProjectImapctMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> UpdateProjectImapctMapping(string mappingid, ProjectImapctMappingRequest request, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectImapctMappingList(string projectid, CancellationToken cancellationToken);
        Task<APIResponse> GetProjectImpactMappingById(string milestoneid, CancellationToken cancellationToken);
        #endregion
        Task<APIResponse> UploadProjectPhoto(string milestoneid,string projectid, IFormFile file, CancellationToken cancellationToken);
    }
}
