using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using digitalsign;
using digitalsign.application.Contracts.V1.CreateModels.Message;
using digitalsign.application.Contracts.V1.InputModels.Message;
using digitalsign.application.Contracts.V1.ViewModels.Message;
using digitalsign.application.Services.Interface;
using digitalsign.dashboard.Controllers;
using digitalsign.usermanagent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace digitalsign.dashboard.Controllers
{
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // GET: Message
        public async Task<ActionResult> Index()
        {
            var result = await _messageService.GetByUserIdAsync(UserId);
            return View(result);
        }

        // GET: Message/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var result = await _messageService.GetAsync(id);
            return View(result);
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MessageInputModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _messageService.AddAsync(UserId, model);
                    return RedirectToAction(nameof(Index));
                }
                return View();
                // TODO: Add insert logic here
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(Guid id)
        {
            _messageService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: Message/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _messageService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}