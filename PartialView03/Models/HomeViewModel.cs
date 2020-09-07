using System.Collections.Generic;

namespace PartialView03.Models
{
    public class BaseModel
    {
        public List<ItemMeta> ItemMeta { get; set; }
    }

    public class ItemMeta
    {
        public string ItemId { get; set; }
        public string Attribute01 { get; set; }
        public string Element01 { get; set; }
    }

    public class HomeViewModel : BaseModel
    {
        public string Item01 { get; set; }
    }
}