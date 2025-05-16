import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { GetProducts } from '@/Lib/Actions/productActions';
import { GetBasket } from '@/Lib/Actions/basketActions';

export interface Product {
    id: string;
    name: string;
    price: number;
    imageUrl: string;
}

interface CartItem extends Product {
    quantity: number;
}

interface ProductsState {
    searchTerm: string;
    products: Product[];
    filteredProducts: Product[];
    cartOpen: boolean;
    cartItems: CartItem[];
    IsLoading: boolean;
}

const initialState: ProductsState = {
    searchTerm: '',
    IsLoading: false,
    products: [],
    filteredProducts: [],
    cartOpen: false,
    cartItems: [],
};

function filterProducts(products: Product[], searchTerm: string): Product[] {
    return products.filter(product =>
        product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
}

const productsSlice = createSlice({
    name: 'products',
    initialState,
    reducers: {
        setSearchTerm: (state, action: PayloadAction<string>) => {
            state.searchTerm = action.payload;
            state.filteredProducts = filterProducts(state.products, state.searchTerm);
        },
        toggleCart: (state) => {
            state.cartOpen = !state.cartOpen;
        },
        addToCart: (state, action: PayloadAction<Product>) => {
            const product = action.payload;
            const existingItem = state.cartItems.find(item => item.id === product.id);

            if (existingItem) {
                existingItem.quantity += 1;
            } else {
                state.cartItems.push({ ...product, quantity: 1 });
            }
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(GetProducts.pending, (state) => {
                state.IsLoading = true;
            })
            .addCase(GetProducts.fulfilled, (state, action: any) => {
                state.IsLoading = false;

                state.products = action.payload.map((product: any) => ({
                    id: product.id,
                    name: product.name,
                    price: product.price,
                    imageUrl: product.image,
                }));

                state.filteredProducts = filterProducts(state.products, state.searchTerm);
            })
            .addCase(GetProducts.rejected, (state) => {
                state.IsLoading = false;
            })
            .addCase(GetBasket.pending, (state) => {
                state.IsLoading = true;
            })
            .addCase(GetBasket.fulfilled, (state, action: any) => {
                state.IsLoading = false;

                state.cartItems = action.payload.products.map((product: any) => ({
                    id: product.productId,
                    name: product.productName,
                    price: product.productPrice,
                    imageUrl: product.productImage,
                    quantity: product.productQuantity,
                }));

                state.filteredProducts = filterProducts(state.products, state.searchTerm);
            })
            .addCase(GetBasket.rejected, (state) => {
                state.IsLoading = false;
            })
            .addDefaultCase((state: ProductsState) => {
                state.filteredProducts = filterProducts(state.products, state.searchTerm);
            })
    },
});

export const getTotalPrice = (state: { catalog: ProductsState }): number =>
    state.catalog.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

export const {
    setSearchTerm,
    toggleCart,
    addToCart,
} = productsSlice.actions;

export const selectCatalog = (state: { catalog: ProductsState }) => state.catalog;

export default productsSlice.reducer;