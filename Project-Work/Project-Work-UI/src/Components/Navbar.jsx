import React from 'react';
import { useLocation } from 'react-router-dom';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTachometerAlt, faFileInvoice, faUsers, faGraduationCap } from '@fortawesome/free-solid-svg-icons';

const menuItems = [
    {
        href: "/home",
        icon: faGraduationCap,
        label: "Home",
        description: "Home page",
        count: null
    },
    {
        href: "/invoices",
        icon: faFileInvoice,
        label: "Invoices",
        description: "Manage invoices",
        count: null
    },
    {
        href: "/users",
        icon: faUsers,
        label: "Users",
        description: "Manage users",
        count: null
    }
];

const Navbar = () => {
    const location = useLocation();
    const currentPath = location.pathname;

    const isActive = (path) => currentPath === path;

    return (
        <div className="fixed top-0 left-0 h-full w-64 bg-[#303841]/60 border border-[#3ec9a7] rounded-md p-5 shadow-lg backdrop-filter backdrop-blur-sm">
            <div className="p-4 bg-gray-600/40 rounded-b-lg">
                <h1 className="font-bold text-lg lg:text-3xl bg-gradient-to-br from-gray-50 via-gray-150 to-transparent bg-clip-text text-transparent">
                    Dashboard<span className="text-[#3ec9a7]">.</span>
                </h1>
                <p className="text-gray-400 text-sm mt-2">Welcome back,</p>
            </div>
            <a href="/" className={`flex flex-col space-y-2 md:space-y-0 md:flex-row mb-5 items-center md:space-x-2 hover:bg-[#3ec9a7]/10 group transition duration-150 ease-linear rounded-lg w-full py-3 px-2 ${isActive('/') ? 'bg-[#3ec9a7] text-gray-400' : ''}`}>
                <div>
                    <img className="rounded-full w-10 h-10 object-cover" src="https://img.freepik.com/free-photo/no-problem-concept-bearded-man-makes-okay-gesture-has-everything-control-all-fine-gesture-wears-spectacles-jumper-poses-against-pink-wall-says-i-got-this-guarantees-something_273609-42817.jpg?w=1800&t=st=1669749937~exp=1669750537~hmac=4c5ab249387d44d91df18065e1e33956daab805bee4638c7fdbf83c73d62f125" alt="" />
                </div>
                <div>
                    <p className={`font-medium leading-4 ${isActive('/') ? 'text-gray-400' : 'group-hover:text-[#3ec9a7]'}`}>Jim Smith</p>
                    <span className={`text-xs text-gray-400 ${isActive('/') ? 'text-gray-400' : ''}`}>Pantazi LLC</span>
                </div>
            </a>
            <hr className="my-2 border-gray-600" />
            <div className="flex flex-col space-y-2 my-5">
                {menuItems.map((item) => (
                    <a
                        key={item.href}
                        href={item.href}
                        className={`hover:bg-[#3ec9a7]/10 transition duration-150 ease-linear rounded-lg py-3 px-2 group ${isActive(item.href) ? 'bg-[#3ec9a7]/10 transition duration-150 ease-linear rounded-lg py-3 px-2 group' : ''}`}
                    >
                        <div className="relative flex flex-col space-y-2 md:flex-row md:space-y-0 space-x-2 items-center">
                            <div>
                                <FontAwesomeIcon
                                    icon={item.icon}
                                    className={`w-6 h-6 ${isActive(item.href) ? 'text-[#3ec9a7]' : 'group-hover:text-[#3ec9a7]'}`}
                                />
                            </div>
                            <div>
                                <p className={`font-bold text-base lg:text-lg leading-4 ${isActive(item.href) ? 'text-[#3ec9a7]' : 'group-hover:text-[#3ec9a7]'}`}>
                                    {item.label}
                                </p>
                                {item.description && (
                                    <p className={`text-gray-400 text-sm hidden md:block ${isActive(item.href) ? 'text-gray-400' : ''}`}>
                                        {item.description}
                                    </p>
                                )}
                            </div>
                            {item.count !== null && (
                                <div className={`absolute -top-3 -right-3 md:top-0 md:right-0 px-2 py-1.5 rounded-full bg-[#3ec9a7] text-xs font-mono font-bold ${isActive(item.href) ? 'text-gray-400' : ''}`}>
                                    {item.count}
                                </div>
                            )}
                        </div>
                    </a>
                ))}
            </div>
        </div>
    );
};

export default Navbar;