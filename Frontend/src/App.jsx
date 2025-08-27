import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ApolloClient, InMemoryCache, ApolloProvider } from '@apollo/client';
import Layout from './components/Layout';
import ProductList from './components/ProductList';
import AccountList from './components/AccountList';
import AccountListSimple from './components/AccountListSimple';
import Home from './components/Home';
import './App.css'

// Configure Apollo Client
const client = new ApolloClient({
  uri: '/graphql', // Use proxy endpoint
  cache: new InMemoryCache(),
  defaultOptions: {
    watchQuery: {
      errorPolicy: 'ignore',
    },
    query: {
      errorPolicy: 'all',
    },
  }
});

function App() {
  return (
    <ApolloProvider client={client}>
      <Router>
        <div className="app">
          <Routes>
            <Route path="/" element={<Layout />}>
              <Route index element={<Home />} />
              <Route path="products" element={<ProductList />} />
              <Route path="accounts" element={<AccountList />} />
              <Route path="accounts-simple" element={<AccountListSimple />} />
            </Route>
          </Routes>
        </div>
      </Router>
    </ApolloProvider>
  )
}

export default App
