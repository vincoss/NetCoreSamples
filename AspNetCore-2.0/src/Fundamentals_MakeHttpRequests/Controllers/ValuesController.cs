using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_MakeHttpRequests.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fundamentals_MakeHttpRequests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHelloClient _client;

        public ValuesController(IHelloClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<Reply>> Get()
        {
            return await _client.GetMessageAsync();
        }
    }
}
