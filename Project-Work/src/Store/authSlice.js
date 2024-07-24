import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
    name: 'auth',
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

export const { toggleSignUp, togglePasswordVisibility } = authSlice.actions;

export const selectAuth = (state) => state.auth;

export default authSlice.reducer;