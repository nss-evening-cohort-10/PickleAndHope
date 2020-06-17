import React from 'react';
import logo from './logo.svg';
import './App.css';
import Pickles from  '../components/pages/Pickles/Pickles'
import Login from '../components/pages/Login/Login'
import Router from 'react-router-dom';
import fbConnection from '../helpers/data/connection';
fbConnection();

function App() {
  return (
    <div className="App">
      <button className="btn btn-danger">Bootstrap Button</button>
      <Login/>
      <Pickles/>
    </div>
  );
}

export default App;
