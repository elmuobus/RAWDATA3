define(['knockout', 'userService', 'storeService'], function (ko, us, store) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let getTitles = () => {
      const { token } = store.auth.getState();
      us.getTitlesBookMark(token, 0, 20, data => {
        titles(data.items);
      })
    };
    
    let removeTitle = (title) => {
      const { token } = store.auth.getState();
      us.delTitleBookmark(token, title.titleId, (status) => {
        if (status === 200)
          titles.remove(title);
      });
    }

    getTitles();

    return {
      titles,
      getTitles,
      removeTitle,
    };
  };
});
