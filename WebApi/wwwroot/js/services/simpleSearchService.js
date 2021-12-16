define([], () => { //rename to searchservice

    let getSearchResult = (title, callback) => {//rename to getSimpleSearchResult, also in other files
        const url = new URL("http://localhost:5002/api/search/simplesearch");
        let params = {//like ?title=userinput&user=null. Should probably just be removed from the simple search method. Maybe do a check in method, so it saves the search to the searchhistory if user is logged in.
            ...(title) && {
                title
            },
            ...(user) && {
                user: null //should be if user==null then null, else username
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };

    //Going to wait with these, as i maybe have to add the user. Fuck adding the user, lol. 
    
    let getBestMatchSearchResult = (searchKeys, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/search/bestmatch");
        let params = {//like ?title=userinput&user=null. Should probably just be removed from the simple search method. Maybe do a check in method, so it saves the search to the searchhistory if user is logged in.
            ...(searchKeys) && {
                searchKeys //if searchkeys exist, add searchkeys to object, as parameter called searchKeys
            },
            ...(nbResult) && {
                nbResult //should be if user==null then null, else username
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getExactMatchSearchResult = (searchKeys, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/search/exactmatch");
        let params = {//like ?title=userinput&user=null. Should probably just be removed from the simple search method. Maybe do a check in method, so it saves the search to the searchhistory if user is logged in.
            ...(searchKeys) && {
                searchKeys //if searchkeys exist, add searchkeys to object, as parameter called searchKeys
            },
            ...(nbResult) && {
                nbResult //should be if user==null then null, else username
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getStructuredActorSearchResult = (title, plot, characters, personNames, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/search/structuredactorsearch");
        let params = {//like ?title=userinput&user=null. Should probably just be removed from the simple search method. Maybe do a check in method, so it saves the search to the searchhistory if user is logged in.
            ...(title) && {
                title //if searchkeys exist, add searchkeys to object, as parameter called searchKeys
            },
            ...(plot) && {
                plot //should be if user==null then null, else username
            },
            ...(characters) && {
                characters //should be if user==null then null, else username
            },
            ...(personNames) && {
                personNames //should be if user==null then null, else username
            },
            ...(nbResult) && {
                nbResult //should be if user==null then null, else username
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };


    let getStructuredStringSearchResult = (title, plot, characters, personNames, nbResult, callback) => {
        const url = new URL("http://localhost:5002/api/search/structuredstringsearch");
        let params = {//like ?title=userinput&user=null. Should probably just be removed from the simple search method. Maybe do a check in method, so it saves the search to the searchhistory if user is logged in.
            ...(title) && {
                title //if searchkeys exist, add searchkeys to object, as parameter called searchKeys
            },
            ...(plot) && {
                plot //should be if user==null then null, else username
            },
            ...(characters) && {
                characters //should be if user==null then null, else username
            },
            ...(personNames) && {
                personNames //should be if user==null then null, else username
            },
            ...(nbResult) && {
                nbResult //should be if user==null then null, else username
            }
        };

        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };


    return {
        getSearchResult,
        getBestMatchSearchResult,
        getExactMatchSearchResult,
        getStructuredActorSearchResult,
        getStructuredStringSearchResult
    }
});