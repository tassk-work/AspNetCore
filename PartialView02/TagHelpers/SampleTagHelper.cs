using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PartialView02.TagHelpers
{
    [HtmlTargetElement("sample")]
    public class SampleTagHelper : PartialTagHelper
    {
        private static readonly string[] AttributeTargets = new string[] { "id", "style" };
        public string InnerHtml { get; private set; }
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

            await base.ProcessAsync(context, output);
        }
    }
}