import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
    name: 'auth',
    initialState: {
        isSignUp: false,
        showPassword: false,
        users: JSON.parse(localStorage.getItem('users')) || [],
    },
    reducers: {
        toggleSignUp: (state) => {
            state.isSignUp = !state.isSignUp;
        },
        togglePasswordVisibility: (state) => {
            state.showPassword = !state.showPassword;
        },
        signIn: (state, action) => {

        },
        signUp: (state, action) => {
            const newUser = action.payload;

            state.users.push(newUser);
            localStorage.setItem('users', JSON.stringify(state.users));
        }
    },
});

export const { toggleSignUp, togglePasswordVisibility, signUp } = authSlice.actions;

export const selectAuth = (state) => state.auth;

export default authSlice.reducer;