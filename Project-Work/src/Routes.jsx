import App from "./App";
import Catalog from "./Components/Catalog";
import Home from "./Components/Home";

const routes = [
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
    children: routes
}];

export default app;