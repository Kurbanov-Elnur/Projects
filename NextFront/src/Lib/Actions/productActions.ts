import { createAsyncThunk } from '@reduxjs/toolkit';
import ApiManager from '../api/apiManager';

export const GetProducts = createAsyncThunk(
    'Products/Get',
    async (_, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Product`,
                Method: 'GET',
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);