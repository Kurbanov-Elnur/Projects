import { createSlice } from '@reduxjs/toolkit';

const catalogSlice = createSlice({
    name: 'catalog',
    initialState: {
        isSignUp: false,
        showPassword: false,
    },
    reducers: {
        toggleSignUp: (state) => {
            state.isSignUp = !state.isSignUp;
        },
        togglePasswordVisibility: (state) => {
            state.showPassword = !state.showPassword;
        },
    },
});

export const { toggleSignUp, togglePasswordVisibility } = catalogSlice.actions;

export const selectCatalog = (state) => state.catalog;

export default catalogSlice.reducer;