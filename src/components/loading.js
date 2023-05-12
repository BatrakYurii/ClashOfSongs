import React, { Component } from "react";

class Loading extends Component{
    constructor(props){
        super(props);
    }

    render(){

        return <div className="loader-container">
        <div className="spinner"></div>
      </div>
    }
}

export default Loading;