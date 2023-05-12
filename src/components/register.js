import React from 'react'
import { connect } from 'react-redux'
import { register } from '../store/actions/registerActions'
import { Form, Row, Col, Button } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css';

class Register extends React.Component {

    constructor(props) {
        super(props);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.onRegister = this.onRegister.bind(this);
        this.onChangeUserName = this.onChangeUserName.bind(this);
        this.onShowPassword = this.onShowPassword.bind(this);
        this.state = {
          userName: '',
          email: '',
          password: '',
          showPassword: false
        };
    }

    onShowPassword(e) {
      this.setState({
        showPassword: e.target.checked,
      });
    }

    onChangeUserName(e) {
        this.setState({ userName: e.target.value });
    }
    onChangeEmail(e) {
        this.setState({ email: e.target.value });
    }

    onChangePassword(e) {
        this.setState({ password: e.target.value });
    }

    onRegister() {
        debugger
        this.props.register({ userName: this.state?.userName, email: this.state?.email, password: this.state?.password});
    }
    
    

    render() {
        return <>
            <Form className="authForm">
              <Form.Group className="mb-3">
                <Form.Label>UserName</Form.Label>
                <Form.Control placeholder="Username" onChange={this.onChangeUserName} />
                <Form.Text className="text-muted">Your username should be unique</Form.Text>
              </Form.Group>
              <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" onChange={this.onChangeEmail}/>
                <Form.Text className="text-muted">
                  We'll never share your email with anyone else.
                </Form.Text>
              </Form.Group>
            
              <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type={this.state.showPassword ? "text" : "password"} placeholder="Password" onChange={this.onChangePassword}/>
              </Form.Group>
              <Form.Group className="mb-3" controlId="formBasicCheckbox">
                <Form.Check type="checkbox" label="Check me out" onChange={this.onShowPassword}/>
              </Form.Group>
              <Button variant="primary" onClick={this.onRegister}>
                Submit
              </Button>
            </Form>  
        </>
    }

}

//const mapStateToProps = (state) => ({ registerForm: state.message });

export default connect(null, { register })(Register);