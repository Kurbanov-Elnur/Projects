import { configureStore } from '@reduxjs/toolkit';
import authReducer from './authSlice';
import navbarSlice from './navbarSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    navbar: navbarSlice,
  },
});

export default store;