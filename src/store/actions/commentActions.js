import { GET_COMMENTS, CREATE_COMMENT, DELETE_COMMENT, COMMENT_ERROR, UNAUTHORIZED } from '../types'
import axios from 'axios'
import host from './host/host'
import {auth, authWithContentType} from "./../requestConfigs/requestConfigs"
export const getComments = (id) => async dispatch => {
    try {
        debugger
        const res = await axios.get(`${host}comment/GetPlayListComments/${id}`)
        dispatch({
            type: GET_COMMENTS,
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
            type: COMMENT_ERROR,
            payload: console.log(e),
        })
    }
}

export const createComment = (commentPostModel) => async dispatch => {
    try {
        debugger;
        let res = await axios.post(`${host}comment/create`, commentPostModel, auth  )

        window.location.reload();
        dispatch({
            type: CREATE_COMMENT,
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
                type: COMMENT_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const deleteComment = (commentId) => async dispatch => {
    try {
        debugger
        await axios.delete(`${host}comment/delete/{commentId}`,
            auth
        )
        dispatch({
            type: DELETE_COMMENT,
            payload: commentId
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
                type: COMMENT_ERROR,
                payload: console.log(e),
            })
        }
    }
}




