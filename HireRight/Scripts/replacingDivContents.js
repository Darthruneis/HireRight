function replaceDivContents(div, url) {
    var $div = $(div);
    ajaxWithLoadingIcon("GET",
        url,
        "html",
        $("#processing"),
        function (data) {
            $div.html(data);
        });
}