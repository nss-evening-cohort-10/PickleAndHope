import axios from "axios";
import {baseUrl} from "../../apikeys.json";

const getPickles = () => new Promise((resolve,reject) => {
    axios.get(`${baseUrl}/api/pickles`)
        .then((result) => resolve(result.data))
        .catch(error => reject(error));
});

export {getPickles};