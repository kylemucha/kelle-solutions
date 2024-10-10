import React, { useEffect } from 'react';
import './Home.css';  // Import the CSS for styling

function Home() {
  useEffect(() => {
    const handleScroll = () => {
      const carousel = document.querySelector('.carousel');
      const scrollPosition = window.scrollY;

      // Adjust the translation based on scroll position to create animation effect
      carousel.style.transform = `translateY(${scrollPosition * 0.2}px)`; // Adjust the multiplier for the desired effect
    };

    // Add scroll event listener
    window.addEventListener('scroll', handleScroll);

    // Clean up the event listener when the component unmounts
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return (
    <div className="home-page">
      <div className="carousel-section">
        <div className="carousel">
          {/* Carousel items */}
          <div className="carousel-item">Image 1</div>
          <div className="carousel-item">Image 2</div>
          <div className="carousel-item">Image 3</div>
        </div>
      </div>

      {/* Text Boxes Overlaid on the Carousel */}
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
