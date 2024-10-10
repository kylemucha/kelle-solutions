// src/pages/Home.js
import React, { useEffect } from 'react';
import '../styles/Home.css';

// Dynamically load images from the folder
const importAllImages = (r) => {
  return r.keys().map(r);
};

const images = importAllImages(require.context('../assets/carousel', false, /\.(png|jpe?g|svg)$/));

function Home() {
  useEffect(() => {
    const handleScroll = () => {
      const carousel = document.querySelector('.carousel');
      const scrollPosition = window.scrollY;

      // Adjust the translation based on scroll position to create animation effect
      carousel.style.transform = `translateY(${scrollPosition * 0.2}px)`; // Adjust the multiplier for the desired effect
    };

    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return (
    <div className="home-page">
      <div className="carousel-section">
        <div className="carousel">
          {images.map((img, index) => (
            <div key={index} className="carousel-item">
              <img src={img} alt={`Carousel ${index}`} className="carousel-image" />
            </div>
          ))}
        </div>

        {/* Overlay Text on the Carousel */}
        <div className="carousel-text">
          <h1 className="company-name">KELLE Solutions</h1>
          <p className="company-message">
            We believe that if we can simplify business management for brokers, we can create a better atmosphere for your agents.
          </p>
        </div>
      </div>

      <div className="content">
        <div className="text-box">
          <h1>Welcome to Our Real Estate Website</h1>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.</p>
        </div>
        <div className="text-box">
          <h1>Find Your Dream Home</h1>
          <p>Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh.</p>
        </div>
        <div className="text-box">
          <h1>Contact Us</h1>
          <p>Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales.</p>
        </div>
      </div>
    </div>
  );
}

export default Home;
