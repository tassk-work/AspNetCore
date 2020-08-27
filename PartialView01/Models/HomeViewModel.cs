using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PartialView01.Models
{
    public class HomeViewModel
    {
        public string Item01 { get; set; }
        public List<(string itemId, List<string> children)> ChildMaster { get; set; }
        public List<SelectListItem> Item01s { set; get; }
    }
}