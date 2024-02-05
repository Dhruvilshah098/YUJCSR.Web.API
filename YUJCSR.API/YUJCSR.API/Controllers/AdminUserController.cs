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
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserBusinessManager _adminUserBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<AdminUserController> _logger;
        public AdminUserController(IAdminUserBusinessManager adminUserBusinessManager, IHttpHelper httpHelper,
            ILogger<AdminUserController> logger)
        {
            _adminUserBusinessManager = adminUserBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> SaveAdminUser([FromBody] AdminUserRequest dto, CancellationToken cancellationtoken)
        {
            var response = await _adminUserBusinessManager.SaveAdminUser(dto, cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllAdminUsers(CancellationToken cancellationtoken)
        {

            var response = await _adminUserBusinessManager.GetAdminUsers(cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
    }
}
