import React from 'react'

import backgroundImage from '../Assets/signIn-background-img.jpg'
import { Outlet } from 'react-router-dom'

export default function Auth() {
    return (
        <div
            className='text-white h-[100vh] w-[100vw] flex justify-center items-center bg-cover bg-center'
            style={{
                backgroundImage: `url(${backgroundImage})`,
                backgroundSize: '100% 100%',
                backgroundPosition: 'center'
            }}
        >
            <Outlet />
        </div>
    )
}