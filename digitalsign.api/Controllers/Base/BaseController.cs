using digitalsign.common.ApiResult;
using digitalsign_api.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace digitalsign_api.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                return User.GetUserId();
            }
        }

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
