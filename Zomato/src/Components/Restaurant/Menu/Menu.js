import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { collectionData } from '../../../Redux/Collection/CollectionSlice'
import star from "../../../Assest/star.jpg"
import './Menu.css'
import Model from '../../Model/Model'
import Explore from '../../Home/Explore/Explore'
import Footer from '../../Home/Footer/Footer'
const Menu = () => {
  const dispatch = useDispatch()
  const [modalOpen, setModalOpen] = useState(false);
  const [searchdata, setSearchData] = useState([]);
  const [product_data, setProductData] = useState([])
  const [price, setPrice] = useState("price")
  const [data, setData] = useState("data");

  const USerdata1 = useSelector(state => state.userdata.data)

  const menu = USerdata1[0].restaurant[0].dishes
  useEffect(() => {
    dispatch(collectionData());
    setProductData(menu);
    setSearchData(menu)
  }, []);

  const searchData2 = (e) => {
    let ss = e.target.value
    if (ss == "") {
      setProductData(searchdata)
    } else {
      let filterdata = product_data.filter((item) => item.dishname.toLowerCase().includes(e.target.value.toLowerCase()))
      // console.log(filterdata, "filter");
      setProductData(filterdata)
    }
  }
  const sortingNum = () => {

    if (price == "price") {
      product_data.sort((a, b) => {
        return a.price - b.price;
      })
      setProductData(product_data)
      setPrice("priceDsc")
    }
    if (price == "priceDsc") {
      product_data.sort((a, b) => {
        return b.price - a.price;
      })
      setProductData(product_data)
      setPrice("price")
    }
  }
  const sorting = (col) => {
    if (data === "data") {
      const sortOrder = [...product_data].sort(
        (a, b) => a[col].toLowerCase() > b[col].toLowerCase() ? 1 : -1
      );
      setProductData(sortOrder)
      setData("Dsc")
    }
    if (data === "Dsc") {
      const sortOrder = [...product_data].sort(
        (a, b) => a[col].toLowerCase() < b[col].toLowerCase() ? 1 : -1
      );
      setProductData(sortOrder)
      setData("data")
    }
  }
  return (
    <>
      <div className='container'>
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
            <input type="search" placeholder="Search for restaurant, cuisine or a dish" onChange={(e) => searchData2(e)} />
          </div>
        </div>
        <div className="card3">
          <img src={star} alt="" />
        </div>

        <div className='sorting'>
          <div className='numsort'>
            {data == "data" ? <p onClick={() => sorting("dishname")}><i className="fas fa-sort-alpha-down-alt" ></i>Z-a</p> : <p onClick={() => sorting("dishname")}><i className="fas fa-sort-alpha-up" ></i> A-z</p>}
          </div>
          <div className='numsort'>
            {price == "price" ? <p onClick={() => sortingNum("price")}><i className="fas fa-sort-numeric-down"></i>9-0</p> : <p onClick={() => sortingNum("price")}><i className="fas fa-sort-numeric-up-alt" ></i> 0-9</p>}
          </div>
        </div>
        <div className='mmm'>
          {
            product_data?.map((item) => {
              return (
                <>
                  <div class="card2">
                    <img src={item.dishimg} alt="" />
                    <div class="">
                      <h4><b>{item.dishname}</b></h4>
                      <span>Price:{item.price} Rs</span>
                      <p>{item.content}</p>
                    </div>
                    <button className="openModalBtn"
                      onClick={() => { setModalOpen(true) }}>Order Now</button>
                  </div>
                </>
              )
            })
          }
        </div>
      </div>
      {modalOpen && <>
        <div className="orderform">
          <div className="orderform-modal">
            {modalOpen && <Model setModalOpen={setModalOpen} />}
          </div>
        </div>

      </>}
      <Explore />
      <Footer />
    </>
  )
}

export default Menu
