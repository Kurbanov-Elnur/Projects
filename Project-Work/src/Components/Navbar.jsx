import React, { useEffect, useRef } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useLocation } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { toggleMenu, closeMenu, setActiveItem, selectNavbar } from '../Store/navbarSlice';
import NavItems from "../Routes";

export default function Navbar() {
    const dispatch = useDispatch();
    const location = useLocation();

    const { menus, activeItem } = useSelector(selectNavbar);
    const moreMenuRef = useRef(null);

    const navItems = NavItems[0].children.filter(item => item.name);
    const moreItems = NavItems[0].children.find(item => item.path === "more").children;

    useEffect(() => {
        const handleClickOutside = (event) => {
            if (moreMenuRef.current && !moreMenuRef.current.contains(event.target)) {
                if (menus.moreMenu) {
                    dispatch(closeMenu('moreMenu'));
                }
            }
        };

        if (menus.moreMenu) {
            document.addEventListener('click', handleClickOutside);
        }

        return () => {
            document.removeEventListener('click', handleClickOutside);
        };
    }, [dispatch, menus.moreMenu]);

    useEffect(() => {
        const currentPath = location.pathname;
        const pathToActiveItem = currentPath.replace('/', '') || 'home';

        if (activeItem !== pathToActiveItem) {
            dispatch(setActiveItem(pathToActiveItem));
        }
    }, [dispatch, location.pathname, activeItem]);

    return (
        <div className="fixed top-0 left-0 w-full z-50">
            <div className="bg-teal-50 shadow-md dark:bg-teal-900 dark:shadow-lg">
                <div className="w-full text-gray-700 dark:text-gray-200">
                    <div className="flex flex-col max-w-screen-xl px-4 mx-auto md:items-center md:justify-between md:flex-row md:px-6 lg:px-8">
                        <div className="flex flex-row items-center justify-between p-4">
                            <span className="text-lg font-semibold tracking-widest text-teal-900 uppercase rounded-lg dark:text-white focus:outline-none focus:shadow-outline">
                                Project Work
                            </span>
                            <button
                                className="rounded-lg md:hidden focus:outline-none focus:shadow-outline"
                                onClick={() => dispatch(toggleMenu('mobileMenu'))}
                            >
                                <svg fill="currentColor" viewBox="0 0 20 20" className="w-6 h-6">
                                    <path
                                        fillRule="evenodd"
                                        d={menus.mobileMenu
                                            ? "M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z"
                                            : "M3 5a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 10a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM9 15a1 1 0 011-1h6a1 1 0 110 2h-6a1 1 0 01-1-1z"}
                                        clipRule="evenodd"
                                    />
                                </svg>
                            </button>
                        </div>
                        <nav className={`flex-col flex-grow pb-4 md:pb-0 md:flex md:justify-end md:flex-row ${menus.mobileMenu ? 'flex' : 'hidden'}`}>
                            {navItems.map((item, index) => (
                                <Link to={`/${item.path.toLowerCase()}`} key={index}>
                                    <button
                                        style={{ textTransform: "capitalize" }}
                                        className={`cursor-pointer px-4 py-2 mt-2 text-sm font-semibold rounded-lg md:mt-0 md:ml-4 focus:outline-none focus:shadow-outline 
                                        ${activeItem === item.path ? 'bg-teal-100 text-teal-900 dark:bg-teal-600 dark:text-white' : 'bg-transparent text-gray-700 dark:text-gray-200 hover:text-gray-900 focus:text-gray-900 hover:bg-teal-50 focus:bg-teal-50 dark:hover:bg-teal-700 dark:focus:bg-teal-700 dark:hover:text-white dark:focus:text-white'}`}
                                        onClick={() => dispatch(setActiveItem(item.path))}
                                    >
                                        {item.name}
                                    </button>
                                </Link>
                            ))}
                            <div className="relative" ref={moreMenuRef}>
                                <button
                                    onClick={() => dispatch(toggleMenu('moreMenu'))}
                                    className="flex flex-row items-center px-4 py-2 mt-2 text-sm font-semibold rounded-lg md:w-auto md:inline md:mt-0 md:ml-4 focus:outline-none focus:shadow-outline 
                                    bg-transparent text-gray-700 dark:text-gray-200 hover:text-gray-900 focus:text-gray-900 hover:bg-teal-50 focus:bg-teal-50 dark:hover:bg-teal-700 dark:focus:bg-teal-700 dark:hover:text-white dark:focus:text-white"
                                >
                                    <span>More</span>
                                    <svg
                                        fill="currentColor"
                                        viewBox="0 0 20 20"
                                        className={`inline w-4 h-4 mt-1 ml-1 transition-transform duration-200 transform md:-mt-1 ${menus.moreMenu ? 'rotate-180' : 'rotate-0'}`}
                                    >
                                        <path
                                            fillRule="evenodd"
                                            d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                            clipRule="evenodd"
                                        />
                                    </svg>
                                </button>
                                {menus.moreMenu && (
                                    <div className="absolute right-0 w-full md:max-w-screen-sm md:w-screen mt-2 origin-top-right z-500">
                                        <div className="px-2 pt-2 pb-4 bg-teal-50 rounded-md shadow-lg dark:bg-teal-900">
                                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                                {moreItems.map(({ title, description, path, icon }) => (
                                                    <Link key={title} to={`more/${path}`}>
                                                        <button
                                                            className="flex flex-row items-start rounded-lg bg-transparent p-2 dark:hover:bg-teal-700 dark:focus:bg-teal-700 dark:focus:text-white dark:hover:text-white dark:text-gray-200 hover:text-gray-900 focus:text-gray-900 hover:bg-teal-50 focus:bg-teal-50 focus:outline-none focus:shadow-outline cursor-pointer"
                                                        >
                                                            <div className="bg-teal-500 text-white rounded-lg p-3">
                                                                <FontAwesomeIcon icon={icon} className="md:h-6 md:w-6 h-4 w-4" />
                                                            </div>
                                                            <div className="ml-3">
                                                                <p className="font-semibold">{title}</p>
                                                                <p className="text-sm">{description}</p>
                                                            </div>
                                                        </button>
                                                    </Link>
                                                ))}
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </div>

                            {menus.mobileMenu && (
                                <div className="flex flex-row items-center justify-end mt-4 space-x-4">
                                    <Link to={'/auth'}>
                                        <button
                                            onClick={() => dispatch(setActiveItem('auth'))}
                                            className={`p-2 rounded-full text-xl ${activeItem === 'auth' ? 'bg-teal-100 text-teal-900 dark:bg-teal-600 dark:text-white' : 'text-gray-700 dark:text-gray-200 hover:text-gray-900 focus:text-gray-900 hover:bg-teal-50 focus:bg-teal-50 dark:hover:bg-teal-700 dark:focus:bg-teal-700 dark:hover:text-white dark:focus:text-white'}`}
                                        >
                                            <FontAwesomeIcon icon={faUserCircle} />
                                        </button>
                                    </Link>
                                </div>
                            )}
                        </nav>
                        <div className="absolute right-0 mr-5 p-5 hidden md:flex space-x-4">
                            <Link to={'/auth'}>
                                <button
                                    onClick={() => dispatch(setActiveItem('auth'))}
                                    className={`p-2 rounded-full text-xl ${activeItem === 'auth' ? 'bg-teal-100 text-teal-900 dark:bg-teal-600 dark:text-white' : 'text-gray-700 dark:text-gray-200 hover:text-gray-900 focus:text-gray-900 hover:bg-teal-50 focus:bg-teal-50 dark:hover:bg-teal-700 dark:focus:bg-teal-700 dark:hover:text-white dark:focus:text-white'}`}
                                >
                                    <FontAwesomeIcon icon={faUserCircle} />
                                </button>
                            </Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}