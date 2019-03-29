using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.Controllers
{
    public class AccountController : AServiceController
    {
        public AccountController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApiLogin login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var request = CreateRequestToService(HttpMethod.Post, Configuration["ServiceEndpoints:AccountLogin"], login);

            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // login failed because bad credentials
                    ModelState.AddModelError("", "Login or password incorrect.");
                    return View();
                }
                ModelState.AddModelError("", "Unexpected server error");
                return View();
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View();
            }

            // login success
            return RedirectToAction("Index", "Home");
        }

        // should also have register and logout

        private bool PassCookiesToClient(HttpResponseMessage apiResponse)
        {
            // the header value contains both the name and value of the cookie itself
            if (apiResponse.Headers.TryGetValues("Set-Cookie", out var values) &&
                values.FirstOrDefault(x => x.StartsWith(ServiceCookieName)) is string headerValue)
            {
                // copy that cookie to the response we will send to the client
                Response.Headers.Add("Set-Cookie", headerValue);
                return true;
            }
            return false;
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:AccountLogout"]);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                return View("Error", new ErrorViewModel());
            }

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                return View("Error", new ErrorViewModel());
            }

            // logout success
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(ApiRegister register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            HttpRequestMessage request = CreateRequestToService(HttpMethod.Post,
                Configuration["ServiceEndpoints:AccountRegister"], register);

            HttpResponseMessage response;
            try
            {
                response = await HttpClient.SendAsync(request);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            var success = PassCookiesToClient(response);
            if (!success)
            {
                ModelState.AddModelError("", "Unexpected server error");
                return View(register);
            }

            // login success
            return RedirectToAction("Index", "Home");
        }
    }
}