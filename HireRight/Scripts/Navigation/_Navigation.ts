export namespace Navigation {
    export function replaceDivContents(div:JQuery, url: string) {
        var $div = $(div);
        ajaxWithLoadingIcon("GET",
            url,
            "html",
            $("#processing"),
            function (data) {
                $div.html(data);
            });
    }

    export function ajaxWithLoadingIcon(method: string, url: string, dataType: string, $loadingIconDiv: JQuery = $("#processing"), successFn:  (data: any, textStatus: string, jqXHR: JQueryXHR) => void = null) {
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
}

function setActiveLinkClass() {
    $('a').each(function () {
        if ($(this).prop('href') === window.location.href) {
            $(this).addClass('current');
        }
    });
}

$(document).ready(setActiveLinkClass);