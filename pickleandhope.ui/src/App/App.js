import React from 'react';
import logo from './logo.svg';
import './App.css';
import Pickles from  '../components/pages/Pickles/Pickles'
import Login from '../components/pages/Login/Login'
import Router from 'react-router-dom';
import fbConnection from '../helpers/data/connection';
fbConnection();

// componentDidMount() {
//   this.removeListener = firebase.auth().onAuthStateChanged((firebaseUser) => {
    
//     firebaseUser.user.getIdToken()
//       //save the token to the session storage
//       .then(token => sessionStorage.setItem('token',token))
//       .then(() => {
//           // fetch call
//           userData.GetUserByEmail(firebaseUser.email)
//             .then((response) => {
//               const internalUser = response.data;
//               if (firebaseUser) {
//                 // call out to api/user by firebase email, ? internalUserId: currentUserObj.id
//                 // pass this into the id space on my link
//                 this.setState({ authed: true, firebaseUser, internalUser });
//               } else {
//                 this.setState({ authed: false });
//               }
//             });
//         });
//   });
// }

function App() {

  const state = {file:{}};

  return (
    <div className="App">
      <button className="btn btn-danger">Bootstrap Button</button>
      <Login/>
      <Pickles/>
    </div>
  );
}

export default App;
