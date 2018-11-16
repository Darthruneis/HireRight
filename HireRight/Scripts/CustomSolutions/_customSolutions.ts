namespace CustomSolutions
{
    export namespace Cards
    {

        class CardCategoryCounts
        {
            public relevant: number;
            public critical: number;
            public nice: number;

            constructor(rel: number, crit: number)
            {
                this.relevant = rel;
                this.critical = crit;
                this.nice = rel - crit;
            }
        }

        class CardCssCache
        {
            public mLeft: string;
            public mRight: string;
            public position: string;

            public restoreCss($card: JQuery): void
            {
                $card.css("position", this.position);
                $card.css("margin-left", this.mLeft);
                $card.css("margin-right", this.mRight);
            }

            constructor(left: string, right: string, pos: string)
            {
                this.mLeft = left;
                this.mRight = right;
                this.position = pos;
            }
        }

        export function bindEvents(selectedIndustry: number, isGeneralSelected: boolean,
            minCriticals: number, maxCriticals: number, maxNice: number) : void
        {
            bindMovementButtons();
            bindFormSubmit(minCriticals, maxCriticals, maxNice);
            bindIndustryToggles();
            bindContinueAndBackButtons();
            bindCategoryTitleToToggle();
            if (selectedIndustry !== 0 || isGeneralSelected)
            {
                if (selectedIndustry !== 0)
                {
                    $(`.industryToggle:not(.generalIndustryToggle)[data-industryid='${selectedIndustry}']`)
                        .trigger("click");
                }
                if (isGeneralSelected)
                {
                    $(".generalIndustryToggle").trigger("click");
                }

                updateCardVisibilityRanks();
                toggleIrrelevantCards();
                $("#ContinueButton").trigger("click");
            }
        }

        function bindIndustryToggles(): void
        {
            $(".industryToggle:not(.generalIndustryToggle)").on("click",
                function (e: Event)
                {
                    var $this: JQuery = $(this);

                    var isBeingRemoved: boolean = $this.hasClass("activeIndustry");
                    $(".industryToggle:not(.generalIndustryToggle)").removeClass("activeIndustry").find("i").hide();

                    if (!isBeingRemoved)
                    {
                        $this.addClass("activeIndustry");
                        $this.find("i").show();
                    }
                    updateCardVisibilityRanks();
                });

            $(".generalIndustryToggle").on("click",
                function (e: Event)
                {
                    var $this: JQuery = $(this);
                    $this.toggleClass("activeIndustry");
                    $this.find("i").toggle();
                    updateCardVisibilityRanks();
                });
        }

        function updateCardVisibilityRanks(): void
        {
            var $toggles: JQuery[] = $(".activeIndustry").toArray().map((value, index, array) => $(value));
            const getHidden: ($card: JQuery) => JQuery = ($card: JQuery) => $card.closest(".categoryCardRow").find("input[type='hidden']");

            $(".categoryCard").each((index, elem) =>
            {
                var $card: JQuery = $(elem);
                var rank: number = getVisibilityRankForCard($card, $toggles);
                $card.data("visibilityrank", rank);
                var $hidden: JQuery = getHidden($card);
                if (rank > 0)
                {
                    $card.show().closest(".categoryCardRow").show();
                    if ($hidden.val() === "Irrelevant")
                        $hidden.val($card.data("cachedhiddenlevel"));
                } else
                {
                    $card.hide().closest(".categoryCardRow").hide();
                    $card.data("cachedhiddenlevel", $hidden.val());
                    $hidden.val("Irrelevant");
                }
            });
        }

        function getVisibilityRankForCard($card: JQuery, $toggles: Array<JQuery>): number
        {
            var specificCategory: string = "";
            var generalCategoryEnabled: boolean = false;
            var rankOfCard: number = 1;
            for (var i: number = 0; i < $toggles.length; i++)
            {
                var $toggle: JQuery = $toggles[i];
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

        function bindMovementButtons(): void
        {
            $("#categoryContainerDiv").on("click",
                ".lessImportantButton",
                function (e)
                {
                    updateImportanceLevel($(this).closest(".categoryCardRow"), false);
                });

            $("#categoryContainerDiv").on("click",
                ".moreImportantButton",
                function (e)
                {
                    updateImportanceLevel($(this).closest(".categoryCardRow"), true);
                });
        }

        function cardCountsAreValid(minCriticals: number, maxCriticals: number, maxNice: number): boolean
        {
            var results: CardCategoryCounts = inspectCardCounts();
            var isValid: boolean = true;
            if (results.critical > maxCriticals)
            {
                isValid = false;
                $("#tooManyCrits").show();
            }
            if (results.nice > maxNice)
            {
                $("#tooManyNice").show();
                isValid = false;
            }
            if (results.critical < minCriticals)
            {
                $("#notEnoughCrits").show();
                isValid = false;
            }

            return isValid;
        }

        function bindContinueAndBackButtons(): void
        {
            $("#ContinueButton").on("click",
                function ()
                {
                    toggleIrrelevantCards();
                });

            $("#BackButton").on("click", () =>
            {
                toggleIrrelevantCards();
            });
        }

        function bindFormSubmit(minCriticals: number, maxCriticals: number, maxNice: number): void
        {
            $("#ReviewButton").on("click",
                function (e: Event)
                {
                    updateCardVisibilityRanks();
                    makeHiddenCardsIrrelevant();
                    var isGeneralSelected: boolean = $(".generalIndustryToggle").hasClass("activeIndustry");
                    var activeToggles: JQuery = $(".activeIndustry:not(.generalIndustryToggle)");
                    var selectedIndustry: number = 0;
                    if (activeToggles != null)
                        selectedIndustry = activeToggles.data("industryid");

                    $("#SelectedIndustry").val(selectedIndustry);
                    $("#IsGeneralSelected").val(isGeneralSelected.toString());

                    var result: boolean = cardCountsAreValid(minCriticals, maxCriticals, maxNice);
                    if (result)
                    {
                        $("#notEnough").hide();
                        $("#notEnoughCrits").hide();
                        $("form").submit();
                    }
                });
        }

        function makeHiddenCardsIrrelevant(): void
        {
            $("#categoryContainerDiv").find(".categoryCardRow").each((index, elem) =>
            {
                var $card: JQuery = $(elem).find(".categoryCard");
                var visibilityRank: number = parseInt($card.data("visibilityrank"));
                if (visibilityRank <= 0)
                {
                    $card.find("input[type='hidden']").val("Irrelevant");
                }
            });
        }

        function inspectCardCounts(): CardCategoryCounts
        {
            var numberOfRelevantCards: number = 0;
            var numberOfCriticalCards: number = 0;
            $(".categoryCardRow input[type='hidden']").each((index, elem) =>
            {
                var hiddenValue = $(elem).val();
                if (hiddenValue !== "Irrelevant")
                {
                    numberOfRelevantCards++;
                    if (hiddenValue === "HighImportance")
                        numberOfCriticalCards++;
                }
            });

            return new CardCategoryCounts(numberOfRelevantCards, numberOfCriticalCards);
        }

        function toggleIrrelevantCards(): void
        {
            $(".importanceHeaders").find(":first-child").toggle();
            $(".importanceHeaders").find(":not(:first-child)").toggleClass("col-xs-4 col-xs-6");

            $("#BackButton").toggle();
            $("#ContinueButton").toggle();
            $("#ReviewButton").toggle();

            $("#notEnoughCrits").hide();
            $("#notEnough").hide();

            $(".categoryCardRow input[type='hidden']").each((index, elem) =>
            {
                var $row: JQuery = $(elem).closest(".categoryCardRow");
                $row.find(".categoryColumn").toggleClass("col-xs-4 col-xs-6");
                $row.find(".categoryColumn.lowestCategory").toggle();
            });
        }

        function toggleButtonsBasedOnImportance($categoryRow: JQuery, newValue: string): void
        {
            if (newValue === "HighImportance")
            {
                $categoryRow.find(".moreImportantButton").hide();
            }
            else if (newValue === "Irrelevant")
            {
                $categoryRow.find(".lessImportantButton").hide();
            } else
            {
                $categoryRow.find(".moreImportantButton").show();
                $categoryRow.find(".lessImportantButton").show();
            }
        }

        function moveCardToNewColumn($categoryRow: JQuery, $newCategoryRow: JQuery, $card: JQuery, cache: CardCssCache, newValue: number): void
        {
            cache.restoreCss($card);

            var detachedHtml: JQuery = $card.detach();
            detachedHtml.removeClass("Irrelevant LowImportance HighImportance");
            detachedHtml.addClass(getStringImportanceLevel(newValue));
            detachedHtml.appendTo($newCategoryRow);

            //restore original height for the row
            $categoryRow.css("height", "auto");
            $categoryRow.data("ismoving", "false");
        }

        function animateCardMovement($categoryRow: JQuery, $newCategoryRow: JQuery, original: number, newValue: number): void
        {
            if (original === newValue) return;

            var $card: JQuery = $categoryRow.find(".categoryCard");
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
                () => moveCardToNewColumn($categoryRow, $newCategoryRow, $card, cache, newValue));
        }

        function updateImportanceLevel($categoryRow: JQuery, increase: boolean): void
        {
            var isMoving: boolean = $categoryRow.data("ismoving").toString().toLower() === "true";
            if (isMoving)
                return;

            $categoryRow.data("ismoving", "true");
            var original: number = getNumericImportanceLevel(getImportanceLevel($categoryRow));
            var current: number = increase ? original + 1 : original - 1;

            var newValue: string = getStringImportanceLevel(current);
            toggleButtonsBasedOnImportance($categoryRow, newValue);

            $categoryRow.find("input[type='hidden']").val(newValue);

            animateCardMovement($categoryRow, $($categoryRow.find(".categoryColumn")[getNumericImportanceLevel(newValue)]), original, current);
        }

        export namespace Tests
        {
            class CustomSolutionsTestCase
            {
                public result: number;
                public expected: number;
                public case: string;

                public status(): boolean
                {
                    return this.result === this.expected;
                }
                public print(): string
                {
                    var passOrFail: string = this.status() ? "PASS" : "FAIL";
                    return `${passOrFail} - r: ${this.result} e:${this.expected} - ${this.case}`;
                }

                constructor(res: number, exp: number, name: string)
                {
                    this.result = res;
                    this.expected = exp;
                    this.case = name;
                }
            }
            export function runTests(): boolean
            {
                var results: CustomSolutionsTestCase[] = new Array<CustomSolutionsTestCase>();
                generalCases(results);
                specificCases(results);
                combinedCases(results);
                noneCases(results);
                var passed: number = 0;
                var total: number = 0;
                for (var i: number = 0; i < results.length; i++)
                {
                    if (!results[i].status())
                        console.log(results[i].print());
                    else
                        passed++;
                    total = i + 1;
                }
                console.log(passed + "/" + total + " test cases passed");
                return passed === total;
            }
            function generalCases(results: CustomSolutionsTestCase[]): void
            {
                results.push(bar1(), bar2(), bar3(), bar4());
            }
            function bar1()
            {
                var $card: JQuery = $("<i class='categoryCard' id='test-bar1-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i class='generalIndustryToggle'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 1: general applied, card for no industries");
            }
            function bar2()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General' id='test-bar2-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i class='generalIndustryToggle'></i>"));
                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 2, "case 2: general applied, card for general and no other industries");
            }
            function bar3()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Artistic' id='test-bar3-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i class='generalIndustryToggle'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 3: general applied, card for other industry");
            }
            function bar4()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General industry-Artistic' id='test-bar4-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i class='generalIndustryToggle'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 2, "case 4: general applied, card for general and other industries");
            }
            function specificCases(results: CustomSolutionsTestCase[]): void
            {
                results.push(bar5(), bar6(), bar7(), bar8(), bar9(), bar10());
            }
            function bar5()
            {
                var $card: JQuery = $("<i class='categoryCard' id='test-bar5-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 5: specific applied, card for no industries");
            }
            function bar6()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General' id='test-bar6-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 6: specific applied, card for general and no other industries");
            }
            function bar7()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Manufacturing' id='test-bar7-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 7: specific applied, card for other industry");
            }
            function bar8()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General industry-Manufacturing' id='test-bar8-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 0, "case 8: specific applied, card for general and other industries");
            }
            function bar9()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General industry-Artistic industry-Manufacturing' id='test-bar9-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 2, "case 9: specific applied, card for general and same industry and other industries");
            }
            function bar10()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Artistic' id='test-bar10-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 2, "case 10: specific applied, card for same industry");
            }
            function combinedCases(results: CustomSolutionsTestCase[]): void
            {
                results.push(bar11(), bar12());
            }
            function bar11()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Artistic' id='test-bar11-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));
                $toggles.push($("<i class='generalIndustryToggle'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case 11: general and specific applied, card for same industry");
            }
            function bar12()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General industry-Artistic' id='test-bar12-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();
                $toggles.push($("<i data-industryname='Artistic'></i>"));
                $toggles.push($("<i class='generalIndustryToggle'></i>"));

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 2, "case 12: general and specific applied, card for general and same industry");
            }
            function noneCases(results: CustomSolutionsTestCase[]): void
            {
                results.push(barNone1(), barNone2(), barNone3(), barNone4(), barNone5());
            }
            function barNone1()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General industry-Artistic' id='test-barn1-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case n1: none applied, card for general and industry");
            }
            function barNone2()
            {
                var $card: JQuery = $("<i class='categoryCard industry-General ' id='test-barn2-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case n2: none applied, card for general");
            }
            function barNone3()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Artistic' id='test-barn3-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case n3: none applied, card for industry");
            }
            function barNone4()
            {
                var $card: JQuery = $("<i class='categoryCard industry-Manufacturing industry-Artistic' id='test-barn4-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case n4: none applied, card for industries");
            }
            function barNone5()
            {
                var $card: JQuery = $("<i class='categoryCard' id='test-barn5-i-tag'></i>");
                var $toggles: JQuery[] = new Array<JQuery>();

                var result: number = getVisibilityRankForCard($card, $toggles);
                return new CustomSolutionsTestCase(result, 1, "case n5: none applied, card for nothing");
            }
        }
    }

    export namespace RadioButtons
    {
        export function bindEvents(): void {
            bindCategoryTitleToToggle();
            bindIndustryButtons();
        }

        function bindIndustryButtons()
        {
            $(".industryToggle").on("click",
                function (e: Event)
                {
                    var $this: JQuery = $(this);
                    $this.toggleClass("activeIndustry");
                    $this.find("i").toggle();

                    hideCategoriesFromInactiveIndustries();
                });
        }

        function hideCategoriesFromInactiveIndustries(): void {
            const activeIndustries: JQuery[] = $.makeArray($(".industryToggle.activeIndustry"))
                .map((value, index, array) => $(value));
            if (activeIndustries.length === 0) {
                $("#categoryContainerDiv .categoryContainer").show();
                return;
            } 
            $("#categoryContainerDiv .categoryContainer").each((i, elem) => {
                var counter: number = 0;
                var $row: JQuery = $(elem).find(".row");
                for (i = 0; i < activeIndustries.length; i++) {
                    const industry: JQuery = activeIndustries[i];
                    const industryName = industry.data("industryname");
                    if ($row.hasClass(`industry-${industryName}`))
                        counter++;
                }
                $(elem).toggle(counter > 0);
            });
        }
    }

    function bindCategoryTitleToToggle() {
        $(".categoryDescriptionToggle").on("click",
            function (e: Event) {
                var $this: JQuery = $(this);
                var $container: JQuery = $this.closest(".categoryContainer");
                var $description: JQuery = $container.find(".categoryDescription");
                $description.toggle();
            });
    }

    function getImportanceLevel($categoryRow: JQuery): string
    {
        return $categoryRow.find("input[type='hidden']").val();
    }
    function getNumericImportanceLevel(stringValue: string): number
    {
        switch (stringValue)
        {
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
    function getStringImportanceLevel(intValue: number): string
    {
        if (intValue >= 2)
            return "HighImportance";
        if (intValue <= 0)
            return "Irrelevant";
        return "LowImportance";
    }
}