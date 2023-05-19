import {configureStore} from '@reduxjs/toolkit'
import collection from './Collection/CollectionSlice'
import location from './Localities/LocalitiesSlice'
import city from './City/city'
const store = configureStore({
    reducer: {
        userdata: collection,
        localities : location,
        citys:city
    },
})
export default store
