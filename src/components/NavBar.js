// src/components/NavBar.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';  // Import the CSS file for styling
import logo from '../assets/Kelle.webp';  // Import your logo
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCog, faSignInAlt } from '@fortawesome/free-solid-svg-icons';  // FontAwesome icons for settings, login

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

      {/* Hamburger menu button */}
      <button className="menu-toggle" onClick={toggleNav}>
        ☰ {/* Hamburger icon */}
      </button>

      {/* Collapsible nav links */}
      <ul className={`nav-links ${isOpen ? 'open' : ''}`}>
        <li className="nav-item">
          <Link to="/" className="nav-link">Home</Link>
        </li>
        <li className="nav-item">
          <Link to="/about" className="nav-link">About</Link>
        </li>
        <li className="nav-item">
          <Link to="/contact" className="nav-link">Contact</Link>
        </li>

        {/* Login button */}
        <li className="nav-item">
          <Link to="/login" className="nav-link">
            <FontAwesomeIcon icon={faSignInAlt} /> Login
          </Link>
        </li>

        {/* Settings cog */}
        <li className="nav-item">
          <Link to="/settings" className="nav-link">
            <FontAwesomeIcon icon={faCog} /> Settings
          </Link>
        </li>
      </ul>
    </nav>
  );
}

export default NavBar;
