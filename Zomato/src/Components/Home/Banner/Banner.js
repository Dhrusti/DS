import React from 'react'
import Label from './Label/Label'
import Navbar from './Navbar/Navbar'
import './Banner.css'

const Banner = () => {
    return (
        <div className='Banner'>
            <Navbar />
            <Label />
        </div>
    )
}

export default Banner