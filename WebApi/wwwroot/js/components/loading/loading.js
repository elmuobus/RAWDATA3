define(['knockout', 'myEventListener'], function (ko, myEventListener) {
  return function (_) {
    let display = ko.observable("");
    
    let close = () => display("close");
    
    myEventListener.subscribe("openLoading", () => {
      display("");
    });
    
    myEventListener.subscribe("closeLoading", () => {
      close();
    });

    return {
      display,
    };
  };
});
