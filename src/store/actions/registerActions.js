import {REGISTER, REGISTER_ERROR, UNAUTHORIZED} from '../types'
import axios from 'axios'
import host from './host/host'

export const register = (personData) => async dispatch => {
    try{
        console.log("Request was sent to server");
        const res = await axios.post(`${host}auth/register`, personData)
        dispatch({
            type: REGISTER,
            payload: res.data
        })
        window.location.href = '/login';
    }
    catch(e){
        dispatch({
            type: REGISTER_ERROR,
            payload: console.log(e),
        })
    }
}