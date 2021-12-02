define([], () => {

    let login = (user, callback) => {
        let param = {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                "Content-Type": "application/json"
            }
        }
        
        console.log("je suis bloqué")

        fetch("api/users/login", param)
            .then(response => response.json())
            .then(json => callback(json));
    };

    return {
        login,
    }
});