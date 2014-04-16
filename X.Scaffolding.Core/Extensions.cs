using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using X.Storage;


namespace X.Scaffolding.Core
{
    /// <summary>
    /// Extension methoda for Html helper
    /// </summary>
    public static class Extensions
    {
        #region Extension methods for Bootstrap

        public static MvcHtmlString BootstrapDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-calendar", "date", "droplist date");
        }

        public static MvcHtmlString BootstrapDateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-calendar", "datetime", "droplist datetime");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapFileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapFileUploadFor(html, expression, null);
        }

        public static MvcHtmlString BootstrapMassFileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="useStorageLocationForPreview"></param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapFileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, bool? useStorageLocationForPreview)
        {
            var id = html.IdFor(expression).ToString();
            return BootstrapFileUploadFor(html, expression, id, useStorageLocationForPreview);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="id">Id of file upload element</param>
        /// <param name="useStorageLocationForPreview">If false, than preview composed from FileManager.Storage url and field value, else only field value used for build preview</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapFileUploadFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string id, bool? useStorageLocationForPreview = null)
        {
            var value = html.ValueFor(expression).ToString();

            var sb = new StringBuilder();

            var name = value.ToLower();

            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine("<span class=\"input-group-addon\">File</span>");
            sb.AppendLine(GetFileUploadHtml(id, 1, false, false, null, null, value));
            sb.AppendLine("</div>");

            var isImage = name.EndsWith("jpg") ||
                          name.EndsWith("jpeg") ||
                          name.EndsWith("png") ||
                          name.EndsWith("gif") ||
                          name.EndsWith("bmp");

            if (!String.IsNullOrEmpty(value) && isImage && useStorageLocationForPreview.HasValue)
            {
                value = useStorageLocationForPreview.Value ? StorageManager.Instance.GetPublicLocation(value) : value;
                sb.AppendLine("<br />");
                sb.AppendFormat(RenderThumbnail(value));
            }

            sb.AppendLine();

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString BootstrapThumbnailFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int width)
        {
            var value = html.ValueFor(expression).ToString();
            return MvcHtmlString.Create(RenderThumbnail(value, width));
        }

        public static MvcHtmlString BootstrapThumbnailFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapThumbnailFor(html, expression, 0);
        }

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var text = html.TextBoxFor(expression, new { type = "text", @class = "form-control" }).ToString();
            return MvcHtmlString.Create(text);
        }

        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var text = html.TextAreaFor(expression, new { type = "text", @class = "form-control" }).ToString();
            return MvcHtmlString.Create(text);
        }

        public static MvcHtmlString BootstrapEmailEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-envelope", "email");
        }

        public static MvcHtmlString BootstrapPhoneEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-earphone", "tel");
        }

        public static MvcHtmlString BootstrapUrlEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-link", "url");
        }

        public static MvcHtmlString BootstrapNumberEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-sound-5-1", "number");
        }

        public static MvcHtmlString BootstrapSearchEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return BootstrapIconEditorFor(html, expression, "glyphicon-search", "search");
        }

        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper html, string name, string optionLabel)
        {
            return RenderBootstrapDropDownList(html.DropDownList(name, null, optionLabel, new { @class = "form-control" }).ToHtmlString());
        }

        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper html, string name, IEnumerable<SelectListItem> selectList)
        {
            return RenderBootstrapDropDownList(html.DropDownList(name, selectList, new { @class = "form-control" }).ToHtmlString());
        }

        #endregion

        #region Extension methods for CKEditor

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

            var ckeditorScriptLocation = String.Format("{0}Scripts/ckeditor/ckeditor.js", GetWebApplicationUrl());

            sb.AppendFormat(@"<script src=""{2}""></script>
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
    
                                </script>", id, lang, ckeditorScriptLocation);

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion

        #region Extension methods for Video

        public static MvcHtmlString VideoPlayerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int width = 0, int height = 315)
        {
            var url = html.ValueFor(expression).ToHtmlString();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<a class=\"thumbnail\" href=\"#\">");
            stringBuilder.AppendLine(new VideoParser().GetPlayer(url, width, height));
            stringBuilder.AppendLine("</a>");
            return MvcHtmlString.Create(((object)stringBuilder).ToString());
        }

        public static MvcHtmlString VideoPlayerEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int width = 0, int height = 315)
        {
            var sb = new StringBuilder();

            sb.AppendLine(html.BootstrapTextBoxFor(expression).ToHtmlString());
            sb.AppendLine("<br />");

            if (!String.IsNullOrEmpty(html.ValueFor(expression).ToString()))
            {
                sb.AppendLine(VideoPlayerFor(html, expression, width, height).ToHtmlString());
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #endregion

        private static MvcHtmlString RenderBootstrapDropDownList(string html)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendLine("<span class=\"input-group-addon\"><span class=\"glyphicon glyphicon-align-justify\"></span></span>");
            sb.Append(html);
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="glyphIconClass"></param>
        /// <param name="type"></param>
        /// <param name="editorClass"></param>
        /// <returns></returns>
        private static MvcHtmlString BootstrapIconEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string glyphIconClass, string type = "text", string editorClass = "")
        {
            var sb = new StringBuilder();

            editorClass = String.IsNullOrEmpty(editorClass) ? "form-control" : "form-control " + editorClass;

            sb.AppendLine("<div class=\"input-group\">");
            sb.AppendFormat("<span class=\"input-group-addon\"><span class=\"glyphicon {0}\"></span></span>", glyphIconClass);
            sb.AppendLine(html.TextBoxFor(expression, new { type = type, @class = editorClass }).ToString());
            sb.AppendLine("</div>");

            return MvcHtmlString.Create(sb.ToString());
        }

        private static string GetWebApplicationUrl()
        {
            try
            {
                var request = HttpContext.Current.Request;
                var urlHelper = new UrlHelper(request.RequestContext);
                var result = String.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority,
                    urlHelper.Content("~"));
                return result;
            }
            catch
            {
                return "/";
            }
        }

        private static string RenderThumbnail(string value, int width = 0)
        {
            if (width != 0)
            {
                return String.Format("<a href=\"#\" class=\"thumbnail\"><img width=\"{1}px\" src=\"{0}\" /></a>", value, width);
            }

            return String.Format("<a href=\"#\" class=\"thumbnail\"><img src=\"{0}\" /></a>", value);
        }

        /// <summary>
        /// Only for compatible, while WebHepers assembly not support MVC 5
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialNumberOfFiles"></param>
        /// <param name="allowMoreFilesToBeAdded"></param>
        /// <param name="includeFormTag"></param>
        /// <param name="addText"></param>
        /// <param name="uploadText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetFileUploadHtml(string name = null, int initialNumberOfFiles = 1, bool allowMoreFilesToBeAdded = true, bool includeFormTag = true, string addText = null, string uploadText = null, string value = null)
        {
            var sb = new StringBuilder();

            if (String.IsNullOrEmpty(value))
            {
                sb.AppendFormat(@"<div class=""file-upload form-control""><div><input id=""{0}"" name=""{0}"" type=""file"" /></div></div>", name);
            }
            else
            {
                sb.AppendFormat(@"<div class=""file-upload form-control""><div><input id=""{0}"" name=""{0}"" type=""file"" value=""{1}"" /></div></div>", name, value);
                sb.AppendFormat(@"<input type=""hidden"" id=""{0}"" name=""{0}"" value=""{1}"" /> ", name, value);
            }

            return sb.ToString();
        }
    }
}