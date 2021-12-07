define(["knockout", "myEventListener"], function (ko, myEventListener) {
    
    let currentView = ko.observable("home");

    let currentAccountButtonView = ko.observable("account-disconnected");

    let menuItems = [
        { title: "Movies", component: "movie" },
        { title: "Series", component: "series" },
        { title: "Video-games", component: "video-game" },
    ];
    
    let goHome = () => {
      currentView("home");  
    };
    
    let headerChangeContent = menuItem => {
        currentView(menuItem.component)
    };

    let headerIsActive = menuItem => {
        return menuItem.component === currentView() ? "active" : "";
    };

    myEventListener.subscribe("changeView", function (data) {
        currentView(data);
    });

    myEventListener.subscribe("changeAccountButtonView", function (data) {
        currentAccountButtonView(data);
    });

    return {
        currentView,
        currentAccountButtonView,
        menuItems,
        goHome,
        headerChangeContent,
        headerIsActive,
    }
});