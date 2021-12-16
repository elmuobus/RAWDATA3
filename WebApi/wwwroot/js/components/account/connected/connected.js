define(['knockout', 'storeService', 'myEventListener'], function (ko, store, myEventListener) {
  return function (_) {
    let username = ko.observable(store.auth.getState().username);
    
    let picture = ko.observable(store.auth.getState().picture);
    
    let profilePage = () => myEventListener.trigger("changeView", "profile");
    
    let disconnected = () => {
      myEventListener.trigger("changeAccountButtonView", "account-disconnected")
      myEventListener.trigger("goHome")
    }

    return {
      username,
      picture,
      disconnected,
      profilePage,
    };
  };
});
