import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { collectionData } from '../../Redux/Collection/CollectionSlice'
import './Res.css'
import res from '../../Assest/res.avif'
import Explore from '../Home/Explore/Explore'
import Footer from '../Home/Footer/Footer'
import { useNavigate } from 'react-router-dom'
const Restaurant = () => {
  const dispatch = useDispatch()

  const USerdata1 = useSelector(state => state.userdata.data)
  const navigate = useNavigate()

  const vv = USerdata1[0].restaurant

  useEffect(() => {
    dispatch(collectionData())
  }, []);

  const hendelClick = () => {
    navigate("/menu")

  }
  return (
    <>
      <div className="container">
        <div className="hero">
          <div className="drop-down">
            <i class="fa-solid fa-location-dot"></i>
            <select>
              <option >Vadodra</option>
              <option >Anand</option>
              <option >Surat</option>
              <option >Ahemdabad</option>
            </select>
          </div>
          <div className="search-bar">
            <i class="fa-solid fa-magnifying-glass"></i>
            <input type="text" placeholder="Search for restaurant, cuisine or a dish" />
          </div>
        </div>
        <div className="card1">
          <img src={res} alt="" />
        </div>
        <div className='mmm'>
          {
            vv?.map((item) => {
              return (
                <>
                  <div className="">
                    <div class="card1" onClick={hendelClick}>
                      <img src={item.foodimg} alt="" />
                      <div class="">
                        <h4><b>{item.name}</b></h4>
                        <span>{item.item}</span>
                        <p>{item.location}</p>
                      </div>
                    </div>
                  </div>
                </>
              )
            })
          }
        </div>
        <Explore />
        <Footer />
      </div>
    </>
  )
}

export default Restaurant
