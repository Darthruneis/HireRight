"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Navigation;
(function (Navigation) {
    function replaceDivContents(div, url) {
        var $div = $(div);
        ajaxWithLoadingIcon("GET", url, "html", $("#processing"), function (data) {
            $div.html(data);
        });
    }
    Navigation.replaceDivContents = replaceDivContents;
    function ajaxWithLoadingIcon(method, url, dataType, $loadingIconDiv, successFn) {
        if ($loadingIconDiv === void 0) { $loadingIconDiv = $("#processing"); }
        if (successFn === void 0) { successFn = null; }
        $.ajax({
            method: method.toUpperCase(),
            url: url,
            dataType: dataType,
            beforeSend: function () {
                $loadingIconDiv.show();
            },
            complete: function () {
                $loadingIconDiv.fadeOut(500);
            },
            success: successFn
        });
    }
    Navigation.ajaxWithLoadingIcon = ajaxWithLoadingIcon;
})(Navigation = exports.Navigation || (exports.Navigation = {}));
function setActiveLinkClass() {
    $('a').each(function () {
        if ($(this).prop('href') === window.location.href) {
            $(this).addClass('current');
        }
    });
}
$(document).ready(setActiveLinkClass);
//# sourceMappingURL=_Navigation.js.map