import { createAsyncThunk } from '@reduxjs/toolkit';
import ApiManager from '../api/apiManager';

interface AddProductToBasketDTO {
    ProductId: string;
    Quantity: number;
}

export const GetBasket = createAsyncThunk(
    'Basket/Get',
    async (_, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Basket`,
                Method: 'GET',
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);

export const AddProductToBasket = createAsyncThunk(
    'Basket/AddProduct',
    async (Product: AddProductToBasketDTO, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Basket`,
                Method: 'POST',
                Params: Product,
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);

export const Increase = createAsyncThunk(
    'Basket/Increase',
    async (productId: string, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Basket/increase`,
                Method: 'POST',
                Params: { productId },
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);

export const Decrease = createAsyncThunk(
    'Basket/Decrease',
    async (productId: string, { rejectWithValue }) => {
        try {
            const response = await ApiManager.apiRequest({
                Url: `Basket/decrease`,
                Method: 'POST',
                Params: { productId },
                WithCredentials: true,
            });
            return response;
        } catch (error: any) {
            return rejectWithValue(error.message);
        }
    }
);