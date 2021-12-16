define(['knockout', 'userService', 'storeService', 'myEventListener'], function (ko, us, store, myEventListener) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let getTitles = () => {
      const { token } = store.auth.getState();
      us.getTitlesBookMark(token, 0, 20, data => {
        console.log(data.items);
        titles(data.items);
      })
    };

    getTitles();

    return {
      titles,
      getTitles,
    };
  };
});
