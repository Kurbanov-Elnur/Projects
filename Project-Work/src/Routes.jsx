import App from "./App";
import Catalog from "./Pages/Catalog";
import Home from "./Pages/Home";
import Authentication from "./Pages/Authentication/Authentication";
import AboutUs from "./Pages/AboutUs";

const appRoutes = [
    {
        path: "/",
        element: <Home />,
    },
    {
        path: "home",
        element: <Home />,
    },
    {
        path: "catalog",
        element: <Catalog />,
    },
    {
        path: "aboutus",
        element: <AboutUs />,
    },
    {
        path: "auth",
        element: <Authentication /> 
    },
];

const app = [
    {
        element: <App />,
        children: appRoutes
    }
];

export default app;