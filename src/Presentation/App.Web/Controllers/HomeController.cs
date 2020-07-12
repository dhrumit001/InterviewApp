using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using App.Services.Logging;
using App.Web.Areas.Admin.Controllers;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

    }
}
