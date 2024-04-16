using Game_Design_DB.ViewModels;
using Game_Design_DB.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Text;

namespace Game_Design_DB.TagHelpers
{
    [HtmlTargetElement("form-many-field", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FormManyFieldTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("field")]
        public string Field { get; set; }


        private readonly IHtmlGenerator _generator;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public FormManyFieldTagHelper(IHtmlGenerator generator)
        {
            _generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            using (var writer = new StringWriter())
            {
                writer.Write(@"<div class=""form-group""><div class=""col-md-offset-2 col-md-10"">");
                var label = _generator.GenerateLabel(
                    ViewContext,
                    For.ModelExplorer,
                    For.Name, null,
                    new { @class = "control-label" });

                label.WriteTo(writer, NullHtmlEncoder.Default);

                var n = For.Model as AssignedSet;
                if (n == null)
                {
                    n = new AssignedSet();
                }
                var index = 0;
                foreach (var obj in n.Objects)
                {
                    writer.Write("<div><input type=\"checkbox\" name=\"" + Field +"[" + index + "].Checked" + "\" value=\"true\"");
                    if (obj.Assigned)
                    {
                        writer.Write(@"checked=""checked""");
                    }
                    writer.Write("/><input type=\"Hidden\" name=\"" + Field + "[" + index + "].ID\" value=\"" + obj.ID.ToString() + "\"/>" + obj.Name + @"</div>");
                    index++;
                }
                writer.Write(@"</div></div>");
                output.Content.SetHtmlContent(writer.ToString());
            }

        }
    }
}
