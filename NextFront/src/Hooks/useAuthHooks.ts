import { useDispatch, useSelector } from 'react-redux';
import * as AuthActions from '../Lib/Actions/authActions';
import { LoginDTO, RegisterDTO } from '../Data/DTOs/Auth.DTO';
import { AppDispatch } from '@/Store/store';
import { useRouter } from 'next/navigation';

export const useAuth = () => {
    const dispatch = useDispatch<AppDispatch>();
    const router = useRouter();

    const Login = async (loginData: LoginDTO) => {
        dispatch(AuthActions.LoginUser(loginData))
            .then((res) => {
                if (res.meta.requestStatus === 'fulfilled') {
                    router.push('/');
                }
            })
    };

    const Register = async (registerData: RegisterDTO) => {
        await dispatch(AuthActions.RegisterUser(registerData));
    };

    const Logout = async () => {
        await dispatch(AuthActions.Logout());
    };

    return { Login, Register, Logout };
};