define(['knockout', 'storeService', 'titleService'], function (ko, store, ts) {
  return function (_) {
    let url = "http://localhost:5001/api/title/basics/tt10850402";
    let poster = ko.observable("poster");
    let title = ko.observable("title");
    let rating = ko.observable("rating");
    let plot = ko.observable("plot");
    let titleType = ko.observable("titleType");
    let genres = ko.observable();

    let formatYear = ko.observable();
    let hourConverted = ko.observable();
    
    let computedHourConverted = (runtimeMinutes) => {
      let hours = Math.floor(runtimeMinutes / 60);
      let minutes = runtimeMinutes % 60;

      hourConverted(`${hours > 0 ? `${hours}h ` : ''}${minutes}m`);
    }

    let getTitle = () => {
      const { titleUrl } = store.view.getState();
      
      ts.getTitle(titleUrl,
        data => {
          poster(data.poster);
          title(data.originalTitle);
          genres(data.genres);
          titleType(data.titleType);
          rating(data.rating);
          plot(data.plot);
          let startYear = data.startYear;
          let runtimes = data.runtimeMinutes;
          let endYear = data.endYear;

          computedHourConverted(data.runtimeMinutes);
          formatYear(data.startYear + (data.endYear !== "" ? `-${data.endYear}` : ''))
        }
      );
    };

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
    };
  };
});
