define(['knockout', 'storeService', 'simpleSearchService', 'myEventListener', 'simpleSearchService'], function (ko, store, searchService, myEventListener, sss) {
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
        
        let getSearch = () => {
            const { searchText, nbResult } = store.search.getState();
            if(searchText!==""){ //So it does not search to start with and return empty so API service gives error.
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
        //Så længe vi ikke rører titles() forbliver indhold det samme, s¨å vi kan lave foreach med elementer der kommer tilbage fra funktion i search.html tror jeg. Så er den tom før inåut er der, med mindre den er helt fuld? Either way ville være sindssygt.
        //Bare forbinde observable med ting. Som i titles. 

        setTimeout(() => {//why
            getSearch();
        }, 100);

        store.search.subscribe(() => {
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
      getSearch
    };
  };
});
