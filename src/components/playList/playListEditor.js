import React, { Component } from "react";
import { connect } from "react-redux";
import { updatePlayList, deletePlayList, createPlayList, getPlayListById } from "../../store/actions/playListActions";
import { searchYoutubeVideo, clearSearchResults } from "../../store/actions/searchActions";
import { Form, Button, InputGroup, FormControl, CloseButton, ListGroup } from 'react-bootstrap'
import SongItem from "./songItem";

class PlayListEditor extends Component {
    constructor(props) {
        super(props);
        this.onChangeInputValue = this.onChangeInputValue.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.onAddSongToPlayList = this.onAddSongToPlayList.bind(this);
        this.onTitleChange = this.onTitleChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onCreate = this.onCreate.bind(this);
        this.onClearSearchResult = this.onClearSearchResult.bind(this);
        this.takeShortDescription = this.takeShortDescription.bind(this);

        this.state = {
            requestText: "",
            songs: [],
            title: "",
            description: ""
        }
    }

    takeShortDescription(str) {
        debugger;
        if (str == null || str == undefined)
            return ''
        let dots = str?.length > 47 ? '...' : '';
        return str?.substring(0, 46) + dots
    }

    componentDidMount() {
        if (this.props.displayMode === "edit")
            this.props.getPlayListById();
    }

    componentWillUnmount() {
        debugger;
        this.props.clearSearchResults();
    }

    onClearSearchResult() {
        debugger;
        this.props.clearSearchResults();
    }

    onChangeInputValue(e) {
        this.setState({
            requestText: e.target.value,
        });
    }

    onAddSongToPlayList(song) {
        const index = this.state.songs.findIndex((x) => x.youTube_Link === song.youTube_Link);
        if (index > -1) {
            this.setState((prevState) => ({
                songs: [
                    ...prevState.songs.slice(0, index),
                    ...prevState.songs.slice(index + 1),
                ],
            }));
        } else {
            this.setState((prevState) => ({
                songs: [...prevState.songs, song],
            }));
        }
    }

    

    onTitleChange(e) {
        this.setState({
            title: e.target.value
        })
    }

    onDescriptionChange(e) {
        this.setState({
            description: e.target.value
        })
    }

    onSearch() {
        this.props.searchYoutubeVideo(this.state.requestText);
    }

    onCreate() {
        debugger;
        let songToCreate = this.state.songs.map((x) => {return {title: x.title, youTube_Link: x.youTube_Link}});
        let postModel = { title: this.state.title, description: this.state.description, songs: songToCreate };
        this.props.createPlayList(postModel);
    }

    render() {
        debugger
        let searchResults = this.props.searchResults;
        let songsList = this.state.songs;

        let isFilledform = false;

        if (songsList.length % 8 == 0 && songsList.length != 0 && this.state.title.length > 4 && this.state.description.length > 4) {
            isFilledform = true;
        }

        let foundSongs = <ListGroup as="ol" numbered style={{ width: "100%", margin: "0 auto 30px" }}>
            {
                searchResults.map((s) => (
                    <SongItem s={s} key={s.youTube_Link} onAddSongToPlayList={this.onAddSongToPlayList} isAdd={true} />
                ))

            }
            {searchResults.length > 0 && (<CloseButton aria-label="Hide" onClick={this.onClearSearchResult} />)}
        </ListGroup>

        let addedSongs = <ListGroup as="ol" numbered style={{ width: "100%", margin: "0 auto 30px" }}>
            {songsList.length > 0 &&( <ListGroup.Item action variant="warning">
                Added songs
            </ListGroup.Item>)}
            {

                songsList.map((s) => (
                    <SongItem s={s} key={s.youTube_Link} onAddSongToPlayList={this.onAddSongToPlayList} isAdd={false} />
                ))

            }
        </ListGroup>

        return <>
            <Form className="create-container">
                <Form.Group className="mb-5">
                    <Form.Label>Title</Form.Label>
                    <Form.Control type="email" placeholder="Best hip-hop 2023..." onChange={this.onTitleChange} />
                    <Form.Text className="text-muted">
                        Create your title name
                    </Form.Text>
                </Form.Group>
                <Form.Group className="mb-5">
                    <Form.Label>Descriprion</Form.Label>
                    <Form.Control type="email" placeholder="Some description" onChange={this.onDescriptionChange} />
                    <Form.Text className="text-muted">
                        Write about your playlist
                    </Form.Text>
                </Form.Group>


                <InputGroup>
                    <FormControl
                        type="text"
                        placeholder="Search"
                        onChange={this.onChangeInputValue}
                        className="search"
                    />
                    <Button variant="primary" onClick={this.onSearch}>
                        Submit
                    </Button>
                </InputGroup>
                {searchResults && foundSongs}
            </Form>


            <div className="d-flex flex-column create-container" >
                <div className="overflow-auto w-100">

                    {addedSongs}
                </div>
            </div>

            <Button onClick={this.onCreate} style={{ margin: "200px" }} disabled={!isFilledform}>Create</Button>
        </>
    }
}


const mapStateToProps = (state) => ({ searchResults: state.search.searchResults });

export default connect(mapStateToProps, { updatePlayList, deletePlayList, createPlayList, getPlayListById, searchYoutubeVideo, clearSearchResults })(PlayListEditor);