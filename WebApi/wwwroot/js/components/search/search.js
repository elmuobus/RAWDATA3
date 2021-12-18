define(['knockout', 'storeService', 'simpleSearchService', 'simpleSearchService'], function (ko, store, searchService, sss) {
    return function (_) {
        let recommended = ko.observableArray([]);

        let searchText = ko.observable("");
        
        searchText.subscribe(function (newValue) {
            if(newValue && newValue!=="")//Error in API when null.
                store.search.dispatch({ type: "SEARCH", payload: newValue });
            if (newValue === "")
                recommended([]); //To empty when no value in searchfield
      });
        
      let searchFilterView = ko.observable("searchPopUpButton");

      let openSearchFilter = () => searchFilterView("searchPopUp");

      let closeSearchFilter = () => searchFilterView("searchPopUpButton");
    
      let triggerSearch = () => {
        store.titles.dispatch({ type: "SEARCH" ,payload: searchText() });
      }

      let searchAttempt = () => {
        store.search.dispatch({ type: "SEARCH", payload: searchText() });
        //console.log("deez");
      }
      
      let goToTitle = (result) => {
        store.view.dispatch({ type: "TITLE", payload: result.url });
      }
        
      let getSearch = () => {
        const { searchText, nbResult } = store.search.getState();
        if(searchText!==""){ //So it does not search to start with and return empty so API service gives error. Maybe not needed as it is done in searchText.subscribe above?
          //store.loading.dispatch({ type: "START" });
          sss.getBestMatchSearchResult(
            searchText,
            nbResult,
            data => {
              recommended(data); //needs to be empty to start with. 
              console.log(data);
              //titles(data.items); //Does not work, as i do not have correct elements to show. So it is done correctly, just need to make autocomplete and fill with titles. Make them links to urls i guess. 
              //store.loading.dispatch({ type: "STOP" });
            },
          );
        }
      };
      //S� l�nge vi ikke r�rer titles() forbliver indhold det samme, s�� vi kan lave foreach med elementer der kommer tilbage fra funktion i search.html tror jeg. S� er den tom f�r in�ut er der, med mindre den er helt fuld? Either way ville v�re sindssygt.
      //Bare forbinde observable med ting. Som i titles. 

      setTimeout(() => {//why
        getSearch();
      }, 100);

      let currentTitles = store.titles.getState();
      store.search.subscribe(() => { //Only need getsearch() to search, adding other stuff for switching pages.
        let previousTitles = currentTitles;
        currentTitles = store.titles.getState();
        if (previousTitles.types !== currentTitles.types) {
          searchText(null);
        }
        getSearch();
      });

      return {
        recommended, 
        searchText,
        searchFilterView,
        openSearchFilter,
        closeSearchFilter,
        triggerSearch,
        searchAttempt,
        getSearch,
        goToTitle,
      };
    };
});
