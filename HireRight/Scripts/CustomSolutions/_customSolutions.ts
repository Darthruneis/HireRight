namespace CustomSolutions {
    class CardCategoryCounts {
        relevant: number;
        critical: number;
        nice: number;

        constructor(rel: number, crit: number) {
            this.relevant = rel;
            this.critical = crit;
            this.nice = rel - crit;
        }
    }

    class CardCssCache {
        mLeft: string;
        mRight: string;
        position: string;

        restoreCss($card: JQuery): void {
            $card.css("position", this.position);
            $card.css("margin-left", this.mLeft);
            $card.css("margin-right", this.mRight);
        }

        constructor(left: string, right: string, pos: string) {
            this.mLeft = left;
            this.mRight = right;
            this.position = pos;
        }
    }

    export function bindEvents() {
        bindMovementButtons();
        bindContinueAndBackButtons();
        bindFormSubmit();
        bindIndustryToggles();
    }

    function bindIndustryToggles() {
        $(".industryToggle:not(.generalIndustryToggle)").on("click",
            function (e: Event) {
                var $this = $(this);
                var isBeingRemoved: boolean = $this.hasClass("activeIndustry");
                $(".industryToggle:not(.generalIndustryToggle)").removeClass("activeIndustry").find("i").hide();

                var visibilityRankChange: number = -1;
                if (!isBeingRemoved) {
                    $this.addClass("activeIndustry");
                    $this.find("i").show();
                    visibilityRankChange = 1;
                }
                updateCardVisibilityRanks(parseInt($this.data("industryid")), visibilityRankChange);
            });

        $(".generalIndustryToggle").on("click",
            function (e: Event) {
                var $this = $(this);
                $this.toggleClass("activeIndustry");
                $this.find("i").toggle();
                var visibilityRankChange: number = $this.hasClass("activeIndustry") ? 1 : -1;
                updateCardVisibilityRanks(parseInt($this.data("industryid")), visibilityRankChange);
            });
    }
    
    function updateCardVisibilityRanks(industry: number, visibilityRankChange: number) {
        $(".categoryCard").each((index, elem) => {
            if ($(elem).data("industry-" + industry) !== 1)
                updateCardVisibilityRank($(elem), -visibilityRankChange);
            else
                updateCardVisibilityRank($(elem), visibilityRankChange);
        });

        $(".categoryCard").each((index, elem) => {
            var visibilityRank: number = parseInt($(elem).data("visibilityrank"));
            if (visibilityRank > 0) {
                $(elem).show();
            } else {
                $(elem).data("visibilityrank", "0");
                $(elem).hide();
            }
        });
    }

    function updateCardVisibilityRank($card: JQuery, visibilityRankChange: number) {
        var visibilityRank: number = parseInt($card.data("visibilityrank"));
        visibilityRank += visibilityRankChange;
        $card.data("visibilityrank", visibilityRank);
    }

    function bindMovementButtons() {
        $("#categoryContainerDiv").on("click",
            ".lessImportantButton",
            function (e) {
                updateImportanceLevel($(this).closest(".categoryCardRow"), false);
            });

        $("#categoryContainerDiv").on("click",
            ".moreImportantButton",
            function (e) {
                updateImportanceLevel($(this).closest(".categoryCardRow"), true);
            });
    }

    function bindContinueAndBackButtons() {
        $("#ContinueButton").on("click",
            function () {
                var results: CardCategoryCounts = inspectCardCounts();

                var isValid: boolean = true;
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

        $("#BackButton").on("click", () => {
            toggleIrrelevantCards();
        });
    }

    function bindFormSubmit() {
        $("form").on("submit",
            function (e: Event) {
                return inspectCardCounts();
            });
    }

    function inspectCardCounts(): CardCategoryCounts {
        var numberOfRelevantCards: number = 0;
        var numberOfCriticalCards: number = 0;
        $(".categoryCardRow input[type='hidden']").each((index, elem) => {
            var hiddenValue = $(elem).val();
            if (hiddenValue !== "Irrelevant") {
                numberOfRelevantCards++;
                if (hiddenValue === "HighImportance")
                    numberOfCriticalCards++;
            }
        });

        return new CardCategoryCounts(numberOfRelevantCards, numberOfCriticalCards);
    }

    function toggleIrrelevantCards(): void {
        $(".importanceHeaders").find(":first-child").toggle();
        $(".importanceHeaders").find(":not(:first-child)").toggleClass("col-xs-4 col-xs-6");

        $("#BackButton").toggle();
        $("#ContinueButton").toggle();
        $("form input[type='submit']").closest("div").toggle();

        $("#notEnoughCrits").hide();
        $("#notEnough").hide();

        $(".categoryCardRow input[type='hidden']").each((index, elem) => {
            var $row = $(elem).closest(".categoryCardRow");
            $row.find(".categoryColumn").toggleClass("col-xs-4 col-xs-6");
            $row.find(".categoryColumn.lowestCategory").toggle();
        });
    }

    function getImportanceLevel($categoryRow: JQuery): string {
        return $categoryRow.find("input[type='hidden']").val();
    }
    function getNumericImportanceLevel(stringValue: string): number {
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
    function getStringImportanceLevel(intValue: number): string {
        if (intValue >= 2)
            return "HighImportance";
        if (intValue <= 0)
            return "Irrelevant";
        return "LowImportance";
    }

    function toggleButtonsBasedOnImportance($categoryRow: JQuery, newValue: string): void {
        if (newValue === "HighImportance") {
            $categoryRow.find(".moreImportantButton").hide();
        }
        else if (newValue === "Irrelevant") {
            $categoryRow.find(".lessImportantButton").hide();
        } else {
            $categoryRow.find(".moreImportantButton").show();
            $categoryRow.find(".lessImportantButton").show();
        }
    }

    function moveCardToNewColumn($categoryRow: JQuery, $newCategoryRow: JQuery, $card: JQuery, cache: CardCssCache) {
        cache.restoreCss($card);

        var detachedHtml = $card.detach();
        detachedHtml.appendTo($newCategoryRow);

        //restore original height for the row
        $categoryRow.css("height", "auto");
    }

    function animateCardMovement($categoryRow: JQuery, $newCategoryRow: JQuery, original: number, newValue: number) {
        if (original === newValue) return;

        var $card = $categoryRow.find(".categoryCard");
        //padding on columns is 15 - moving will always cross 2, so 15 + 15 = 30
        var distanceToMove: number = parseInt($card.css("width")) + 30;
        var cache: CardCssCache = new CardCssCache($card.css("margin-left"), $card.css("margin-right"), $card.css("position"));

        var mLeft: number = distanceToMove;
        var mRight: number = distanceToMove;
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
        },
            500,
            () => moveCardToNewColumn($categoryRow, $newCategoryRow, $card, cache));
    }

    function updateImportanceLevel($categoryRow: JQuery, increase: boolean): void {
        var original = getNumericImportanceLevel(getImportanceLevel($categoryRow));
        var current = increase ? original + 1 : original - 1;

        var newValue: string = getStringImportanceLevel(current);
        toggleButtonsBasedOnImportance($categoryRow, newValue);

        $categoryRow.find("input[type='hidden']").val(newValue);

        animateCardMovement($categoryRow, $($categoryRow.find(".categoryColumn")[getNumericImportanceLevel(newValue)]), original, current);
    }
}