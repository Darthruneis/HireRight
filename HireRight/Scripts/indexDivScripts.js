$(document).ready(function () {
    replaceInfoDivHtmlWith("#introDiv");
});

function replaceInfoDivHtmlWith(data) {
    var innerhtml = $(data).html();
    $("#infoDiv").html(innerhtml);
}