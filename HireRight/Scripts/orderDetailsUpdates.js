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
    $.ajax({
        method: "GET",
        url: "/Order/GetTotalPrice?itemSelected=" + $("#listOfProducts option:selected").val() + "&quantity=" + $("#Order_Quantity").val(),
        dataType: "html",
        beforeSend: function () {
            $("#orderTotal").html("loading...");
        }
    }).done(function (response) {
        $("#orderTotal").html(response);
    });
}