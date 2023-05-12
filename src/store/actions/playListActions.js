import { GET_PLAYLISTS, GET_PLAYLIST, CREATE_PLAYLIST, UPDATE_PLAYLIST, DELETE_PLAYLIST, PLAYLIST_ERROR, UNAUTHORIZED, SEARCH_VIDEO, SEARCH_ERROR} from '../types'
import axios from 'axios'
import host from './host/host'
import {auth, authWithContentType} from "./../requestConfigs/requestConfigs"
export const getPlayLists = () => async dispatch => {
    try {
        debugger
        const query = new URLSearchParams(window.location.search);
        const queryStr = decodeURIComponent(query.toString());
        const res = await axios.get(`${host}playlist/Get?${queryStr}`,auth)

        //const res = await axios.get(`${host}playlist/Get`,auth)
        dispatch({
            type: GET_PLAYLISTS,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response?.status === 401) {
            window.location.href = '/login';
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
        }
        dispatch({
            type: PLAYLIST_ERROR,
            payload: console.log(e),
        })
    }
}
export const getPlayListById = (id) => async dispatch => {
    try {
        debugger
        const resPl = await axios.get(`${host}playlist/get/${id}`)
        const resUser = await axios.get(`${host}user/getById/${resPl.data.userId}`)
        dispatch({
            type: GET_PLAYLIST,
            payload: {playList: resPl.data, user: resUser.data}
        })
    }
    catch (e) {
        dispatch({
            type: PLAYLIST_ERROR,
            payload: console.log(e),
        })
    }
}

// export const getPlayListByUserId = (id) => async dispatch => {
//     try {
//         debugger
//         const resPl = await axios.get(`${host}playlist/GetAllByUser/${id}`)
        
//         dispatch({
//             type: GET_PLAYLIST,
//             payload: {playList: resPl, user: resUser}
//         })
//     }
//     catch (e) {
//         dispatch({
//             type: PLAYLIST_ERROR,
//             payload: console.log(e),
//         })
//     }
// }


export const updatePlayList = (ad) => async dispatch => {
    try {
        debugger
        const res = await axios.put(`${host}api/ads/${ad.id}`,
            { header: ad.header, description: ad.description, price: ad.price, categories: ad.categories },
            auth
        )
        dispatch({
            type: UPDATE_PLAYLIST,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: PLAYLIST_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const createPlayList = (postModel) => async dispatch => {
    try {
        debugger;
        let res = await axios.post(`${host}playList/create`, postModel, auth  )
        let createdAd = res.data;

        window.location.href = "/"
        dispatch({
            type: CREATE_PLAYLIST,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: PLAYLIST_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const deletePlayList = (ad) => async dispatch => {
    try {
        debugger
        await axios.delete(`${host}api/ads/${ad.id}`,
            auth
        )
        dispatch({
            type: DELETE_PLAYLIST,
            payload: ad
        })
    }
    catch (e) {
        if (e.response?.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: PLAYLIST_ERROR,
                payload: console.log(e),
            })
        }
    }
}




