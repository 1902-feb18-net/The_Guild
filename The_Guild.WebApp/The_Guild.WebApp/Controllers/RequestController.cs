using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using The_Guild.WebApp.ApiModels;

namespace The_Guild.WebApp.Controllers
{
    public class RequestController : AServiceController
    {
        public RequestController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        // GET: Request
        public async Task<ActionResult> Index()
        {
            var request = CreateRequestToService(HttpMethod.Get, "/api/request");

            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error");
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            var requests = JsonConvert.DeserializeObject<List<ApiRequest>>(jsonString);

            return View(requests);
        }

        // GET: Request/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Request/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApiRequest apiRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(apiRequest);
                }

                var request = CreateRequestToService(HttpMethod.Post, "/api/request/create", apiRequest);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // log it
                return View(apiRequest);
            }
        }

        // GET: Request/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Request/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApiRequest apiRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(apiRequest);
                }
                var request = CreateRequestToService(HttpMethod.Post, "/api/request/edit", apiRequest);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // log it
                return View(apiRequest);
            }
        }

        // GET: Request/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Request/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ApiRequest apiRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(apiRequest);
                }
                var request = CreateRequestToService(HttpMethod.Post, "/api/request/delete", apiRequest);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // log it
                return View(apiRequest);
            }
        }
    }
}