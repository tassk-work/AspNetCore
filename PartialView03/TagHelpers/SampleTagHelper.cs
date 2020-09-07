using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PartialView03.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PartialView03.TagHelpers
{
    [HtmlTargetElement(TAG_NAME)]
    public class SampleTagHelper : PartialTagHelper
    {
        public const string TAG_NAME = "sample";
        private const string ItemIdAttributeName = "itemid";

        [HtmlAttributeName(ItemIdAttributeName)]
        public string ItemId { get; set; }

        public string InnerHtml { get; private set; }
        public string ChildHtml
        {
            get
            {
                var html = InnerHtml;
                var itemMeta = _baseModel.ItemMeta.FirstOrDefault(x => x.ItemId == ItemId);
                html = Regex.Replace(html, $@"(?<=class="".*element01.*"".*?>)(.*)(?=<)", itemMeta.Element01);
                html = Regex.Replace(html, $@"(?<=attribute01="")(.*?)(?="")", itemMeta.Attribute01);
                return html;
            }
        }
        private BaseModel _baseModel;

        public SampleTagHelper(
            ICompositeViewEngine viewEngine,
            IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope)
        {
            Model = this;
            Name = "_Sample";
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            InnerHtml = (await output.GetChildContentAsync()).GetContent();
            _baseModel = ViewContext.ViewData.Model as BaseModel;

            await base.ProcessAsync(context, output);
        }
    }
}