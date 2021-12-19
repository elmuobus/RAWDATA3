define([], () => {

    let getTitles = (textInput, page, pageSize, types, callback) => {
        const  url = new URL(`http://localhost:5002/api/title/basics${types ? "/specific" : ""}`);
        let params = {
            ...(textInput) && {
                SearchTitle: textInput
            },
            ...(pageSize) && {
                PageSize: pageSize
            },
            ...(page) && {
                Page: page
            },
            ...(types) && {
                Types: types
            },
        };
        
        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };

    let getTitle = (url, callback) => {
        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };
    
    let seeBookmark = () => {
        
    }

    return {
        getTitles,
        getTitle
    }
});