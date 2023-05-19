import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import vif from "../../../../Assest/zz.avif"
import { cityData } from '../../../../Redux/City/city'
import './Label.css'
const Label = () => {
  // const citys = useSelector(state => state.citys.city.data)
  // console.log(citys,"city");

  // const dispatch = useDispatch()
  // useEffect(() => {
  //   dispatch(cityData())
  // },[]);
  return (
    <>
      <div className='label'>
        <img src={vif} alt="" />
      </div>
      <div className="font">
        <span>Discover the best food & drinks in Vadodara</span>
      </div>
      <div className="hero">
        <div className="drop-down">
          <i class="fa-solid fa-location-dot"></i>
          <select>
          {/* {
                citys?.map((item) => {
                  return (<option >{item.cityName} </option>)
                })
              } */}
              <option >Vadodra </option>
          </select>
        </div>
        <div className="search-bar">
        <i class="fa-solid fa-magnifying-glass"></i>
          <input type="text" placeholder="Search for restaurant, cuisine or a dish" />
        </div>
      </div>
    </>
  )
}

export default Label
