using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using netcoreapp31test.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace netcoreapp31test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryConfiguration tc;

        public HomeController(ILogger<HomeController> logger, TelemetryConfiguration telemetryConfiguration)
        {
            _logger = logger;
            tc = telemetryConfiguration;
        }

        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.bing.com/");
                //HTTP GET
                var responseTask = client.GetAsync("/");
                responseTask.Wait();

                var result = responseTask.Result;
            }

            TelemetryClient tcc = new TelemetryClient(tc);
            tc.DisableTelemetry = false;
            tcc.TrackTrace("Success from Index!!!");
            _logger.LogCritical("ILogger Worked!!!");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("EnvironmentVariables")]
        public IEnumerable<EnvironmentVariable> GetEnvironmentVariables()
        {
            return EnvironmentVariable.GetEnvironmentVariables();
        }

        [HttpGet("LoadedDLLs")]

        public IEnumerable<LoadedDLL> GetLoadedDLLs()
        {
            return LoadedDLL.GetLoadedDLLs();
        }
    }
}
