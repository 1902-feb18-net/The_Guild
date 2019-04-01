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

    public class RankRequirementsController : AServiceController
    {
        public RankRequirementsController(HttpClient httpClient, IConfiguration configuration)
           : base(httpClient, configuration)
        { }

        // GET: RankRequirements
        public async Task<ActionResult> Index()
        {
            var request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:RankRequirements"]);
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
            var requirements = JsonConvert.DeserializeObject<List<RankRequirements>>(jsonString);

            request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Ranks"]);
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
            var ranks = JsonConvert.DeserializeObject<List<Ranks>>(jsonString);

            var viewModels = requirements.Select(r => new RankRequirementsViewModel
            {
                Id = r.Id,
                CurrentRankId = r.CurrentRankId,
                MinTotalStats = r.MinTotalStats,
                NumberRequests = r.NumberRequests,
                NextRankId = r.NextRankId,
                CurrentRank = ranks.Select(a => new Ranks
                {
                    Id = a.Id,
                    Name = a.Name,
                    Fee = a.Fee
                }).First(x => x.Id == r.CurrentRankId),
                NextRank = ranks.Select(a => new Ranks
                {
                    Id = a.Id,
                    Name = a.Name,
                    Fee = a.Fee
                }).First(x => x.Id == r.NextRankId)
            }).ToList();
            
            return View(viewModels);
        }

        // GET: Rank/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:RankRequirements"]}/{id}");
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
            RankRequirements requirements = JsonConvert.DeserializeObject<RankRequirements>(jsonString);

            RankRequirementsViewModel viewModel = new RankRequirementsViewModel();

            return View(viewModel);
        }

        // GET: Rank/Create
        public async Task<ActionResult> Create()
        {
            RankRequirements requirements = new RankRequirements();

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

            requirements.Ranks = ranks;
            return View(requirements);
        }

        // POST: Rank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RankRequirements requirements)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(requirements);
                }

                var request = CreateRequestToService(HttpMethod.Post, Configuration["ServiceEndpoints:RankRequirements"], requirements);

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
                return View(requirements);
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
        public async Task<ActionResult> Edit(int id, RankRequirements requirements)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(requirements);
                }
                var request = CreateRequestToService(HttpMethod.Put, $"{Configuration["ServiceEndpoints:RankRequirements"]}/{id}", requirements);
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
                return View(requirements);
            }
        }

        // GET: Rank/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //if (!(Account?.Roles?.Contains("admin") ?? false))
            //{
            //    // access denied
            //    return View("Error", new ErrorViewModel());
            //}
            // implementation of GET Details identical
            return await Details(id);
        }

        // POST: Rank/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, RankRequirements requirements)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(requirements);
                }
                var request = CreateRequestToService(HttpMethod.Delete, $"{Configuration["ServiceEndpoints:RankRequirements"]}/{id}", requirements);

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
                return View(requirements);
            }
        }
    }
}
