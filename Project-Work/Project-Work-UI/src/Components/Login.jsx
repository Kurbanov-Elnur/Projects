import React from 'react';
import { Link } from 'react-router-dom';
import { BiUser } from 'react-icons/bi';
import { AiOutlineUnlock } from 'react-icons/ai';
import { FaCheck } from 'react-icons/fa';

export default function Login() {
    return (
        <div className='bg-slate-800 border border-slate-400 rounded-md p-8 shadow-lg backdrop-filter backdrop-blur-sm bg-opacity-30 relative'>
            <h1 className='text-4xl font-bold text-white text-center mb-6'>Login</h1>

            <div className='relative my-4'>
                <input
                    type="email"
                    className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-gray-800 peer'
                    placeholder=''
                />
                <label
                    htmlFor=""
                    className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-gray-800 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                    Your Email
                </label>

                <BiUser className='absolute top-4 right-4' />
            </div>

            <div className='relative my-4'>
                <input
                    type="password"
                    className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-gray-300 appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-gray-800 peer'
                    placeholder=''
                />
                <label
                    htmlFor=""
                    className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-gray-800 peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                    Your Password
                </label>

                <AiOutlineUnlock className='absolute top-4 right-4' />
            </div>

            <div className='flex justify-between items-center'>
                <div className='relative flex gap-2 items-center'>
                    <input
                        type="checkbox"
                        id="rememberMe"
                        className='appearance-none w-5 h-5 border-2 border-gray-400 rounded-sm checked:bg-gray-400 checked:border-gray-400 relative'
                    />
                    <label
                        htmlFor="rememberMe"
                        className='text-white flex items-center relative'>
                        <FaCheck className='absolute top-0 left-0 w-5 h-5 text-white hidden checked:block' />
                        Remember Me
                    </label>
                </div>

                <span className='text-gray-800 cursor-pointer hover:text-gray-100 transition-colors duration-300'>Forgot Password?</span>
            </div>

            <button className='w-full mb-4 text-[18px] mt-6 rounded-full bg-gray-800 text-white border border-gray-800 hover:border-gray-700 py-2 transition-colors duration-300'>
                Login
            </button>

            <div className='flex justify-center'>
                <span className='m-4'>New Here? <Link className="text-gray-800 hover:text-gray-100 transition-colors duration-300" to='/register'>Create an Account</Link></span>
            </div>
        </div>
    );
}