import { LoginDTO, RegisterDTO } from "../DTOs/Auth.DTO";

export interface AuthData {
    LoginData: LoginDTO,
    RegisterData: RegisterDTO,
    IsRegister: boolean,
    IsLoading: boolean,
    ShowPassword: boolean,
}