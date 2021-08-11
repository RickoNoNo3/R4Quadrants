import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import {ActionTypes, Store} from './Redux/Redux';

Store.dispatch({
  type: ActionTypes.NET_DOWNLOAD_TASKLIST,
})

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);
