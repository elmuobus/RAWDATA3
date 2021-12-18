define(['knockout', 'storeService'], function (ko, store) {
  return function (_) {
    let username = ko.observable(store.auth.getState().username);
    
    let picture = ko.observable(store.auth.getState().picture);
    
    let profilePage = () => store.view.dispatch({type: "VIEW", payload: "profile"});
    
    let disconnected = () => {
      store.view.dispatch({type: "VIEW", payload: "titles"});
      store.view.dispatch({type: "ACCOUNT", payload: "account-connected"});
    }

    return {
      username,
      picture,
      disconnected,
      profilePage,
    };
  };
});
