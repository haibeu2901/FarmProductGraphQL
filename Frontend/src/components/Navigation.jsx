import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Navigation.css';

const Navigation = () => {
  const location = useLocation();

  return (
    <nav className="navigation">
      <div className="nav-container">
        <Link to="/" className="nav-logo">Dashboard</Link>
        <div className="nav-buttons">
          <Link 
            to="/" 
            className={`nav-button ${location.pathname === '/' ? 'active' : ''}`}
          >
            Home
          </Link>
          <Link 
            to="/products" 
            className={`nav-button ${location.pathname === '/products' ? 'active' : ''}`}
          >
            Products
          </Link>
          <Link 
            to="/accounts" 
            className={`nav-button ${location.pathname === '/accounts' ? 'active' : ''}`}
          >
            Accounts
          </Link>
          <Link 
            to="/accounts-simple" 
            className={`nav-button ${location.pathname === '/accounts-simple' ? 'active' : ''}`}
          >
            Accounts Simple
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Navigation;
