"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Orders;
(function (Orders) {
    function bindEvents() {
        updateDiscounts();
        $("#listOfProducts").change(updateDiscounts);
        $("#Order_Quantity").change(updateTotal);
    }
    Orders.bindEvents = bindEvents;
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
    Orders.updateDiscounts = updateDiscounts;
    function updateTotal() {
        var $orderTotalDiv = $("#orderTotal");
        var selectedProduct = $("#listOfProducts option:selected").val();
        var quantity = $("#Order_Quantity").val();
        $.ajax({
            method: "GET",
            url: "/Order/GetTotalPrice?itemSelected=" + selectedProduct + "&quantity=" + quantity,
            dataType: "html",
            beforeSend: function () {
                $orderTotalDiv.html("loading...");
            }
        }).done(function (response) {
            $orderTotalDiv.html(response);
        });
    }
    Orders.updateTotal = updateTotal;
})(Orders = exports.Orders || (exports.Orders = {}));
//# sourceMappingURL=_Order.js.map