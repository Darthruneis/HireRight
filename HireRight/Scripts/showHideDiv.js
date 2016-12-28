function hideDiv(divId) {
    $(divId).hide();
}

function showDiv(divId) {
    $(divId).show();
}

function toggleDiv(divId) {
    $(divId).toggle();
}

function toggleMultiple(show, hide) {
    $(show).toggle();
    $(hide).toggle();
}