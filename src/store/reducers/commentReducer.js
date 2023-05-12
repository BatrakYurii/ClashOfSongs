import { GET_COMMENTS, CREATE_COMMENT, DELETE_COMMENT, COMMENT_ERROR} from '../types'

const initialState = {
    comments: []
}

export default function (state = initialState, action) {
    switch (action.type) {

        case GET_COMMENTS:
            debugger
            let formatedComments = action.payload;
            for(let i = 0; i < formatedComments.length; i++){
                if(formatedComments[i].user !== null)
                    formatedComments[i].user.avatarImage = "https://localhost:7257/" + formatedComments[i].user.avatarImage.replace(`/\\\\/g`, "/");
            }
            // formatedComments.forEach(c => {
            //     if (c.avatarImage !== null) {
            //         c.avatarImage = "https://localhost:7257/" + c.avatarImage.replace(`/\\\\/g`, "/");
            //     }
            // });
            return {
                ...state,
                comments: formatedComments,
                
            }

            case DELETE_COMMENT:
            let deletedId = action.payload.id;
            return {
                ...state,
                comments: this.state.comments.filter(a => a.id !== deletedId)
            }

            case CREATE_COMMENT:
                debugger
                return {
                    ...state,
            }

        default: return state
    }
}