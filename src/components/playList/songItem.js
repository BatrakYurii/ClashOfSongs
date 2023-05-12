import React, { Component } from "react";
import { connect } from "react-redux";
import {Image, ListGroup, Badge, Row, Col } from 'react-bootstrap'
import plusPng from "./plus.png"
import trashPng from "./../../images/trh.png";

class SongItem extends Component {
    constructor(props) {
        super(props);

        this.takeShortDescription = this.takeShortDescription.bind(this);
        this.onPushAddButton = this.onPushAddButton.bind(this);

        this.state = {
            isSelected : false
        }
    }

    takeShortDescription(str) {
        debugger;
        if (str == null || str == undefined)
            return ''
        let dots = str?.length > 47 ? '...' : '';
        return str?.substring(0, 46) + dots
    }

    onPushAddButton(){
        debugger;
        
        let selected = this.state.isSelected;
        this.setState({
            isSelected: !selected
        })
        this.props.onAddSongToPlayList(this.props.s)
    }

    render() {

        
        return <>
            <ListGroup.Item
                as="li"
                className="d-flex justify-content-between align-items-start"
                key={this.props.s.youTube_Link}
                style={ this.state.isSelected ? {backgroundColor : "#a3fc97"} : {backgroundColor : "white"}}
            >
                <Image src={this.props.s.thumbnailUrl} alt="image" rounded style={{ width: "100px", height: "56px", margin: "10px" }} />
                <div className="ms-2 me-auto">
                    <div className="fw-bold" style={{ textAlign: "start", fontSize: "20px" }}>{this.takeShortDescription(this.props.s.title)}</div>
                    <div style={{ textAlign: "start" }}>{this.props.s.channelTitle}</div>
                </div>
                <Col sm={6} md={4} lg={2}>
                    <Row>
                        <Badge bg="primary" pill>
                            {`Views: ${this.props.s.viewCount}`}
                        </Badge>
                    </Row>
                    <Row className="justify-content-end">
                        <Image className="preview-list-img" src={this.props.isAdd ? plusPng : trashPng} alt="add" onClick={this.onPushAddButton} />
                    </Row>
                </Col>
            </ListGroup.Item>
        </>
    }
}

export default SongItem;