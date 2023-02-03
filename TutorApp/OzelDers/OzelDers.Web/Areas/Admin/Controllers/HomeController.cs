using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace OzelDers.Web.Areas.Admin.Controllers;

public class HomeController : Controller
{
    [Authorize]
    [Area("Admin")]
    public IActionResult Index()
    {
        return View();
    }
}
