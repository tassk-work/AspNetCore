using Microsoft.AspNetCore.Mvc;
using PartialViewsSample.Models;

namespace PartialViewsSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string child)
        {
            var model = new HomeViewModel
            {
                Item01 = "01",
            };
            model.ChildMaster.Add((nameof(model.Item01), child ?? "div1"));
            return View(model);
        }
    }
}
