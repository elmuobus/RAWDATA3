define(["knockout", "userService"], function (ko, us) {

    let accountViewButton = ko.observable("notConnected");

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
                accountViewButton("connected")
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
                accountViewButton("connected")
            } else {
                errorMessage("Your username and/or password is invalid");
                password("");
            }
        })
    }

    let logout = () => accountViewButton("notConnected");

    return {
        accountViewButton,
        userName,
        password,
        errorMessage,
        closeAccountModal,
        openLoginModal,
        openSignUpModal,
        accountModal,
        login,
        logout,
    };
});