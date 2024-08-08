import React from 'react';
import { Link } from 'react-router-dom';
import { BiUser } from 'react-icons/bi';
import { AiOutlineUnlock } from 'react-icons/ai';

export default function Register() {
    return (
        <div className='bg-slate-800 border border-slate-400 rounded-md p-8 shadow-lg backdrop-filter backdrop-blur-sm bg-opacity-30 relative'>
            <h1 className='text-4xl font-bold text-white text-center mb-6'>Register</h1>

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
                    Password
                </label>

                <AiOutlineUnlock className='absolute top-4 right-4' />
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
                    Confirm Password
                </label>

                <AiOutlineUnlock className='absolute top-4 right-4' />
            </div>

            <button className='w-full mb-4 text-[18px] mt-6 rounded-full bg-gray-800 text-white border border-gray-800 hover:border-gray-700 py-2 transition-colors duration-300'>
                Register
            </button>

            <div className='flex justify-center'>
                <span className='m-4'>Already Created An Account? <Link className="text-gray-800 hover:text-gray-100 transition-colors duration-300" to='/login'>Login</Link></span>
            </div>
        </div>
    );
}