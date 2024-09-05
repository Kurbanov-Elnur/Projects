import { configureStore } from '@reduxjs/toolkit';
import authReducer from "./authSlice"
import forgotReducer from "./forgotSlice"

const store = configureStore({
  reducer: {
    auth: authReducer,
    forgot: forgotReducer,
  },
});

export default store;