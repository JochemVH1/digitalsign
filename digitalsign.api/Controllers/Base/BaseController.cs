using digitalsign.common.ApiResult;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign_api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public IActionResult ProcessResult<T>(ApiResult<T> apiResult)
        {
            if (apiResult.Success)
            {
                return Ok(apiResult);
            }
            return Conflict(apiResult);
        }
    }
}
