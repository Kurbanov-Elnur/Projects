'use client'

import { useDispatch, useSelector } from 'react-redux';
import * as authSlice from '../../Store/Reducers/authSlice';
import './Authentication.css';
import { AppDispatch, RootState } from '../../Store/store';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faGooglePlusG, faFacebookF, faGithub, faLinkedinIn } from '@fortawesome/free-brands-svg-icons';
import { useAuth } from '@/Hooks/useAuthHooks';

export default function Authentication() {
  const dispatch: AppDispatch = useDispatch();
  const authState = useSelector((state: RootState) => (state.auth));
  const authHook = useAuth();

  return (
    <div className='login-body'>
      <div className={`container ${authState.IsRegister ? 'active' : ''}`}>
        <div className={`panel sign-up ${authState.IsRegister ? 'active' : ''}`}>
          <div className="container-form">
            <h1 className='text-5xl font-bold'>Create Account</h1>
            <div className="social-icons">
              <button type="button" className="icon"><FontAwesomeIcon icon={faGooglePlusG}></FontAwesomeIcon></button>
              <button type="button" className="icon"><FontAwesomeIcon icon={faFacebookF}></FontAwesomeIcon></button>
              <button type="button" className="icon"><FontAwesomeIcon icon={faGithub}></FontAwesomeIcon></button>
              <button type="button" className="icon"><FontAwesomeIcon icon={faLinkedinIn}></FontAwesomeIcon></button>
            </div>
            <span className="text-lg">or use your email for registration</span>
            <input onChange={(e) => dispatch(authSlice.setRegisterData({
              ...authState.RegisterData, Username: e.target.value
            }))} type="text" placeholder="User name" className="input-large" />
            <input onChange={(e) => dispatch(authSlice.setRegisterData({
              ...authState.RegisterData, Email: e.target.value
            }))} type="email" placeholder="Email" className="input-large" />
            <div className="password-container">
              <input
                onChange={(e) => dispatch(authSlice.setRegisterData({
                  ...authState.RegisterData, Password: e.target.value
                }))}
                type={authState.ShowPassword ? 'text' : 'password'}
                placeholder="Password"
                className="input-large"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => dispatch(authSlice.setShowPassword(!authState.ShowPassword))}
              >
                <i className={`fa ${authState.ShowPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
              </button>
            </div>
            <div className="password-container">
              <input
                onChange={(e) => dispatch(authSlice.setRegisterData({
                  ...authState.RegisterData, ConfirmPassword: e.target.value
                }))}
                type={authState.ShowPassword ? 'text' : 'password'}
                placeholder="Confirm Password"
                className="input-large"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => dispatch(authSlice.setShowPassword(!authState.ShowPassword))}
              >
                <i className={`fa ${authState.ShowPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
              </button>
            </div>
            <button className="button-primary text-lg py-3 px-6" onClick={() => {authHook.Register(authState.RegisterData)}}>Sign Up</button>
          </div>
        </div>
        <div className={`panel sign-in ${!authState.IsRegister ? 'active' : ''}`}>
          <div className="container-form">
            <h1 className='text-5xl font-bold'>Sign In</h1>
            <div className="social-icons">
              <button type="button" className="icon"><i className="fa-brands fa-google-plus-g"></i></button>
              <button type="button" className="icon"><i className="fa-brands fa-facebook-f"></i></button>
              <button type="button" className="icon"><i className="fa-brands fa-github"></i></button>
              <button type="button" className="icon"><i className="fa-brands fa-linkedin-in"></i></button>
            </div>
            <span className="text-lg">or use your email and password</span>
            <input
              onChange={(e) => dispatch(authSlice.setLoginData({
                ...authState.LoginData, Username: e.target.value
              }))}
              type="email"
              placeholder="Username"
              className="input-large" />
            <div className="password-container">
              <input
                onChange={(e) => dispatch(authSlice.setLoginData({
                  ...authState.LoginData, Password: e.target.value
                }))}
                type={authState.ShowPassword ? 'text' : 'password'}
                placeholder="Password"
                className="input-large"
              />
              <button
                type="button"
                className="password-toggle"
                onClick={() => dispatch(authSlice.setShowPassword(!authState.ShowPassword))}
              >
                <i className={`fa ${authState.ShowPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
              </button>
            </div>
            <button type="button" className="link-primary text-lg">Forgot Your Password?</button>
            <button className="button-primary text-lg py-3 px-6" onClick={() => {authHook.Login(authState.LoginData)}}>Sign In</button>
          </div>
        </div>
        <div className="toggle-container">
          <div className="toggle">
            <div className="toggle-panel toggle-left">
              <h1 className='text-5xl font-bold'>Welcome Back!</h1>
              <p className="text-lg">Enter your personal details to use all of site features</p>
              <button className="hiddenBtn text-lg" onClick={() => dispatch(authSlice.setIsRegister(false))}>Sign In</button>
            </div>
            <div className="toggle-panel toggle-right">
              <h1 className='text-5xl font-bold'>Hello, Friend!</h1>
              <p className="text-lg">Register with your personal details to use all of site features</p>
              <button className="hiddenBtn text-lg" onClick={() => dispatch(authSlice.setIsRegister(true))}>Sign Up</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}