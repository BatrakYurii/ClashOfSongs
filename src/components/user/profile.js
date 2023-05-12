import React, { Component} from "react";
import { connect, useSelector } from "react-redux";
import { getProfile, updateProfile } from "../../store/actions/profileActions";
import { Container, Row, Col, Card, Button } from 'react-bootstrap'
import { useParams } from "react-router-dom";
import PlayListItem from "../playList/playListItem";
import withRouter from "../../store/reducers/withRouter";


class Profile extends Component {
    constructor(props){
        super(props);

        this.state = {
            id: this.props.params.id
        }

    }


    async componentDidMount(){
        debugger;
        await this.props.getProfile(this.state.id);
    }
    
    render(){
        debugger;
        const { profileData } = this.props.profile;
        const { userData} = this.props.userData;

        let editButton = <Button variant="outline-dark" data-mdb-ripple-color="dark" style={{ zIndex: "1" }}>Edit profile</Button>;

        return<>
        <section className="h-100 gradient-custom-2">
                 <Container className="py-5 h-100">
                     <Row className="d-flex align-items-center h-100">
                         <Col lg={9} xl={7} style={{ width: '100%' }}>
                             <Card style={{ width: '100%' }} > 
                                 <div className="rounded-top text-white d-flex flex-row" style={{ backgroundImage: `url(${process.env.PUBLIC_URL}images/hedaerProfile.jpg)`, height: "400px", width: "100%" }}>
                                     <div className="ms-4 mt-5 d-flex flex-column " style={{ width: "250px" }}>
                                         <Card.Img variant="top" src={profileData.avatarImage} className="img-fluid img-thumbnail mt-4 mb-2" style={{ width: "250px", zIndex: "1" }} />
                                         {userData.userId == this.state.id && editButton}
                                     </div>
                                     <div className="ms-3" style={{ margin: "auto", width: "794px", padding: "10px"}}>
                                         <div style={{backgroundColor: "#EAEAEA", display: "inline-block", padding: "15px", boxShadow: "0 0 5px 1px #000000", color: "black"}}>
                                             <h4><b>{profileData.userName}</b></h4>
                                             <p>{profileData.email}</p>
                                         </div>
                                     </div>
                                 </div>
                                 <Card.Body className="p-4 text-black" style={{ backgroundColor: "#f8f9fa" }}>
                                     <div className="d-flex justify-content-end text-center py-1">
                                         <div>
                                             <p className="mb-1 h5">{profileData.playLists.length}</p>
                                             <p className="small text-muted mb-0">PlayLists</p>
                                         </div>
                                         <div className="px-3">
                                             <p className="mb-1 h5">1026</p>
                                             <p className="small text-muted mb-0">Followers</p>
                                         </div>
                                         <div>
                                             <p className="mb-1 h5">478</p>
                                             <p className="small text-muted mb-0">Following</p>
                                         </div>
                                     </div>
                                 </Card.Body>
                                 <Card.Body className="p-4 text-black">
                                     <div className="mb-5">
                                         <p className="lead fw-normal mb-1">About</p>
                                         <div className="p-4" style={{ backgroundColor: "#f8f9fa" }}>
                                             <p className="font-italic mb-1">Web Developer</p>
                                             <p className="font-italic mb-1">Lives in New York</p>
                                             <p className="font-italic mb-0">Photographer</p>
                                         </div>
                                     </div>
                                     <div className="d-flex justify-content-between align-items-center mb-4">
                                         <p className="lead fw-normal mb-0">Best playlists</p>
                                         <p className="mb-0"><a href="#!" className="text-muted">Show all</a></p>
                                     </div>
                                     <Row className="g-2">
                                         {profileData.playLists.map(a => 
                                             <PlayListItem playList={a} key={a.id} />)}
                                     </Row>
                                 </Card.Body>
                             </Card>
                         </Col>
                     </Row>
                 </Container>
             </section>
     </>
    }
 
}

const mapStateToProps = (state) => ({ profile: state.profile, userData: state.login });
export default withRouter(connect(mapStateToProps, { updateProfile, getProfile })(Profile));






