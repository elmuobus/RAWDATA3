define(['knockout', 'userService', 'storeService', 'myEventListener'], function (ko, us, store, myEventListener) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImFsZXhpc2QiLCJuYmYiOjE2Mzg3Mzc5MjUsImV4cCI6MTYzODc0NTEyNSwiaWF0IjoxNjM4NzM3OTI1fQ.XBcj0AnaUuCyOmhjcGL_uDMdb4u72s8UbXmIldy6Bns"

    let getTitles = () => {
      us.getTitlesBookMark(token, 0, 20, data => {
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
