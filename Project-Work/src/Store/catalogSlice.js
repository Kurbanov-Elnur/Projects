import { createSlice } from '@reduxjs/toolkit';

const catalogSlice = createSlice({
    name: 'catalog',
    initialState: {
        searchTerm: '',
        products: [
            { name: 'Chanel', price: '$36', imageUrl: require('../Assets/Products/chanel.png') },
            { name: 'Mac Book', price: '$2000', imageUrl: require('../Assets/Products/macbook.png') },
            { name: 'Man Mix', price: '$12', imageUrl: require('../Assets/Products/man-mix.png') },
            { name: 'Nike', price: '$120', imageUrl: require('../Assets/Products/nike.png') },
            { name: 'Watch', price: '$80', imageUrl: require('../Assets/Products/watch.png') },
            { name: 'Woman Mix', price: '$20', imageUrl: require('../Assets/Products/woman-mix.png') },
        ],
        filteredProducts: [],
    },
    reducers: {
        setSearchTerm(state, action) {
            state.searchTerm = action.payload;
            state.filteredProducts = filterProducts(state.products, state.searchTerm);
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

export const { setSearchTerm } = catalogSlice.actions;

export const selectCatalog = (state) => state.catalog;

export default catalogSlice.reducer;