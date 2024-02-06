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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusinessManager _companyBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<CSOController> _logger;

        public CompanyController(ICompanyBusinessManager companyBusinessManager, IHttpHelper httpHelper,
            ILogger<CSOController> logger)
        {
            _companyBusinessManager = companyBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
        }

        [HttpPost("onboard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> OnBoardCompany([FromBody] CompanyRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _companyBusinessManager.OnBoardCompany(dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllCompanies(string approval, CancellationToken cancellationtoken)
        {
            string[] status = { "all", "approved", "pending" };
            if (!status.Contains(approval))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _companyBusinessManager.GetCompanyList(approval, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("{companyid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetCSOProfile(string companyid, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(companyid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _companyBusinessManager.GetCompanyProfile(companyid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("{companyid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> updateCSOProfile(string companyid, [FromBody] CompanyRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(companyid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _companyBusinessManager.UpdateCompanyProfile(companyid, request, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> CheckLogin([FromBody] LoginRequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _companyBusinessManager.CheckLogin(request.UserName, request.Password, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }

    }
}
