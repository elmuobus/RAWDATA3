define(['knockout', 'myEventListener'], function (ko, myEventListener) {
  return function (_) {
    let disconnected = () => myEventListener.trigger("changeAccountButtonView", "account-disconnected")

    return {
      disconnected,
    };
  };
});
