import React from 'react'

import backgroundImage from '../Assets/background-img.jpg'
import { Outlet } from 'react-router-dom'

export default function Auth() {
    return (
        <div
            className='text-white h-[100vh] w-full flex justify-center items-center bg-cover bg-center'
            style={{
                backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2)), url(${backgroundImage})`,
                backgroundSize: '100% 100%',
                backgroundPosition: 'center'
            }}
        >
           <Outlet />
        </div>
    )
}