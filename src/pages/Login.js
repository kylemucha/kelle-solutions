// src/pages/Login.js
import React, { useState } from 'react';
import axios from 'axios'; // Import Axios
import NavBar from '../components/NavBar'; // Import your NavBar
import '../styles/Login.css'; // Import your CSS

// Import all images dynamically from the carousel folder
const importAllImages = (r) => {
  return r.keys().map(r);
};

// Load all images from ../assets/carousel
const images = importAllImages(require.context('../assets/carousel', false, /\.(png|jpe?g|svg)$/));

function Login() {
  const [userType, setUserType] = useState(null); // State to track selected user type
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isRegistering, setIsRegistering] = useState(false); // New state to toggle between login and registration

  const handleUserTypeChange = (type) => {
    setUserType(type);
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5000/api/users/login', {
        username: email, // Using 'username' as per backend requirements
        password,
      });

      // Handle successful login
      console.log('Login successful:', response.data);
      // You can store the token or redirect based on userType
    } catch (error) {
      console.error('Login failed:', error.response.data); // Log error
      alert('Login failed. Please check your credentials.');
    }
  };

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5000/api/users/register', {
        username: email, // Using 'username' as per backend requirements
        password,
        userType, // Send user type during registration
      });

      // Handle successful registration
      console.log('Registration successful:', response.data);
      // Optionally redirect or show a success message
    } catch (error) {
      console.error('Registration failed:', error.response.data); // Log error
      alert('Registration failed. Please try again.');
    }
  };

  const handleBack = () => {
    // Reset to the user type selection
    setUserType(null);
    setEmail('');
    setPassword('');
    setIsRegistering(false); // Reset registration state
  };

  return (
    <>
      <NavBar />
      <div className="login-page">
        {/* Carousel Background */}
        <div className="login-carousel-section">
          <div className="login-carousel">
            {images.map((img, index) => (
              <div key={index} className="login-carousel-item">
                <img src={img} alt={`Carousel ${index + 1}`} className="login-carousel-image" />
              </div>
            ))}
          </div>
        </div>
        
        {/* User Type Selection */}
        {!userType ? (
          <div className="user-selection">
            <h2>Login with...</h2>
            <div className="user-buttons">
              <button className="login-admin-button" onClick={() => handleUserTypeChange('Admin')}>Admin</button>
              <button className="login-broker-button" onClick={() => handleUserTypeChange('Broker')}>Broker</button>
              <button className="login-agent-button" onClick={() => handleUserTypeChange('Agent')}>Agent</button>
            </div>
          </div>
        ) : (
          <>
            <h2>{isRegistering ? 'Register' : `${userType} Login`}</h2>
            {isRegistering ? (
              <form className="login-form" onSubmit={handleRegister}>
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
                <button type="submit" className="login-button">Register</button> {/* Styled Register Button */}
                <button type="button" className="login-back-button" onClick={handleBack}>Back</button> {/* Back Button */}
              </form>
            ) : (
              <form className="login-form" onSubmit={handleLogin}>
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
                <button type="submit" className="login-button">Login</button> {/* Styled Login Button */}
                <button type="button" className="login-back-button" onClick={handleBack}>Back</button> {/* Back Button */}
                <button type="button" className="register-toggle" onClick={() => setIsRegistering(true)}>Register</button> {/* Toggle to Registration */}
              </form>
            )}
          </>
        )}
      </div>
    </>
  );
}

export default Login;
