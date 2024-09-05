import React, { useRef } from 'react';
import { Link } from 'react-router-dom';
import { BiUser } from 'react-icons/bi';
import { AiOutlineUnlock } from 'react-icons/ai';
import { useDispatch, useSelector } from 'react-redux';
import { forgotPassword, returnToSignIn, handleCodeChange, selectForgot } from '../Store/forgotSlice';
import emailVerifyData from "../Models/emailVerifyData";

export default function Forgot() {
    const dispatch = useDispatch();
    const { codeSent, verificationCode, isVerified } = useSelector(selectForgot);

    const verifyData = useRef(new emailVerifyData());

    return (
        <div className='bg-[#1c3d5a] border border-[#3ec9a7] rounded-md p-8 shadow-lg backdrop-filter backdrop-blur-sm bg-opacity-30 relative'>
            <h1 className='text-4xl font-bold text-white text-center mb-6'>Forgot password</h1>

            {!isVerified && (
                <div>
                    <div className='relative my-4'>
                        <input
                            ref={(e) => verifyData.current.Email = e}
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

                    {codeSent && (
                        <div className="flex justify-between items-center mt-6">
                            {verificationCode.map((digit, index) => (
                                <input
                                    key={index}
                                    type="text"
                                    maxLength="1"
                                    value={digit}
                                    onChange={(e) => dispatch(handleCodeChange({
                                        index: index,
                                        value: e.target.value
                                    }))}
                                    className='block w-12 h-12 text-center text-lg text-white bg-transparent border-2 border-[#3ec9a7] rounded-md focus:outline-none focus:ring-0 focus:border-[#ffcb42]'
                                />
                            ))}
                        </div>
                    )}
                </div>
            )}

            {isVerified && (
                <div>
                    <div className='relative my-4'>
                        <input
                            ref={(p) => (verifyData.current.Password = p)}
                            type="password"
                            className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-[#3ec9a7] appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-[#ffcb42] peer'
                            placeholder=''
                        />
                        <label
                            htmlFor=""
                            className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-[#ffcb42] peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                            New Password
                        </label>

                        <AiOutlineUnlock className='absolute top-4 right-4 text-[#ffcb42]' />
                    </div>
                    <div className='relative my-4'>
                        <input
                            ref={(p) => (verifyData.current.ConfirmPassword = p)}
                            type="password"
                            className='block w-72 py-2.5 px-0 text-sm text-white bg-transparent border-0 border-b-2 border-[#3ec9a7] appearance-none focus:outline-none focus:ring-0 focus:text-white focus:border-[#ffcb42] peer'
                            placeholder=''
                        />
                        <label
                            htmlFor=""
                            className='absolute text-sm text-white duration-300 transform -translate-y-6 scale-75 top-3 -z-10 origin-[0] peer-focus:left-0 peer-focus:text-[#ffcb42] peer-placeholder-shown:scale-100 peer-placeholder-shown:translate-y-0 peer-focus:scale-75 peer-focus:-translate-y-6'>
                            Confirm Password
                        </label>

                        <AiOutlineUnlock className='absolute top-4 right-4 text-[#ffcb42]' />
                    </div>
                </div>
            )}

            <button
                className='w-full mb-4 text-[18px] mt-6 rounded-full bg-[#ffcb42] text-[#1c3d5a] border border-[#ffcb42] hover:bg-[#e0e4e8] py-2 transition-colors duration-300'
                onClick={() => dispatch(forgotPassword({ email: verifyData.current.Email.value }))}
            >
                {isVerified ? 'Confirm' : codeSent ? 'Verify' : 'Send verification code'}
            </button>

            <div className='flex justify-center'>
                <span className='m-4'>
                    <Link onClick={() => dispatch(returnToSignIn())} className="text-[#e0e4e8] hover:text-white transition-colors duration-300" to='/signin'>Sign In</Link>
                </span>
            </div>
        </div>
    );
}