import axios from "axios";
import {baseUrl} from "../../apikeys.json";

const uploadFile = (file) => new Promise((resolve,reject) => {
    let form = new FormData();
    form.append('file', file);

    axios.post(`${baseUrl}/api/images`, form)
        .then(() => {
            resolve();
        })
        .catch(error => reject(error));
})

export {uploadFile};