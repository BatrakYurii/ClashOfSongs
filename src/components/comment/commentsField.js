import React from "react";
import { Component } from "react";
import CommentItem from "./commentItem";
import { ListGroup, Col, Row, Button } from "react-bootstrap";
import CommentCreator from "./commentCreator";

class CommentsField extends Component {
    constructor(props) {
        super(props);

    }



    render() {


        return <>
            <div className="comments-field w-50 m-auto">
                <Col>
                    <Row className="footer">
                        <CommentCreator />
                    </Row>
                    <Row className="main-block">
                        <ListGroup>
                            {this.props.comments?.map(c =>
                                <CommentItem comment={c} key={c.id}/>
                            )}
                        </ListGroup>

                    </Row>
                    <Row className="pagination">

                    </Row>
                </Col>
            </div>
        </>
    }
}

export default CommentsField;