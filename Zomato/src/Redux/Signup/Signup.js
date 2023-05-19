import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from 'axios'

export const getSignup = createAsyncThunk(
  'signup/getUserById',
  async (sign) => {
    const respose = await axios.post('http://localhost:3000/signup', sign)
    console.log(respose.data);
    return respose.data;
  }
)
const signupSlice = createSlice({
  name: 'users',
  initialState: {
    loading: false,
    response: "",
    sign: "",
  },
  reducers: {

  },
  extraReducers: {
    [getSignup.pending]: (state) => {
      state.loading = true;
    },
    [getSignup.fulfilled]: (state, action) => {
      state.sign = action.payload;
    },
    [getSignup.rejected]: (state) => {
      state.response = "error";
    }
  }
})

export default signupSlice.reducer