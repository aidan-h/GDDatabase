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
        [HtmlTargetElement("form-field")]
        public class FormFieldTagHelper : TagHelper
        {
            [HtmlAttributeName("asp-for")]
            public ModelExpression For { get; set; }

            private readonly IHtmlGenerator _generator;

            [ViewContext]
            public ViewContext ViewContext { get; set; }

            public FormFieldTagHelper(IHtmlGenerator generator)
            {
                _generator = generator;
            }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                using (var writer = new StringWriter())
                {
                    writer.Write(@"<div class=""form-group"">");

                    var label = _generator.GenerateLabel(
                                    ViewContext,
                                    For.ModelExplorer,
                                    For.Name, null,
                                    new { @class = "control-label" });

                    label.WriteTo(writer, NullHtmlEncoder.Default);

                    var textArea = _generator.GenerateTextBox(ViewContext,
                                        For.ModelExplorer,
                                        For.Name,
                                        For.Model,
                                        null,
                                        new { @class = "form-control" });

                    textArea.WriteTo(writer, NullHtmlEncoder.Default);

                    var validationMsg = _generator.GenerateValidationMessage(
                                            ViewContext,
                                            For.ModelExplorer,
                                            For.Name,
                                            null,
                                            ViewContext.ValidationMessageElement,
                                            new { @class = "text-danger" });

                    validationMsg.WriteTo(writer, NullHtmlEncoder.Default);

                    writer.Write(@"</div>");

                    output.Content.SetHtmlContent(writer.ToString());

                }

            }
        }
    }
