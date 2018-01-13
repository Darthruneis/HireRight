namespace Collapsibles {
    export function bindCollapsibles(expandTooltip: string = "Expand this section", collapseTooltip: string = "Collapse this section") {
        $("body").on("click", ".collapseIcon", function () {
            const $buttonClicked = $(this);

            var divId = $buttonClicked.data("toggledivid");
            var div = $(`#${divId}`);

            div.toggle();

            $buttonClicked.toggleClass("glyphicon-minus-sign glyphicon-plus-sign");

            if ($buttonClicked.hasClass("glyphicon-minus-sign"))
                $buttonClicked.attr("title", collapseTooltip);
            else
                $buttonClicked.attr("title", expandTooltip);
        });
    }
}