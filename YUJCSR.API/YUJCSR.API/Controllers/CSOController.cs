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
    public class CSOController : ControllerBase
    {
        private readonly ICSOBusinessManager _csoBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<CSOController> _logger;

        public CSOController(ICSOBusinessManager csoBusinessManager, IHttpHelper httpHelper,
            ILogger<CSOController> logger)
        {
            _csoBusinessManager = csoBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
        }

        [HttpPost("onboard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> OnBoardCSO([FromBody] CSORequest dto, CancellationToken cancellationtoken)
        {
            var response = await _csoBusinessManager.OnBoardCSO(dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllAdminUsers(string approval, CancellationToken cancellationtoken)
        {
            string[] status = { "all", "approved", "pending" };
            if (!status.Contains(approval))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            var response = await _csoBusinessManager.GetCSOList(approval, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet("{csoid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetCSOProfile(string csoid, CancellationToken cancellationtoken)
        {
         
            if (string.IsNullOrEmpty(csoid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }
            
            var response = await _csoBusinessManager.GetCSOProfile(csoid, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpPost("{csoid}/profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> updateCSOProfile(string csoid,[FromBody] CSORequest request, CancellationToken cancellationtoken)
        {

            if (string.IsNullOrEmpty(csoid))
            {
                return new BadRequestObjectResult("Invalid paramer");
            }

            var response = await _csoBusinessManager.UpdateCSOProfile(csoid, request, cancellationtoken);
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

            var response = await _csoBusinessManager.CheckLogin(request.UserName,request.Password, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }


    }
}
