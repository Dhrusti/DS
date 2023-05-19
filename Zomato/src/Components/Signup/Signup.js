import React, { useState } from 'react'
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { getSignup } from "../../Redux/Signup/Signup"
import './Sign.css'
const Signup = (props) => {
  const navigate = useNavigate();
  const { setModalOpen, setdata } = props;
  const dispatch = useDispatch()
  const [upload, setUpload] = useState(
    {
      name: "",
      email: "",
      password: "",
      phonenumber: "",
      country: ""
    }
  );

  const handleChange = (e) => {
    setUpload({
      ...upload,
      [e.target.name]: e.target.value
    });
  }

  const handleClick = () => {
    if (upload.name != "" && upload.email != "" && upload.password != "" && upload.phonenumber != "" & upload.country != "") {
      dispatch(getSignup(upload))
      navigate("/Restaurant")
    }
    else if (!(upload.phonenumber.match('[0-9]{10}'))) {
      alert('Please provide valid phone number')
    }
    else if (upload.phonenumber?.length >= 11) {
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
          <section class="">
            <div class="login_box">
              <div class="left">
                <div class="contact">
                  <form>
                    <h3>SIGN UP</h3>
                    <input type="text" className="login__input" placeholder="Name" name='name' onChange={(event) => { handleChange(event) }} />
                    <input type="email" className="login__input" placeholder="Email" name='email' onChange={(event) => { handleChange(event) }} />
                    <input type="password" className="login__input" placeholder="Password" name='password' onChange={(event) => { handleChange(event) }} />
                    <input type="number" className="login__input" placeholder="Phone-Number" name='phonenumber' onChange={(event) => { handleChange(event) }} />
                    <input type="country" className="login__input" placeholder="Country" name='country' onChange={(event) => { handleChange(event) }} />
                    {/* <span onClick={()=>{setdata(false); 
                  setModalOpen(true);
                    }}>Already have an account</span> */}
                    <button class="submit"
                      onClick={handleClick} type='button'
                    >SUBMIT</button>
                  </form>
                </div>
              </div>
            </div>
          </section>
        </div>
      </div>
    </div>
  )
}
export default Signup
