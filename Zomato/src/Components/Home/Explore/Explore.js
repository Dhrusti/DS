import React from 'react'
import './Explore.css'
const Explore = () => {
    return (
        <div>
            <div className="container">
                <div className="explore-main">
                    <div className="explore-root">
                        <h1>Explore options near me</h1>
                    </div>
                    <div className="explore-text">
                        <h5>Popular cuisines near me</h5>
                        <i class="fa-solid fa-angle-down"></i>
                    </div>
                    <div className="explore-sec">
                        <h5>Popular restaurant types near me</h5>
                        <i class="fa-solid fa-angle-down"></i>
                    </div>
                    <div className="explore-third">
                        <h5>Cities We Deliver To</h5>
                        <i class="fa-solid fa-angle-down"></i>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Explore
