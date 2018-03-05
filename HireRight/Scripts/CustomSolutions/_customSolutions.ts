namespace CustomSolutions {
    export function bindEvents() {
        $("#categoryContainerDiv").on("click",
            ".lessImportantButton",
            function (e) {
                decreaseImportanceLevel($(this).closest(".categoryCardRow"));
            });
        $("#categoryContainerDiv").on("click",
            ".moreImportantButton",
            function (e) {
                increaseImportanceLevel($(this).closest(".categoryCardRow"));
            });
        $("#ContinueButton, #BackButton").on("click", () => { toggleIrrelevantCards(); });
    }

    function toggleIrrelevantCards(): void {
        $(".importanceHeaders").find(":not(:first-child)").toggleClass("col-xs-4 col-xs-6");
        $(".importanceHeaders").find(":first-child").toggle();
        $(".categoryCardRow input[type='hidden']").each((index, elem) => {
            var $row = $(elem).closest(".categoryCardRow");
            var $newValue = $(elem).val();
            if ($newValue === "Irrelevant") {
                $row.toggle();
            } else {
                $row.find(".col-xs-4, .col-xs-6").toggleClass("col-xs-4 col-xs-6").first().toggle();
            }
            toggleButtonsBasedOnImportance($row, $newValue);
        });
        $("form input[type='submit']").closest("div").toggle();
        $("#ContinueButton").toggle();
        $("#BackButton").toggle();
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

    function updateImportanceLevel($categoryRow: JQuery, increase: boolean): void {
        var current = getNumericImportanceLevel(getImportanceLevel($categoryRow));
        if (increase)
            current = current + 1;
        else
            current = current - 1;

        var newValue = getStringImportanceLevel(current);
        toggleButtonsBasedOnImportance($categoryRow, newValue);

        $categoryRow.find("input[type='hidden']").val(newValue);

        var detachedHtml = $categoryRow.find(".categoryCard").detach();
        detachedHtml.appendTo($categoryRow.find(".col-xs-4")[getNumericImportanceLevel(newValue)]);
    }

    function increaseImportanceLevel($categoryRow): void {
        updateImportanceLevel($categoryRow, true);
    }

    function decreaseImportanceLevel($categoryRow): void {
        updateImportanceLevel($categoryRow, false);
    }
}