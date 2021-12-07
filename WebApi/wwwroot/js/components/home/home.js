define(['knockout', 'titleService', 'storeService', 'myEventListener'], function (ko, ts, store, myEventListener) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let getTitles = () => {
      const { searchText, currentPage, pageSize } = store.titles.getState();
      ts.getTitles(
        searchText,
        currentPage - 1,
        pageSize,
        data => {
          titles(data.items);
          myEventListener.trigger("closeLoading");
          store.titles.dispatch({type: "RESULT", payload: data.totalPage + 1})
          myEventListener.trigger("changePagination")
        },
      );
    };
    
    setTimeout(() => {
      getTitles();
    }, 100);
    
    myEventListener.subscribe("searching", () => getTitles());

    myEventListener.subscribe("changeCurrentPage", () => getTitles());

    return {
      titles,
      getTitles,
    };
  };
});
