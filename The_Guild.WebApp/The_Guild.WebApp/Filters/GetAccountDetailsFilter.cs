using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using The_Guild.WebApp.ApiModels;
using The_Guild.WebApp.Controllers;

namespace The_Guild.WebApp.Filters
{
    public class GetAccountDetailsFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;

        public GetAccountDetailsFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            // if the controller is an aservicecontroller, then
            // fetch the details, otherwise, do nothing.
            if (context.Controller is AServiceController controller)
            {
                HttpRequestMessage request = controller.CreateRequestToService(
                    HttpMethod.Get, _configuration["ServiceEndpoints:AccountDetails"]);

                HttpRequestMessage request2 = controller.CreateRequestToService(
                    HttpMethod.Get, _configuration["ServiceEndpoints:Users"]);

                HttpResponseMessage response = await controller.HttpClient.SendAsync(request);

                HttpResponseMessage response2 = await controller.HttpClient.SendAsync(request2);

                if (!response.IsSuccessStatusCode)
                {
                    // setting "Result" in a filter short-circuits the rest
                    // of the MVC pipeline
                    // but i won't do that, i should just log it.
                }
                else
                {
                    if (!response2.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var jsonString2 = await response2.Content.ReadAsStringAsync();
                        ApiAccountDetails details = JsonConvert.DeserializeObject<ApiAccountDetails>(jsonString);
                        List<ApiUsers> users = JsonConvert.DeserializeObject<List<ApiUsers>>(jsonString2);
                        foreach(ApiUsers user in users)
                        {
                            if(user.Username == details.Username)
                            {
                                details.UserId = user.Id;
                            }
                        }
                        controller.ViewData["accountDetails"] = details;
                        //controller.TempData["accountDetails"] = details.ToString();
                        controller.Account = details;
                    }
                }
            }

            await next();
        }
    }
}
