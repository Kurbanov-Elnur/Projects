import './App.css';
import { Outlet } from "react-router-dom";

function App() {
  return (
    <div className="App">
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