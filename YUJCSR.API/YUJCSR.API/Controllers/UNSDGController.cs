using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YUJCSR.Business.Interface;
using YUJCSR.Common.Interface;
using YUJCSR.Domain.Response;

namespace YUJCSR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UNSDGController : ControllerBase
    {
        private readonly IUNSDGBusinessManager _unsdgBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<AdminUserController> _logger;
        public UNSDGController(IUNSDGBusinessManager unsdgBusinessManager, IHttpHelper httpHelper,
            ILogger<AdminUserController> logger)
        {
            _unsdgBusinessManager = unsdgBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllAdminUsers(CancellationToken cancellationtoken)
        {

            var response = await _unsdgBusinessManager.GetUSDGoals(cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
    }
}
