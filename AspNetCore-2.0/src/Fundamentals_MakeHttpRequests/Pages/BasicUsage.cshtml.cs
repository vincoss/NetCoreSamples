using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fundamentals_MakeHttpRequests.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fundamentals_MakeHttpRequests.Pages
{
    public class BasicUsageModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<GitHubBranch> Branches { get; private set; }

        public bool GetBranchesError { get; private set; }

        public BasicUsageModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/aspnet/docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Branches = await response.Content .ReadAsAsync<IEnumerable<GitHubBranch>>();
            }
            else
            {
                GetBranchesError = true;
                Branches = Array.Empty<GitHubBranch>();
            }

            Branches = Branches.Concat(new[] { new GitHubBranch { Name = $"HttpClient hash code: {client.GetHashCode()}" } });
        }
    }
}