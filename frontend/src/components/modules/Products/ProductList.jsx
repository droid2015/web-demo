import { useState, useEffect } from 'react';
import { productService } from '../../../services/productService';
import './ProductList.css';

const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    try {
      setLoading(true);
      const data = await productService.getAll();
      setProducts(data);
    } catch (err) {
      setError('Failed to load products');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this product?')) {
      try {
        await productService.delete(id);
        loadProducts();
      } catch (err) {
        alert('Failed to delete product');
      }
    }
  };

  if (loading) return <div>Loading products...</div>;
  if (error) return <div className="error">{error}</div>;

  return (
    <div className="product-list">
      <div className="page-header">
        <h1>Products</h1>
      </div>

      <div className="products-grid">
        {products.map(product => (
          <div key={product.id} className="product-card">
            <h3>{product.name}</h3>
            <p className="product-description">{product.description}</p>
            <div className="product-details">
              <p className="product-price">${product.price}</p>
              <p className="product-stock">Stock: {product.stockQuantity}</p>
            </div>
            <div className="product-actions">
              <button 
                onClick={() => handleDelete(product.id)}
                className="btn-delete"
              >
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>

      {products.length === 0 && (
        <div className="empty-state">
          <p>No products found</p>
        </div>
      )}
    </div>
  );
};

export default ProductList;
