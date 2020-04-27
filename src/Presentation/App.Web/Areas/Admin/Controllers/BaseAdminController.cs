using App.Web.Framework.Controllers;
using App.Web.Framework.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    public class BaseAdminController : BaseController
    {
    }
}
