var CustomSolutions;
(function (CustomSolutions) {
    "use strict";
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
    }
    CustomSolutions.bindEvents = bindEvents;
    function bindIndustryToggles() {
        $(".industryToggle:not(.generalIndustryToggle)").on("click", function (e) {
            var $this = $(this);
            var isBeingRemoved = $this.hasClass("activeIndustry");
            $(".industryToggle:not(.generalIndustryToggle)").removeClass("activeIndustry").find("i").hide();
            if (!isBeingRemoved) {
                $this.addClass("activeIndustry");
                $this.find("i").show();
            }
            updateCardVisibilityRanks();
        });
        $(".generalIndustryToggle").on("click", function (e) {
            var $this = $(this);
            $this.toggleClass("activeIndustry");
            $this.find("i").toggle();
            updateCardVisibilityRanks();
        });
    }
    function updateCardVisibilityRanks() {
        var $toggles = $(".activeIndustry").toArray().map(function (value, index, array) { return $(value); });
        var getHidden = function ($card) { return $card.closest(".categoryCardRow").find("input[type='hidden']"); };
        $(".categoryCard").each(function (index, elem) {
            var $card = $(elem);
            var rank = getVisibilityRankForCard($card, $toggles);
            $card.data("visibilityrank", rank);
            var $hidden = getHidden($card);
            if (rank > 0) {
                $card.show().closest(".categoryCardRow").show();
                if ($hidden.val() === "Irrelevant")
                    $hidden.val($card.data("cachedhiddenlevel"));
            }
            else {
                $card.hide().closest(".categoryCardRow").hide();
                $card.data("cachedhiddenlevel", $hidden.val());
                $hidden.val("Irrelevant");
            }
        });
    }
    function getVisibilityRankForCard($card, $toggles) {
        var specificCategory = "";
        var generalCategoryEnabled = false;
        var rankOfCard = 1;
        for (var i = 0; i < $toggles.length; i++) {
            var $toggle = $toggles[i];
            if ($toggle.hasClass("generalIndustryToggle"))
                generalCategoryEnabled = true;
            else
                specificCategory = $toggle.data("industryname");
        }
        if (generalCategoryEnabled)
            if ($card.hasClass("industry-General"))
                rankOfCard += 1;
            else
                rankOfCard -= 1;
        if (specificCategory !== "")
            if ($card.hasClass("industry-" + specificCategory))
                rankOfCard += 1;
            else
                rankOfCard -= 1;
        if (specificCategory === "" && !generalCategoryEnabled)
            return 1;
        if (rankOfCard > 2)
            rankOfCard = 2;
        return rankOfCard;
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
            makeHiddenCardsIrrelevant();
            var counts = inspectCardCounts();
            var result = true;
            if (counts.relevant < 1 || counts.relevant > 9) {
                $("#notEnough").show();
                result = false;
            }
            else
                $("#notEnough").hide();
            if (counts.critical < 3) {
                $("#notEnoughCrits").show();
                result = false;
            }
            else
                $("#notEnoughCrits").hide();
            return result;
        });
    }
    function makeHiddenCardsIrrelevant() {
        $("#categoryContainerDiv").find(".categoryCardRow").each(function (index, elem) {
            var $card = $(elem).find(".categoryCard");
            var visibilityRank = parseInt($card.data("visibilityrank"));
            if (visibilityRank <= 0) {
                $card.find("input[type='hidden']").val("Irrelevant");
            }
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
    var Tests;
    (function (Tests) {
        var CustomSolutionsTestCase = /** @class */ (function () {
            function CustomSolutionsTestCase(res, exp, name) {
                this.result = res;
                this.expected = exp;
                this.case = name;
            }
            CustomSolutionsTestCase.prototype.status = function () {
                return this.result === this.expected;
            };
            CustomSolutionsTestCase.prototype.print = function () {
                var passOrFail = this.status() ? "PASS" : "FAIL";
                return passOrFail + " - r: " + this.result + " e:" + this.expected + " - " + this.case;
            };
            return CustomSolutionsTestCase;
        }());
        function runTests() {
            var results = new Array();
            generalCases(results);
            specificCases(results);
            combinedCases(results);
            noneCases(results);
            var passed = 0;
            var total = 0;
            for (var i = 0; i < results.length; i++) {
                if (!results[i].status())
                    console.log(results[i].print());
                else
                    passed++;
                total = i + 1;
            }
            console.log(passed + "/" + total + " test cases passed");
            return passed === total;
        }
        Tests.runTests = runTests;
        function generalCases(results) {
            results.push(bar1(), bar2(), bar3(), bar4());
        }
        function bar1() {
            var $card = $("<i class='categoryCard' id='test-bar1-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 1: general applied, card for no industries");
        }
        function bar2() {
            var $card = $("<i class='categoryCard industry-General' id='test-bar2-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 2, "case 2: general applied, card for general and no other industries");
        }
        function bar3() {
            var $card = $("<i class='categoryCard industry-Artistic' id='test-bar3-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 3: general applied, card for other industry");
        }
        function bar4() {
            var $card = $("<i class='categoryCard industry-General industry-Artistic' id='test-bar4-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 2, "case 4: general applied, card for general and other industries");
        }
        function specificCases(results) {
            results.push(bar5(), bar6(), bar7(), bar8(), bar9(), bar10());
        }
        function bar5() {
            var $card = $("<i class='categoryCard' id='test-bar5-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 5: specific applied, card for no industries");
        }
        function bar6() {
            var $card = $("<i class='categoryCard industry-General' id='test-bar6-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 6: specific applied, card for general and no other industries");
        }
        function bar7() {
            var $card = $("<i class='categoryCard industry-Manufacturing' id='test-bar7-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 7: specific applied, card for other industry");
        }
        function bar8() {
            var $card = $("<i class='categoryCard industry-General industry-Manufacturing' id='test-bar8-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 0, "case 8: specific applied, card for general and other industries");
        }
        function bar9() {
            var $card = $("<i class='categoryCard industry-General industry-Artistic industry-Manufacturing' id='test-bar9-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 2, "case 9: specific applied, card for general and same industry and other industries");
        }
        function bar10() {
            var $card = $("<i class='categoryCard industry-Artistic' id='test-bar10-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 2, "case 10: specific applied, card for same industry");
        }
        function combinedCases(results) {
            results.push(bar11(), bar12());
        }
        function bar11() {
            var $card = $("<i class='categoryCard industry-Artistic' id='test-bar11-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case 11: general and specific applied, card for same industry");
        }
        function bar12() {
            var $card = $("<i class='categoryCard industry-General industry-Artistic' id='test-bar12-i-tag'></i>");
            var $toggles = new Array();
            $toggles.push($("<i data-industryname='Artistic'></i>"));
            $toggles.push($("<i class='generalIndustryToggle'></i>"));
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 2, "case 12: general and specific applied, card for general and same industry");
        }
        function noneCases(results) {
            results.push(barNone1(), barNone2(), barNone3(), barNone4(), barNone5());
        }
        function barNone1() {
            var $card = $("<i class='categoryCard industry-General industry-Artistic' id='test-barn1-i-tag'></i>");
            var $toggles = new Array();
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case n1: none applied, card for general and industry");
        }
        function barNone2() {
            var $card = $("<i class='categoryCard industry-General ' id='test-barn2-i-tag'></i>");
            var $toggles = new Array();
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case n2: none applied, card for general");
        }
        function barNone3() {
            var $card = $("<i class='categoryCard industry-Artistic' id='test-barn3-i-tag'></i>");
            var $toggles = new Array();
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case n3: none applied, card for industry");
        }
        function barNone4() {
            var $card = $("<i class='categoryCard industry-Manufacturing industry-Artistic' id='test-barn4-i-tag'></i>");
            var $toggles = new Array();
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case n4: none applied, card for industries");
        }
        function barNone5() {
            var $card = $("<i class='categoryCard' id='test-barn5-i-tag'></i>");
            var $toggles = new Array();
            var result = getVisibilityRankForCard($card, $toggles);
            return new CustomSolutionsTestCase(result, 1, "case n5: none applied, card for nothing");
        }
    })(Tests = CustomSolutions.Tests || (CustomSolutions.Tests = {}));
})(CustomSolutions || (CustomSolutions = {}));
//# sourceMappingURL=_customSolutions.js.map