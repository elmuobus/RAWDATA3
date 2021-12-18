define(["knockout", "storeService", "myEventListener"], function (ko, store, myEventListener) {
    
    let currentView = ko.observable(store.view.getState().component);
    
    let currentMenuItem = ko.observable();

    let currentAccountButtonView = ko.observable(store.view.getState().accountComponent);

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
        store.view.dispatch({type: "VIEW", payload: "titles"});
    }
    
    let headerChangeContent = menuItem => {
        if (currentView() !== "titles") {
            store.view.dispatch({type: "VIEW", payload: "titles"});
        }
        currentMenuItem(menuItem.title);
        store.titles.dispatch({type: "TYPES", payload: menuItem.types});
    };

    let headerIsActive = menuItem => {
        return menuItem.title === currentMenuItem() ? "active" : "";
    };

    let currentViewState = store.view.getState();
    store.view.subscribe(() => {
        let previousViewState = currentViewState;
        currentViewState = store.view.getState();

        if (previousViewState.component !== currentViewState.component) {
            currentView(currentViewState.component);
            resetType();
        }
        if (previousViewState.accountComponent !== currentViewState.accountComponent) {
            currentAccountButtonView(currentViewState.accountComponent);
        }
        if (previousViewState.titleUrl !== currentViewState.titleUrl
          && currentViewState.titleUrl !== null) {
            store.view.dispatch({type: "VIEW", payload: "title-page"});
        }
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