define(["knockout", "storeService", "myEventListener"], function (ko, store, myEventListener) {
    
    let currentView = ko.observable("titles");
    
    let currentMenuItem = ko.observable();

    let currentAccountButtonView = ko.observable("account-disconnected");

    let menuItems = [
        { title: "Movies", types: "movie,tvMovie" },
        { title: "Series", types: "tvMiniSeries,tvEpisode,tvSeries" },
        { title: "Video-games", types: "videoGame" },
    ];
    
    let resetType = () => {
        currentMenuItem(null);
        store.titles.dispatch({type: "TYPES", payload: null});
    };
    
    let headerHomeContent = () => {
        currentView('titles');
        resetType();
    }
    
    let headerChangeContent = menuItem => {
        if (currentView() !== "titles") {
            currentView("titles");
        }
        currentMenuItem(menuItem.title);
        store.titles.dispatch({type: "TYPES", payload: menuItem.types});
    };

    let headerIsActive = menuItem => {
        return menuItem.title === currentMenuItem() ? "active" : "";
    };

    myEventListener.subscribe("goHome", function () {
        headerHomeContent();
    });

    myEventListener.subscribe("changeView", function (data) {
        currentView(data);
        resetType();
    });

    myEventListener.subscribe("changeAccountButtonView", function (data) {
        currentAccountButtonView(data);
    });

    return {
        currentView,
        currentAccountButtonView,
        menuItems,
        headerHomeContent,
        headerChangeContent,
        headerIsActive,
    }
});