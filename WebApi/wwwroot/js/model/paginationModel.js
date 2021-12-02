define(["knockout", "homeModel"], function (ko, homeModel) {
    let pageSize = ko.observable(20);

    let currentPageNumber = ko.observable(5);

    let totalPages = ko.observable();

    let goFirstPage = () => {
        currentPageNumber(1)
        homeModel.getTitles()
    }

    let goLastPage = () => {
        currentPageNumber(totalPages())
        homeModel.getTitles()
    }

    let goPrevPage = () => {
        currentPageNumber(currentPageNumber() - 1)
        homeModel.getTitles()
    }

    let goNextPage = () => {
        currentPageNumber(currentPageNumber() + 1)
        homeModel.getTitles()
    }

    let goToPage = (number) => {
        currentPageNumber(number);
        homeModel.getTitles()
    }

    let pages = ko.observableArray([]);

    let initPageArray = () => {
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
});