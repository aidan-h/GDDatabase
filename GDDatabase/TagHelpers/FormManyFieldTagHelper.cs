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
    [HtmlTargetElement("form-many-field")]
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
            using (var writer = new StringWriter())
            {
                writer.Write(@"<div class=""form-group""><div class=""col-md-offset-2 col-md-10"">");

                var n = For.Model as ICollection<PersonAssigned>;
                foreach (var author in n)
                {
                    writer.Write(@"<div><input type=""checkbox"" name=" + Field + @""" value=" + author.ID + @""" ");
                    if (author.Assigned)
                    {
                        writer.Write(@"checked=""checked""");
                    }
                    writer.Write(@"/> " + author.Name + @"</div>");
                }
                writer.Write(@"</div></div>");
                output.Content.SetHtmlContent(writer.ToString());
            }

        }
    }
}
