using Microsoft.AspNetCore.Mvc;

namespace PartialView02.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}