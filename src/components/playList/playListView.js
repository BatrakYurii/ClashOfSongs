import React, { Component } from "react";
import { connect } from "react-redux";
import { getPlayListById } from "../../store/actions/playListActions";
import { getComments, createComment, deleteComment } from "../../store/actions/commentActions";
import PreviewImages from "./previewImage";
import { Button, Row, Col } from "react-bootstrap";
import CommentsField from "../comment/commentsField";
import withRouter from "../../store/reducers/withRouter";
import host from "../../store/actions/host/host";

class PlayListView extends Component {
    constructor(props) {
        super(props);

        this.state = {
            id: this.props.params.id
        }
    }

    async componentDidMount() {
        debugger;
        await this.props.getPlayListById(this.state.id);
        this.props.getComments(this.state.id);
    }

    render() {
        debugger;
        let playList = this.props.playList;
        let userCreator = this.props.userCreator;

        return <>
            <Col>
                {playList ? (
                    <Row className="w-100 mw-100 container-fluid">
                        <Col xs={3}>
                            <Row>
                                <PreviewImages images={playList.previewImages} />                                
                            </Row>
                            <Row className="font-italic text-underline ">
                                     <a className="text-secondary" href={`../profile/${userCreator.id}`}>By {userCreator.userName}</a>
                            </Row>
                            
                        </Col>
                        
                        
                        <Col className="border border-dark rounded my-2 p-2" style={{backgroundColor: "#EFEEEE"}} xs={8}> 
                            <Row className="">
                                <div className="h2 mx-auto ">
                                    {playList.title}
                                </div>                                
                            </Row>
                            <Row>
                                <div className="h4 m-2">
                                    {playList.description}
                                </div>
                            </Row>
                            <Row className="p-2">
                                <div className="mx-auto d-flex flex-column w-75">
                                    <Button className="w-100" variant="danger" href={`../game/${this.state.id}`} >Play</Button>
                                    <Button className="w-100" variant="warning" >Statistic</Button>
                                </div>
                            </Row>
                        </Col>
                    </Row>) : null}
                <Row>
                    <CommentsField comments={this.props.comments}/>
                </Row>

            </Col>


        </>
    }
}


const mapStateToProps = (state) => ({ playList: state.playLists.playListView.playList, userCreator: state.playLists.playListView.user, comments: state.comment.comments });

export default withRouter(connect(mapStateToProps, { getPlayListById, getComments, createComment, deleteComment })(PlayListView));