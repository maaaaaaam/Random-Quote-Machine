//All in one file

import React from 'react';
import ReactDOM from "react-dom";
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import {Provider, connect} from "react-redux";

async function getQuote() {
    let arr;
    try {
        // console.log('fetching');
        await fetch('https://gist.githubusercontent.com/camperbot/5a022b72e96c4c9585c32bf6a75f62d9/raw/e3c6895ce42069f0ee7e991229064f167fe8ccdc/quotes.json')
            .then(res => res.json()).then(res => {
                arr = res.quotes
            })
        // console.log('fetched')
    } catch(err) {
        console.log('err:', err);
    }
    const index = Math.floor(Math.random() * (arr.length))
    return {text: arr[index].quote, author: arr[index].author};
}


function reducer(state = {text: 'press the button', author: ''}, action) {
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
    }
    render() {
        return (
            <div>
                <p>{this.props.quote.text}</p>
                <p>{this.props.quote.author}</p>
                <button onClick={this.props.dispatching}>Get Quote</button>
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