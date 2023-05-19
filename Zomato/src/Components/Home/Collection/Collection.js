import React, { useEffect } from 'react'
import './Collection.css'
import { useDispatch, useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { collectionData } from '../../../Redux/Collection/CollectionSlice'
const Collection = () => {
    const dispatch = useDispatch()
    const navigate = useNavigate()
    const USerdata = useSelector(state => state.userdata.data)
    // console.log(USerdata, "use");

    useEffect(() => {
        dispatch(collectionData())
    }, []);

    const hendelsubmit = () => {
        navigate("/Restaurant")
    }
    return (
        <div>
            <div className="container">
                <div className="col">
                    <h2>Collections</h2>
                    <div className="collection">
                        <span>Explore curated lists of top restaurants, cafes, pubs, and bars in Vadodara, based on trends</span>
                        <div className="span">
                            <span>All collections in Vadodara</span>
                            <i class="fa-solid fa-caret-right"></i>
                        </div>
                    </div>
                </div>
                <div className="collection">
                    {
                        USerdata?.map((item) => {
                            return (
                                <>
                                    <div class="card" onClick={hendelsubmit}>
                                        <img src={item.image} alt="" />
                                        <div className="contain">
                                            <span>{item.name}</span>
                                            <p>{item.places} <i class="fa-solid fa-caret-right"></i></p>
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

export default Collection
