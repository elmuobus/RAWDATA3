define(['knockout', 'userService', 'myEventListener'], function (ko, us, myEventListener) {
  return function (_) {
    let accountModal = ko.observable("noModal");

    let closeAccountModal = () => {
      userName("");
      password("");
      errorMessage("");
      accountModal("noModal");
    }

    let openLoginModal = () => accountModal("loginModal");

    let openSignUpModal = () => accountModal("signUpModal");

    let userName = ko.observable("");

    let password = ko.observable("");

    let errorMessage = ko.observable("");

    let token = ko.observable();

    let login = () => {
      let user = {
        username: userName(),
        password: password(),
      }
      errorMessage("");

      us.login(user, data => {
        if (data.token) {
          userName("");
          password("");
          token(data.token)
          closeAccountModal();
          connected();
        } else {
          errorMessage("Your username and/or password is invalid");
          password("");
        }
      })
    };

    let register = () => {
      let user = {
        username: userName(),
        password: password(),
      }
      errorMessage("");

      us.login(user, data => {
        if (data.token) {
          userName("");
          password("");
          token(data.token)
          closeAccountModal();
          connected();
        } else {
          errorMessage("Your username and/or password is invalid");
          password("");
        }
      })
    }
    
    let connected = () => myEventListener.trigger("changeAccountButtonView", "account-connected")

    return {
      userName,
      password,
      errorMessage,
      closeAccountModal,
      openLoginModal,
      openSignUpModal,
      accountModal,
      login,
    };
  };
});
