define(["redux"], (redux) => {
  const reducer = (state = {}, action) => {
    switch (action.type) {
      case "LOGIN":
        state.token = action.payload;
        break;
      case "LOGOUT":
        state.token = null;
        break;
      case "DELETE":
        state.token = null;
        break;
    }
    return state;
  }
  
  const store = redux.createStore(reducer);

  const login = (token) => store.dispatch({ type: "LOGIN", payload: token });

  const logout = () => store.dispatch({ type: "LOGOUT" });

  const deleteProfile = () => store.dispatch({ type: "DELETE" });
  
  return {
    store,
    login,
    logout,
    deleteProfile,
  }
});