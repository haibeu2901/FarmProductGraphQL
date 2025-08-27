import React from 'react';
import { useQuery, gql } from '@apollo/client';
import './ProductList.css';

const GET_PRODUCTS = gql`
  query {
    products {
      productName
      description
      unit
      sellingPrice
    }
  }
`;

const ProductList = () => {
  const { loading, error, data } = useQuery(GET_PRODUCTS);

  if (loading) return <div className="loading">Loading products...</div>;
  if (error) return <div className="error">Error: {error.message}</div>;

  return (
    <div className="product-list-container">
      <h1 className="page-title">Product Catalog</h1>
      <div className="products-grid">
        {data.products.map((product, index) => (
          <div key={index} className="product-card">
            <h3 className="product-name">{product.productName}</h3>
            <p className="product-description">{product.description}</p>
            <div className="product-details">
              <span className="product-unit">Unit: {product.unit}</span>
              <span className="product-price">${product.sellingPrice}</span>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductList;
