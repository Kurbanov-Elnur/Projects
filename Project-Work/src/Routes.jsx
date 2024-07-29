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
        name: "Home",
        element: <Home />,
    },
    {
        path: "catalog",
        name: "Catalog",
        element: <Catalog />,
    },
    {
        path: "aboutus",
        name: "About Us",
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