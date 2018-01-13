var Collapsibles;
(function (Collapsibles) {
    function bindCollapsibles(expandTooltip, collapseTooltip) {
        if (expandTooltip === void 0) { expandTooltip = "Expand this section"; }
        if (collapseTooltip === void 0) { collapseTooltip = "Collapse this section"; }
        $("body").on("click", ".collapseIcon", function () {
            var $buttonClicked = $(this);
            var divId = $buttonClicked.data("toggledivid");
            var div = $("#" + divId);
            div.toggle();
            $buttonClicked.toggleClass("glyphicon-minus-sign glyphicon-plus-sign");
            if ($buttonClicked.hasClass("glyphicon-minus-sign"))
                $buttonClicked.attr("title", collapseTooltip);
            else
                $buttonClicked.attr("title", expandTooltip);
        });
    }
    Collapsibles.bindCollapsibles = bindCollapsibles;
})(Collapsibles || (Collapsibles = {}));
//# sourceMappingURL=Collapsibles.js.map