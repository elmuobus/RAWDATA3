﻿define([], () => {
    let subscribers = [];
    
    let subscribe = (event, callback, target) => {
        let subscriber = { event, callback, target};

        if (!subscribers.find(x => x.target === target && x.event === event))
            subscribers.push(subscriber);
    };

    let trigger = (event, data) => {

        subscribers.forEach(x => {
            if (x.event === event) x.callback(data);
        });
    };

    return {
        subscribe,
        trigger
    }

});