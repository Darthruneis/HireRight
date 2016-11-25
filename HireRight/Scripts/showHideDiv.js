function hideDiv(divId) {
    document.getElementById(divId).hidden = true;
}

function showDiv(divId) {
    document.getElementById(divId).hidden = false;
}

function toggleDiv(divId) {
    document.getElementById(divId).hidden = !document.getElementById(divId).hidden;
}