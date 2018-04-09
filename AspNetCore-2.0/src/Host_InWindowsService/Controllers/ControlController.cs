using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Host_InWindowsService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Host_InWindowsService.Controllers
{
    // http://localhost:5000/api/control/getProcesses
    [Produces("application/json")]
    [Route("api/Control")]
    public class ControlController : Controller
    {
        private ILogger _logger;
        private IControlService _controlService;

        public ControlController(IControlService controlService, ILogger<ControlController> logger)
        {
            if (controlService == null)
            {
                throw new ArgumentNullException(nameof(controlService));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _controlService = controlService;
            _logger = logger;
        }

        // GET api/control/getProcesses/
        [HttpGet("GetProcesses")]
        public IEnumerable<string> GetProcesses(int id)
        {
            return _controlService.GetProcesses();
        }

        // GET api/home/killProcess/id
        [HttpPost("KillProcess/{id}")]
        public string KillProcess(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "-1";
            }

            _controlService.KillProcess(new[] { id });

            return id;
        }
    }
}