//This ready function is called every time the partial view is sent to the browser
//which is necessary in order to wire up the newly-rendered dropdowns
$(document).ready(function () {
    $(".categoryDropDownDiv").change(function (e) {
        var element = $(this);
        var value = element.find(":selected").text();
        var id = element.data("categoryid");
        if (value !== "Not Important") {
            addOrUpdateSelectedCategories(id, value);
        } else {
            unselectCategory(id);
        }
    });
});

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