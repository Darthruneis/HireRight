var CustomSolutions;
(function (CustomSolutions) {
    var CardCategoryCounts = /** @class */ (function () {
        function CardCategoryCounts(rel, crit) {
            this.relevant = rel;
            this.critical = crit;
            this.nice = rel - crit;
        }
        return CardCategoryCounts;
    }());
    var CardCssCache = /** @class */ (function () {
        function CardCssCache(left, right, pos) {
            this.mLeft = left;
            this.mRight = right;
            this.position = pos;
        }
        CardCssCache.prototype.restoreCss = function ($card) {
            $card.css("position", this.position);
            $card.css("margin-left", this.mLeft);
            $card.css("margin-right", this.mRight);
        };
        return CardCssCache;
    }());
    function bindEvents() {
        bindMovementButtons();
        bindContinueAndBackButtons();
        bindFormSubmit();
    }
    CustomSolutions.bindEvents = bindEvents;
    function bindMovementButtons() {
        $("#categoryContainerDiv").on("click", ".lessImportantButton", function (e) {
            updateImportanceLevel($(this).closest(".categoryCardRow"), false);
        });
        $("#categoryContainerDiv").on("click", ".moreImportantButton", function (e) {
            updateImportanceLevel($(this).closest(".categoryCardRow"), true);
        });
    }
    function bindContinueAndBackButtons() {
        $("#ContinueButton").on("click", function () {
            var results = inspectCardCounts();
            var isValid = true;
            if (results.relevant === 0 || results.relevant > 9) {
                $("#notEnough").show();
                isValid = false;
            }
            if (results.critical < 3) {
                $("#notEnoughCrits").show();
                isValid = false;
            }
            if (isValid)
                toggleIrrelevantCards();
        });
        $("#BackButton").on("click", function () {
            toggleIrrelevantCards();
        });
    }
    function bindFormSubmit() {
        $("form").on("submit", function (e) {
            return inspectCardCounts();
        });
    }
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
        return new CardCategoryCounts(numberOfRelevantCards, numberOfCriticalCards);
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
    function moveCardToNewColumn($categoryRow, $newCategoryRow, $card, cache) {
        cache.restoreCss($card);
        var detachedHtml = $card.detach();
        detachedHtml.appendTo($newCategoryRow);
        //restore original height for the row
        $categoryRow.css("height", "auto");
    }
    function animateCardMovement($categoryRow, $newCategoryRow, original, newValue) {
        if (original === newValue)
            return;
        var $card = $categoryRow.find(".categoryCard");
        //padding on columns is 15 - moving will always cross 2, so 15 + 15 = 30
        var width = parseInt($card.css("width")) + 30;
        var cache = new CardCssCache($card.css("margin-left"), $card.css("margin-right"), $card.css("position"));
        //preserve the height of the entire row during the animation
        $categoryRow.css("height", $card.css("height"));
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
        }, 500, function () { return moveCardToNewColumn($categoryRow, $newCategoryRow, $card, cache); });
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
        animateCardMovement($categoryRow, $($categoryRow.find(".categoryColumn")[getNumericImportanceLevel(newValue)]), original, current);
    }
})(CustomSolutions || (CustomSolutions = {}));
//# sourceMappingURL=_customSolutions.js.map