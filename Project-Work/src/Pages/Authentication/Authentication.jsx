import React, { useEffect, useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { toggleSignUp, togglePasswordVisibility, signUp, signIn, selectAuth } from '../../Store/authSlice';
import './Authentication.css';
import signUpUser from '../../Models/signUpUser';
import signInUser from '../../Models/signInUser';
import '@fortawesome/fontawesome-free/css/all.min.css';

export default function Authentication() {
    const newUser = useRef(new signUpUser());
    const loginUser = useRef(new signInUser());

    const dispatch = useDispatch();
    const { isSignUp, showPassword, errors } = useSelector(selectAuth);

    useEffect (() =>{
        console.log(errors)
    });

    return (
        <div className='login-body'>
            <div className={`container ${isSignUp ? 'active' : ''}`}>
                <div className={`panel sign-up ${isSignUp ? 'active' : ''}`}>
                    <div className="container-form">
                        <h1 className='text-5xl font-bold'>Create Account</h1>
                        <div className="social-icons">
                            <button type="button" className="icon"><i className="fa-brands fa-google-plus-g"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-facebook-f"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-github"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-linkedin-in"></i></button>
                        </div>
                        <span className="text-lg">or use your email for registration</span>
                        <input ref={(u) => (newUser.current.UserName = u)} type="text" placeholder="User name" className="input-large" />
                        <input ref={(e) => newUser.current.Email = e} type="email" placeholder="Email" className="input-large" />
                        { errors.emailError.hasError && <div className="text-red-500">{errors.emailError.message}</div>}
                        <div className="password-container">
                            <input
                                ref={(p) => newUser.current.Password = p}
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Password"
                                className="input-large"
                            />
                            <button
                                type="button"
                                className="password-toggle"
                                onClick={() => dispatch(togglePasswordVisibility())}
                            >
                                <i className={`fa ${showPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
                            </button>
                        </div>
                        <div className="password-container">
                            <input
                                ref={(cp) => newUser.current.ConfirmPassword = cp}
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Confirm Password"
                                className="input-large"
                            />
                            <button
                                type="button"
                                className="password-toggle"
                                onClick={() => dispatch(togglePasswordVisibility())}
                            >
                                <i className={`fa ${showPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
                            </button>
                        </div>
                        { errors.passwordError.hasError && <div className="text-red-500">{errors.passwordError.message}</div>}
                        <button onClick={() => dispatch(signUp({
                            UserName: newUser.current.UserName.value,
                            Email: newUser.current.Email.value,
                            Password: newUser.current.Password.value,
                            ConfirmPassword: newUser.current.ConfirmPassword.value,
                        }))} className="button-primary text-lg py-3 px-6">Sign Up</button>
                    </div>
                </div>
                <div className={`panel sign-in ${!isSignUp ? 'active' : ''}`}>
                    <div className="container-form">
                        <h1 className='text-5xl font-bold'>Sign In</h1>
                        <div className="social-icons">
                            <button type="button" className="icon"><i className="fa-brands fa-google-plus-g"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-facebook-f"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-github"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-linkedin-in"></i></button>
                        </div>
                        <span className="text-lg">or use your email and password</span>
                        <input ref={(e) => (loginUser.current.Email = e)} type="email" placeholder="Email" className="input-large" />
                        { errors.emailError.hasError && <div className="text-red-500">{errors.emailError.message}</div>}
                        <div className="password-container">
                            <input
                                ref={(p) => (loginUser.current.Password = p)}
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Password"
                                className="input-large"
                            />
                            <button
                                type="button"
                                className="password-toggle"
                                onClick={() => dispatch(togglePasswordVisibility())}
                            >
                                <i className={`fa ${showPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
                            </button>
                        </div>
                        { errors.passwordError.hasError && <div className="text-red-500">{errors.passwordError.message}</div>}
                        <button type="button" className="link-primary text-lg">Forgot Your Password?</button>
                        <button onClick={() => dispatch(signIn({
                            Email: loginUser.current.Email.value,
                            Password: loginUser.current.Password.value
                        }))}
                            className="button-primary text-lg py-3 px-6">Sign In</button>
                    </div>
                </div>
                <div className="toggle-container">
                    <div className="toggle">
                        <div className="toggle-panel toggle-left">
                            <h1 className='text-5xl font-bold'>Welcome Back!</h1>
                            <p className="text-lg">Enter your personal details to use all of site features</p>
                            <button className="hiddenBtn text-lg" onClick={() => dispatch(toggleSignUp())}>Sign In</button>
                        </div>
                        <div className="toggle-panel toggle-right">
                            <h1 className='text-5xl font-bold'>Hello, Friend!</h1>
                            <p className="text-lg">Register with your personal details to use all of site features</p>
                            <button className="hiddenBtn text-lg" onClick={() => dispatch(toggleSignUp())}>Sign Up</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}