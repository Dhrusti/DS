import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import '../Signup/Sign.css'

const Login = (props) => {
  const navigate = useNavigate();
  const { setModalOpen, setdata } = props
  const [loginformData, setLoginFormData] = useState({
    email: "",
    password: "",
  });

  const handleChangeLogin = (e) => {
    setLoginFormData({
      ...loginformData,
      [e.target.name]: e.target.value
    });
  }

  const handleSubmitLogin = () => {
    axios
      .get("http://localhost:3000/signup")
      .then((responses) => responses.data)
      .then((usersData) => {
        const usersEmail = (usersData.map((user) => user.email));
        const usersPassword = (usersData.map((user) => user.password));
        if (usersEmail.includes(loginformData.email) && usersPassword.includes(loginformData.password) && loginformData.email != "" && loginformData.password != "") {
          navigate("/Restaurant")
        }
        else { alert("not match") }
      }
      )
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
                <div class="top_link"><a href="#"><img src="https://drive.google.com/u/0/uc?id=16U__U5dJdaTfNGobB_OpwAJ73vM50rPV&export=download" alt="" /> <Link to="/">Go to home</Link>
                </a></div>
                <div class="contact">
                  <form>
                    <h3>LOG IN</h3>
                    <input type="email" id="form1Example1" className="login__input" name="email" onChange={handleChangeLogin} />
                    <input type="password" id="form1Example2" className="login__input" name="password" onChange={handleChangeLogin} />
                    <span onClick={() => {
                      setModalOpen(false);
                      setdata(true);
                    }}>Don't have an account</span>
                    <button class="submit"
                      onClick={handleSubmitLogin} type='button'
                    >LET'S GO</button>
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

export default Login
