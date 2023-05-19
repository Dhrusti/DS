import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import './Model.css'
const Model = () => {
    const navigate = useNavigate();
    const [upload, setUpload] = useState(
        {
          name: "",
          address: "",
          email: "",
          phonenumber: "",
          card: ""
        }
      );
    const handleChange = (e) => {
        setUpload({
            ...upload,
            [e.target.name]: e.target.value
          });
    }
    const hendeldone = ()=>{
        if (upload.name != "" && upload.address != "" && upload.email != "" && upload.phonenumber != "" & upload.card != "") {
            navigate("/done")
          }
          else if (upload.phonenumber?.length>=11){
            alert('Please provide valid 10 degit phone number')
          }
           else {
            alert("All Fileds are Required")
          }
    }
    
    return (
        <div>
            <div className="modalBackground">
                <div className="modalContainer">
                    <div className="titleCloseBtn">
                    </div>
                    <div className="title">
                        <h1>Please Order Here</h1>
                    </div>
                    <form>
                    <div className="input-order">
                        <input type="text" name='name' placeholder='Name:'onChange={(event) => { handleChange(event) }} />
                    </div>
                    <div className="input-order">
                        <input type="text" name='address' placeholder='Address:' onChange={(event) => { handleChange(event) }} />
                    </div>
                    <div className="input-order">
                        <input type="email" name='email'  placeholder='Email:' onChange={(event) => { handleChange(event) }} />
                    </div>
                    <div className="input-order">
                        <input type="number" name='phonenumber' placeholder='PhoneNumber:' onChange={(event) => { handleChange(event) }}/>
                    </div>
                    <div className="input-order">
                        <input type="number" name='card' placeholder='Pincode' onChange={(event) => { handleChange(event) }}/>
                    </div>
                    </form>
                    <div className="footer">
                        <button onClick={hendeldone}>Confirm</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Model
