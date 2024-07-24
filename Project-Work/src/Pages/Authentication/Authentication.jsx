import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { toggleSignUp, togglePasswordVisibility, selectAuth } from '../../Store/authSlice';
import './Authentication.css';
import '@fortawesome/fontawesome-free/css/all.min.css';

export default function Authentication() {
    const dispatch = useDispatch();
    const { isSignUp, showPassword } = useSelector(selectAuth);

    return (
        <div className='login-body'>
            <div className={`container ${isSignUp ? 'active' : ''}`}>
                <div className={`panel sign-up ${isSignUp ? 'active' : ''}`}>
                    <form className="container-form">
                        <h1 className='text-4xl font-bold'>Create Account</h1>
                        <div className="social-icons">
                            <button type="button" className="icon"><i className="fa-brands fa-google-plus-g"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-facebook-f"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-github"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-linkedin-in"></i></button>
                        </div>
                        <span>or use your email for registration</span>
                        <input type="text" placeholder="Name" />
                        <input type="email" placeholder="Email" />
                        <div className="password-container">
                            <input
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Password"
                            />
                            <button
                                type="button"
                                className="password-toggle"
                                onClick={() => dispatch(togglePasswordVisibility())}
                            >
                                <i className={`fa ${showPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
                            </button>
                        </div>
                        <button type="button" className="button-primary">Sign Up</button>
                    </form>
                </div>
                <div className={`panel sign-in ${!isSignUp ? 'active' : ''}`}>
                    <form className="container-form">
                        <h1 className='text-4xl font-bold'>Sign In</h1>
                        <div className="social-icons">
                            <button type="button" className="icon"><i className="fa-brands fa-google-plus-g"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-facebook-f"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-github"></i></button>
                            <button type="button" className="icon"><i className="fa-brands fa-linkedin-in"></i></button>
                        </div>
                        <span>or use your email and password</span>
                        <input type="email" placeholder="Email" />
                        <div className="password-container">
                            <input
                                type={showPassword ? 'text' : 'password'}
                                placeholder="Password"
                            />
                            <button
                                type="button"
                                className="password-toggle"
                                onClick={() => dispatch(togglePasswordVisibility())}
                            >
                                <i className={`fa ${showPassword ? 'fa-eye-slash' : 'fa-eye'}`}></i>
                            </button>
                        </div>
                        <button type="button" className="link-primary">Forgot Your Password?</button>
                        <button type="button" className="button-primary">Sign In</button>
                    </form>
                </div>
                <div className="toggle-container">
                    <div className="toggle">
                        <div className="toggle-panel toggle-left">
                            <h1 className='text-4xl font-bold'>Welcome Back!</h1>
                            <p>Enter your personal details to use all of site features</p>
                            <button className="hiddenBtn" onClick={() => dispatch(toggleSignUp())}>Sign In</button>
                        </div>
                        <div className="toggle-panel toggle-right">
                            <h1 className='text-4xl font-bold'>Hello, Friend!</h1>
                            <p>Register with your personal details to use all of site features</p>
                            <button className="hiddenBtn" onClick={() => dispatch(toggleSignUp())}>Sign Up</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}