using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using digitalsign;
using digitalsign.application.Contracts.V1.CreateModels.User;
using digitalsign.application.Contracts.V1.InputModels.User;
using digitalsign.application.Services.Interface;
using digitalsign.common.Enumeration;
using digitalsign.dashboard.Controllers;
using digitalsign.usermanagent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace digitalsign.dashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userservice;

        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IUserService userservice,
            IHttpClientFactory httpClientFactory)
        {
            _userservice = userservice;
            _httpClientFactory = httpClientFactory;
        }
        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserInputModel model)
        {
            try
            {
                var isUserRegistered = false;
                var json = JsonConvert.SerializeObject(model);
                using (var data = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    var httpClient = _httpClientFactory.CreateClient("identity server");
                    var result = await httpClient.PostAsync("account/register", data);

                    result.EnsureSuccessStatusCode();
                }
                return RedirectToAction("ApplicationAccount", "User");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ApplicationAccount()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var vm = BuildUserRegistrationModelFromClaims(identity.Claims);
            return View(vm);
        }

        private UserRegistrationInputModel BuildUserRegistrationModelFromClaims(IEnumerable<Claim> claims)
        {
            var model = new UserRegistrationInputModel();
            foreach (var claim in claims)
            {
                if (string.Equals(claim.Type.ToUpper(), "GIVEN_NAME"))
                {
                    model.FirstName = claim.Value;
                }
                if (string.Equals(claim.Type.ToUpper(), "FAMILY_NAME"))
                {
                    model.LastName = claim.Value;
                }
                if (string.Equals(claim.Type.ToUpper(), "EMAIL"))
                {
                    model.Email = claim.Value;
                }
                if (string.Equals(claim.Type.ToUpper(), "ROLE"))
                {
                    model.Role = (UserRole)Enum.Parse(typeof(UserRole), claim.Value);
                }
                if (string.Equals(claim.Type.ToUpper(), "SUB"))
                {
                    model.Identity = Guid.Parse(claim.Value);
                }
            }
            return model;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ApplicationAccount(UserRegistrationInputModel model)
        {
            try
            {
                var result = await _userservice.AddAsync(model);
                if (result != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}