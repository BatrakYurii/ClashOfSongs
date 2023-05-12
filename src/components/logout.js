import React, { Component } from "react";
import { connect } from "react-redux";
import { logout } from "../store/actions/loginActions";
import { Form, Button } from 'react-bootstrap'
import { ThemeConsumer } from "react-bootstrap/esm/ThemeProvider";

class Logout extends Component {
    constructor(props) {
        super(props);
    }

    handleLogout() {
        this.props.logout();
    }

    render() {
        return <button onClick={this.handleLogout}>
        Logout</button>
    }
}

export default connect(null, { logout })(Logout);