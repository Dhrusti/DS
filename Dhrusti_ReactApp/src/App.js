// import '../node_modules/bootstrap/dist/css/bootstrap.min.css'
// import '../node_modules/bootstrap/dist/js/bootstrap.bundle'

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import './App.css';
import 'react-toastify/dist/ReactToastify.css';
import { Routes, Route } from 'react-router-dom';
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer } from 'react-toastify';
import Body from './components/body.component';
import Login from './components/login.component';
import Header from './components/header.component';
import ChartsOfAccounts from './Pages/ChartOfAccounts';
import PageNotFound from './Pages/PageNotFound';

function App() {
  const navigate = useNavigate();

  const [UserId, setUserId] = useState('');
  const [Password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [isUserLoggedIn, setUserLoggedIn] = useState(false);

  const setUserLoggedInHandler = (event) => {
    setUserId(event.userId);
    setPassword(event.password);
    setRememberMe(event.rememberMe);
    setUserLoggedIn(event.success);
  }

  useEffect(() => {
    const UserDetails = JSON.parse(sessionStorage.getItem("UserDetails"));
    if (UserDetails !== null) {
      setUserId(UserDetails.userId);
      setPassword(UserDetails.password);
      setRememberMe(UserDetails.rememberMe);
      setUserLoggedIn(UserDetails.success);
    } else {
      setUserLoggedIn(false);
      navigate("/");
    }
  }, [])

  return (
    <div className="App">
      {
        isUserLoggedIn ? <Header UserDetails={UserId} /> : <div />
      }
      <Routes>
        <Route path='*' element={<PageNotFound/>}/>
        <Route exact path="/" element={isUserLoggedIn ? <Body /> : <Login onUserLoggerdIn={setUserLoggedInHandler} />} />
        <Route path="/sign-in" element={isUserLoggedIn ? <Body /> : <Login onUserLoggerdIn={setUserLoggedInHandler} />} />
        <Route path="/dashboard" element={<Body />} />
        <Route path="/chartsOfAccounts" element={<ChartsOfAccounts />} />
      </Routes>
      <ToastContainer />
    </div>
  );
}

export default App;