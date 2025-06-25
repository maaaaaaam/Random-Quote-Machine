import React from 'react';
import ReactDOM from "react-dom";
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import {Provider, connect} from "react-redux";
import 'bootstrap/dist/css/bootstrap.min.css'
import './index.css';
import QuoteBox from './Components/QuoteBox.jsx';
import { reducer, quoteAction } from './redux/actionReducer.js';

const store = createStore(reducer, applyMiddleware(thunk))

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

const Container = connect(mapStateToProps, mapDispatchToProps)(QuoteBox);

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