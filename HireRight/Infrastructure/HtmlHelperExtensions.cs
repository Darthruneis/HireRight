using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HireRight.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString HiddenShowRowDiv(this HtmlHelper helper, string hiddenId, string shownId, string whatToShowText)
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
            divToReturn.Attributes.Add("class", "row");
            divToReturn.Attributes.Add("id", hiddenId);
            divToReturn.Attributes.Add("hidden", "hidden");
            divToReturn.InnerHtml = showAgainText.ToString() + showDiv.ToString();

            return new MvcHtmlString(divToReturn.ToString());
        }

        public static MvcHtmlString HideRowButtonInColumnDiv(this HtmlHelper helper, string hiddenId, string shownId)
        {
            //the button to show the div again
            TagBuilder hideButton = new TagBuilder("button");
            hideButton.Attributes.Add("class", "btn btn-default glyphicon glyphicon-minus-sign");
            hideButton.Attributes.Add("type", "button");
            hideButton.Attributes.Add("onclick", $"toggleMultiple({hiddenId}, {shownId})");

            //the div which holds the button
            TagBuilder divtoReturn = new TagBuilder("div");
            divtoReturn.Attributes.Add("class", "col-xs-1");
            divtoReturn.InnerHtml = hideButton.ToString();

            return new MvcHtmlString(divtoReturn.ToString());
        }

        public static MvcHtmlString ProfileSampleDownloadListItem(this HtmlHelper helper, string text, string actionName)
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            MvcHtmlString smallScreenViewLink = new MvcHtmlString($"<a href=\"{url.Action(actionName, "Reports", new { inline = true })}\" target=\"_blank\" title=\"View in a new tab\">" +
                        $"<span class=\"btn btn-default glyphicon glyphicon-folder-open\" style=\"display: inline;\"></span>" +
                        $"</a>");
            MvcHtmlString smallScreenDownloadLink = new MvcHtmlString($"<a href=\"{url.Action(actionName, "Reports", new { inline = false })}\" title=\"Download\"><span class=\"btn btn-default glyphicon glyphicon-download\"></span></a>");
            string innerHtml = $"{smallScreenViewLink}"
                             + $" {smallScreenDownloadLink}"
                             + $" {text}";
            return new MvcHtmlString("<div class=\"col-xs-12\" style=\"padding: 2px; clear: both;\" >" + innerHtml + "</div>");
        }

        public static MvcHtmlString ValidatedEditorWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isRequired = false, object htmlAttributes = null)
        {
            object htmlattributes = htmlAttributes;

            MvcHtmlString labelString = helper.LabelFor(expression);
            MvcHtmlString editorString = helper.EditorFor(expression, new { htmlAttributes = htmlattributes ?? new { @class = "form-control", style = "width: 100%;" } });
            MvcHtmlString validationString = helper.ValidationMessageFor(expression);

            return new MvcHtmlString(labelString + (isRequired ? "<span style=\"color: red;\" class=\"glyphicon glyphicon-asterisk\"></span>" : string.Empty) + "<br />" + editorString + validationString);
        }
    }
}