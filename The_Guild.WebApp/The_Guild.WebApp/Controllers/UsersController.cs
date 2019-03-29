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
using The_Guild.WebApp.Models;
using The_Guild.WebApp.ViewModel;

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
            var request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:User"]);      //Configuration["ServiceEndpoints:Character"]);
            var response = await HttpClient.SendAsync(request);

            //var request2 = CreateRequestToService(HttpMethod.Get, "/api/ranks");
            //var response2 = await HttpClient.SendAsync(request);

            // Change the redirects to actual places not home

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Error", new ErrorViewModel());
            }
            //if (!response2.IsSuccessStatusCode)
            //{
            //    if (response.StatusCode == HttpStatusCode.Unauthorized)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    return View("Error");
            //}

            var jsonString = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<ApiUsers>>(jsonString);

            //var jsonString2 = await response.Content.ReadAsStringAsync();
            //var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString2);

            var viewModels = users.Select(u => new UserIndexModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RankId = u.RankId,
                //Ranks = ranks
            }).ToList();

            
            return View(viewModels);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Users"]}/{id}");
            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            Users user = JsonConvert.DeserializeObject<Users>(jsonString);

            // Uncomment once ranks are available in the api...


            // request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Rank"]}/{id}");
            // response = await HttpClient.SendAsync(request);

            //if (!response.IsSuccessStatusCode)
            //{
            //    if (response.StatusCode == HttpStatusCode.Unauthorized)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //    return View("Error", new ErrorViewModel());
            //}

            // jsonString = await response.Content.ReadAsStringAsync();
            //Ranks rank = JsonConvert.DeserializeObject<Ranks>(jsonString);




            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            Users user = new Users();
            return View(user);
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

                ApiUsers tUser = new ApiUsers
                {
                    Id = users.Id,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Salary = users.Salary,
                    Strength = users.Strength,
                    Dex = users.Dex,
                    Wisdom = users.Wisdom,
                    Intelligence = users.Intelligence,
                    Charisma = users.Charisma,
                    Constitution = users.Constitution,
                    RankId = users.RankId
                };
                var request = CreateRequestToService(HttpMethod.Post, Configuration["ServiceEndpoints:Users"], tUser);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error", new ErrorViewModel());
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
        public async Task<ActionResult> Edit(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints: Users"]}/{id}");
            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            Users user = JsonConvert.DeserializeObject<Users>(jsonString);
            return View(user);
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
                var request = CreateRequestToService(HttpMethod.Put, $"{Configuration["ServiceEndpoints:Users"]}/{id}", users);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error", new ErrorViewModel());
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
        public async Task<ActionResult> Delete(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Users"]}/{id}");
            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            Users user = JsonConvert.DeserializeObject<Users>(jsonString);
            return View(user);
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
                var request = CreateRequestToService(HttpMethod.Delete, $"{Configuration["ServiceEndpoints:Users"]}/{id}", users);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error", new ErrorViewModel());
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