define(["knockout", "titleService"], function (ko, ts) {
    let accountViewButton = ko.observable("notConnected");
    
    let login = () => accountViewButton("connected");
    
    let logout = () => accountViewButton("notConnected");

    let currentView = ko.observable("list");
    
    let prevPageUrl = ko.observable();
    let nextPageUrl = ko.observable();
    let titles = ko.observableArray([]);

    let prevPage = () => {
        
    }
    
    let nextPage = () => {
        
    }

    let searchFilterView = ko.observable("searchPopUpButton");

    let openSearchFilter = () => searchFilterView("searchPopUp");

    let closeSearchFilter = () => searchFilterView("searchPopUpButton");

    ts.getTitles(data => {
        console.log(data);
        titles(data.items);
    });

    return {
        accountViewButton,
        login,
        logout,
        currentView,
        titles,
        searchFilterView,
        openSearchFilter,
        closeSearchFilter,
    }
});