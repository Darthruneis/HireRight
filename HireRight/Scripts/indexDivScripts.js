function displayInfoDiv(data) {
    $("#infoDiv").html($(data).innerHTML);
    $("#infoDiv").attr("hidden", false);
    $("#introDiv").attr("hidden", true);
}
function displayIntroDiv() {
    $("#infoDiv").attr("hidden", true);
    $("#introDiv").attr("hidden", false);
}