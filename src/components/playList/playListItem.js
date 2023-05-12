import React, { Component } from "react";
import { connect } from "react-redux";
import playLists from "./playLists";
import { Container, Row, Col, Button, Card } from 'react-bootstrap'
import PreviewImages from "./previewImage";

class PlayListItem extends Component {
    constructor(props) {
        super(props);

    }

    renderEditButton(ad) {
        const displayMode = this.props.displayMode
        // if (displayMode === "my") {
        //     return <Button variant="dark" onClick={() => this.onEditClick(playlist)}>Edit</Button>
        // }
    }

    takeShortDescription(str) {
        if (str == null || str == undefined)
            return ''
        let dots = str?.length > 47 ? '...' : '';
        return str?.substring(0, 46) + dots
    }

    render() {

        return <>
            <Col className="mb-2">
                <Card>
                    <Card.Link href={`playlist/${this.props.playList.id}`}>
                        <PreviewImages images={this.props.playList.previewImages}/>
                    </Card.Link>
                    <Card.Body>
                        <Card.Title className="card-title"><b>{this.props.playList.title}</b></Card.Title>
                        <Card.Text>
                            {this.takeShortDescription(this.props.playList.description)}<br />
                        </Card.Text>
                        {this.renderEditButton(this.props.playlist)}
                    </Card.Body>
                </Card>
            </Col>
        </>
    }
}

export default PlayListItem;