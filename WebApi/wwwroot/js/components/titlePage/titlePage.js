define(['knockout', 'titleService', 'myEventListener'], function (ko, ts, myEventListener) {
    return function (_) {
        let url = "http://localhost:5001/api/title/basics/tt10850402";
        let poster = ko.observable("poster");
        let title = ko.observable("title");
        let rating = ko.observable("rating");
        let plot = ko.observable("plot");
        let titleType = ko.observable("titleType");
        let genres = ko.observable();

        let getTitle = () => {
                ts.getTitle(url,
                    data => {
                        console.log(data);
                        poster(data.poster);
                        title(data.originalTitle);
                        genres(data.genres);
                        titleType(data.titleType);
                        let startYear = data.startYear;
                        let runtimes = data.runtimeMinutes;
                        let endYear = data.endYear;
                        $(document).ready(function() {
                            let hours = Math.floor(runtimes / 60);
                            let minutes = runtimes % 60;
                            let formatHour = minutes + "m";
                            let formatYear = startYear;
                            if (hours > 0)
                                formatHour = hours + "h " + formatHour;
                            if (endYear != "")
                                formatYear = formatYear + "-" + endYear;
                            $('.hourConverted').html(formatHour);
                            $('.formatYear').html(formatYear);
                        });
                        rating(data.rating);
                        plot(data.plot);
                        myEventListener.trigger("closeLoading");
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
            getTitle
        };
    };
});
