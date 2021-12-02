define(
    ["knockout", "titleService", "accountModel", "headerModel", "homeModel", "paginationModel"],
    function (ko, ts, accountModel, headerModel, homeModel, paginationModel) {
    
    let currentView = ko.observable("home");
    
    homeModel.getTitles();

    return {
        ...accountModel,
        ...headerModel,
        ...homeModel,
        ...paginationModel,
        currentView,
    }
});