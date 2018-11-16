using System;
using System.Linq.Expressions;
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

        public static MvcHtmlString ValidatedEditorWithLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isRequired = false, object htmlAttributes = null)
        {
            object htmlattributes = htmlAttributes;

            MvcHtmlString labelString = helper.LabelFor(expression);
            MvcHtmlString editorString = helper.EditorFor(expression, new { htmlAttributes = htmlattributes ?? new { style = "width: 100%;" } });
            MvcHtmlString validationString = helper.ValidationMessageFor(expression);

            return new MvcHtmlString($"{labelString}{(isRequired ? "<span class='asterisk-required'>*</span>" : string.Empty)}<br />{editorString}{validationString}");
        }

        public static MvcHtmlString CollapseButton(this HtmlHelper helper, string divToToggleId, string expandTitle = "Expand this section", string collapseTitle = "Collapse this section", string customClass = "collapseIcon")
        {
            return new MvcHtmlString($"<button type='button' class='btn btn-default {customClass} pull-right glyphicon glyphicon-minus-sign' data-toggledivid='{divToToggleId}' title='{collapseTitle}' data-expandtitle='{expandTitle}' data-collapsetitle='{collapseTitle}'></button>");
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string buttonText = "Submit")
        {
            return new MvcHtmlString($"<input type='submit' value='{buttonText}' class='btn btn-primary pull-right' />");
        }
    }
}