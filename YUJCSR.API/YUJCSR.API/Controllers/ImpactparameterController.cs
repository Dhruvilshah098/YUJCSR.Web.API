using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YUJCSR.Business.Interface;
using YUJCSR.Common.Interface;
using YUJCSR.Domain.Response;

namespace YUJCSR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpactparameterController : ControllerBase
    {
        private readonly IImpactBusinessManager _impactBusinessManager;
        private readonly IHttpHelper _httpHelper;
        public readonly ILogger<ImpactparameterController> _logger;
        public ImpactparameterController(IImpactBusinessManager impactBusinessManager, IHttpHelper httpHelper,
            ILogger<ImpactparameterController> logger)
        {
            _impactBusinessManager = impactBusinessManager;
            _httpHelper = httpHelper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(APIResponse))]
        public async Task<IActionResult> GetAllAdminUsers(CancellationToken cancellationtoken)
        {

            var response = await _impactBusinessManager.GetImpactParameters(cancellationtoken);
            return _httpHelper.HandleResponse(response);
        }
    }
}
