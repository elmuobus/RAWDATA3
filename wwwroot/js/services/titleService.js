define([], () => {

    let getTitles = (callback) => {
        fetch("api/title/basics")
            .then(response => response.json())
            .then(json => callback(json));
    };

    return {
        getTitles,
    }
});