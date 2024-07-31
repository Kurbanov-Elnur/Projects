import { configureStore } from '@reduxjs/toolkit';
import authReducer from './authSlice';
import navbarSlice from './navbarSlice';
import catalogSlice from './productsSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    navbar: navbarSlice,
    catalog: catalogSlice,
  },
});

export default store;