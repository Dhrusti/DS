import React, { useEffect, useState } from 'react'
import '../../../Restaurant/Menu/Menu.css'
import Login from '../../../Login/Login';
import Signup from '../../../Signup/Signup';
import './Navbar.css';
const Navbar = () => {
  const [modalOpen, setModalOpen] = useState(false);
  const [modalOpen1, setModalOpen1] = useState(false);


  return (
    <>
      <div className='navbar'>
        <div className="mainbar">
          <div className="logo">
            <ul>
              <li><a href="">Get the App</a></li>
            </ul>
          </div>
          <div className="nav">
            <ul>
              <li><a href="">Investor Relations</a></li>
              <li><a href="">Add restaurant</a></li>
              <button className="openModalBtn"
                onClick={() => { setModalOpen(true) }}>Log in</button>
              <button className="openModalBtn"
                onClick={() => { setModalOpen1(true) }}>Sign up</button>
            </ul>
          </div>
        </div>
      </div>
      {modalOpen && <>
        <div className="orderform">
          <div className="orderform-modal">
            {modalOpen && <Login setModalOpen={setModalOpen}  setdata={setModalOpen1}/>}
          </div>
        </div>
      </>}
      {modalOpen1 && <>
        <div className="orderform">
          <div className="orderform-modal">
            {modalOpen1 && <Signup setModalOpen1={setModalOpen1} setdata={setModalOpen}/>}
          </div>
        </div>
      </>}

    </>
  )
}

export default Navbar
