import React from 'react';
import { Link } from 'react-router-dom';
import '../../styles/admin-home.css';

const Home = () => {
  return (
    <div className="home-container">
      <div className="home-grid">
        <Link to="/cars" className="home-title">
          <i className="ri-car-line icon"></i>
          <p>Cars</p>
        </Link>
        <Link to="/users" className="home-title">
          <i className="ri-user-line icon"></i>
          <p>Users</p>
        </Link>
        <Link to="/rentals" className="home-title">
          <i className="ri-profile-line icon"></i>
          <p>Rentals</p>
        </Link>
        <Link to="/blogs" className="home-title">
          <i className="ri-message-2-line icon"></i>
          <p>Blogs</p>
        </Link>
      </div>
    </div>
  );
}

export default Home;
