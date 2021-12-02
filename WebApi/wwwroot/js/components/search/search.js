define(['knockout', 'myEventListener'], function (ko, myEventListener) {
  return function (_) {
    let searchText = ko.observable("");

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");
    
    let triggerSearch = () => myEventListener.trigger("searching", searchText());

    return {
      searchText,
      searchFilterView,
      openSearchFilter,
      closeSearchFilter,
      triggerSearch,
    };
  };
});
