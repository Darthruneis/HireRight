function replaceDivContents(div, url) {
    var $div = $(div);
    $.ajax({
        url: url,
        method: "GET",
        success: function (data) {
            $div.html(data);
        }
    });
}