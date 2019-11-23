using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.Autherization.ApplicationUser;
using digitalsign.application.Services.Interface;
using digitalsign_api.Contracts.V1;
using digitalsign_api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign_api.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        
        [HttpPost(ApiRoutes.Idenity.Register)]
        public async Task<IActionResult> Register([FromBody] ApplicationUserCreateModel createModel) {
            return ProcessResult(await _identityService.RegisterAsync(createModel));
            
        }

        [HttpPost(ApiRoutes.Idenity.Login)]
        public async Task<IActionResult> Login([FromBody] ApplicationUserLoginModel loginModel) {
            return ProcessResult(await _identityService.LoginAsync(loginModel));
        } 

        [HttpPost(ApiRoutes.Idenity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] ApplicationUserRefreshModel refreshModel) {
            return ProcessResult(await _identityService.RefreshAsync(refreshModel));
        }
    }
}
