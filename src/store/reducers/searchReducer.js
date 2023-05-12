import { SEARCH_VIDEO, SEARCH_ERROR, CLEAR_SEARCH_RESULTS } from '../types'
import host from '../actions/host/host';

const initialState = {
    searchResults: []
}

export default function (state = initialState, action) {
    switch (action.type) {
        case SEARCH_VIDEO:
            debugger
            // let searchResult = action.payload.map(item =>{
            //     return {...item, isSelected: false}
            // })
            debugger;
            return {
                ...state,
                searchResults: action.payload
            }

            case CLEAR_SEARCH_RESULTS:
            debugger;
            return {
                ...state,
                searchResults: initialState.searchResults
            }

        default: return state
    }
}