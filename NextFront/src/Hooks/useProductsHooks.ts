import { useDispatch } from 'react-redux';
import * as ProductActions from '../Lib/Actions/productActions';
import { AppDispatch } from '@/Store/store';
import { useEffect } from 'react';

export const useProducts = () => {
    const dispatch = useDispatch<AppDispatch>();

    useEffect(() => {
        GetProducts();
    }, []);

    const GetProducts = async () => {
        dispatch(ProductActions.GetProducts())
    };

    return { GetProducts };
};