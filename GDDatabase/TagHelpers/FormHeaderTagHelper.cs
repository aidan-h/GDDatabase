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
    [HtmlTargetElement("form-header", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FormHeaderTagHelper : TagHelper
    {
        public string Subject { get; set; }
        public string Description { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            using (var writer = new StringWriter())
            {
                writer.Write(@"<h1>" + Subject + "</h1><h4>" + Description + "</h4><hr />");
                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}
