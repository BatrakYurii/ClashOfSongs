import host from '../actions/host/host';
import { START_GAME, CHOOSE, GET_PAIR, GAME_ERROR, UNAUTHORIZED} from '../types'

const initialState = {
    songPair: [],
    playListInfo: {
        songsCount: 0,
        title: ""
    },
    sessionId: ""
}



export default function (state = initialState, action) {
    switch (action.type) {

        case START_GAME:
            let playListIfno = {title: action.payload.title, songsCount: action.payload.songs.length}
            return {
                ...state,  
                playListInfo: playListIfno          
            }

        case GET_PAIR:
            debugger
            let formatedPair = action.payload;
            formatedPair.forEach(s => {
                s.youTube_Link = "https://www.youtube.com/watch?v=" + s.youTube_Link;
            });
            return {
                ...state,
                songPair: formatedPair
            }

           

        case CHOOSE:
        return {
            ...state
        }
            
        default: return state
    }
}