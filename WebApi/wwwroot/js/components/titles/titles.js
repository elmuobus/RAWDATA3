define(['knockout', 'titleService', 'storeService', 'myEventListener', 'simpleSearchService'], function (ko, ts, store, myEventListener, sss) {
  return function (_) {
    let titles = ko.observableArray([]);
    
    let getTitles = () => {
      const { searchText, currentPage, pageSize, types } = store.titles.getState();
      store.loading.dispatch({type: "START"});
      ts.getTitles(
        searchText,
        currentPage - 1,
        pageSize,
        types,
        data => {
            titles(data.items);
            console.log(data.items);
          store.loading.dispatch({type: "STOP"});
          store.titles.dispatch({type: "RESULT", payload: data.totalPage + 1})
        },
      );
    };
    
    setTimeout(() => {
      getTitles();
    }, 100);

    let currentTitles = store.titles.getState();
    store.titles.subscribe(() => {
      let previousTitles = currentTitles;
      currentTitles = store.titles.getState();
      
        if (previousTitles.types !== currentTitles.types) {
            store.titles.dispatch({ type: "SEARCH", payload: "" });
            store.search.dispatch({ type: "SEARCH", payload: "asdfsfesf" }); //Empty search. Just need to clear observable somehow. Or searchtext in 
            store.titles.dispatch({type: "PAGINATION", payload: 1});
      }
      if (previousTitles.currentPage !== currentTitles.currentPage
      || previousTitles.searchText !== currentTitles.searchText
      || previousTitles.types !== currentTitles.types) {
        getTitles();
      }
    });

    /*
    let getSearch = () => {
        const { searchText, nbResult} = store.search.getState();
        store.loading.dispatch({ type: "START" });
        sss.getBestMatchSearchResult(
            searchText,
            nbResult,
            data => {
                console.log(data);
                //titles(data.items); //Does not work, as i do not have correct elements to show. So it is done correctly, just need to make autocomplete and fill with titles. Make them links to urls i guess. 
                store.loading.dispatch({ type: "STOP" });
            },
        );
      };
    //Så længe vi ikke rører titles() forbliver indhold det samme, s¨å vi kan lave foreach med elementer der kommer tilbage fra funktion i search.html tror jeg. Så er den tom før inåut er der, med mindre den er helt fuld? Either way ville være sindssygt.
    //Bare forbinde observable med ting. Som i titles. Kunne sætte i titles, men mangler værdier og heller ikke sådan vi vil bruge på hjemmeside. 

    setTimeout(() => {
        getSearch();
    }, 100);
    
  store.search.subscribe(() => {
      getSearch();
  });
  */ 

  return {
      titles,
      getTitles,
    };
  };
});
