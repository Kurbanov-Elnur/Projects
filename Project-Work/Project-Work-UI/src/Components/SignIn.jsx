import React, { useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { BiUser } from 'react-icons/bi';
import { AiOutlineUnlock } from 'react-icons/ai';
import { FaCheck } from 'react-icons/fa';

import { selectAuth, signIn } from '../Store/authSlice';
import signInUser from "../Models/signInUser"

export default function SignIn() {
    const dispatch = useDispatch();

    const user = useRef(new signInUser());
    const { errors } = useSelector(selectAuth);

    return (
        <div className='bg-[#1c3d5a] border border-[#3ec9a7] rounded-md p-8 shadow-lg backdrop-filter backdrop-blur-sm bg-opacity-30 relative'>
            <h1 className='text-4xl font-bold text-white text-center mb-6'>Sign In</h1>

            <div className='relative my-4'>
                <input
                    ref={(e) => (user.current.Email = e)}
                    type="email"
                    className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-[#3ec9a7] appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-[#ffcb42] peer'
                    placeholder=''
                />
                <label
                    htmlFor=""
                    className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-[#ffcb42] peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                    Your Email
                </label>

                <BiUser className='absolute top-4 right-4 text-[#ffcb42]' />
            </div>

            <div className='relative my-4'>
                <input
                    ref={(p) => (user.current.Password = p)}
                    type="password"
                    className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-[#3ec9a7] appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-[#ffcb42] peer'
                    placeholder=''
                />
                <label
                    htmlFor=""
                    className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-[#ffcb42] peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                    Your Password
                </label>

                <AiOutlineUnlock className='absolute top-4 right-4 text-[#ffcb42]' />
            </div>

            <div className='flex justify-between items-center'>
                <div className='relative flex gap-2 items-center'>
                    <input
                        type="checkbox"
                        id="rememberMe"
                        className='appearance-none w-5 h-5 border-2 border-[#3ec9a7] rounded-sm checked:bg-[#3ec9a7] checked:border-[#3ec9a7] relative'
                    />
                    <label
                        htmlFor="rememberMe"
                        className='text-white flex items-center relative'>
                        <FaCheck className='absolute top-0 left-0 w-5 h-5 text-white hidden checked:block' />
                        Remember Me
                    </label>
                </div>

                <span className='text-[#e0e4e8] cursor-pointer hover:text-white transition-colors duration-300'><Link to={"/forgot"}>Forgot Password?</Link></span>
            </div>

            <button onClick={() => dispatch(signIn({
                Email: user.current.Email.value,
                Password: user.current.Password.value
            }))}
                className='w-full mb-4 text-[18px] mt-6 rounded-full bg-[#ffcb42] text-[#1c3d5a] border border-[#ffcb42] hover:bg-[#e0e4e8] py-2 transition-colors duration-300'>
                Sign In
            </button>
        </div>
    );
}