import { createSlice } from '@reduxjs/toolkit';

const productsSlice = createSlice({
    name: 'products',
    initialState: {
        searchTerm: '',
        products: [
            { id: 1, name: 'Chanel', price: 36, imageUrl: require('../Assets/Products/chanel.png') },
            { id: 2, name: 'Mac Book', price: 2000, imageUrl: require('../Assets/Products/macbook.png') },
            { id: 3, name: 'Man Mix', price: 12, imageUrl: require('../Assets/Products/man-mix.png') },
            { id: 4, name: 'Nike', price: 120, imageUrl: require('../Assets/Products/nike.png') },
            { id: 5, name: 'Watch', price: 80, imageUrl: require('../Assets/Products/watch.png') },
            { id: 6, name: 'Woman Mix', price: 20, imageUrl: require('../Assets/Products/woman-mix.png') },
        ],
        filteredProducts: [],
        cartOpen: false,
        cartItems: [],
    },
    reducers: {
        setSearchTerm: (state, action) => {
            state.searchTerm = action.payload;
            state.filteredProducts = filterProducts(state.products, state.searchTerm);
        },
        toggleCart: (state) => {
            state.cartOpen = !state.cartOpen;
        },
        addToCart: (state, action) => {
            const product = action.payload;
            const existingItem = state.cartItems.find(item => item.id === product.id);

            if (existingItem) {
                existingItem.quantity += 1;
            } else {
                state.cartItems.push({ ...product, quantity: 1 });
            }
        },
        removeFromCart: (state, action) => {
            const productId = action.payload;
            state.cartItems = state.cartItems.filter(item => item.id !== productId);
        },
        incrementQuantity: (state, action) => {
            const productId = action.payload;
            const item = state.cartItems.find(item => item.id === productId);
            if (item) {
                item.quantity += 1;
            }
        },
        decrementQuantity: (state, action) => {
            const productId = action.payload;
            const item = state.cartItems.find(item => item.id === productId);
            if (item) {
                item.quantity = item.quantity > 1 ? item.quantity - 1 : 1;
            }
        },
    },
    extraReducers: (builder) => {
        builder.addDefaultCase((state) => {
            state.filteredProducts = filterProducts(state.products, state.searchTerm);
        });
    },
});

function filterProducts(products, searchTerm) {
    return products.filter(product =>
        product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
}

export const getTotalPrice = (state) => 
    state.catalog.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

export const { setSearchTerm, toggleCart, addToCart, removeFromCart, incrementQuantity, decrementQuantity } = productsSlice.actions;

export const selectCatalog = (state) => state.catalog;

export default productsSlice.reducer;