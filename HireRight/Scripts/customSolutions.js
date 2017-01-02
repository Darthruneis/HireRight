$(function () {
    $("#showContactAndCompanyInfo")
        .click(function () {
            toggleMultiple('#collapsedCompanyAndContactInfo', '#contactAndCompanyInfo');
        });

    $("#hideContactAndCompanyInfo")
        .click(function () {
            toggleMultiple('#contactAndCompanyInfo', '#collapsedCompanyAndContactInfo');
        });
});