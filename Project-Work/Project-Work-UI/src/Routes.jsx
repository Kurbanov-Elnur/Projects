import App from "./App";
import Home from "./Pages/Home";
import Forgot from "./Components/Forgot"
import Auth from "./Pages/Auth";
import SignIn from "./Components/SignIn";

const authChildren = [
    {
        index: true,
        element: <SignIn />
    },
    {
        path: "signin",
        element: <SignIn />
    },
    {
        path: "forgot",
        element: <Forgot />
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
        name: "Home",
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