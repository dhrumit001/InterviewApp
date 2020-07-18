using Microsoft.AspNetCore.Mvc;

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
