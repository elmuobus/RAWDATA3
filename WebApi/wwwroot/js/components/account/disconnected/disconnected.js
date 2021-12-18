define(['knockout', 'userService', 'storeService'], function (ko, us, store) {
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
    
    let rePassword = ko.observable("");

    let errorMessage = ko.observable("");

    let login = () => {
      const name = userName();
      const pw = password();

      if (!name || !pw) {
        errorMessage("Not all field fill");
        return;
      }

      errorMessage("");

      const user = {
        username: name,
        password: pw,
      }

      us.login(user, data => {
        if (data.token) {
          userName("");
          password("");
          store.auth.dispatch({ type: "LOGIN", payload: data });
          closeAccountModal();
          connected();
        } else {
          errorMessage("Your username and/or password is invalid");
          password("");
        }
      })
    };

    let register = () => {
      const name = userName();
      const pw = password();
      const rePw = rePassword();
      
      if (!name || !pw || !rePw) {
        errorMessage("Not all field fill");
        return;
      }

      if (pw !== rePw) {
        errorMessage("Not the same password");
        return;        
      }

      errorMessage("");

      const user = {
        username: name,
        password: pw,
      }

      us.register(user, data => {
        if (data.username) {
          userName("");
          password("");
          closeAccountModal();
        } else {
          errorMessage("Username already used");
          password("");
        }
      })
    }
    
    let connected = () => store.view.dispatch({type: "ACCOUNT", payload: "account-connected"});

    return {
      userName,
      password,
      rePassword,
      errorMessage,
      closeAccountModal,
      openLoginModal,
      openSignUpModal,
      accountModal,
      login,
      register,
    };
  };
});
