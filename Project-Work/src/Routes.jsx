import App from "./App";
import Catalog from "./Pages/Catalog";
import Home from "./Pages/Home";

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
    }
];

const app = [
    {
        element: <App />,
        children: appRoutes
    }
];

export default app;