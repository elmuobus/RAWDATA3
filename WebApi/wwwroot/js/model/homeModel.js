define(["knockout", "titleService", "headerModel","paginationModel"], 
  function (ko, ts, hm, pm) {
    let titles = ko.observableArray([]);

    let getTitles = () => {
      ts.getTitles(
        hm.searchText(),
        pm.currentPageNumber() - 1,
        pm.pageSize(),
        data => {
          titles(data.items);
          pm.currentPageNumber(data.current + 1);
          pm.totalPages(data.totalPage + 1);
          pm.initPageArray();
        },
      );
    }

  return {
    titles,
    getTitles,
  };
});