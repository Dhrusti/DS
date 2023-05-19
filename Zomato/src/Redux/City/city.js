import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from 'axios'

export const cityData = createAsyncThunk(
  'city/cityData',
  async () => {
    const respose = await axios({
        method: 'post',
        url: 'https://utilities.archesoftronix.in/api/GetCity',
        headers: {'xapikey':'pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp'},
        data: {
          "statesID":1704,
          "pageNumber":0,
          "pageSize":0,
          "orderby":true,
          "iso":"string"
        }
    })
    return respose.data;
  }
);

const usersSlice = createSlice({
  name: 'city',
  initialState: {
    loading: false,
    response: "",
    data: null
  },
  reducers: {
    
  },
  extraReducers: {
    [cityData.pending]: (state) => {
      state.loading = true;
    },
    [cityData.fulfilled]: (state, action) => {
      state.city = action.payload;
    },
    [cityData.rejected]: (state) => {
      state.response = "error";
    }
  }
})

export default usersSlice.reducer