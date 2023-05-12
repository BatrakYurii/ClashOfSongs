import { SEARCH_VIDEO, SEARCH_ERROR, CLEAR_SEARCH_RESULTS, UNAUTHORIZED} from '../types'
import axios from 'axios'
import host from './host/host'
import {auth, authWithContentType} from "./../requestConfigs/requestConfigs"

export const searchYoutubeVideo = (text) => async dispatch => {
    try {
        debugger
        const res = await axios.get(`${host}youtubesearch/get/${text}`, auth)
        dispatch({
            type: SEARCH_VIDEO,
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
                type: SEARCH_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const clearSearchResults = () => async dispatch => {
    try {
        debugger
        dispatch({
            type: CLEAR_SEARCH_RESULTS,
        })
    }
    catch (e) {
        dispatch({
            type: SEARCH_ERROR,
            payload: console.log(e),
        })
    }
}