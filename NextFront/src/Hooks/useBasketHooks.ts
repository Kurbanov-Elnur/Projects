import { useDispatch } from 'react-redux';
import * as BasketActions from '../Lib/Actions/basketActions';
import { AppDispatch } from '@/Store/store';
import { useEffect } from 'react';

export const useBasket = () => {
    const dispatch = useDispatch<AppDispatch>();

    useEffect(() => {
        GetBasket();
    }, []);

    const GetBasket = async () => {
        dispatch(BasketActions.GetBasket())
    };

    const AddToBasket = async (productId: string) => {
        dispatch(BasketActions.AddProductToBasket({ ProductId: productId, Quantity: 1 }))
            .then((response) => {
                if (response.meta.requestStatus === 'fulfilled')
                    GetBasket();
            })
    }

    const Increase = async (productId: string) => {
        dispatch(BasketActions.Increase(productId))
            .then((response) => {
                if (response.meta.requestStatus === 'fulfilled')
                    GetBasket();
            })
    }

    const Decrease = async (productId: string) => {
        dispatch(BasketActions.Decrease(productId))
            .then((response) => {
                if (response.meta.requestStatus === 'fulfilled')
                    GetBasket();
            })
    }

    return { GetBasket, AddToBasket, Increase, Decrease };
};