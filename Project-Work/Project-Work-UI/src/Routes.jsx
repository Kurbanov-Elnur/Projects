import App from "./App";
import Home from "./Pages/Home";
import Register from "./Components/Register"
import Auth from "./Pages/Auth";
import Login from "./Components/Login";

const authChildren = [
    {
        index: true,
        element: <Login />
    },
    {
        path: "login",
        element: <Login />
    },
    {
        path: "register",
        element: <Register />
    }
];

const appRoutes = [
    {
        path: "/",
        element: <Auth />,
        children: authChildren,
    },
    {
        path: "home",
        element: <Home />,
    },   
    {
        path: "auth",
        element: <Auth />,
        children: authChildren,
    },
];

const app = [
    {
        element: <App />,
        children: appRoutes
    }
];

export default app;