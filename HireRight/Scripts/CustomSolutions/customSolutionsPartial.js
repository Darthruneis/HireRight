//This ready function is called every time the partial view is sent to the browser
//which is necessary in order to wire up the newly-rendered dropdowns
$(document).ready(wireUpDropdownDivsOnPartialViewLoad);

function wireUpDropdownDivsOnPartialViewLoad() {
    $(".categoryDropDownDiv").change(function () {
        var element = $(this);
        var value = element.find(":selected").text();
        var id = element.data("categoryid");
        if (value !== "Not Important") {
            addOrUpdateSelectedCategories(id, value);
        } else {
            unselectCategory(id);
        }
    });
}

// ReSharper disable UseOfImplicitGlobalInFunctionScope
//selectedCategories is an array that is defined in the Index view, because it must be
//maintained across the category list partial view being replaced.
function addOrUpdateSelectedCategories(id, importance) {
    var categoryInCollection;
    for (var i = 0; i < selectedCategories.length; i++) {
        if (selectedCategories[i].Id === id) {
            categoryInCollection = selectedCategories[i];
            break;
        }
    }

    if (categoryInCollection === undefined || categoryInCollection === null) {
        selectedCategories.push({ Id: id, Importance: importance });
    }
    else
        categoryInCollection.Importance = importance;
}

function unselectCategory(id) {
    var categoryIndex = -1;
    for (var i = 0; i < selectedCategories.length; i++) {
        if (selectedCategories[i].Id === id) {
            categoryIndex = i;
            break;
        }
    }
    if (categoryIndex < 0) return;

    selectedCategories.splice(categoryIndex, 1);
}
// ReSharper restore UseOfImplicitGlobalInFunctionScope