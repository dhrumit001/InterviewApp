using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Web.Models;
using App.Services.Logging;
using App.Web.Areas.Admin.Controllers;

namespace App.Web.Controllers
{
    public class HomeController : BaseAdminController
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
