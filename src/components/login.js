import React, { Component } from "react";
import { connect } from "react-redux";
import { login } from "../store/actions/loginActions";
import { Form, Button } from 'react-bootstrap'
import { ThemeConsumer } from "react-bootstrap/esm/ThemeProvider";
import Loading from "./loading";

class Login extends Component {
  constructor(props) {
    super(props);
    this.onInputLogin = this.onInputLogin.bind(this);
    this.onChangeEmail = this.onChangeEmail.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);
    this.onShowPassword = this.onShowPassword.bind(this);

    this.state = {
      email: "",
      password: "",
      loading: false,
      showPassword: false,
      showLoading: false
    };
  }



  onShowPassword(e) {
    this.setState({
      showPassword: e.target.checked,
    });
  }

  onChangeEmail(e) {
    this.setState({
      email: e.target.value,
    });
  }

  onChangePassword(e) {
    this.setState({
      password: e.target.value,
    });
  }

  onInputLogin() {
    debugger;
    this.setState({ showLoading: true });
      setTimeout(() => {
        this.props.login({ email: this.state.email, password: this.state.password });
      }, 500);

  }


  render() {

    let message = this.props?.message;

    return <> <Form className="authForm">
      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="email" placeholder="Enter email" onChange={this.onChangeEmail} />
        <Form.Text className="text-muted">
          We'll never share your email with anyone else.
        </Form.Text>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type={this.state.showPassword ? 'text' : 'password'} placeholder="Password" onChange={this.onChangePassword} />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formBasicCheckbox">
        <Form.Check type="checkbox" label="Check me out" onClick={this.onShowPassword} />
      </Form.Group>
      <Button variant="primary" onClick={this.onInputLogin}>
        Submit
      </Button>
      <Form.Group>
        {this.state.showLoading && message !== "Login failed" && <Loading />}
      </Form.Group>
    </Form>
    {message}  </> 
  }
}

const mapStateToProps  = (state) => ({message: state?.login?.message});
export default connect(mapStateToProps, {login} )(Login);