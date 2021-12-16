define(['knockout', 'storeService', 'myEventListener'], function (ko, store, myEventListener) {
  return function (_) {
    
    let pageSize = ko.observable(store.titles.getState().size);
    let currentPageNumber = ko.observable(store.titles.getState().currentPage);
    let totalPages = ko.observable(store.titles.getState().total);
    let pages = ko.observableArray([]);
    
    const goFirstPage = () => changePage(1);

    const goLastPage = () => changePage(totalPages());

    const goPrevPage = () => changePage(currentPageNumber() - 1);

    const goNextPage = () => changePage(currentPageNumber() + 1);

    const goToPage = (number) => changePage(number);
    
    const initPageArray = () => {
      const page = currentPageNumber()
      const lastPage = totalPages()
      let beforePages = page > 1 ? page - 1: page;
      let afterPages = page < lastPage ? page + 1: page;

      if (page === lastPage) {
        beforePages -= 2;
      } else if (page === lastPage - 1) {
        beforePages -= 1;
      }
      if (page === 1) {
        afterPages += 2;
      } else if (page === 2) {
        afterPages += 1;
      }

      pages(Array.from({length: (afterPages - beforePages) + 1}, (_, i) => i + beforePages));
    }

    const changePage = (number) => {
      store.titles.dispatch({type: "PAGINATION", payload: number});
    };
    
    let currentTitles = store.titles.getState();
    store.titles.subscribe(() => {
      let previousTitles = currentTitles;
      currentTitles = store.titles.getState();

      if (previousTitles.currentPage !== currentTitles.currentPage) {
        currentPageNumber(currentTitles.currentPage);
      }
      if (previousTitles.totalPage !== currentTitles.totalPage) {
        totalPages(currentTitles.totalPage);
      }
      if (previousTitles.currentPage !== currentTitles.currentPage
      || previousTitles.totalPage !== currentTitles.totalPage) {
        initPageArray();
      }
    });

    return {
      pageSize,
      currentPageNumber,
      totalPages,
      goFirstPage,
      goLastPage,
      goPrevPage,
      goNextPage,
      pages,
      goToPage,
      initPageArray,
    };
  };
});
