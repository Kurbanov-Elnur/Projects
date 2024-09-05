import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { apiRequest } from "../apiManager";
import ApiData from '../Models/apiData';

export const forgotPassword = createAsyncThunk(
    'forgot/forgotPassword',
    async (email, thunkAPI) => {
        const state = thunkAPI.getState();

        if (!state.forgot.codeSent) {
            thunkAPI.dispatch(resetErrors());

            const apiData = new ApiData(
                'http://localhost:5031/api/v1/Email/SendVerificationCode',
                'POST',
                email.email
            );

            try {
                await apiRequest(apiData);
                thunkAPI.dispatch(setCodeSent(true));
            } catch (error) {
                return thunkAPI.rejectWithValue(error.response.data);
            }
        }
        else {
            const code = state.forgot.verificationCode.join('');

            const apiData = new ApiData(
                'http://localhost:5031/api/v1/Email/VerifyEmail',
                'POST',
                { email: email.email, code }
            );

            try {
                await apiRequest(apiData);
                thunkAPI.dispatch(setIsVerified(true));
            } catch (error) {
                return thunkAPI.rejectWithValue(error.response.data);
            }
        }
    }
);

const forgotSlice = createSlice({
    name: 'forgot',
    initialState: {
        codeSent: false,
        verificationCode: Array(4).fill(''),
        isVerified: false,
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
        handleCodeChange: (state, action) => {
            const { index, value } = action.payload;
        
            if (value === "" && index > 0) {
                state.verificationCode[index] = value;
            } else if (/^\d$/.test(value)) {
                state.verificationCode[index] = value;
            }
        },
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
        },
        setCodeSent: (state, action) => {
            state.codeSent = action.payload;
        },
        setIsVerified: (state, action) => {
            state.isVerified = action.payload;
        },
        returnToSignIn: (state) => {
            state.codeSent = false;
            state.verificationCode = Array(4).fill('');
            state.isVerified = false;

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
    }
});

export const { resetErrors, handleCodeChange, setEmailError, setPasswordError, setCodeSent, setIsVerified, returnToSignIn } = forgotSlice.actions;

export const selectForgot = (state) => state.forgot;

export default forgotSlice.reducer;