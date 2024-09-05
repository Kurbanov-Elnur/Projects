import './App.css';
import { Outlet } from "react-router-dom";

function App() {
  return (
    <div
      className='App text-white h-[100vh] w-full flex justify-center items-center bg-cover bg-center'
    >
      <header>
      </header>
      <main>
        <Outlet />
      </main>
      <footer>
      </footer>
    </div>
  );
}

export default App;