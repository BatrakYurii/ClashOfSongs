import { combineReducers } from 'redux'
import loginReducer from './loginReducer'
import registerReducer from './registerReducer'
import playListReducer from './playListReducer'
import profileReducer from './profileReducer'
import searchReducer from './searchReducer'
import gameReducer from './gameReducer'
import commentReducer from './commentReducer'


export default combineReducers({
  login: loginReducer,
  register: registerReducer,
  playLists: playListReducer,
  profile: profileReducer,
  search: searchReducer,
  game: gameReducer,
  comment: commentReducer
})