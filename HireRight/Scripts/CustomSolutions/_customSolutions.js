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
        bindIndustryToggles();
        toggleCards();
    }
    CustomSolutions.bindEvents = bindEvents;
    function bindIndustryToggles() {
        $(".industryToggle:not(.generalIndustryToggle)").on("click", function (e) {
            var $this = $(this);
            var isBeingRemoved = $this.hasClass("activeIndustry");
            $(".industryToggle:not(.generalIndustryToggle)").removeClass("activeIndustry").find("i").hide();
            var visibilityRankChange = -1;
            if (!isBeingRemoved) {
                $this.addClass("activeIndustry");
                $this.find("i").show();
                visibilityRankChange = 1;
            }
            updateCardVisibilityRanks(parseInt($this.data("industryid")), visibilityRankChange);
        });
        $(".generalIndustryToggle").on("click", function (e) {
            var $this = $(this);
            $this.toggleClass("activeIndustry");
            $this.find("i").toggle();
            var visibilityRankChange = $this.hasClass("activeIndustry") ? 1 : -1;
            updateCardVisibilityRanks(parseInt($this.data("industryid")), visibilityRankChange);
        });
    }
    function updateCardVisibilityRanks(industry, visibilityRankChange) {
        $(".categoryCard").each(function (index, elem) {
            if ($(elem).data("industry-" + industry) != null)
                updateCardVisibilityRank($(elem), visibilityRankChange);
        });
        toggleCards();
    }
    function toggleCards() {
        $(".categoryCard").each(function (index, elem) {
            var visibilityRank = parseInt($(elem).data("visibilityrank"));
            var $elem = $(elem);
            var getHidden = function () { return $elem.closest(".categoryCardRow").find("input[type='hidden']"); };
            if (visibilityRank > 0) {
                $elem.show().closest(".categoryCardRow").show();
                if (getHidden().val() === "Irrelevant")
                    getHidden().val($elem.data("cachedhiddenlevel"));
            }
            else {
                $elem.data("visibilityrank", "0");
                $elem.hide().closest(".categoryCardRow").hide();
                $elem.data("cachedhiddenlevel", getHidden().val());
                getHidden().val("Irrelevant");
            }
        });
    }
    function updateCardVisibilityRank($card, visibilityRankChange) {
        var visibilityRank = parseInt($card.data("visibilityrank"));
        visibilityRank += visibilityRankChange;
        $card.data("visibilityrank", visibilityRank);
    }
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
            $row.find(".categoryColumn").toggleClass("col-xs-4 col-xs-6");
            $row.find(".categoryColumn.lowestCategory").toggle();
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
        var distanceToMove = parseInt($card.css("width")) + 30;
        var cache = new CardCssCache($card.css("margin-left"), $card.css("margin-right"), $card.css("position"));
        var mLeft = distanceToMove;
        var mRight = distanceToMove;
        if (original > newValue)
            //moving to the right
            mLeft *= -1;
        else
            //moving to the left
            mRight *= -1;
        //preserve the height of the entire row during the animation
        $categoryRow.css("height", $card.css("height"));
        $card.css("position", "absolute");
        $card.animate({
            'margin-left': mLeft,
            'margin-right': mRight
        }, 500, function () { return moveCardToNewColumn($categoryRow, $newCategoryRow, $card, cache); });
    }
    function updateImportanceLevel($categoryRow, increase) {
        var original = getNumericImportanceLevel(getImportanceLevel($categoryRow));
        var current = increase ? original + 1 : original - 1;
        var newValue = getStringImportanceLevel(current);
        toggleButtonsBasedOnImportance($categoryRow, newValue);
        $categoryRow.find("input[type='hidden']").val(newValue);
        animateCardMovement($categoryRow, $($categoryRow.find(".categoryColumn")[getNumericImportanceLevel(newValue)]), original, current);
    }
})(CustomSolutions || (CustomSolutions = {}));
//# sourceMappingURL=_customSolutions.js.map