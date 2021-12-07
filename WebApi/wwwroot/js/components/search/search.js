define(['knockout', 'storeService', 'myEventListener'], function (ko, store, myEventListener) {
  return function (_) {
    let searchText = ko.observable("");

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");
    
    let triggerSearch = () => {
      store.titles.dispatch({ type: "SEARCH" ,payload: searchText() });
      myEventListener.trigger("searching");
    }
    
    return {
      searchText,
      searchFilterView,
      openSearchFilter,
      closeSearchFilter,
      triggerSearch,
    };
  };
});
