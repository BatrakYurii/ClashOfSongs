import React, { Component } from "react";
import { ListGroup, Image, Col, Row } from "react-bootstrap";

class CommentItem extends Component {
    constructor(props) {
        super(props);

        this.onImageClick = this.onImageClick.bind(this);

    }

    onImageClick(){
        window.location.href = `../profile/${this.props.comment.user.id}`;
    }

    render() {
        let comment;
        debugger;
        if(this.props.comment !== null ){
            comment = <Row className="h-75">
            <Col xs={2}>
                <Row>
                    <Image onClick={this.onImageClick} className="rounded mx-auto" src={this.props.comment.user.avatarImage} style={{ width: '80px', maxHeight: '100%'}}/>
                </Row>
                <Row className="mw-100 h-50">
                    <p>{this.props.comment.user.userName}</p>
                </Row>
            </Col>
            <Col className="text-left" style={{borderLeft:"1px solid black"}}>
                <p className="text-left">{this.props.comment.content}</p>
            </Col>
        </Row>
        }

        return <>
        <ListGroup.Item>
            {comment}
        </ListGroup.Item>            
        </>
    }
}

export default CommentItem;