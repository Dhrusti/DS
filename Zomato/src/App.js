import './App.css';
import { Route, Routes } from 'react-router-dom';
import Restaurant from './Components/Restaurant/Restaurant';
import Main from './Components/Main/Main';
import Menu from './Components/Restaurant/Menu/Menu';
import OrderDone from './Components/Restaurant/OrderDone/OrderDone';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<Main />} />
        <Route path="/Restaurant" element={<Restaurant />} />
        <Route path="/menu" element={<Menu />} />
        <Route path="/done" element={<OrderDone />} />
      </Routes>
      </div>
  );
}

export default App;
