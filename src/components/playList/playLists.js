import React, { Component } from "react";
import { connect } from "react-redux";
import playListItem from "./playListItem";
import { Container, Row, Col, Pagination, Form, Button, Dropdown } from 'react-bootstrap'
import { getPlayLists, createPlayList, updatePlayList, deletePlayList, getPlayListById } from "../../store/actions/playListActions";
import PlayListItem from "./playListItem";
import GamePage from "../game/gamePage";


class PlayLists extends Component {
    constructor(props) {
        super(props);

        this.createQueryString = this.createQueryString.bind(this);
        this.onChangeSearchInput = this.onChangeSearchInput.bind(this);
        this.createQuerystringFilter = this.createQuerystringFilter.bind(this);
        this.createQueryString = this.createQueryString.bind(this);
        this.handleCheckboxChange = this.handleCheckboxChange.bind(this);
        this.setDidFilteredState = this.setDidFilteredState.bind(this);

        //this.handleCheckboxChange = this.handleCheckboxChange.bind(this);

        this.state = {
            searchText: "",
            sizes: [8, 16, 32, 64]
        };
    }

    componentDidMount() {
        if (this.props.displayMode == "my") {
            this.props.getAds("api/users/Myads");
        }
        else {
            debugger
            this.props.getPlayLists();
        }
        this.setDidFilteredState();
        //this.convertDate = this.convertDate.bind(this);
    }

    setDidFilteredState() {
        debugger
        const query = new URLSearchParams(window.location.search);

        const searchText = query.get('searchText') || '';
        this.setState({
            searchText: searchText,
        });

        const decodedUrl = decodeURIComponent(window.location.search);
        let sizes = [];


        if (decodedUrl !== '') {
            const searchParams = new URLSearchParams(decodedUrl);

            sizes = Array.from(searchParams.entries())
                .filter(([key, value]) => key.startsWith('sizes['))
                .map(([key, value]) => Number(value));

            if (sizes.length > 0) {
                this.setState({
                    sizes: sizes,
                });
            }
        }


       
    }

    onChangeSearchInput(e) {
        this.setState({
            searchText: e.target.value,
        });
    }

    // handleCheckboxChange(){

    // }

    handleCheckboxChange(event) {
        const { value, checked } = event.target;
        const { sizes } = this.state;

        if (checked) {
            // Если чекбокс выбран, добавляем число в массив
            this.setState((prevState) => ({
                sizes: [...prevState.sizes, parseInt(value)],
            }));
        } else {
            // Если чекбокс не выбран, удаляем число из массива
            this.setState((prevState) => ({
                sizes: prevState.sizes.filter((num) => num !== parseInt(value)),
            }));
        }
    }


    createQuerystringFilter() {
        debugger

        if (this.state === null) {
            return ''
        }
        const parameters = this.state;
        const query = new URLSearchParams();

        //query.set("page", )

        //const sizes = parameters.sizes.map(size => `sizes[]=${size}`).join("&");
        //query.set('sizes', sizes)
        if (parameters.sizes !== null && parameters.sizes.length > 0) {
            parameters.sizes.forEach((size, index) => {
                query.set(`sizes[${index}]`, size);
            });
        }


        if (parameters?.searchText !== null && parameters?.searchText !== '') {
            query.set('searchText', parameters.searchText)
        }
        return `?${query.toString()}`;
    }

    createQueryString(pageNumber) {
        debugger;
        let query = new URLSearchParams(window.location.search);
        query.set("page", pageNumber)
        return query.toString();
    }

    render() {
        debugger
        const { playLists, paginationParameters } = this.props.playLists;
        const { sizes, searchText } = this.state;
        let pagination;
        let active = paginationParameters.page;
        //let totalPages = Math.ceil(paginationParameters.total / paginationParameters.pageSize)
        let items = [];
        let display;
        let filters;

        filters = <Form className="w-50 mx-auto" style={{ marginTop: "20px" }}>
            <Row>
                <Col xs={2}>
                    <Dropdown className="my-auto" >
                        <Dropdown.Toggle variant="secondary" id="dropdown-checkbox">
                            Size
                        </Dropdown.Toggle>
                        <Dropdown.Menu style={{ minWidth: "5rem", paddingInline: "0.5rem" }}>
                            <Form>
                                <Form.Check
                                    style={{ width: "40px" }}
                                    type="switch"
                                    id="checkbox-8"
                                    label="8"
                                    value={8}
                                    checked={sizes.includes(8)}
                                    onChange={this.handleCheckboxChange}
                                />
                                <Form.Check
                                    style={{ width: "40px" }}
                                    type="switch"
                                    id="checkbox-16"
                                    label="16"
                                    value={16}
                                    checked={sizes.includes(16)}
                                    onChange={this.handleCheckboxChange}
                                />
                                <Form.Check
                                    style={{ width: "40px" }}
                                    type="switch"
                                    id="checkbox-32"
                                    label="32"
                                    value={32}
                                    checked={sizes.includes(32)}
                                    onChange={this.handleCheckboxChange}
                                />
                                <Form.Check
                                    style={{ width: "40px" }}
                                    type="switch"
                                    id="checkbox-64"
                                    label="64"
                                    value={64}
                                    checked={sizes.includes(64)}
                                    onChange={this.handleCheckboxChange}
                                />
                                {/* Остальные чекбоксы */}
                            </Form>
                        </Dropdown.Menu>
                    </Dropdown>
                </Col>
                <Col xs={7}>
                    <Form.Group>
                        {/* <Form.Label htmlFor="inputPassword5">Search</Form.Label> */}
                        <Form.Control
                            type="text"
                            id="inputPassword5"
                            placeholder="Ukrainian pop..."
                            onChange={this.onChangeSearchInput}
                            value={searchText}
                        />
                        <Form.Text id="passwordHelpBlock" muted>
                            Type keywords to find playlists
                        </Form.Text>
                    </Form.Group>
                </Col>
                <Col xs={1}>
                    <Button variant="primary" href={`playlists${this.createQuerystringFilter()}`}>Search</Button>
                </Col>
            </Row>
        </Form>

        for (let number = 1; number <= paginationParameters.pageCount; number++) {
            items.push(
                <Pagination.Item key={number} href={window.location.pathname + "?" + this.createQueryString(number)} active={number === active}>
                    {number}
                </Pagination.Item>,
            );
        }

        pagination = (
            <div>
                <Pagination className="justify-content-center">{items}</Pagination>
            </div>
        );

        display = <div>
            <Row md={3} className="g-3">
                {playLists?.map(a =>
                    <PlayListItem playList={a} key={a.id} />
                )} </Row>
        </div>



        return (<>
            {filters}{display}{pagination}

        </>)
    }

}

const mapStateToProps = (state) => ({ playLists: state.playLists });

export default connect(mapStateToProps, { getPlayLists, updatePlayList, deletePlayList, createPlayList, getPlayListById })(PlayLists);