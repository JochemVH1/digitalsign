using digitalsign;
using digitalsign.dashboard.Controllers;
using digitalsign.dashboard.Extensions;
using digitalsign.usermanagent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace digitalsign.dashboard.Controllers
{
    public class BaseController : Controller
    {
        protected Guid UserId
        {
            get
            {
                ClaimsPrincipal principal = User;
                return principal.GetUserId();
            }
        }
    }
}
