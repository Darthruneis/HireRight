var CustomSolutions;
(function (CustomSolutions) {
    function getImportanceLevel($categoryRow) {
        return $categoryRow.find("input[type='hidden']").val();
    }
    function getNumericImportanceLevel(stringValue) {
        switch (stringValue) {
            case "Irrelevant":
                return 0;
            case "HighImportance":
                return 2;
            case "LowImportance":
            default:
                //reset to middle column
                return 1;
        }
    }
    function getStringImportanceLevel(intValue) {
        if (intValue >= 2)
            return "HighImportance";
        if (intValue <= 0)
            return "Irrelevant";
        return "LowImportance";
    }
    function updateImportanceLevel($categoryRow, increase) {
        var current = getNumericImportanceLevel(getImportanceLevel($categoryRow));
        if (increase)
            current = current + 1;
        else
            current = current - 1;
        var newValue = getStringImportanceLevel(current);
        if (newValue === "HighImportance") {
            $categoryRow.find(".moreImportantButton").hide();
        }
        else if (newValue === "Irrelevant") {
            $categoryRow.find(".lessImportantButton").hide();
        }
        else {
            $categoryRow.find(".moreImportantButton").show();
            $categoryRow.find(".lessImportantButton").show();
        }
        $categoryRow.find("input[type='hidden']").val(newValue);
        var detachedHtml = $categoryRow.find(".categoryCard").detach();
        detachedHtml.appendTo($categoryRow.find(".col-xs-4")[getNumericImportanceLevel(newValue)]);
    }
    function increaseImportanceLevel($categoryRow) {
        updateImportanceLevel($categoryRow, true);
    }
    function decreaseImportanceLevel($categoryRow) {
        updateImportanceLevel($categoryRow, false);
    }
    function bindEvents() {
        $("#categoryContainerDiv").on("click", ".lessImportantButton", function (e) {
            decreaseImportanceLevel($(this).closest(".categoryCardRow"));
        });
        $("#categoryContainerDiv").on("click", ".moreImportantButton", function (e) {
            increaseImportanceLevel($(this).closest(".categoryCardRow"));
        });
    }
    CustomSolutions.bindEvents = bindEvents;
})(CustomSolutions || (CustomSolutions = {}));
//# sourceMappingURL=_customSolutions.js.map