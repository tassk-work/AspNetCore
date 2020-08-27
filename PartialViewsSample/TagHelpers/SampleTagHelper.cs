using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PartialViewsSample.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PartialViewsSample.TagHelpers
{
    [HtmlTargetElement("sample")]
    public class SampleTagHelper : PartialTagHelper
    {
        private const string ItemIdAttributeName = "itemid";
        private const string ChildAttributeName = "child";
        private static readonly string[] AttributeTargets = new string[] { "id", "style" };

        [HtmlAttributeName(ItemIdAttributeName)]
        public string ItemId { get; set; }
        [HtmlAttributeName(ChildAttributeName)]
        public string Child { get; set; }

        public HomeViewModel ViewModel { get; private set; }
        public string InnerHtml { get; private set; }
        public string ChildHtml => Regex.Match(InnerHtml, $"<{Child}>(.*)</{Child}>", RegexOptions.Singleline).Groups[1].Value;
        public string AttributeStr { get; private set; }
        public string AppendClass { get; private set; }

        public SampleTagHelper(
            ICompositeViewEngine viewEngine,
            IViewBufferScope viewBufferScope) : base(viewEngine, viewBufferScope)
        {
            Model = this;
            Name = "_Sample";
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var attributes = output.Attributes.Where(x => AttributeTargets.Contains(x.Name));
            if (attributes.Any())
                AttributeStr = string.Join(" ", attributes.Select(x => $"{x.Name}={x.Value}"));

            var classAttribute = output.Attributes.FirstOrDefault(x => x.Name == "class");
            if (classAttribute != null)
                AppendClass = classAttribute.Value.ToString();

            InnerHtml = (await output.GetChildContentAsync()).GetContent();
            ViewModel = ViewContext.ViewData.Model as HomeViewModel;
            if (ViewModel != null)
                Child ??= ViewModel.ChildMaster.FirstOrDefault(x => x.itemId == ItemId).child;

            await base.ProcessAsync(context, output);
        }
    }
}