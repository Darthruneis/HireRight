namespace Collapsibles {
    export function bindCollapsibles() {
        $("body").on("click", ".collapseIcon", function () {
            const $buttonClicked = $(this);

            var divId = $buttonClicked.data("toggledivid");
            var div = $(`#${divId}`);

            div.toggle();

            swapCollapseButtonIconAndTitle($buttonClicked);
        });
    }

    export function swapCollapseButtonIconAndTitle($button: JQuery) {
        $button.toggleClass("glyphicon-minus-sign glyphicon-plus-sign");

        if ($button.hasClass("glyphicon-minus-sign"))
            $button.attr("title", $button.data("collapsetitle"));
        else
            $button.attr("title", $button.data("expandtitle"));
    }
}