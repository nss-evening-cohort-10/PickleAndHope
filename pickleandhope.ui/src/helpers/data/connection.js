import firebase from 'firebase';
import constants from '../../constants';

const firebaseApp = () => {
  firebase.initializeApp(constants.firebaseConfig);
};

export default firebaseApp;