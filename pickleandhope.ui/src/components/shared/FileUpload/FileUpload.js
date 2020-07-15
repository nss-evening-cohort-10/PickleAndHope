import React,{Component} from "react"

class FileUpload extends Component {
    
    fileChanged = (e) => {
        this.props.onChange(e.target.files[0]);
    };

    render() {

        return (
            <div>
                <label for="file">File To Upload</label>
                <input name="file" type="file" accept=".jpg,.png,.gif" onChange={this.fileChanged}/>
            </div>
        );

    }
}

export default FileUpload;