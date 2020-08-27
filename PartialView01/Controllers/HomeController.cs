using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartialView01.Models;
using System.Collections.Generic;

namespace PartialView01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Item01 = "01",
                Item01s = new List<SelectListItem>
                {
                    new SelectListItem { Value = "01", Text = "Mexico" },
                },
                ChildMaster = new List<(string, List<string>)>
                {
                    (nameof(HomeViewModel.Item01), new List<string> { "sample1" })
                },
            };
            return View(model);
        }

        public IActionResult Index02()
        {
            var model = new HomeViewModel
            {
                Item01 = "01",
                ChildMaster = new List<(string, List<string>)>
                {
                    (nameof(HomeViewModel.Item01), new List<string> { "sample2" })
                }
            };
            return View(nameof(Index), model);
        }
    }
}
