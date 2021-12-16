define([], () => {

    let getRecommendedTitles = (titleId, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/sitefunctions/recommended");
        let params = {
            ...(titleId) && {
                titleId //should probably add variable that selects number of recommended titles. 
            },
            ...(nbResult) && {
                nbResult
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };
    
    let getCoActors = (actorId, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/sitefunctions/findcoplayers");
        let params = {
            ...(actorId) && {
                actorId //should probably add variable that selects number of recommended titles. 
            },
            ...(nbResult) && {
                nbResult
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getPopularActorsInMovie = (titleId, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/sitefunctions/popularactorinmovie");
        let params = {
            ...(titleId) && {
                titleId //should probably add variable that selects number of recommended titles. 
            },
            ...(nbResult) && {
                nbResult
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getPopularActorsCoPlayers = (actorId, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/sitefunctions/recommended");
        let params = {
            ...(actorId) && {
                actorId //should probably add variable that selects number of recommended titles. 
            },
            ...(nbResult) && {
                nbResult
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };



    return {
        getRecommendedTitles,
        getCoActors,
        getPopularActorsInMovie,
        getPopularActorsCoPlayers
    }
});