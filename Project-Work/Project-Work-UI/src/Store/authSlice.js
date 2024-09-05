import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { apiRequest } from "../apiManager";

import ApiData from '../Models/apiData';

export const signIn = createAsyncThunk(
    'auth/signIn',
    async (userData, thunkAPI) => {
        const state = thunkAPI.getState();
        const { errors } = state.auth;

        thunkAPI.dispatch(resetErrors());

        const hasErrors = checkData(userData.Email, userData.Password, errors);
        
        if (hasErrors) {
            return thunkAPI.rejectWithValue({
                message: "Invalid input data",
            });
        }

        const apiData = new ApiData(
            'http://localhost:5031/api/v1/Auth/SignIn',
            'POST',
            { email: userData.Email, password: userData.Password }
        );

        try {
            const response = await apiRequest(apiData);
            console.log(response.accessToken);
            return response;
        } catch (error) {
            return thunkAPI.rejectWithValue(error.response.data);
        }
    }
);

const authSlice = createSlice({
    name: 'auth',
    initialState: {
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
        resetErrors: (state) => {
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
        },
        setEmailError: (state, action) => {
            state.errors.emailError.hasError = action.payload;
        },
        setPasswordError: (state, action) => {
            state.errors.passwordError.hasError = action.payload;
        }
    }
});

function checkData(email, password, errors) {
    let error = false;

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/;

    if (!emailRegex.test(email)) {
        errors.emailError.hasError = true;
        error = true;
    }

    if (!passwordRegex.test(password)) {
        errors.passwordError.hasError = true;
        error = true;
    }

    return error;
}

export const { resetErrors, setEmailError, setPasswordError } = authSlice.actions;
export const selectAuth = (state) => state.auth;

export default authSlice.reducer;