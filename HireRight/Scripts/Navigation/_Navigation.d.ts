declare namespace Navigation {
    function replaceDivContents(div: JQuery, url: string): void;
    function ajaxWithLoadingIcon(method: string, url: string, dataType: string, $loadingIconDiv?: JQuery, successFn?: (data: any, textStatus: string, jqXHR: JQueryXHR) => void): void;
}
declare function setActiveLinkClass(): void;
