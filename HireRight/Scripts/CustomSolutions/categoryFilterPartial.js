﻿function filterCategories(page, size) {
    var description = $("#DescriptionFilter").val();
    var title = $("#TitleFilter").val();
    var url = "/CustomSolutions/FilterCategories" + "?page=" + page + "&size=" + size + "&title=" + title + "&description=" + description;
    var processing = $("#processing");
    $.ajax({
        method: "GET",
        url: url,
        dataType: "html",
        success: function (data) {
            $("#CategoryListDiv").html(data);
        },
        complete: function () {
            processing.hide();
        },
        beforeSend: function () {
            processing.show();
        }
    });
}