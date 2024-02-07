using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YUJCSR.Business.Interface;
using YUJCSR.Common.Interface;
using YUJCSR.Domain.Request;
using YUJCSR.Domain.Response;

namespace YUJCSR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectBusinessManager _projectBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<ProjectController> _logger;

        private readonly IConfiguration configuration;
        public ProjectController(IProjectBusinessManager projectBusinessManager, IHttpHelper httpHelper,
            IConfiguration config, ILogger<ProjectController> logger)
        {
            _projectBusinessManager = projectBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
            configuration = config;

        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> SaveProject([FromBody] ProjectRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProject(dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjects(CancellationToken cancellationtoken)
        {

            var response = await _projectBusinessManager.GetProjectList(cancellationtoken);
            return new OkObjectResult(response);
        }

        [HttpGet("{projectid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetProjectById(string projectid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(projectid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _projectBusinessManager.GetProjectById(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("{projectid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProject(string projectid, [FromBody] ProjectRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(projectid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _projectBusinessManager.UpdateProject(projectid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }

        #region "Project Budget"
        [HttpPost("{projectid}/budget")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> SaveProjectBudget(string projectid, [FromBody] ProjectBudgetRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProjectBudget(projectid, dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("{projectid}/budgets")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjectBudgetss(string projectid, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.GetProjectBudgetList(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("budgets/{budgetid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetProjectBudgetById(string budgetid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(budgetid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.GetProjectBudgetById(budgetid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("budgets/{budgetid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProjectBudget(string budgetid, [FromBody] ProjectBudgetRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(budgetid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _projectBusinessManager.UpdateProjectBudget(budgetid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        #endregion
        #region "Project Milestones"
        [HttpPost("{projectid}/milestone")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> SaveProjectMilestone(string projectid, [FromBody] ProjectMilestoneRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProjectMilestone(projectid, dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("{projectid}/milestones")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjectMilestones(string projectid, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.GetProjectMilestoneList(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("milestone/{milestoneid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetProjectMilestoneById(string milestoneid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(milestoneid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.GetProjectMilestoneById(milestoneid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("milestone/{milestoneid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProjectMilestone(string milestoneid, [FromBody] ProjectMilestoneRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(milestoneid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _projectBusinessManager.UpdateProjectMilestone(milestoneid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        #endregion
        #region "Project CSO Mapping"
        [HttpPost("csomapping/{projectid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> MapProjectCSO(string projectid, [FromBody] ProjectCSOMappingRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProjectCSOMapping(projectid, dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("csomapping/{projectid}/csos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjectCSOs(string projectid, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.GetProjectCSOMappingList(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("csomapping/{mappingid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetProjectCSOMappingById(string mappingid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(mappingid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.GetProjectCSOMappingById(mappingid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("csomapping/{mappingid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProjectCSOMapping(string mappingid, [FromBody] ProjectCSOMappingRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(mappingid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.UpdateProjectCSOMapping(mappingid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        #endregion
        #region "Photo Upload"
        [HttpPost("milestone/{milestoneid}/photo")]
        // [Consumes("application/json", "multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UploadMilestonePhoto(string milestoneid, string projectid, string doctypeid, IFormFile photo, CancellationToken cancellationtoken)
        {

            try
            {
                _projectBusinessManager.UploadProjectPhoto(milestoneid, projectid, doctypeid, photo, cancellationtoken);

            }
            catch (Exception ex)
            {

                throw;
            }

            if (string.IsNullOrEmpty(milestoneid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            //var response = await _projectBusinessManager.UpdateProjectCSOMapping(mappingid, request, cancellationtoken);
            return _httpHelper.HandleResponse(new APIResponse(200) { Status = true, Message = "Photo uploaded" });
        }
        [HttpPost("project/{projectid}/photo")]
        // [Consumes("application/json", "multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UploadProjectPhoto(string projectid, string doctypeid, IFormFile photo, CancellationToken cancellationtoken)
        {

            try
            {
                _projectBusinessManager.UploadProjectDocument(projectid, doctypeid, photo, cancellationtoken);

            }
            catch (Exception ex)
            {

                throw;
            }

            //var response = await _projectBusinessManager.UpdateProjectCSOMapping(mappingid, request, cancellationtoken);
            return _httpHelper.HandleResponse(new APIResponse(200) { Status = true, Message = "Photo uploaded" });
        }

        #endregion
        #region "Project UNSDG Mapping"
        [HttpPost("UNSDG/{projectid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> MapProjectUNSDG(string projectid, [FromBody] ProjectUNSDGMappingRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProjectUNSGDMapping(projectid, dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("UNSDG/{projectid}/unsdgs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjectUNSDG(string projectid, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.GetProjectUNSGDMappingList(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }

        [HttpPost("UNSDG/{mappingid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProjectUNSDGMapping(string mappingid, [FromBody] ProjectUNSDGMappingRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(mappingid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.UpdateProjectUNSGDMapping(mappingid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        #endregion
        #region "Project Impact Mapping"
        [HttpPost("{projectid}/impact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> MapProjectImpact(string projectid, [FromBody] ProjectImapctMappingRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.SaveProjectImapctMapping(projectid, dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("impact/{projectid}/impact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllProjectImpacts(string projectid, CancellationToken cancellationtoken)
        {
            var response = await _projectBusinessManager.GetProjectImapctMappingList(projectid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("impact/{mappingid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetProjectImpactMappingById(string mappingid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(mappingid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.GetProjectImpactMappingById(mappingid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("impact/{mappingid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> UpdateProjectimpactMapping(string mappingid, [FromBody] ProjectImapctMappingRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(mappingid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _projectBusinessManager.UpdateProjectImapctMapping(mappingid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        #endregion
    }
}
