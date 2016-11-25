using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HireRight.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ValidatedEditorWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            MvcHtmlString labelString = helper.LabelFor(expression);
            MvcHtmlString editorString = helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control", style = "width: 100%;" } });
            MvcHtmlString validationString = helper.ValidationMessageFor(expression);

            return new MvcHtmlString(labelString + "<br />" + editorString + "<br />" + validationString);
        }

        public static MvcHtmlString ValidatedEditorWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            object htmlattributes = htmlAttributes;

            MvcHtmlString labelString = helper.LabelFor(expression);
            MvcHtmlString editorString = helper.EditorFor(expression, new { htmlAttributes = htmlattributes });
            MvcHtmlString validationString = helper.ValidationMessageFor(expression);

            return new MvcHtmlString(labelString + "<br />" + editorString + "<br />" + validationString);
        }
    }
}