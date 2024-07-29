import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
    name: 'auth',
    initialState: {
        isSignUp: false,
        showPassword: false,
        users: JSON.parse(localStorage.getItem('users')) || [],
        currentUser: null,
    },
    reducers: {
        toggleSignUp: (state) => {
            state.isSignUp = !state.isSignUp;
        },
        togglePasswordVisibility: (state) => {
            state.showPassword = !state.showPassword;
        },
        signIn: (state, action) => {
            console.log('Users from localStorage:', JSON.parse(localStorage.getItem('users')));

            const userData = action.payload;

            const user = state.users.find(user => user.Email === userData.Email && user.Password === userData.Password);

            if (user) {
                state.currentUser = user;
                console.log('Hello');
            } else {
                console.error('Invalid email or password');
            }
        },
        signUp: (state, action) => {
            const newUser = action.payload;

            if (!state.users.find(user => user.Email === newUser.Email) && newUser.Password === newUser.ConfirmPassword) {
                state.users.push({
                    UserName: newUser.UserName,
                    Email: newUser.Email,
                    Password: newUser.Password
                });

                localStorage.setItem('users', JSON.stringify(state.users));
            } else {
                console.error('User already exists or passwords do not match');
            }
        },
        signOut: (state) => {
            state.currentUser = null;
        }
    },
});

export const { toggleSignUp, togglePasswordVisibility, signIn, signUp, signOut } = authSlice.actions;

export const selectAuth = (state) => state.auth;

export default authSlice.reducer;