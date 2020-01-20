using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using digitalsign.application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign.usermanagent.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IMessageService _messageService;

        public DashboardController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<ActionResult> Index()
        {
            var result = await _messageService.GetAsync();
            return View(result);
        }
    }
}