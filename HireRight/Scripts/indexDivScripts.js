function displayInfoDiv(data) {
    document.getElementById("infoDiv").innerHTML = document.getElementById(data).innerHTML;
    document.getElementById("infoDiv").hidden = false;
    document.getElementById("introDiv").hidden = true;
}

function displaySubInfoDiv(data) {
    document.getElementById("subInfoDiv").innerHTML = document.getElementById(data).innerHTML;
    document.getElementById("subInfoDiv").hidden = false;
    document.getElementById("introDiv").hidden = true;
}

function displayIntroDiv() {
    document.getElementById("infoDiv").hidden = true;
    document.getElementById("introDiv").hidden = false;
}