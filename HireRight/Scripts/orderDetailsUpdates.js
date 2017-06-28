function updateDiscounts() {
    $.ajax({
        method: "GET",
        url: "/Order/GetDiscounts?itemSelected=" + $("#listOfProducts option:selected").val(),
        dataType: "html",
        beforeSend: function () {
            $("#productDiscounts").html("loading...");
        }
    }).done(function (response) {
        $("#productDiscounts").html(response);
    });
}

function updateTotal() {
    var $orderTotalDiv = $("#orderTotal");
    $.ajax({
        method: "GET",
        url: "/Order/GetTotalPrice?itemSelected=" + $("#listOfProducts option:selected").val() + "&quantity=" + $("#Quantity").val(),
        dataType: "html",
        beforeSend: function () {
            $orderTotalDiv.html("loading...");
        },
        error: function () {
            $orderTotalDiv.html("Unable to retrieve total.");
        }
    }).done(function (response) {
        $orderTotalDiv.html(response);
    });
}