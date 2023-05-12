import { LOGIN, LOGOUT, LOGIN_ERROR } from '../types';
import axios from 'axios';
import host from './host/host';

export const login = (userCredentials) => async (dispatch) => {
  try {
    const res = await axios.post(`${host}auth/login`, userCredentials);
    console.log(res);
    dispatch({
      type: LOGIN,
      payload: res.data,
    });
    window.location.href = '/';
  } catch (e) {
    dispatch({
      type: LOGIN_ERROR,
      payload: console.log(e),
    });
  }
};

export const logOut = () => async (dispatch) => {
  try {
    const retrievedStoreStr = localStorage.getItem('userData');
    let userData = JSON.parse(retrievedStoreStr);
    dispatch({
      type: LOGOUT,
      payload: userData,
    });
    window.location.href = '/';
  } catch (e) {
    dispatch({
      type: LOGIN_ERROR,
      payload: console.log(e),
    });
  }
};

export const logout = () => {
  return (dispatch) => {
    // dispatch action to update the state with a null access token
    dispatch({ type: 'SET_ACCESS_TOKEN', payload: null });

    // remove user data from local storage
    localStorage.removeItem('userData');
  };
};
