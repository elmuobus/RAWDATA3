define(["knockout", "titleService"], function (ko, ts) {
    let accountViewButton = ko.observable("notConnected");
    
    let login = () => accountViewButton("connected");
    
    let logout = () => accountViewButton("notConnected");

    let currentView = ko.observable("list");
    
    let pageSize = ko.observable(20);
    
    let currentPageNumber = ko.observable(5);
    
    let totalPages = ko.observable();

    let titles = ko.observableArray([]);
    
    let pages = ko.observableArray([]);
    
    let goFirstPage = () => {
        currentPageNumber(1)
        getTitles()
    }
    
    let goLastPage = () => {
        currentPageNumber(totalPages())
        getTitles()
    }
    
    let goPrevPage = () => {
        currentPageNumber(currentPageNumber() - 1)
        getTitles()            
    }
    
    let goNextPage = () => {
        currentPageNumber(currentPageNumber() + 1)
        getTitles()
    }
    
    let goToPage = (number) => {
        currentPageNumber(number);
        getTitles()
    }
    
    let searchText = ko.observable("");
    
    let getTitles = () => {
        ts.getTitles(searchText(), currentPageNumber() - 1, pageSize(), data => {
            titles(data.items);
            currentPageNumber(data.current + 1);
            totalPages(data.totalPage + 1);
            initPageArray();
        });
    }
    
    let initPageArray = () => {
        const page = currentPageNumber()
        const lastPage = totalPages()
        let beforePages = page - 1;
        let afterPages = page + 1;
        
        if (page === lastPage) {
            afterPages -= 1;
            beforePages -= 2;
        } else if (page === lastPage - 1) {
            beforePages -= 1;
        }
        if (page === 1) {
            beforePages += 1;
            afterPages += 2;
        } else if (page === 2) {
            afterPages += 1;
        }

        pages(Array.from({length: (afterPages - beforePages) + 1}, (_, i) => i + beforePages));
    }

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");

    getTitles();

    return {
        accountViewButton,
        login,
        logout,
        currentView,
        titles,
        searchText,
        getTitles,
        pageSize,
        goFirstPage,
        goLastPage,
        goPrevPage,
        goNextPage,
        pages,
        goToPage,
        currentPageNumber,
        totalPages,
        searchFilterView,
        openSearchFilter,
        closeSearchFilter,
    }
});