var CategoriesList = {};

CategoriesList.displayPageOfCategories = function ($divToUpdate,
    enableActionsForCategories,
    page,
    size,
    description,
    title) {
    ajaxWithLoadingIcon(
        "GET",
        "/Categories/FilterCategories",
        "html",
        {
            page: page,
            size: size,
            description: description,
            title: title,
            enableActions: enableActionsForCategories
        }
        ,
        function (data) {
            $divToUpdate.html(data);
        }
    );
};

CategoriesList.bindPagingButtons = function ($divToUpdate, currentPage, enableActionsForCategories) {
    $divToUpdate.on("click",
        "#page1Button",
        function () {
            CategoriesList.displayPageOfCategories($divToUpdate, enableActionsForCategories, $(this).data("page"), 10, "", "");
        });
    $divToUpdate.on("click",
        "#FilterButton",
        function () {
            CategoriesList.displayPageOfCategories($divToUpdate, enableActionsForCategories, 1, 10, "", "");
        });
    $divToUpdate.on("click",
        "#prevPageButton",
        function () {
            CategoriesList.displayPageOfCategories($divToUpdate, enableActionsForCategories, $(this).data("page"), 10, "", "");
        });
    $divToUpdate.on("click",
        "#nextPageButton",
        function () {
            CategoriesList.displayPageOfCategories($divToUpdate, enableActionsForCategories, $(this).data("page"), 10, "", "");
        });
    $divToUpdate.on("click", "#lastPageButton", function() {
        CategoriesList.displayPageOfCategories($divToUpdate, enableActionsForCategories, $(this).data("page"), 10, "", "");
    });
};

// ReSharper disable UseOfImplicitGlobalInFunctionScope
//selectedCategories is an array that is defined in the Index view, because it must be
//maintained across the category list partial view being replaced.
CategoriesList.addOrUpdateSelectedCategories = function (id, importance) {
    var categoryInCollection;
    for (var i = 0; i < selectedCategories.length; i++) {
        if (selectedCategories[i].Id === id) {
            categoryInCollection = selectedCategories[i];
            break;
        }
    }

    if (categoryInCollection === undefined || categoryInCollection === null) {
        selectedCategories.push({ Id: id, Importance: importance });
    } else
        categoryInCollection.Importance = importance;
};

CategoriesList.unselectCategory = function (id) {
    var categoryIndex = -1;
    for (var i = 0; i < selectedCategories.length; i++) {
        if (selectedCategories[i].Id === id) {
            categoryIndex = i;
            break;
        }
    }
    if (categoryIndex < 0) return;

    selectedCategories.splice(categoryIndex, 1);
};

//This ready function is called every time the partial view is sent to the browser
//which is necessary in order to wire up the newly-rendered dropdowns
CategoriesList.wireUpDropdownDivsOnPartialViewLoad = function () {
    $(".categoryDropDownDiv")
        .change(function () {
            var element = $(this);
            var value = element.find(":selected").text();
            var id = element.data("categoryid");
            if (value !== "Not Important") {
                CategoriesList.addOrUpdateSelectedCategories(id, value);
            } else {
                CategoriesList.unselectCategory(id);
            }
        });
};

CategoriesList.bindevents = function (enableActionsForCategories, $divToUpdate, currentPage) {
    $(document).ready(function () {
        CategoriesList.wireUpDropdownDivsOnPartialViewLoad();
        CategoriesList.displayPageOfCategories(
            $divToUpdate,
            enableActionsForCategories,
            currentPage,
            10, //$("#Categories_CategoryFilter_PageSize").val(),
            "", //$("#Categories_CategoryFilter_CategoryFilter").val(),
            ""); //$("#Categories_CategoryFilter_TitleFilter").val());
    });

    CategoriesList.bindPagingButtons($divToUpdate, currentPage, enableActionsForCategories);
};

// ReSharper restore UseOfImplicitGlobalInFunctionScope