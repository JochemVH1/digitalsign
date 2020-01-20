using digitalsign.usermanagent.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace digitalsign.usermanagent.Controllers
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
