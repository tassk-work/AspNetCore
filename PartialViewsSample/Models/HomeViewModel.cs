using System.Collections.Generic;

namespace PartialViewsSample.Models
{
    public class HomeViewModel
    {
        public string Item01 { get; set; }

        public List<(string itemId, string child)> ChildMaster = new List<(string, string)>();
    }
}