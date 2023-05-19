import { Link, useNavigate } from "react-router-dom";

const Header = (props) => {
    const navigate = useNavigate();

    const onLogoutClick = () => {
        sessionStorage.removeItem("UserDetails");
        navigate("/");
        window.location.reload(true);
    }

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                <a className="navbar-brand" href="#">
                    <img src="https://getbootstrap.com/docs/5.0/assets/brand/bootstrap-logo.svg" alt="" width="30" height="24" className="d-inline-block align-text-top" />
                    React Mark_I
                </a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <Link className="nav-link" to="/dashboard">Dashboard</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/chartsOfAccounts">Charts of Accounts</Link>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="#">Vouchers</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link">Reports</a>
                        </li>
                    </ul>
                    <form className="d-flex">
                        <div className="dropdown form-control me-2">
                            <a href="#" className="d-flex align-items-center link-dark text-decoration-none dropdown-toggle" id="dropdownUser2" data-bs-toggle="dropdown" aria-expanded="true">
                                <img src="https://github.com/mdo.png" alt="" width="32" height="32" className="rounded-circle me-2" />
                                <strong>{props.UserDetails}</strong>
                            </a>
                            <ul className="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownUser2">
                                <li><a className="dropdown-item" href="#">Profile</a></li>
                                <li><a className="dropdown-item" href="#">Settings</a></li>
                                <li><hr className="dropdown-divider" /></li>
                                <li><a className="dropdown-item" onClick={onLogoutClick}>Logout</a></li>
                            </ul>
                        </div>
                    </form>
                </div>
            </div>
        </nav>
    )
}

export default Header;