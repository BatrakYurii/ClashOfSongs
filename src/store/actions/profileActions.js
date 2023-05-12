import store from '../store';
import { PROFILE_ERROR, UPDATE_PROFILE, UNAUTHORIZED, GET_PROFILE} from "./../types"
import axios from 'axios'
import host from './host/host';
import { auth, authWithContentType } from "./../requestConfigs/requestConfigs"


export const updateProfile = (profileModel) => async dispatch => {
    try {
        debugger

        if(profileModel.image != null)
        {
            let formData = new FormData();
            formData.append("image", profileModel.image);

            await axios.post(`${host}api/images/profile`, formData, authWithContentType)
        }
        const res = await axios.post(`${host}api/users`, profileModel, auth)
        dispatch({
            type: UPDATE_PROFILE,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
        }
        dispatch({
            type: PROFILE_ERROR,
            payload: console.log(e),
        })
    }
}

export const getProfile = (userId) => async dispatch => {
    try {
        debugger
        const resUser = await axios.get(`${host}user/getById/${userId}`)
        const resPl = await axios.get(`${host}playlist/GetAllByUser/${resUser.data.id}`)
        resUser.data.playLists = resPl.data
        dispatch({
            type: GET_PROFILE,
            payload: resUser.data
        })
    }
    catch(e) {
        }
        dispatch({
            type: PROFILE_ERROR,
        })
    }

