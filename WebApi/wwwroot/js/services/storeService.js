define(["redux"], (redux) => {
  
  //auth store
  
  const defaultAuthConfig = {
    username: "",
    token: null,
    picture: "static/images/noProfileImage.jpg"
  };
  
  const authReducer = (state = defaultAuthConfig, action) => {
    switch (action.type) {
      case "LOGIN":
        state = {
          ...state,
          ...action.payload //{ username, token }
        };
        break;
      case "LOGOUT":
        state.token = null;
        break;
      case "DELETE":
        state = {};
        break;
    }
    return state;
  }
  
  const auth = redux.createStore(authReducer);

  //search store
  
  const defaultTitlesConfig = {
    searchText: "",
    currentPage: 1,
    pageSize: 20,
    totalPage: 0,
  }

  const titlesReducer = (state = defaultTitlesConfig, action) => {
    switch (action.type) {
      case "SEARCH":
        state = {
          ...state,
          searchText: action.payload
        };
        break;
      case "PAGINATION":
        state = {
          ...state,
          currentPage: action.payload
        };
        break;
      case "RESULT":
        state = {
          ...state,
          totalPage: action.payload
        };
        break;
    }
    return state;
  }

  const titles = redux.createStore(titlesReducer);

  return {
    auth,
    titles,
  }
});