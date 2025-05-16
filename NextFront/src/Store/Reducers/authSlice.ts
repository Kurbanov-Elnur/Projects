import { AuthData } from '@/Data/Models/Auth.model';
import { LoginDTO, RegisterDTO } from '@/Data/DTOs/Auth.DTO';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { LoginUser } from '@/Lib/Actions/authActions';
import { toast } from 'react-hot-toast';

const initialState: AuthData = {
    LoginData: {
        Username: '',
        Password: '',
    },
    RegisterData: {
        Name: 'Elnur',
        Surname: 'Mamedov',
        Username: '',
        Email: '',
        Password: '',
        ConfirmPassword: '',
    },
    IsRegister: false,
    IsLoading: false,
    ShowPassword: false,
};

const authSlice = createSlice({
    name: `auth`,
    initialState,
    reducers: {
        setLoginData: (state, action: PayloadAction<LoginDTO>) => {
            state.LoginData = action.payload;
        },
        setRegisterData: (state, action: PayloadAction<RegisterDTO>) => {
            state.RegisterData = action.payload;
        },
        setIsRegister: (state, action: PayloadAction<boolean>) => {
            state.IsRegister = action.payload;
        },
        setIsLoading: (state, action: PayloadAction<boolean>) => {
            state.IsLoading = action.payload;
        },
        setShowPassword: (state, action: PayloadAction<boolean>) => {
            state.ShowPassword = action.payload;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(LoginUser.pending, (state) => {
                state.IsLoading = true;
            })
            .addCase(LoginUser.fulfilled, (state, action: any) => {
                state.IsLoading = false;
                localStorage.setItem('userName', action.payload.userName);

                toast.success(`${localStorage.getItem('userName')} Sizi yenidən görməyə şadıq!`);
            })
    }
});

export const { setLoginData, setRegisterData, setIsRegister, setIsLoading, setShowPassword } = authSlice.actions;
export default authSlice.reducer;