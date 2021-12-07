define(['knockout', 'storeService', 'myEventListener'], function (ko, store, myEventListener) {
  return function (_) {
    let names = ko.observableArray([]);

    let getNames = () => {
    };

    getNames();

    return {
      names,
      getNames,
    };
  };
});