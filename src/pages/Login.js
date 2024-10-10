import React, { useState } from 'react';
import NavBar from '../components/NavBar'; // Import your NavBar
import '../styles/Login.css'; // Import your CSS

// Import all images dynamically from the carousel folder
const importAllImages = (r) => {
  return r.keys().map(r);
};

// Load all images from ../assets/carousel
const images = importAllImages(require.context('../assets/carousel', false, /\.(png|jpe?g|svg)$/));

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = (e) => {
    e.preventDefault();
    console.log('Login:', { email, password });
  };

  return (
    <>
      <NavBar />
      <div className="login-page">
        {/* Carousel Background */}
        <div className="carousel-section">
          <div className="carousel">
            {images.map((img, index) => (
              <div key={index} className="carousel-item">
                <img src={img} alt={`Carousel ${index + 1}`} className="carousel-image" />
              </div>
            ))}
          </div>
        </div>
        {/* Overlay Login Form */}
        <form className="login-form" onSubmit={handleLogin}>
          <h2>Login</h2>
          <div className="form-group">
            <label>Email:</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div className="form-group">
            <label>Password:</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <button type="submit">Login</button>
        </form>
      </div>
    </>
  );
}

export default Login;
