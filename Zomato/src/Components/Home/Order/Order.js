import React from 'react'
import './Order.css'
import left from '../../../Assest/left.avif'
import right from '../../../Assest/right.avif'
const Order = () => {
    return (
        <div className='container'>
            <div className="order-main">
                <div className="order-online">
                    <img src={left} alt="" />
                    <p>Order Online</p>
                    <span>Stay home and order to your doorstep</span>
                </div>
                <div className="dianing">
                    <img src={right} alt="" />
                    <p>Dining</p>
                    <span>View the city's favourite dining venues</span>
                </div>
            </div>
        </div>
    )
}

export default Order
