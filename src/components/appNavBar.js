import React, { Component, Profiler } from 'react'
import { Navbar, Container, Nav, Button, NavDropdown } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css';
import "./../../src/styles.css"
import logo from "./../images/logo.png";



export default class AppNavBar extends Component{
constructor(props){
    super(props);
    this.onLogout = this.onLogout.bind(this);
}

    initUserData(){
        return {
          userName: '',
          isLogin: false,
          imageUrl: '',
          id: '',
          roles: [],
        }
    }

    onLogout(){
        debugger;
        localStorage.clear();
        window.location.href = "/"
    }

    render(){
        const host = "https://localhost:7257/"
        const retrievedStoreStr = localStorage.getItem('userData') 
        let userData = JSON.parse(retrievedStoreStr)
        let userName 
        let profileRef = "/profile";
        let profileAdsRef = `/myPlayList`
        let image;


        if(userData == null || userData==undefined)
        { 
          userData = this.initUserData();
        }

        if(userData.userName === '')
        {
            userName = "Guest";
        }
        else{
          userName =userData.userName;
        }

        if(!userData.isLogin)
        {
            profileRef = "/login"
            profileAdsRef = "/login"
        }

        if(userData.imageUrl === null||userData.imageUrl === '')
        {
            image = "https://localhost:7257/profileImages/default.png"
        }
        else
        {
          image = "https://localhost:7257/" + userData.imageUrl
        }
        let unAuthorize = <><Nav.Link href="/login">Login</Nav.Link>
        <Nav.Link href="/register">Register</Nav.Link></>;

        let authorize = <>
            <NavDropdown title={<img className='userNavImage' src={image}/>}>
                <NavDropdown.Item href={`/profile/${userData.userId}`}>Private data</NavDropdown.Item>
                <NavDropdown.Item href="/my">My PlayLists</NavDropdown.Item>
                <NavDropdown.Item onClick={this.onLogout}>Log out</NavDropdown.Item>
            </NavDropdown>
        </>

        return (
            <>
                <Navbar variant='light' className="navBar">
                    <Container>
                        <Navbar.Brand href="/">
                            <img src={logo} alt='logo' id='logo'/>
                        </Navbar.Brand>
                        <Navbar.Toggle aria-controls="basic-navbar-nav" />
                        <Navbar.Collapse id="basic-navbar-nav">
                        <Nav ms={4} className="me-auto ms-5">
                            <Nav.Link href="/">Home</Nav.Link>
                            <Nav.Link href="/create">Create</Nav.Link>
                            <Nav.Link href="/info">Info</Nav.Link>
                        </Nav>
                        <Nav className="ml-auto">
                            {userData.isLogin ? authorize : unAuthorize}
                        </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </>
        )
    }
}

