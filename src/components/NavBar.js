// src/components/NavBar.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';  
import logo from '../assets/Kelle.webp';  // Import your logo
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCog, faSignInAlt } from '@fortawesome/free-solid-svg-icons';  // Icons for settings and login

function NavBar() {
  const [isOpen, setIsOpen] = useState(false);

  const toggleNav = () => {
    setIsOpen(!isOpen);
  };

  return (
    <nav className="navbar">
      <Link to="/" className="logo">
        <img src={logo} alt="Kelle Solutions Logo" />
      </Link>

      <button className="menu-toggle" onClick={toggleNav}>
        ☰ {/* Hamburger icon */}
      </button>

      <ul className={`nav-links ${isOpen ? 'open' : ''}`}>
        <li className="nav-item">
          <Link to="/" className="nav-link">Home</Link>
        </li>
        <li className="nav-item">
          <Link to="/login" className="nav-link">
            <FontAwesomeIcon icon={faSignInAlt} /> Login {/* Login Link */}
          </Link>
        </li>
        <li className="nav-item">
          <Link to="/settings" className="nav-link">
            <FontAwesomeIcon icon={faCog} /> Settings {/* Settings Link */}
          </Link>
        </li>
      </ul>
    </nav>
  );
}

export default NavBar;
