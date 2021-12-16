define(['knockout', 'storeService'], function (ko, store) {
  return function (_) {
    let display = ko.observable("close");
    
    let currentLoading = store.loading.getState();
    store.loading.subscribe(() => {
      let previousLoading = currentLoading;
      currentLoading = store.loading.getState();
      
      if (previousLoading !== currentLoading) {
        display(currentLoading ? "" : "close");
      }
    });
    
    return {
      display,
    };
  };
});
