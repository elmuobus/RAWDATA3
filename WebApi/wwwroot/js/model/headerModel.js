define(["knockout"], function (ko) {
    let searchText = ko.observable("");

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");

    return {
        searchText,
        searchFilterView,
        openSearchFilter,
        closeSearchFilter,
    };
});