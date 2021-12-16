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
    types: null,
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
      case "TYPES":
        state = {
          ...state,
          types: action.payload
        };
        break;
    }
    return state;
  }

  const titles = redux.createStore(titlesReducer);


  const defaultLoadingConfig = false;

  const loadingReducer = (state = defaultLoadingConfig, action) => {
    switch (action.type) {
      case "START":
        state = true;
        break;
      case "STOP":
        state = false;
        break;
    }
    return state;
  }

  const loading = redux.createStore(loadingReducer);

  return {
    auth,
    titles,
    loading,
  }
});