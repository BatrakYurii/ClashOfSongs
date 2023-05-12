import React, { Component } from "react";
import { connect } from "react-redux";
import { Form, Button } from "react-bootstrap";
import withRouter from "../../store/reducers/withRouter";
import { createComment, deleteComment } from "../../store/actions/commentActions";

class CommentCreator extends Component {
    constructor(props) {
        super(props);
        this.onChangeContent = this.onChangeContent.bind(this);
        this.onCreateComment = this.onCreateComment.bind(this);

        this.state = {
            content: "",
            error: null // начальное состояние элемента с сообщением об ошибке
        }
    }

    onChangeContent(e) {
        if (e.target.value.length <= 130) { // проверяем, что символов не больше 130
            this.setState({
                content: e.target.value,
                error: null // сбрасываем состояние элемента с сообщением об ошибке, если символов не больше 130
            });
        } else {
            this.setState({
                error: "Comment too long" // устанавливаем состояние элемента с сообщением об ошибке, если символов больше 130
            });
        }
    }

    onCreateComment() {
        this.props.createComment({ content: this.state.content, playListId: this.props.params.id });
    }


    render() {

        return <>
            <Form
                className=""
                style={{ backgroundColor: "#EFEEEE" }}
                value={this.state.content}
                maxLength={130}
                >
                <div className="m-2">
                    <Form.Label>Write comment</Form.Label>
                    <Form.Control type="text" as="textarea" rows={3} placeholder="New comment..." onChange={this.onChangeContent} />
                    {this.state.error && <p style={{ color: "red" }}>{this.state.error}</p>} {/* отображаем элемент с сообщением об ошибке, если состояние не null */}
                    <Button variant="primary" className="m-2" onClick={this.onCreateComment}>
                        Create
                    </Button>
                </div>

            </Form>
        </>
    }
}


export default withRouter(connect(null, { createComment, deleteComment })(CommentCreator));
