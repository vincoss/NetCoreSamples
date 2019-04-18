using Fundamentals_MakeHttpRequests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fundamentals_MakeHttpRequests.Services
{
    public class GitHubHttpClientService
    {
        public readonly HttpClient _client;

        public GitHubHttpClientService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");

            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");

            _client = client;
        }

        public async Task<IEnumerable<GitHubIssue>> GetAspNetDocsIssues()
        {
            var response = await _client.GetAsync("/repos/aspnet/docs/issues?state=open&sort=created&direction=desc");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IEnumerable<GitHubIssue>>();

            return result;
        }
    }
}
