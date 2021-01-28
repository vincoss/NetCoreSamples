using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebSiteRegionTester.Models;

namespace WebSiteRegionTester.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var info = RequestInfo();
            ViewBag.Info = info;
            return View();
        }

        private IEnumerable<string> RequestInfo()
        {
            var sb = new List<string>();

            sb.Add($"MachineName:     {Environment.MachineName}");
            sb.Add($"DateTime.Now:    {DateTime.Now}");
            sb.Add($"DateTime.UtcNow: {DateTime.UtcNow}");
            sb.Add($"GetLocalIp:      {GetLocalIp()}");
            sb.Add($"RemoteIp:        {RemoteIp()}");
            sb.Add($"CurrentTimeZone: {System.TimeZoneInfo.Local}");

            sb.AddRange(GetRequestHeaders());

            return sb;
        }

        private string GetLocalIp()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }

        private string RemoteIp()
        {
            if(HttpContext.Connection.RemoteIpAddress != null)
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return null;
        }

        public IEnumerable<string> GetRequestHeaders()
        {
            foreach(var h in this.HttpContext.Request.Headers)
            {
                yield return $"{h.Key}:         {h.Value}";
            }
        }
    }
}
