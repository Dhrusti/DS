import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from 'axios'

export const collectionData = createAsyncThunk(
  'collection/collectionData',
  async () => {
    const respose = await axios.get(`http://localhost:3000/collections`)
    // console.log(respose.data,"ss")
    return respose.data;
  }
);

const usersSlice = createSlice({
  name: 'collection',
  initialState: {
    loading: false,
    response: "",
    data: null
  },
  reducers: {
    
  },
  extraReducers: {
    [collectionData.pending]: (state) => {
      state.loading = true;
    },
    [collectionData.fulfilled]: (state, action) => {
      state.data = action.payload;
    },
    [collectionData.rejected]: (state) => {
      state.response = "error";
    }
  }
})

export default usersSlice.reducer