import { createSlice } from '@reduxjs/toolkit';

const navbarSlice = createSlice({
    name: 'navbar',
    initialState: {
        menus: {
            mobileMenu: false,
            moreMenu: false,
        },
        activeItem: 'home',
    },
    reducers: {
        toggleMenu(state, action) {
            const menuName = action.payload;
            state.menus[menuName] = !state.menus[menuName];
        },
        closeMenu(state, action) {
            const menuName = action.payload;
            state.menus[menuName] = false;
        },
        setActiveItem(state, action) {
            state.activeItem = action.payload;
        },
    },
});



export const { toggleMenu, closeMenu, setActiveItem } = navbarSlice.actions;
export const selectNavbar = (state) => state.navbar;

export default navbarSlice.reducer;