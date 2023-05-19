import React from 'react';
import './Phone.css';
import tel from '../../../Assest/tel.avif';
import gpay from '../../../Assest/gpay.webp';
import app from '../../../Assest/appstore.webp'
const Phone = () => {
    return (
        <div>
            <div className="container">
                <div className="main-app">
                    <div className="root-app">
                        <img src={tel} alt="" />
                    </div>
                    <div className="app-info">
                        <h1>Get the Zomato app</h1>
                        <p>We will send you a link, open it on your phone to download the app</p>
                        <div className="radio">
                        <div>
                            <input type="radio" name='name' />
                            <label htmlFor="">Email</label>
                        </div>
                        <div>
                            <input type="radio" name='name' />
                            <label htmlFor="">Phone</label>
                        </div>
                        </div>
                        <div className="input">
                            <div className="input-area">
                                <input type="text" placeholder='Email' />
                            </div>
                            <div className="input-button">
                                <button>Share App Link</button>
                            </div>
                        </div>
                        <div className="download">
                            <span>Download app from</span>
                        </div>
                        <div className="trans-button">
                            <div className="gpay">
                            <img src={gpay} alt="" />
                            </div>
                            <div className="app">
                            <img src={app} alt="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Phone
