import React, { Component } from "react";
import { connect } from "react-redux";
import playLists from "./playLists";
import { Container, Row, Col, Button, Card, Image, CardImg } from 'react-bootstrap'

class PreviewImages extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        let preview = <div className="imgContainer">
            <Row className="rowStyle">
                <Col xs={6}>
                    <img src={this.props.images[0]} />
                </Col>
                <Col xs={6}>
                    <img src={this.props.images[1]} />
                </Col>
            </Row>
            <Row className="rowStyle">
                <Col xs={6}>
                    <img src={this.props.images[2]} />
                </Col >
                <Col xs={6}>
                    <img src={this.props.images[3]} />
                </Col>
            </Row>
        </div>


        return <>
            {preview}
        </>
    }

}

export default PreviewImages;