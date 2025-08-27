import React from 'react';
import { Link } from 'react-router-dom';
import './Home.css';

const Home = () => {
  return (
    <div className="home-container">
      <div className="hero-section">
        <h1 className="hero-title">Welcome to Dashboard</h1>
        <p className="hero-subtitle">
          Manage your products and accounts efficiently with our modern interface
        </p>
        <div className="hero-actions">
          <Link to="/products" className="hero-button primary">
            View Products
          </Link>
          <Link to="/accounts" className="hero-button secondary">
            View Accounts
          </Link>
        </div>
      </div>
      
      <div className="features-section">
        <div className="feature-card">
          <div className="feature-icon">📦</div>
          <h3>Product Management</h3>
          <p>Browse and manage your product catalog with GraphQL integration</p>
          <Link to="/products" className="feature-link">Explore Products →</Link>
        </div>
        
        <div className="feature-card">
          <div className="feature-icon">👥</div>
          <h3>Account Management</h3>
          <p>View and manage user accounts with REST API integration</p>
          <Link to="/accounts" className="feature-link">View Accounts →</Link>
        </div>
        
        <div className="feature-card">
          <div className="feature-icon">👤</div>
          <h3>Simple Account List</h3>
          <p>Clean, minimal account directory similar to product list design</p>
          <Link to="/accounts-simple" className="feature-link">View Simple List →</Link>
        </div>
      </div>
    </div>
  );
};

export default Home;
