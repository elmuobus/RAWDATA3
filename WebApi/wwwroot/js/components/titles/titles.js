define(['knockout', 'titleService', 'storeService', 'myEventListener'], function (ko, ts, store, myEventListener) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let getTitles = () => {
      const { searchText, currentPage, pageSize, types } = store.titles.getState();
      store.loading.dispatch({type: "START"});
      ts.getTitles(
        searchText,
        currentPage - 1,
        pageSize,
        types,
        data => {
          titles(data.items);
          store.loading.dispatch({type: "STOP"});
          store.titles.dispatch({type: "RESULT", payload: data.totalPage + 1})
        },
      );
    };
    
    setTimeout(() => {
      getTitles();
    }, 100);

    let currentTitles = store.titles.getState();
    store.titles.subscribe(() => {
      let previousTitles = currentTitles;
      currentTitles = store.titles.getState();
      
      if (previousTitles.types !== currentTitles.types) {
        store.titles.dispatch({type: "PAGINATION", payload: 1});
      }
      if (previousTitles.currentPage !== currentTitles.currentPage
      || previousTitles.searchText !== currentTitles.searchText
      || previousTitles.types !== currentTitles.types) {
        getTitles();
      }
    });
    
    return {
      titles,
      getTitles,
    };
  };
});
