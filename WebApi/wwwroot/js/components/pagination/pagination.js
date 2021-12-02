define(['knockout', 'myEventListener'], function (ko, myEventListener) {
  return function (_) {
    let pageSize = ko.observable(20);
    let currentPageNumber = ko.observable(5);
    let totalPages = ko.observable();
    let pages = ko.observableArray([]);
    
    const goFirstPage = () => {
      currentPageNumber(1)
      changePage();
    }

    const goLastPage = () => {
      currentPageNumber(totalPages())
      changePage();
    }

    const goPrevPage = () => {
      currentPageNumber(currentPageNumber() - 1)
      changePage();
    }

    const goNextPage = () => {
      currentPageNumber(currentPageNumber() + 1)
      changePage();
    }

    const goToPage = (number) => {
      currentPageNumber(number);
      changePage();
    }
    
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

    const changePage = () => myEventListener.trigger("changeCurrentPage", currentPageNumber());

    myEventListener.subscribe("changePagination", ({ current, total }) => {
      currentPageNumber(current);
      totalPages(total);
      initPageArray();
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
