import App from "./App";
import Catalog from "./Pages/Catalog";
import Home from "./Pages/Home";
import Authentication from "./Pages/Authentication/Authentication";
import AboutUs from "./Pages/AboutUs";
import News from "./Pages/News";
import { faNewspaper } from '@fortawesome/free-solid-svg-icons';

const moreItems = [
    {
        path: "news",
        title: 'News',
        description: "Stay updated with the latest news",
        icon: faNewspaper,
        element: <News/>
    },
];

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
        path: "more",
        children: moreItems,
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