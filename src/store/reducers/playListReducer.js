import { GET_PLAYLISTS, GET_PLAYLIST, UPDATE_PLAYLIST, DELETE_PLAYLIST, PLAYLIST_ERROR, CREATE_PLAYLIST, SEARCH_VIDEO, SEARCH_ERROR } from '../types'
import host from '../actions/host/host';
import { act } from 'react-dom/test-utils';
import { Accordion } from 'react-bootstrap';
const initialState = {
    playLists: [],
    paginationParameters: { page: '', pageSize: '', pageCount: '' },
    playListView: {}
}



export default function (state = initialState, action) {
    switch (action.type) {

        case GET_PLAYLISTS:
            debugger
            let formatedAds = action.payload.playLists;
            formatedAds.forEach(pl => {
                if (pl.previewImages !== null) {
                    for (let i = 0; i < pl.previewImages.length; i++) {
                        pl.previewImages[i] = "https://localhost:7257/" + pl.previewImages[i].replace(`/\\\\/g`, "/");
                    }
                }
            });

            return {
                ...state,
                playLists: formatedAds,
                paginationParameters: action.payload.pagination         
            }

        case GET_PLAYLIST:
            debugger;

            let formatedPl = action.payload;
           
            for (let i = 0; i < formatedPl.playList.previewImages.length; i++) {
                formatedPl.playList.previewImages[i] = "https://localhost:7257/" + formatedPl.playList.previewImages[i].replace(`/\\\\/g`, "/");
            }


            return {
                ...state,
                playListView: formatedPl
            }

        case UPDATE_PLAYLIST:
            debugger
            let ad = action.payload;
             if (ad.images !== null) {
                for (let i = 0; i < ad.images.length; i++) {
                    ad.images[i] = host + ad.images[i];
                }
            }
            let newAds = state.ads.filter(a => a.id !== ad.id)
            newAds.push(ad)
            window.location.reload();
            return {
                ...state,
                ads: newAds
            }

           

            case DELETE_PLAYLIST:
            let deletedId = action.payload.id;
            return {
                ...state,
                ads: this.state.ads.filter(a => a.id !== deletedId)
            }
            case CREATE_PLAYLIST:
                debugger
                return {
                    ...state,
                }

        default: return state
    }
}