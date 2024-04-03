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
    [HtmlTargetElement("detail-field", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DetailFieldTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "dl";
            output.Attributes.Add("class", "row");
            using (var writer = new StringWriter())
            {
                writer.Write(@"<dt class = ""col-sm-2"">" + For.Name + @"</dt>");
                var elements = For.ModelExplorer.Metadata.ElementMetadata;
                if (elements != null)
                {
                    var n = (IEnumerable<Object>)For.Model;
                    foreach (var element in n) {
                        var explorer = elements.GetModelExplorerForType(elements.ModelType, element);
                        writer.Write(@"<dd class=""col-sm-10"">" + explorer.GetSimpleDisplayText() + @"</dd>");
                    }
                } else
                {
                    writer.Write(@"<dd class=""col-sm-10"">" + For.ModelExplorer.GetSimpleDisplayText() + @"</dd>");

                }
                output.Content.SetHtmlContent(writer.ToString());
            }
        }
    }
}
