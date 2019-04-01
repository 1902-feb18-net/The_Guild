using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Models;
using The_Guild.WebApp.ViewModel;


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
            var request = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Request"]);

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
            var requests = JsonConvert.DeserializeObject<List<Request>>(jsonString);


            List<RequestViewModel> viewModels = new List<RequestViewModel>();

            foreach (Request dbRequest in requests)
            {
                var progRequest = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Progress"]}/{dbRequest.ProgressId}");
                var progResponse = await HttpClient.SendAsync(progRequest);
                var progJsonString = await progResponse.Content.ReadAsStringAsync();
                var dbProg = JsonConvert.DeserializeObject<Progress>(progJsonString);

                Ranks dbRank = null;
                if (dbRequest.RankId != null)
                {
                    var rankRequest = CreateRequestToService(HttpMethod.Get, $"/api/ranks/{dbRequest.RankId}");
                    var rankResponse = await HttpClient.SendAsync(rankRequest);
                    var rankJsonString = await rankResponse.Content.ReadAsStringAsync();
                    dbRank = JsonConvert.DeserializeObject<Ranks>(rankJsonString);
                }

                RequestViewModel requestViewModel = new RequestViewModel(dbRequest)
                {
                    Progress = dbProg,
                    Rank = dbRank
                };
                viewModels.Add(requestViewModel);
            }


            return View(viewModels);
        }

        // GET: Request/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Request"]}/{id}");

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
            Request dbRequest = JsonConvert.DeserializeObject<Request>(jsonString);

            var progRequest = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Progress"]}/{dbRequest.ProgressId}");
            var progResponse = await HttpClient.SendAsync(progRequest);
            var progJsonString = await progResponse.Content.ReadAsStringAsync();
            var dbProg = JsonConvert.DeserializeObject<Progress>(progJsonString);

            Ranks dbRank = null;
            if (dbRequest.RankId != null)
            {
                var rankRequest = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Ranks"]}/{dbRequest.RankId}");
                var rankResponse = await HttpClient.SendAsync(rankRequest);
                var rankJsonString = await rankResponse.Content.ReadAsStringAsync();
                dbRank = JsonConvert.DeserializeObject<Ranks>(rankJsonString);
            }

            RequestViewModel requestViewModel = new RequestViewModel(dbRequest)
            {
                Progress = dbProg,
                Rank = dbRank
            };

            return View(requestViewModel);
        }

        // GET: Request/Create
        public async Task<ActionResult> Create()
        {
            RequestViewModel dbRequest = new RequestViewModel();
            //get all available customers to choose during submission?
            var usersRequest = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Users"]);
            var usersResponse = await HttpClient.SendAsync(usersRequest);

            if (!usersResponse.IsSuccessStatusCode)
            {
                if (usersResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                return View("Error", new ErrorViewModel());
            }

            var usersJsonString = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(usersJsonString);
            foreach (Users customer in users)
            {
                dbRequest.requesters.Add(new RequesterViewModel(customer));
            }
            return View(dbRequest);
        }

        // POST: Request/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RequestViewModel dbRequest)
        {
            try
            {     
                if (!ModelState.IsValid)
                {
                    return View(dbRequest);
                }

                var Request = new Request()
                {
                    Descript = dbRequest.Descript,
                    Requirements = dbRequest.Requirements,
                    Reward = dbRequest.Reward
                };

                //add Requesting group members to request response
                for (var i = 0; i < dbRequest.requesters.Count; i++)
                {
                    if (dbRequest.requesters[i].Checked)
                    {
                        var user = new Users()
                        {
                            Id = dbRequest.requesters[i].Id
                        };
                        Request.Requesters.Add(user);
                    }
                }    

                var request = CreateRequestToService(HttpMethod.Post, Configuration["ServiceEndpoints:Request"], Request);

                var response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    return View("Error", new ErrorViewModel());
                }

                return RedirectToAction("Index", "request");
            }
            catch
            {
                // log it
                return View(dbRequest);
            }
        }

        // GET: Request/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var request = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Request"]}/{id}");
            var response = await HttpClient.SendAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            var dbReq = JsonConvert.DeserializeObject<Request>(jsonString);

            RequestViewModel edit = new RequestViewModel(dbReq);

            //get all available progresses and ranks to choose from
            var progRequest = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Progress"]);
            var progResponse = await HttpClient.SendAsync(progRequest);
            var progJsonString = await progResponse.Content.ReadAsStringAsync();
            var progresses = JsonConvert.DeserializeObject<List<Progress>>(progJsonString);
            edit.progresses = progresses;

            var request2 = CreateRequestToService(HttpMethod.Get, Configuration["ServiceEndpoints:Ranks"]);
            var response2 = await HttpClient.SendAsync(request2);
            var jsonString2 = await response2.Content.ReadAsStringAsync();
            var ranks = JsonConvert.DeserializeObject<List<Ranks>>(jsonString2);
            edit.ranks = ranks;

            return View(edit);
        }

        // POST: Request/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RequestViewModel dbRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dbRequest);
                }
                var request = CreateRequestToService(HttpMethod.Put, $"{Configuration["ServiceEndpoints:Request"]}/{id}", dbRequest);

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
                return View(dbRequest);
            }
        }

        // GET: Request/Delete/5
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

        // POST: Request/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Request dbRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dbRequest);
                }
                var request = CreateRequestToService(HttpMethod.Delete, $"{Configuration["ServiceEndpoints:Request"]}/{id}", dbRequest);

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
                return View(dbRequest);
            }
        }

        public async Task<ActionResult> AddAdvToRequest(int id)
        {
            ApiAccountDetails dets = (ApiAccountDetails)ViewData["accountDetails"];
            int userId = dets.UserId;
            var userRequest = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Users"]}/{userId}");
            var userResponse = await HttpClient.SendAsync(userRequest);
            var jsonString = await userResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Users>(jsonString);
            

            var questRequest = CreateRequestToService(HttpMethod.Get, $"{Configuration["ServiceEndpoints:Request"]}/{id}");
            var questResponse = await HttpClient.SendAsync(questRequest);
            var jsonString2 = await questResponse.Content.ReadAsStringAsync();
            var req = JsonConvert.DeserializeObject<Request>(jsonString2);

            QuesterViewModel quest = new QuesterViewModel
            {
                Username = user.Username,
                Quest = req.Descript,
                request = req
            };
            return View(quest);
        }

        [HttpPost]
        public async Task<ActionResult> SendAdvToRequest(Request req)
        {
            if (!ModelState.IsValid)
            {
                return View(req);
            }
            ApiAccountDetails dets = (ApiAccountDetails)ViewData["accountDetails"];
            int userId = dets.UserId;

            AdventureParty party = new AdventureParty
            {
                AdventurerId = userId,
                RequestId = req.Id,
                Name = $"Party{userId+req.Id}"
            };
            try
            {
                var request = CreateRequestToService(HttpMethod.Post, $"{Configuration["ServiceEndpoints:Request"]}/{req.Id}", party);

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
                return View(req);
            }
        }
    }
}