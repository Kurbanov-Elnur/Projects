import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
    name: 'auth',
    initialState: {
        isSignUp: false,
        showPassword: false,
        users: JSON.parse(localStorage.getItem('users')) || [],
        currentUser: null,
        errors: {
            emailError: {
                hasError: false,
                message: "Invalid email format!"
            },
            passwordError: {
                hasError: false,
                message: "Password must be at least 8 characters long and include at least one letter and one number!"
            }
        },
    },
    reducers: {
        toggleSignUp: (state) => {
            reseterrors(state);
            state.isSignUp = !state.isSignUp;
        },
        togglePasswordVisibility: (state) => {
            state.showPassword = !state.showPassword;
        },
        signIn: (state, action) => {
            const userData = action.payload;

            reseterrors(state);

            if (!checkData(state, userData.Email, userData.Password)) {
                const user = state.users.find(user => user.Email === userData.Email);
                if (user) {
                    if (user.Password === userData.Password)
                        state.currentUser = user;
                    else {
                        state.errors.passwordError.hasError = true;
                        state.errors.passwordError.message = "Password does not match! If you forgot your password, please reset it.";
                    }
                } else {
                    state.errors.emailError.hasError = true;
                    state.errors.emailError.message = "User not found!";
                }
            }
        },
        signUp: (state, action) => {
            const newUser = action.payload;

            reseterrors(state);

            if (!checkData(state, newUser.Email, newUser.Password)) {
                if (!state.users.find(user => user.Email === newUser.Email) && newUser.Password === newUser.ConfirmPassword) {
                    state.users.push({
                        UserName: newUser.UserName,
                        Email: newUser.Email,
                        Password: newUser.Password
                    });

                    localStorage.setItem('users', JSON.stringify(state.users));
                } else {
                    if (state.users.find(user => user.Email === newUser.Email)) {
                        state.errors.emailError.hasError = true;
                        state.errors.emailError.message = "User already exists!";
                    }
                    if (newUser.Password !== newUser.ConfirmPassword) {
                        state.errors.passwordError.hasError = true;
                        state.errors.passwordError.message = "Passwords do not match!";
                    }
                }
            }
        },
        signOut: (state) => {
            state.currentUser = null;
        }
    },
});

function checkData(state, email, password) {
    let error = false

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;

    if (!emailRegex.test(email)) {
        state.errors.emailError.hasError = true;
        error = true;
    }

    if (!passwordRegex.test(password)) {
        state.errors.passwordError.hasError = true;
        error = true
    }

    return error;
}

function reseterrors(state) {
    state.errors = {
        emailError: {
            hasError: false,
            message: "Invalid email format!"
        },
        passwordError: {
            hasError: false,
            message: "Password must be at least 8 characters long and include at least one letter and one number!"
        }
    };
}

export const { toggleSignUp, togglePasswordVisibility, signIn, signUp, signOut } = authSlice.actions;

export const selectAuth = (state) => state.auth;

export default authSlice.reducer;