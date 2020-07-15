import React, {Component} from "react";
import SinglePickle from "../../shared/SinglePickle/SinglePickle";
import {getPickles} from "../../../helpers/data/pickleData";
import FileUpload from '../../shared/FileUpload/FileUpload';
import {uploadFile} from '../../../helpers/data/fileUpload';
import {baseUrl} from "../../../apikeys.json";


class Pickles extends Component {

    state = {
        pickles: [ 
            // {id:1, type:"dill", numberInStock:12, size:"large", price:5},
            // {id:2, type:"dill", numberInStock:34, size:"large", price:7} 
        ],
        file:{}
    };

    componentDidMount() {
        getPickles()
            .then(pickles => this.setState({pickles:pickles}));
    }

    
    render() {
        
        const {pickles} = this.state;
        const generatePickles = () => pickles.map(pickle => 
            <SinglePickle key={pickle.id} pickle={pickle} /> 
        );

        const uploadOnClick = () =>  {
            const {file} = this.state; 
    
            uploadFile(file);
        }

        return (
            <div>
                <FileUpload onChange={(file) => this.setState({file:file})}/>
                <button onClick={uploadOnClick} >Click Me</button>
                {generatePickles()}

                <img src={`${baseUrl}/api/images/2`}/>
                <img src={`${baseUrl}/api/images/3`}/>
                <img src={`${baseUrl}/api/images/4`}/>
                <img src={`${baseUrl}/api/images/5`}/>

            </div>)
    }

}

export default Pickles;