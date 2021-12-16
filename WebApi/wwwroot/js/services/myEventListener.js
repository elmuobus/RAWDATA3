define([], () => {
    let subscribers = []; //List of events
    
    let subscribe = (event, callback, target) => {
        let subscriber = { event, callback, target};
        console.log("register")

        if (!subscribers.find(x => x.target === target && x.event === event))
            subscribers.push(subscriber);
    };

    let trigger = (event, data) => {//what

        subscribers.forEach(x => {
            if (x.event === event) x.callback(data); //for alle subscribere, hvis event er den som vi kalder trigger med, kør callback med (nye?) data. Man trigger et event. 
        });
    };

    return {
        subscribe,
        trigger
    }

});