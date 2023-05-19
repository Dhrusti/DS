import React, { useEffect } from 'react'
import { Localities } from '../../../Redux/Localities/LocalitiesSlice'
import { useDispatch, useSelector } from 'react-redux'
import './Location.css'
const Location = () => {
    const dispatch = useDispatch()

    const Location = useSelector(state => state.localities.data)
    // console.log(Location, "use");

    useEffect(() => {
        dispatch(Localities())
    }, []);
    return (
        <div>
            <div className="container">
                <div className="location">
                    <p>Popular localities in and around Vadodara</p>
                </div>
                <div className="main-map">
                {
                    Location?.map((item) => {
                        return (
                            <>
                                <div className="main-card">
                                    <div className="card-content">
                                        <div className="root-contaent">
                                        <h5>{item.placesname}</h5>
                                    <p>{item.placesnum}</p>
                                        </div>
                                 <div className="arrow">
                                    <i class="fa-solid fa-angle-right"></i>
                                 </div>
                                    </div>
                                </div>
                            </>
                        )
                    })
                }
                </div>
            </div>
        </div>
    )
}

export default Location
