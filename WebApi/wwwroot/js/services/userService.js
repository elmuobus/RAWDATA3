define([], () => {
    let login = (user, callback) => {
        let options = {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                "Content-Type": "application/json"
            }
        }
        
        fetch("api/users/login", options)
            .then(response => response.json())
            .then(json => callback(json));
    };

    let register = (user, callback) => {
        let options = {
            method: "POST",
            body: JSON.stringify(user),
            headers: {
                "Content-Type": "application/json"
            }
        }

        fetch("api/users/register", options)
          .then(response => response.json())
          .then(json => callback(json));
    };
    
    let getTitlesBookMark = (token, page, pageSize, callback) => {
        const  url = new URL("http://localhost:5002/api/users/titlebookmarks");
        let params = {
            ...(pageSize) && {
                PageSize: pageSize
            },
            ...(page) && {
                Page: page
            },
        };
        let options = {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString(), options)
          .then(response => response.json())
          .then(json => callback(json));
    };

    let delTitleBookmark = (token, titleId, callback) => {
        let options = {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }

        fetch(`api/users/titlebookmarks/${titleId}`, options)
          .then(response => callback(response.status));
    };
    
    let delProfile = (token, callback) => {
        let options = {
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        }

        fetch("api/users", options)
          .then(response => callback(response.status));
    };

    return {
        login,
        register,
        delProfile,
        delTitleBookmark,
        getTitlesBookMark,
    }
});