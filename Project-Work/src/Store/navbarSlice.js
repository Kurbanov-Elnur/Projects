import { createSlice } from '@reduxjs/toolkit';

const navbarSlice = createSlice({
    name: 'navbar',
    initialState: {
        open: false,
        activeItem: localStorage.getItem('activeNavItem') || 'Home',
    },
    reducers: {
        toggleMenu(state) {
            state.open = !state.open;
        },
        closeMenu(state) {
            state.open = false;
        },
        setActiveItem(state, action) {
            state.activeItem = action.payload;
            localStorage.setItem('activeNavItem', action.payload);
        },
    },
});

export const { toggleMenu, closeMenu, setActiveItem } = navbarSlice.actions;
export const selectNavbar = (state) => state.navbar;

export default navbarSlice.reducer;