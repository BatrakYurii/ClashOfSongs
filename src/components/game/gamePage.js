import React, { Component } from "react";
import { connect } from "react-redux";
import { getPlayListById } from "../../store/actions/playListActions";
import { startGame, choose, getPlair } from "../../store/actions/gameActions";
import ReactPlayer from 'react-player/youtube'
import { Row, Col, Button } from "react-bootstrap";
import withRouter from "../../store/reducers/withRouter";

class GamePage extends Component {
    constructor(props) {
        super(props);

        this.onChooseSong = this.onChooseSong.bind(this);
        this.onHandleMouseEnter = this.onHandleMouseEnter.bind(this);

        this.state = {
            firstMuted: true,
            secondMuted: true,
            round: 1,
            circle: 0
        }
    }

    async componentDidMount() {
        debugger;
        const id = this.props.params.id;
        await this.props.startGame(id);
        this.setState({
            circle: this.props.playListInfo.songsCount / 2
        })
        this.props.getPlair();

    }

    async onChooseSong(songUrl) {
        debugger;
        let songId = songUrl.split('=')[1];
        setTimeout(async () => {
            await this.props.choose(songId);

            if (this.state.circle === this.state.round) {
                let previousCircle = this.state.circle;
                this.setState({
                    round: 1,
                    circle: previousCircle / 2
                })
            }
            else {
                let round = this.state.round;
                this.setState({
                    round: round + 1
                })
            }

            this.props.getPlair();
        }, 100);

    }

    onHandleMouseEnter(id) {
        debugger
        if (id == 0) {
            this.setState({
                firstMuted: false,
                secondMuted: true
            })
        }
        else {
            this.setState({
                firstMuted: true,
                secondMuted: false
            })
        }

    }



    render() {
        debugger;
        let songPair;
        let playListTitle;
        let gameMain;

        if (this.props.songPair != null && this.props.songPair.length == 2) {
            songPair = this.props.songPair;
            gameMain = <> <Row className="h-100 g-0" >
                <Col xs={6} className="w-50 h-100">
                    <ReactPlayer className='react-player justify-content-end m-0'
                        url={songPair[0].youTube_Link}
                        controls="true"
                        playing={true}
                        width='100%'
                        height='100%'
                        volume="0.701"
                        onMouseEnter={() => this.onHandleMouseEnter(0)}
                        muted={this.state.firstMuted}
                    />
                </Col>
                <Col xs={6} className="w-50 h-100">
                    <ReactPlayer className='react-player justify-content-start m-0'
                        url={songPair[1].youTube_Link}
                        controls="true"
                        playing={true}
                        width='100%'
                        height='100%'
                        volume="0.701"
                        onMouseEnter={() => this.onHandleMouseEnter(1)}
                        muted={this.state.secondMuted}
                    />
                </Col>
            </Row>
                <Row className="rounded-bottom g-0" style={{ height: "50px" }}>
                    <Col className="p-0">
                        <Button className="w-100 rounded-0 h-100 btn btn-lg font-weight-bold" onClick={() => this.onChooseSong(songPair[0].youTube_Link)} variant="primary">SELECT</Button>
                    </Col>
                    <Col className="p-0">
                        <Button className="w-100 rounded-0 h-100 btn btn-lg font-weight-bold" onClick={() => this.onChooseSong(songPair[1].youTube_Link)} variant="warning" style={{ backgroundColor: "#ed5b2d", borderBottomLeftRadius: "0.25rem" }}>SELECT</Button>
                    </Col>
                </Row>
            </>
        }
        else if (this.props.songPair != null && this.props.songPair.length == 1) {
            songPair = this.props.songPair;
            gameMain = <ReactPlayer className='react-player m-0'
                url={songPair[0].youTube_Link}
                controls="true"
                playing="true"
                width='100%'
                height='100%'
                volume="0.701"
            />
        }
        else {
            songPair = [{ "youtube_Link": "null" }, { "youtube_Link": "null" }]
        }

        if (typeof (this.props.playListInfo) == "undefined") {
            playListTitle = "PlayList"
        }
        else {
            playListTitle = this.props.playListInfo.title;
        }


        return <>
            <div className="video-container">
                <div class="border border-warning rounded-top fs-2" style={{ height: "50px", backgroundColor: "black", color: "white" }}>
                    {playListTitle} Round: {this.state.round}/{this.state.circle}
                </div>
                {gameMain}

            </div>

        </>
    }
}

const mapStateToProps = (state) => ({ songPair: state.game.songPair, playListInfo: state.game.playListInfo });

export default withRouter(connect(mapStateToProps, { getPlayListById, startGame, choose, getPlair })(GamePage)); // оборачиваем компонент в withRouter перед connect