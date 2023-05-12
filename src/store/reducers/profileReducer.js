import {UPDATE_PROFILE, PROFILE_ERROR, UNAUTHORIZED, GET_PROFILE} from '../types'

const initialUser = {
    accessToken: '',
    userId: '',
    userName: '',
    email: '',
    imageUrl: '',
    roles: [],
    isLogin: false
}
const initialState = {
    profileData:{
        id: '',
        userName: '',
        email: '',
        avatarImage: '',
        playLists: []
    }
}

export default function(state = initialState, action){
    switch(action.type){

        case UPDATE_PROFILE:

            const retrievedStoreStr = localStorage.getItem('userData') 
            const userData = JSON.parse(retrievedStoreStr) 
            userData.userName = action.payload.userName
            userData.imageUrl = action.payload.imagePath
            userData.email = action.payload.email
            localStorage.setItem("userData", JSON.stringify(userData));
        return {
            ...state,
        }
        case GET_PROFILE:
            debugger;
            let result = action.payload;
            result.playLists.forEach(pl => {
                if (pl !== null) {
                  for (let i = 0; i < pl.previewImages.length; i++) {
                    pl.previewImages[i] = "https://localhost:7257/" + pl.previewImages[i].replace(`/\\\\/g`, "/");
                  }
                }
              });
              
            result.avatarImage = "https://localhost:7257/" + action.payload.avatarImage;
        return {
            ...state,
            profileData: result
        }
        case PROFILE_ERROR:
            return {
                ...state,
            }
        case UNAUTHORIZED:
            localStorage.removeItem("userData");
            localStorage.setItem("userData", JSON.stringify(initialUser)); 
            return {
                ...state,
                profileData: initialState.profileData
            }
        default: return state
    }
}