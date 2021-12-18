define(['knockout', 'userService', 'storeService'], function (ko, us, store) {
  return function (_) {
    let userName = ko.observable(store.auth.getState().username);
    
    let picture = ko.observable(store.auth.getState().picture);

    let titlesBookmark = ko.observable(10);
    let namesBookmark = ko.observable(18);

    let profileModal = ko.observable("noModal");
    
    let deleteProfileConfirmation = () => profileModal("deleteAccountModal");
    
    let closeAccountModal = () => profileModal("noModal");

    let getProfileInfo = () => {}
    
    let goTitlesBookmark = () => store.view.dispatch({type: "VIEW", payload: "bookmark-titles"});
    
    let deleteProfile = () => {
      const { token } = store.auth.getState();
      closeAccountModal();
      us.delProfile(token, (status) => {
        console.log(status)
        if (status === 200) {
          store.view.dispatch({type: "VIEW", payload: "titles"});
          store.view.dispatch({type: "ACCOUNT", payload: "account-disconnected"});
        }
      });
    }
    
    return {
      picture,
      userName,
      titlesBookmark,
      namesBookmark,
      profileModal,
      deleteProfileConfirmation,
      closeAccountModal,
      getProfileInfo,
      goTitlesBookmark,
      goNamesBookmark,
      deleteProfile,
    };
  };
});
