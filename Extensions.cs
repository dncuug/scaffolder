using System;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.Web.Helpers;

namespace X.Scaffolding
{
    /// <summary>
    /// Extension methoda for Html helper
    /// </summary>
    public static class Extensions
    {
        //
        // Summary:
        //     Returns an CKEdiotr for element for each property in the object that is represented
        //     by the System.Linq.Expressions.Expression expression.
        //
        // Parameters:
        //   html:
        //     The HTML helper instance that this method extends.
        //
        //   expression:
        //     An expression that identifies the object that contains the properties to
        //     display.
        //
        // Type parameters:
        //   TModel:
        //     The type of the model.
        //
        //   TValue:
        //     The type of the value.
        //
        // Returns:
        //     An HTML input element for each property in the object that is represented
        //     by the expression.
        public static MvcHtmlString HtmlEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var id = html.IdFor(expression).ToString();
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            var sb = new StringBuilder();

            sb.AppendLine(html.TextAreaFor(expression).ToString());
            sb.AppendFormat(@"<script src=""/Scripts/ckeditor/ckeditor.js""></script>
                                <script>
                                    var editor = CKEDITOR.replace('{0}', {{
                                        language: '{1}',
                                        filebrowserUploadUrl: '/system/CKEditorFileUpload',
                                        toolbar : 'Full',
 
                                        toolbar_Full :
                                        [
                                            {{ name: 'document', items : [ 'Source','-','Save','NewPage','DocProps','Preview','Print','-','Templates' ] }},
                                            {{ name: 'clipboard', items : [ 'Cut','Copy','Paste','PasteText','PasteFromWord','-','Undo','Redo' ] }},
                                            {{ name: 'editing', items : [ 'Find','Replace','-','SelectAll','-','SpellChecker', 'Scayt' ] }},
                                            //{{ name: 'forms', items : [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton',  'HiddenField' ] }},
                                            '/',
                                            {{ name: 'basicstyles', items : [ 'Bold','Italic','Underline','Strike','Subscript','Superscript','-','RemoveFormat' ] }},
                                            {{ name: 'paragraph', items : [ 'NumberedList','BulletedList','-','Outdent','Indent','-','Blockquote','CreateDiv', '-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock','-','BidiLtr','BidiRtl' ] }},
                                            {{ name: 'links', items : [ 'Link','Unlink','Anchor' ] }},
                                            {{ name: 'insert', items : [ 'Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak','Iframe' ] }},
                                            '/',
                                            {{ name: 'styles', items : [ 'Styles','Format','Font','FontSize' ] }},
                                            {{ name: 'colors', items : [ 'TextColor','BGColor' ] }},
                                            {{ name: 'tools', items : [ 'Maximize', 'ShowBlocks','-','About' ] }}
                                        ],
                                        removeButtons: 'Save,NewPage,Templates,Flash',

                                    }});
    
                                    $('.save').click(function() {{
                                        var teaxtarea = $(""#{0}"");
                                        var html = CKEDITOR.instances.{0}.getData();
                                        teaxtarea.val(html);
                                    }});
    
                                </script>", id, lang);

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var sb = new StringBuilder();
            sb.AppendLine(
                html.TextBoxFor(expression, new { type = "datetime", @class = "droplist date form-control" }).ToString());
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString ImageUploadFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var id = html.IdFor(expression).ToString();
            var value = html.ValueFor(expression).ToString();

            var sb = new StringBuilder();

            var name = value.ToLower();

            sb.AppendLine(FileUpload.GetHtml(id, 1, false, false, null, null).ToString());

            var isImage = name.Contains("jpg") ||
                          name.Contains("jpeg") ||
                          name.Contains("png") ||
                          name.Contains("gif") ||
                          name.Contains("bmp");

            if (!String.IsNullOrEmpty(value) && isImage)
            {
                sb.AppendFormat("<img class=\"preview\" src=\"{0}\" />", value);
            }

            sb.AppendLine();

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}