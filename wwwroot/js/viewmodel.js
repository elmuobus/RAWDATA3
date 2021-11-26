define(["knockout", "dataService", "titleService"], function (ko, ds, ts) {
    let accountViewButton = ko.observable("notConnected");
    
    let login = () => accountViewButton("connected");
    
    let logout = () => accountViewButton("notConnected");

    let currentView = ko.observable("list");

    let prevPageUrl = ko.observable();
    let nextPageUrl = ko.observable();
    let titles = ko.observableArray([]);

    let selectName = ko.observable();
    let selectDescription = ko.observable();
    
    let addCategoryView = () => currentView("add");

    let cancelAddCategory = () => currentView("list");
    
    let prevPage = () => {
        
    }
    
    let nextPage = () => {
        
    }

    ts.getTitles(data => {
        console.log(data);
        titles(data);
    });

    return {
        accountViewButton,
        login,
        logout,
        currentView,
        addCategoryView,
        cancelAddCategory,
        titles,
        selectName,
        selectDescription
    }
});