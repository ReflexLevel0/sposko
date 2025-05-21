import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext } from "../context/AuthContext";
import "./Navbar.css"; // Plain CSS

const Navbar = () => {
  const { user, logout } = useContext(AuthContext);

  // Logo (besplatni SVG)
  const logo = (
    <img src="https://cdn.jsdelivr.net/gh/twitter/twemoji@14.0.2/assets/svg/26bd.svg" alt="logo" style={{ height: 32 }} />
  );

  return (
    <nav className="navbar">
      <div className="navbar-left">{logo}</div>
      <div className="navbar-right">
        <Link to="/">Naslovnica</Link>
        {!user && (
          <>
            <Link to="/groups">Slobodne grupe</Link>
            <Link to="/login">Prijava</Link>
            <Link to="/register">Registracija</Link>
          </>
        )}
        {user && user.role === "parent" && (
          <>
            <Link to="/groups">Slobodne grupe</Link>
            <Link to="/my-trainings">Moji treninzi</Link>
            <Link to="/profile">Moj profil</Link>
            <button onClick={logout}>Odjava</button>
          </>
        )}
        {user && user.role === "trainer" && (
          <>
            <Link to="/create-group">Kreiraj grupu</Link>
            <Link to="/my-trainings">Moji treninzi</Link>
            <Link to="/profile">Moj profil</Link>
            <button onClick={logout}>Odjava</button>
          </>
        )}
        {user && user.role === "admin" && (
          <>
            <Link to="/approve-trainers">Odobri prijavu</Link>
            <button onClick={logout}>Odjava</button>
          </>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
