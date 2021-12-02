define(['knockout', 'titleService', 'myEventListener'], function (ko, ts, myEventListener) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let searchText = "";
    let currentPageNumber = 1;
    let pageSize = 20;
    
    let getTitles = () => {
      ts.getTitles(
        searchText,
        currentPageNumber - 1,
        pageSize,
        data => {
          titles(data.items);
          myEventListener.trigger("closeLoading");
          myEventListener.trigger("changePagination", ({
            current: data.current + 1,
            total: data.totalPage + 1,
          }))
        },
      );
    };
    
    setTimeout(() => {
      getTitles();
    }, 100);
    
    myEventListener.subscribe("searching", (text) => {
      searchText = text;
      getTitles();
    });

    myEventListener.subscribe("changeCurrentPage", (page) => {
      currentPageNumber = page;
      getTitles();
    });

    return {
      titles,
      getTitles,
    };
  };
});
