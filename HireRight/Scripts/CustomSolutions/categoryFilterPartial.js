﻿function filterCategories(page, size) {
    var description = $("#Filter_DescriptionFilter").val();
    var title = $("#Filter_TitleFilter").val();
    var url = "/CustomSolutions/FilterCategories" + "?page=" + page + "&size=" + size + "&title=" + title + "&description=" + description;
    var processing = $("#processing");
    ajaxWithLoadingIcon("GET",
                        url,
                        "html",
                        processing,
                        updateCategoryList);
}

function updateCategoryList(data) {
    $("#CategoryListDiv").html(data);
}