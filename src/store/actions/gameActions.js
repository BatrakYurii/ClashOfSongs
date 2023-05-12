import { START_GAME, CHOOSE, GET_PAIR, GAME_ERROR, UNAUTHORIZED } from '../types'
import axios from 'axios'
import host from './host/host'
import { session } from '../requestConfigs/requestConfigs';
import { parse } from 'cookie';

export const startGame = (id) => async dispatch => {
    try {
        debugger
        const playList = await axios.get(`${host}playlist/Get/${id}`,)
        debugger;
        await axios.post(`${host}game/StartGame`,
            { title: playList.data.title, description: playList.data.description, songs: playList.data.songs }, { withCredentials: true });

        // const setCookieHeader = response.headers['set-cookie'];
        // var cookie;
        // if (setCookieHeader) {
        //     cookie = parse(setCookieHeader[0]);
        // }


        dispatch({
            type: START_GAME,
            payload: playList.data
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
            type: GAME_ERROR,
            payload: console.log(e),
        })
    }
}

export const getPlair = () => async dispatch => {
    try {
        debugger
        const res = await axios.get(`${host}game/GetPair`, {
            withCredentials: true
        })
        dispatch({
            type: GET_PAIR,
            payload: res.data
        })
    }
    catch (e) {
        dispatch({
            type: GAME_ERROR,
            payload: console.log(e),
        })
    }
}

export const choose = (songId) => async dispatch => {
    try {
        debugger
        const res = await axios.get(`${host}game/choose/${songId}`, {
            withCredentials: true
        })
        dispatch({
            type: CHOOSE,
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
                type: GAME_ERROR,
                payload: console.log(e),
            })
        }
    }
}

