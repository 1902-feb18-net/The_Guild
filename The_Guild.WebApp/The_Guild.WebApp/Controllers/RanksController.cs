﻿using System;
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
    public class RanksController : AServiceController
    {
        public RanksController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        // GET: Rank
        public async Task<ActionResult> Index()
        {
            var request = CreateRequestToService(HttpMethod.Get, "/api/ranks");

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

            var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString);

            return View(ranks);
        }

        // GET: Rank/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"/api/ranks/{id}");

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
            ApiRanks rank = JsonConvert.DeserializeObject<ApiRanks>(jsonString);
            return View(rank);
        }

        // GET: Rank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApiRanks rank)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rank);
                }

                var request = CreateRequestToService(HttpMethod.Post, "/api/ranks", rank);

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
                return View(rank);
            }
        }

        // GET: Rank/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rank/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ApiRanks rank)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rank);
                }
                var request = CreateRequestToService(HttpMethod.Put, $"/api/ranks/{id}", rank);

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
                return View(rank);
            }
        }

        // GET: Rank/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rank/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ApiRanks rank)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(rank);
                }
                var request = CreateRequestToService(HttpMethod.Delete, $"/api/ranks/{id}", rank);

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
                return View(rank);
            }
        }
    }
}