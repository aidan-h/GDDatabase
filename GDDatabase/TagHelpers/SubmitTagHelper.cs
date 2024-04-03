using Game_Design_DB.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Text;

namespace Game_Design_DB.TagHelpers
{
    [HtmlTargetElement("submit", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SubmitTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            using (var writer = new StringWriter())
            {
                writer.Write(@"<div class=""form-group""><input type=""submit"" value=""Save"" class=""btn btn-primary"" /></div>");
                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}
