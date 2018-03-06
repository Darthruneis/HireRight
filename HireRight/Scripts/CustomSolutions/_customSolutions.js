var CustomSolutions;
(function (CustomSolutions) {
    function bindEvents() {
        $("#categoryContainerDiv").on("click", ".lessImportantButton", function (e) {
            decreaseImportanceLevel($(this).closest(".categoryCardRow"));
        });
        $("#categoryContainerDiv").on("click", ".moreImportantButton", function (e) {
            increaseImportanceLevel($(this).closest(".categoryCardRow"));
        });
        $("#ContinueButton").on("click", function () {
            var isValid = inspectCardCounts();
            if (isValid)
                toggleIrrelevantCards();
        });
        $("#BackButton").on("click", function () {
            toggleIrrelevantCards();
        });
        $("form").on("submit", function (e) {
            return inspectCardCounts();
        });
    }
    CustomSolutions.bindEvents = bindEvents;
    function inspectCardCounts() {
        var numberOfRelevantCards = 0;
        var numberOfCriticalCards = 0;
        $(".categoryCardRow input[type='hidden']").each(function (index, elem) {
            var hiddenValue = $(elem).val();
            if (hiddenValue !== "Irrelevant") {
                numberOfRelevantCards++;
                if (hiddenValue === "HighImportance")
                    numberOfCriticalCards++;
            }
        });
        var isValid = true;
        if (numberOfRelevantCards === 0 || numberOfRelevantCards > 9) {
            $("#notEnough").show();
            isValid = false;
        }
        if (numberOfCriticalCards < 3) {
            $("#notEnoughCrits").show();
            isValid = false;
        }
        return isValid;
    }
    function toggleIrrelevantCards() {
        $(".importanceHeaders").find(":first-child").toggle();
        $(".importanceHeaders").find(":not(:first-child)").toggleClass("col-xs-4 col-xs-6");
        $("#BackButton").toggle();
        $("#ContinueButton").toggle();
        $("form input[type='submit']").closest("div").toggle();
        $("#notEnoughCrits").hide();
        $("#notEnough").hide();
        $(".categoryCardRow input[type='hidden']").each(function (index, elem) {
            var $row = $(elem).closest(".categoryCardRow");
            var $newValue = $(elem).val();
            $row.find(".categoryColumn").toggleClass("col-xs-4 col-xs-6");
            $row.find(".categoryColumn.lowestCategory").toggle();
            if ($newValue === "Irrelevant") {
                $row.toggle();
            }
        });
    }
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
    function toggleButtonsBasedOnImportance($categoryRow, newValue) {
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
    }
    function updateImportanceLevel($categoryRow, increase) {
        var original = getNumericImportanceLevel(getImportanceLevel($categoryRow));
        var current = original;
        if (increase)
            current = current + 1;
        else
            current = current - 1;
        var newValue = getStringImportanceLevel(current);
        toggleButtonsBasedOnImportance($categoryRow, newValue);
        $categoryRow.find("input[type='hidden']").val(newValue);
        moveCardToNewColumn($categoryRow, $($categoryRow.find(".categoryColumn")[getNumericImportanceLevel(newValue)]), original, current);
    }
    function moveCardToNewColumn($categoryRow, $newCategoryRow, original, newValue) {
        if (original === newValue)
            return;
        var $card = $categoryRow.find(".categoryCard");
        //padding on columns is 15 - moving will always cross 2, so 15 + 15 = 30
        var width = parseInt($card.css("width")) + 30;
        var cache = {
            mLeft: $card.css("margin-left"),
            mRight: $card.css("margin-right"),
            position: $card.css("position")
        };
        //preserve the height of the entire row during the animation
        $categoryRow.css("height", $card.css("height"));
        var promise = function () {
            //restore original css
            $card.css("position", cache.position);
            $card.css("margin-left", cache.mLeft);
            $card.css("margin-right", cache.mRight);
            var detachedHtml = $card.detach();
            detachedHtml.appendTo($newCategoryRow);
            //restore original height for the row
            $categoryRow.css("height", "auto");
        };
        $card.css("position", "absolute");
        var mLeft;
        var mRight;
        if (original > newValue) {
            //moving to the right
            mLeft = -width;
            mRight = width;
        }
        else {
            mLeft = width;
            mRight = -width;
        }
        $card.animate({
            'margin-left': mLeft,
            'margin-right': mRight
        }, 700, promise);
    }
    function increaseImportanceLevel($categoryRow) {
        updateImportanceLevel($categoryRow, true);
    }
    function decreaseImportanceLevel($categoryRow) {
        updateImportanceLevel($categoryRow, false);
    }
})(CustomSolutions || (CustomSolutions = {}));
//# sourceMappingURL=_customSolutions.js.map