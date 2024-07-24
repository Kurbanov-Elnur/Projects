import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import './index.css';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Routes from "./Routes"
import store from './Store/store'

const router = createBrowserRouter(Routes);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <Provider store={store}>
        <RouterProvider router={router} />
    </Provider>
);