import React from 'react';
import ReactDOM from "react-dom";
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import {Provider, connect} from "react-redux";
import { changeColor, getQuote } from "./functions.js";
import 'bootstrap/dist/css/bootstrap.min.css'
import './index.css';

function reducer(state = {text: 'Quotes appear here', author: 'Author'}, action) {
    switch(action.type) {
        case 'QUOTE': return action.state;
        default: return state;
    }
}

const store = createStore(reducer, applyMiddleware(thunk))

function quoteAction() {
    return async (dispatch) => {
        try {
            const res = await getQuote();
            dispatch({type: 'QUOTE', state: res});
        } catch {
            dispatch({type: 'QUOTE', state: {text: 'err', author: 'err'}})
        }
    }
}

class Box extends React.Component {
    constructor(props) {
        super(props);
        this.click = this.click.bind(this)
    }
    click() {
        this.props.dispatching()
        changeColor();
    }
    render() {
        return (
            <div id='div'>
                <p id='text'><svg xmlns="http://www.w3.org/2000/svg" height='1em' fill='currentColor' viewBox="0 0 448 512"><path d="M0 216C0 149.7 53.7 96 120 96l8 0c17.7 0 32 14.3 32 32s-14.3 32-32 32l-8 0c-30.9 0-56 25.1-56 56l0 8 64 0c35.3 0 64 28.7 64 64l0 64c0 35.3-28.7 64-64 64l-64 0c-35.3 0-64-28.7-64-64l0-32 0-32 0-72zm256 0c0-66.3 53.7-120 120-120l8 0c17.7 0 32 14.3 32 32s-14.3 32-32 32l-8 0c-30.9 0-56 25.1-56 56l0 8 64 0c35.3 0 64 28.7 64 64l0 64c0 35.3-28.7 64-64 64l-64 0c-35.3 0-64-28.7-64-64l0-32 0-32 0-72z"/></svg>{this.props.quote.text}</p>
                <div className='d-flex justify-content-between align-items-center'>
                    <button onClick={this.click}>GET QUOTE</button>
                    <p id='author'>- {this.props.quote.author}</p>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {quote: state}
};
const mapDispatchToProps = (dispatch) => {
    return {
        dispatching: () => {
            dispatch(quoteAction())
        }
    }
};
const Container = connect(mapStateToProps, mapDispatchToProps)(Box);
class Wrapper extends React.Component {
    render() {
        return (
            <Provider store={store}>
                <Container/>
            </Provider>
        );
    }
}

ReactDOM.render(<Wrapper/>, document.querySelector('#root'))