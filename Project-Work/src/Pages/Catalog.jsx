import React, { useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { selectCatalog, setSearchTerm } from '../Store/catalogSlice';
import '@fortawesome/fontawesome-free/css/all.min.css';

export default function Catalog() {
  const dispatch = useDispatch();
  const { searchTerm, filteredProducts } = useSelector(selectCatalog);

  const [cartOpen, setCartOpen] = useState(false);
  const toggleCart = () => setCartOpen(!cartOpen);

  return (
    <div>
      <div className="relative mt-6 max-w-lg mx-auto">
        <span className="absolute inset-y-0 left-0 pl-3 flex items-center">
          <svg className="h-5 w-5 text-gray-500" viewBox="0 0 24 24" fill="none">
            <path d="M21 21L15 15M17 10C17 13.866 13.866 17 10 17C6.13401 17 3 13.866 3 10C3 6.13401 6.13401 3 10 3C13.866 3 17 6.13401 17 10Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" />
          </svg>
        </span>
        <input
          className="w-full border rounded-md pl-10 pr-4 py-2 focus:border-blue-500 focus:outline-none focus:shadow-outline"
          type="text"
          placeholder="Search"
          value={searchTerm}
          onChange={e => dispatch(setSearchTerm(e.target.value))}
        />
      </div>

      <div
        className={`fixed right-0 top-16 max-w-xs w-full h-full px-6 py-4 transition duration-300 transform overflow-y-auto bg-white border-l-2 border-gray-300 rounded-lg ${cartOpen ? 'translate-x-0 ease-out' : 'translate-x-full ease-in'}`}
      >
        <div className="flex items-center justify-between">
          <h3 className="text-2xl font-medium text-gray-700">Your cart</h3>
          <button onClick={toggleCart} className="text-gray-600 focus:outline-none">
            <i className="fas fa-times h-6 w-6" />
          </button>
        </div>
        <hr className="my-3" />
        <div className="flex justify-between mt-6">
          <div className="flex">
            <img className="h-20 w-20 object-cover rounded" src="https://images.unsplash.com/photo-1593642632823-8f785ba67e45?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1189&q=80" alt="" />
            <div className="mx-3">
              <h3 className="text-sm text-gray-600">Mac Book Pro</h3>
              <div className="flex items-center mt-2">
                <button className="text-gray-500 focus:outline-none focus:text-gray-600">
                  <i className="fas fa-minus h-5 w-5" />
                </button>
                <span className="text-gray-700 mx-2">2</span>
                <button className="text-gray-500 focus:outline-none focus:text-gray-600">
                  <i className="fas fa-plus h-5 w-5" />
                </button>
              </div>
            </div>
          </div>
          <span className="text-gray-600">20$</span>
        </div>
        <div className="mt-8">
          <form className="flex items-center justify-center">
            <input className="form-input w-48" type="text" placeholder="Add promocode" />
            <button className="ml-3 flex items-center px-3 py-2 bg-blue-600 text-white text-sm uppercase font-medium rounded hover:bg-blue-500 focus:outline-none focus:bg-blue-500">
              <span>Apply</span>
            </button>
          </form>
        </div>
        <a className="flex items-center justify-center mt-4 px-3 py-2 bg-blue-600 text-white text-sm uppercase font-medium rounded hover:bg-blue-500 focus:outline-none focus:bg-blue-500">
          <span>Checkout</span>
          <i className="fas fa-chevron-right ml-2" />
        </a>
      </div>

      <div className="mt-16 px-4">
        <h3 className="text-gray-600 text-2xl font-medium">Products</h3>
        <div className="grid gap-6 grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 mt-6">
          {filteredProducts.map((product, index) => (
            <div key={index} className="w-full max-w-sm mx-auto rounded-md shadow-md overflow-hidden">
              <div
                className="flex items-end justify-end h-56 w-full bg-cover"
                style={{ backgroundImage: `url('${product.imageUrl}')` }}
              >
                <button className="p-2 rounded-full bg-indigo-600 text-white mx-5 -mb-4 hover:bg-indigo-500 focus:outline-none focus:bg-indigo-500">
                  <svg
                    className="h-5 w-5"
                    fill="none"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                  >
                    <path d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"></path>
                  </svg>
                </button>
              </div>
              <div className="px-5 py-3">
                <h3 className="text-gray-700 uppercase">{product.name}</h3>
                <span className="text-gray-500 mt-2">{product.price}</span>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div >
  );
}