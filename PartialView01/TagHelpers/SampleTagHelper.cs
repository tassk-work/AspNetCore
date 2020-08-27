using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PartialView01.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PartialView01.TagHelpers
{
    [HtmlTargetElement(TAG_NAME)]
    public class SampleTagHelper : PartialTagHelper
    {
        public const string TAG_NAME = "sample";
        private const string ItemIdAttributeName = "itemid";
        private const string ChildAttributeName = "child";

        [HtmlAttributeName(ItemIdAttributeName)]
        public string ItemId { get; set; }
        [HtmlAttributeName(ChildAttributeName)]
        public List<string> Children { get; set; }

        public string InnerHtml { get; private set; }
        public string ChildHtml
        {
            get
            {
                var html = InnerHtml;
                foreach (var tag in Children)
                    html = Regex.Replace(html, $"<{tag}>(.*)</{tag}>", "$1", RegexOptions.Singleline);
                html = Regex.Replace(html, $@"<{TAG_NAME}\d>(.*)</{TAG_NAME}\d>", "", RegexOptions.Singleline);
                return html;
            }
        }

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
            var viewModel = ViewContext.ViewData.Model as HomeViewModel;
            Children ??= viewModel.ChildMaster.FirstOrDefault(x => x.itemId == ItemId).children;

            await base.ProcessAsync(context, output);
        }
    }
}