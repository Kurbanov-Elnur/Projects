import './App.css';
import Navbar from './Components/Navbar';
import { Outlet } from "react-router-dom";

function App() {
  return (
    <div className="App">
      <header>
        <Navbar />
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