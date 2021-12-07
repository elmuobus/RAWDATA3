define(['knockout', 'userService', 'storeService', 'myEventListener'], function (ko, us, store, myEventListener) {
  return function (_) {
    let userName = ko.observable(store.auth.getState().username);
    
    let picture = ko.observable(store.auth.getState().picture);

    let titlesBookmark = ko.observable(10);
    let namesBookmark = ko.observable(18);

    let profileModal = ko.observable("noModal");
    
    let deleteProfileConfirmation = () => profileModal("deleteAccountModal");
    
    let closeAccountModal = () => profileModal("noModal");

    let getProfileInfo = () => {}
    
    let goTitlesBookmark = () => myEventListener.trigger("changeView", "bookmark-titles");
    
    let goNamesBookmark = () => myEventListener.trigger("changeView", "bookmark-names");
    
    let deleteProfile = () => {
      const { token } = store.auth.getState();
      console.log(token);
      us.delProfile(token);
      closeAccountModal();
    }
    
    return {
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
