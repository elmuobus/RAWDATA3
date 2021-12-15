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
  
  const defaultTitlesConfig = {//Default state. Til at starte med er der ingen searchText. 
    searchText: "",
    types: null,
    currentPage: 1,
    pageSize: 20,
    totalPage: 0,
  }

  const titlesReducer = (state = defaultTitlesConfig, action) => {
    switch (action.type) {
      case "SEARCH": //Is called by search function, but why. To save the value of searchText, so it can be used globally. 
        state = {//Afhængig af action.type gør den det under. Den returner state objekt, hvor det her vist er staten man gav, samt searchtext. Hvad er action.payload. 
          ...state,
          searchText: action.payload//action.payload contains relevant data. 
        };
        break;
      case "PAGINATION":
        state = {
          ...state, //Existing data
          currentPage: action.payload //plus new data, noth in the same object. So the object has the initial state, which has the values that can be passed in that reducer. Each reducer has different defaultstate, which is an object. payload of action is content. 
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
    return state; //new state returned, either updated or the same if action did not match any of the switch statements. 
  }

    const titles = redux.createStore(titlesReducer);

    //Probably need store for each of the methods. 


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