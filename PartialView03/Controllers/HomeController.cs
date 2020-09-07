using Microsoft.AspNetCore.Mvc;
using PartialView03.Models;
using System.Collections.Generic;

namespace PartialView03.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Item01 = "01",
                ItemMeta = new List<ItemMeta>
                {
                    new ItemMeta
                    {
                        ItemId = nameof(HomeViewModel.Item01),
                        Element01 = "customized text",
                        Attribute01 = "customized attribute",
                    },
                }
            };
            return View(model);
        }
    }
}
