// src/components/NavBar.js
import React from 'react';
import { Link } from 'react-router-dom';
import './NavBar.css';  // Import the CSS file for styling
import logo from '../assets/Kelle.webp';  // Import the Kelle.webp logo

function NavBar() {
  return (
    <nav className="navbar">
      <Link to="/" className="logo">
        <img src={logo} alt="Kelle Solutions Logo" />
      </Link>
      <ul className="nav-links">
        <li className="nav-item">
          <Link to="/" className="nav-link">Home</Link>
        </li>
        <li className="nav-item">
          <Link to="/about" className="nav-link">About</Link>
        </li>
        <li className="nav-item">
          <Link to="/contact" className="nav-link">Contact</Link>
        </li>
      </ul>
    </nav>
  );
}

export default NavBar;
