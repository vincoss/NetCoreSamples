using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fundamentals_MakeHttpRequests.Models;
using Fundamentals_MakeHttpRequests.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fundamentals_MakeHttpRequests.Pages
{
    public class TypedClientModel : PageModel
    {
        private readonly GitHubHttpClientService _gitHubService;

        public IEnumerable<GitHubIssue> LatestIssues { get; private set; }

        public bool HasIssue => LatestIssues.Any();

        public bool GetIssuesError { get; private set; }

        public TypedClientModel(GitHubHttpClientService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task OnGet()
        {
            try
            {
                LatestIssues = await _gitHubService.GetAspNetDocsIssues();
            }
            catch (HttpRequestException)
            {
                GetIssuesError = true;
                LatestIssues = Array.Empty<GitHubIssue>();
            }

            LatestIssues = LatestIssues.Concat(new[] { new GitHubIssue { Title = $"HttpClient hash code: {_gitHubService._client.GetHashCode()}" } });
        }
    }
}