import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface NavbarState {
  menus: {
    mobileMenu: boolean;
    moreMenu: boolean;
  };
  activeItem: string;
}

const initialState: NavbarState = {
  menus: {
    mobileMenu: false,
    moreMenu: false,
  },
  activeItem: 'home',
};

const navbarSlice = createSlice({
  name: 'navbar',
  initialState,
  reducers: {
    toggleMenu(state, action: PayloadAction<keyof NavbarState['menus']>) {
      const menuName = action.payload;
      state.menus[menuName] = !state.menus[menuName];
    },
    closeMenu(state, action: PayloadAction<keyof NavbarState['menus']>) {
      const menuName = action.payload;
      state.menus[menuName] = false;
    },
    setActiveItem(state, action: PayloadAction<string>) {
      state.activeItem = action.payload;
    },
  },
});

export const { toggleMenu, closeMenu, setActiveItem } = navbarSlice.actions;
export const selectNavbar = (state: { navbar: NavbarState }) => state.navbar;

export default navbarSlice.reducer;