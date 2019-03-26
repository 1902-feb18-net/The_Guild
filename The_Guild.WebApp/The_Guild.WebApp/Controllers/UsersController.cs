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
using The_Guild.WebApp.Models;

namespace The_Guild.WebApp.Controllers
{
    public class UsersController : AServiceController
    {
        public UsersController(HttpClient httpClient, IConfiguration configuration)
            : base(httpClient, configuration)
        { }

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var request = CreateRequestToService(HttpMethod.Get, "api/users");

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

            var characters = JsonConvert.DeserializeObject<List<Users>>(jsonString);

            return View(characters);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"/api/users/{id}");

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
            Users user = JsonConvert.DeserializeObject<Users>(jsonString);
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(users);
                }

                var request = CreateRequestToService(HttpMethod.Post, "/api/users/create", users);

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
                return View(users);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(users);
                }
                var request = CreateRequestToService(HttpMethod.Put, $"/api/users/{id}", users);

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
                return View(users);
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(users);
                }
                var request = CreateRequestToService(HttpMethod.Delete, $"/api/users/{id}", users);

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
                return View(users);
            }
        }
    }
}