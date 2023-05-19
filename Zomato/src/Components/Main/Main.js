import React from 'react'
import Banner from '../Home/Banner/Banner'
import Collection from '../Home/Collection/Collection'
import Explore from '../Home/Explore/Explore'
import Footer from '../Home/Footer/Footer'
import Order from '../Home/Order/Order'
import Phone from '../Home/Phoneapp/Phone'
import Location from '../Home/Location/Location';
const Main = () => {
  return (
    <div>
      <Banner />
      <Order />
      <Collection />
      <Location />
      <Phone />
      <Explore />
      <Footer />
    </div>
  )
}

export default Main
