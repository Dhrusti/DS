import React from "react";
import { useNavigate } from "react-router-dom";
import { useRef, useState, useEffect } from "react";
import { LoginAsync } from "../config/axiosCalls";
import { toast } from "react-toastify";

const Login = (props) => {
  // const { setAuth } = useContext(AuthContext);
  const userRef = useRef();
  const navigate = useNavigate();

  const [userId, setUser] = useState("");
  const [password, setPwd] = useState("");
  const [rememberMe, setRememberMe] = useState(true);
  const [success, setSuccess] = useState(false);

  const UserDetails = { userId, password, rememberMe, success: true };

  useEffect(() => {
    userRef.current.focus();
  }, []);

  useEffect(() => {}, [userId, password, rememberMe]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const response = await LoginAsync({ userId, password, rememberMe });

    if (response.status === 200 && response.data.status) {
      setUser("");
      setPwd("");
      setRememberMe(false);
      setSuccess(true);
      props.onUserLoggerdIn(UserDetails);
      toast(response.data.message);
      navigate("/dashboard");
      sessionStorage.setItem("UserDetails", JSON.stringify(UserDetails));
    } else if (response.status !== false) {
      toast(response.data.message);
    } else {
      toast(response.message);
    }
    // const accessToken = response?.data?.accessToken;
    // const roles = response?.data?.roles;
    // setAuth({ userId, password, roles, accessToken });
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-inner">
        {success ? (
          <section>
            <h1>You are logged in!</h1>
            <br />
            <p>
              <a href="#">Go to Home</a>
            </p>
          </section>
        ) : (
          <section>
            <form onSubmit={handleSubmit}>
              <h3>Sign In</h3>
              <div className="mb-3">
                <label>Email address</label>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Username"
                  id="username"
                  ref={userRef}
                  autoComplete="off"
                  onChange={(e) => setUser(e.target.value)}
                  value={userId}
                  required
                />
              </div>
              <div className="mb-3">
                <label>Password</label>
                <input
                  type="password"
                  className="form-control"
                  placeholder="Enter password"
                  id="password"
                  onChange={(e) => setPwd(e.target.value)}
                  value={password}
                  required
                />
              </div>
              <div className="mb-3">
                <div className="custom-control custom-checkbox">
                  <input
                    type="checkbox"
                    className="custom-control-input"
                    id="rememberMe"
                    onChange={(e) => setRememberMe(e.target.checked)}
                    checked={rememberMe}
                  />
                  <label className="custom-control-label" htmlFor="rememberMe">
                    Remember me?
                  </label>
                </div>
              </div>
              <div className="d-grid">
                <button type="submit" className="btn btn-primary">
                  Submit
                </button>
              </div>
            </form>
          </section>
        )}
      </div>
    </div>
  );
};

export default Login;
