define([], () => {

    let getTitles = (textInput, page, pageSize, callback) => {
        const  url = new URL("http://localhost:5002/api/title/basics");
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
        };
        
        url.search = new URLSearchParams(params).toString();

        fetch(url.toString())
            .then(response => response.json())
            .then(json => callback(json));
    };
    
    return {
        getTitles,
    }
});