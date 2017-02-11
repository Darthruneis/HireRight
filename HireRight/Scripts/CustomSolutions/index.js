//This variable needs to be created on the Index so that it is static relative to the
//partial views which will be reloaded, but will need to modify this collection based on user actions
var selectedCategories = [];
$(document).ready(function () {
    var form = $("#CustomSolutionsIndexForm");
    form.on('submit', function (e) {
        var newHiddenFields = [];
        for (var i = 0; i < selectedCategories.length; i++) {
            var inputIdPrefix = "CategoriesFromOtherPages" + i + "__";
            var inputName = "CategoriesFromOtherPages[" + i + "].";
            var selectedCategory = selectedCategories[i];
            var hiddenFieldForId = '<input type="hidden" name="' +
                inputName + 'Id" id="' + inputIdPrefix + '' + 'Id" value="' +
                selectedCategory.Id + '" />';
            newHiddenFields.push(hiddenFieldForId);
            var hiddenFieldForCategory = '<input type="hidden" name="' + inputName + 'Importance" id="' + inputIdPrefix + '' + 'Importance" value="' + selectedCategory.Importance + '" />';
            newHiddenFields.push(hiddenFieldForCategory);
        }
        form.append(newHiddenFields.join(""));
        return true;
    }
    );
});