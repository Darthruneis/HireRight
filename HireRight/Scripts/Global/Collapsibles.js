var Collapsibles;
(function (Collapsibles) {
    function bindCollapsibles() {
        $("body").on("click", ".collapseIcon", function () {
            var $buttonClicked = $(this);
            var divId = $buttonClicked.data("toggledivid");
            var div = $("#" + divId);
            div.toggle();
            swapCollapseButtonIconAndTitle($buttonClicked);
        });
    }
    Collapsibles.bindCollapsibles = bindCollapsibles;
    function swapCollapseButtonIconAndTitle($button) {
        $button.toggleClass("glyphicon-minus-sign glyphicon-plus-sign");
        if ($button.hasClass("glyphicon-minus-sign"))
            $button.attr("title", $button.data("collapsetitle"));
        else
            $button.attr("title", $button.data("expandtitle"));
    }
    Collapsibles.swapCollapseButtonIconAndTitle = swapCollapseButtonIconAndTitle;
})(Collapsibles || (Collapsibles = {}));
//# sourceMappingURL=Collapsibles.js.map