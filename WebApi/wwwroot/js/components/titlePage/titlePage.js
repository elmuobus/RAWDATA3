define(['knockout', 'storeService', 'titleService', 'userService'], function (ko, store, ts, us) {
  return function (_) {
    let id = ko.observable();
    let poster = ko.observable();
    let title = ko.observable();
    let rating = ko.observable();
    let plot = ko.observable();
    let titleType = ko.observable();
    let genres = ko.observable();
    
    let bookmark = ko.observable(null);

    let formatYear = ko.observable();
    let hourConverted = ko.observable();
    
    let computedHourConverted = (runtimeMinutes) => {
      let hours = Math.floor(runtimeMinutes / 60);
      let minutes = runtimeMinutes % 60;

      hourConverted(`${hours > 0 ? `${hours}h ` : ''}${minutes}m`);
    }

    let getTitle = () => {
      const { titleUrl } = store.view.getState();
      const { token } = store.auth.getState();
      
      ts.getTitle(titleUrl,
        data => {
          id(data.id);
          poster(data.poster);
          title(data.originalTitle);
          genres(data.genres);
          titleType(data.titleType);
          rating(data.rating);
          plot(data.plot);

          computedHourConverted(data.runtimeMinutes);
          formatYear(data.startYear + (data.endYear !== "" ? `-${data.endYear}` : ''))

          if (token !== null) {
            us.getTitleBookMark(token, id(), status => {
              if (status === 204) {
                bookmark(false);
              }
              if (status === 200) {
                bookmark(true);
              }
            });
          }
        }
      );
    };
    
    let toggleBookmark = () => { //can go into it if authenticated so token !== null
      const { token } = store.auth.getState();
      
      if (bookmark() === true) {
        us.delTitleBookmark(token, id(), (status) => {
          if (status === 200) {
            bookmark(false);
          }
        })
      } else {
        us.addTitleBookmark(token, id(), (status) => {
          if (status === 201) {
            bookmark(true);
          }
        })
      }
    }

    getTitle();

    return {
      poster,
      title,
      plot,
      titleType,
      rating,
      genres,
      formatYear,
      hourConverted,
      bookmark,
      toggleBookmark,
    };
  };
});
