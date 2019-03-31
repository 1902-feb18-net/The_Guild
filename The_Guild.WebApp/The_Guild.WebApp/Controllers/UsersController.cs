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
            var request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Users"]);
            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Error", new ErrorViewModel());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<ApiUsers>>(jsonString);

            request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Ranks"]);
            response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Error");
            }
            var jsonString2 = await response.Content.ReadAsStringAsync();
            var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString2);

            var viewModels = users.Select(u => new UserIndexModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RankId = u.RankId,
                Rank = ranks.Select(r => new ApiRanks
                {
                    Id = r.Id,
                    Fee = r.Fee,
                    Name = r.Name
                }).Where(r => r.Id == u.RankId).First()
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
            var rId = user.RankId;

            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}/{rId}");
            response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            var rank = JsonConvert.DeserializeObject<ApiRanks>(jsonString);

            user.Rank = rank;

            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Users"]}/{id}/SubmittedRequests");
            response = await HttpClient.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            var SubmittedRequests = JsonConvert.DeserializeObject<List<Request>>(jsonString); //might be empty

            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Users"]}/{id}/AcceptedRequests");
            response = await HttpClient.SendAsync(request);
            jsonString = await response.Content.ReadAsStringAsync();
            var AcceptedRequests = JsonConvert.DeserializeObject<List<Request>>(jsonString); //might be empty

            UserViewModel viewModel = new UserViewModel(user)
            {
                submittedRequests = SubmittedRequests,
                acceptedRequests = AcceptedRequests
            };

            return View(viewModel);
        }

        // GET: Users/Create
        public async Task<ActionResult> Create()
        {
            Users user = new Users();

            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}");
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
            var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString);

            user.Ranks = ranks;

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
            catch(Exception e)
            {
                // log it
                return View(users);
            }
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int id)
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

            var rId = user.RankId;
            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}/{rId}");
            response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ApiRanks rank = JsonConvert.DeserializeObject<ApiRanks>(jsonString);

            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}");
            response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString);
            user.Rank = rank;
            user.Ranks = ranks;
            
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Users users)
        {
            try
            {
                HttpRequestMessage request;
                HttpResponseMessage response;
                string jsonString;
                List<ApiRanks> ranks;

                if (!ModelState.IsValid)
                {
                    request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}");
                    response = await HttpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                        return View("Error", new ErrorViewModel());
                    }

                    jsonString = await response.Content.ReadAsStringAsync();
                    ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString);
                    return View(users);
                }
                request = CreateRequestToService(HttpMethod.Put, $"{Configuration["ServiceEndpoints:Users"]}/{id}", users);

                response = await HttpClient.SendAsync(request);

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

                var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}");
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
                var ranks = JsonConvert.DeserializeObject<List<ApiRanks>>(jsonString);

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

            var rId = user.RankId;
            request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}/{rId}");
            response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            jsonString = await response.Content.ReadAsStringAsync();
            ApiRanks rank = JsonConvert.DeserializeObject<ApiRanks>(jsonString);

            user.Rank = rank;


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