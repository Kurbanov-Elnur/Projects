import { configureStore } from '@reduxjs/toolkit';
import authReducer from './Reducers/authSlice';
import navbarSlice from './Reducers/navbarSlice';
import catalogSlice from './Reducers/productsSlice';

const store = configureStore({
  reducer: {
    auth: authReducer,
    navbar: navbarSlice,
    catalog: catalogSlice,
  },
});


export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;