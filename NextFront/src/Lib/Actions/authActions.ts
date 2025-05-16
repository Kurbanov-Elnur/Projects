import { createAsyncThunk } from '@reduxjs/toolkit';
import ApiManager from '../api/apiManager';
import { LoginDTO, RegisterDTO } from '../../Data/DTOs/Auth.DTO';

export const LoginUser = createAsyncThunk(
    'Auth/Login',
    async (LoginDTO: LoginDTO, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Auth/SignIn`,
                Method: 'POST',
                Data: LoginDTO,
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);

export const RegisterUser = createAsyncThunk(
    'Auth/Register',
    async (RegisterDTO: RegisterDTO, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Auth/SignUp`,
                Method: 'POST',
                Data: RegisterDTO,
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);

export const Logout = createAsyncThunk(
    'Auth/Logout',
    async (_, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Auth/Logout`,
                Method: 'POST',
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);