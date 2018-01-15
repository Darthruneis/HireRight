﻿function ajaxWithLoadingIcon(method, url, dataType, loadingIconDiv, successFn) {
    var processing = $(loadingIconDiv);
    $.ajax({
        method: method.toUpperCase(),
        url: url,
        dataType: dataType,
        beforeSend: function () {
            processing.show();
        },
        complete: function () {
            processing.fadeOut(750);
        },
        success: successFn
    });
}