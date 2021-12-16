define(['knockout', 'storeService', 'simpleSearchService', 'myEventListener'], function (ko, store, searchService, myEventListener) {
    return function (_) {
    

    let searchText = ko.observable("");

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");
    
    let triggerSearch = () => {
      store.titles.dispatch({ type: "SEARCH" ,payload: searchText() });
    }

    let currentTitles = store.titles.getState();
    store.titles.subscribe(() => {
        let previousTitles = currentTitles;
        currentTitles = store.titles.getState();

        if (previousTitles.searchText !== currentTitles.searchText) {
            searchText(currentTitles.searchText);
        }
    });
    
    return {
      searchText,
      searchFilterView,
      openSearchFilter,
      closeSearchFilter,
        triggerSearch,
    };
  };
});
