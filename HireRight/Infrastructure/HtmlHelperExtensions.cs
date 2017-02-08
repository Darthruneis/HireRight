using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HireRight.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString HideRowDiv(this HtmlHelper helper, string hiddenId, string shownId, string whatToShowText)
        {
            //the div that contains the text describing how to show the div again
            TagBuilder showAgainText = new TagBuilder("div");
            showAgainText.Attributes.Add("class", "col-xs-11");
            showAgainText.InnerHtml = "Press the <span class=\"glyphicon glyphicon-plus-sign\"></span> button to show " + whatToShowText;

            //the button to show the div again
            TagBuilder showButton = new TagBuilder("button");
            showButton.Attributes.Add("class", "btn btn-default glyphicon glyphicon-plus-sign");
            showButton.Attributes.Add("type", "button");
            showButton.Attributes.Add("onclick", $"toggleMultiple({shownId}, {hiddenId})");

            //the div which holds the button
            TagBuilder showDiv = new TagBuilder("div");
            showDiv.Attributes.Add("class", "col-xs-1");
            showDiv.InnerHtml = showButton.ToString();

            //the div that will be returned
            TagBuilder divToReturn = new TagBuilder("div");
            divToReturn.Attributes.Add("class", "row borderedRow");
            divToReturn.Attributes.Add("id", hiddenId);
            divToReturn.Attributes.Add("hidden", "hidden");
            divToReturn.InnerHtml = showAgainText.ToString() + showDiv.ToString();

            return new MvcHtmlString(divToReturn.ToString());
        }

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