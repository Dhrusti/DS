import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from 'axios'

export const Localities = createAsyncThunk(
  'Location/Localities',
  async () => {
    const respose = await axios.get('http://localhost:3000/places')
    // console.log(respose.data,"ss")
    return respose.data;
  }
);

const locationSlice = createSlice({
  name: 'Location',
  initialState: {
    loading: false,
    response: "",
    data: null
  },
  reducers: {

  },
  extraReducers: {
    [Localities.pending]: (state) => {
      state.loading = true;
    },
    [Localities.fulfilled]: (state, action) => {
      state.data = action.payload;
    },
    [Localities.rejected]: (state) => {
      state.response = "error";
    }
  }
})

export default locationSlice.reducer