using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Services.Logging;
using App.Web.Areas.Admin.Controllers;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Information("Test Message");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
