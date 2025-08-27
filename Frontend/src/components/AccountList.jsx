import React, { useState, useEffect } from 'react';
import AccountCard from './AccountCard';
import './AccountList.css';

const AccountList = () => {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchAccounts = async () => {
      try {
        setLoading(true);
        console.log('Fetching accounts from /api/Account/GetAllAccounts...');
        
        // Try both proxied and direct URLs
        let response;
        let url = '/api/Account/GetAllAccounts';
        
        try {
          response = await fetch(url);
          console.log('Proxy response status:', response.status);
        } catch (proxyError) {
          console.log('Proxy failed, trying direct URL:', proxyError);
          url = 'https://localhost:7097/api/Account/GetAllAccounts';
          response = await fetch(url, {
            mode: 'cors',
            credentials: 'include',
            headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json',
            }
          });
          console.log('Direct response status:', response.status);
        }
        
        console.log('Final URL used:', url);
        console.log('Response ok:', response.ok);
        console.log('Response headers:', [...response.headers.entries()]);
        
        if (!response.ok) {
          const errorText = await response.text();
          console.log('Error response text:', errorText);
          throw new Error(`HTTP error! status: ${response.status} - ${errorText}`);
        }
        
        const data = await response.json();
        console.log('Raw API response:', data);
        console.log('Data type:', typeof data);
        console.log('Is array:', Array.isArray(data));
        console.log('Data length:', data?.length);
        
        // Handle different response structures
        let accountsArray;
        if (Array.isArray(data)) {
          accountsArray = data;
        } else if (data && Array.isArray(data.data)) {
          accountsArray = data.data;
        } else if (data && Array.isArray(data.accounts)) {
          accountsArray = data.accounts;
        } else {
          accountsArray = [];
        }
        
        console.log('Processed accounts array:', accountsArray);
        console.log('Accounts array length:', accountsArray.length);
        
        setAccounts(accountsArray);
      } catch (err) {
        console.error('Error fetching accounts:', err);
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchAccounts();
  }, []);

  if (loading) return <div className="loading">Loading accounts...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="account-list-container">
      <h1 className="page-title">Account List</h1>
      
      <div className="accounts-grid">
        {accounts && accounts.length > 0 ? accounts.map((account, index) => (
          <AccountCard 
            key={account.accountId || account.id || index} 
            account={account} 
            index={index} 
          />
        )) : (
          <div className="no-accounts">
            <p>No accounts found</p>
            <button 
              onClick={() => window.location.reload()} 
              style={{
                background: '#ff6b6b',
                color: 'white',
                border: 'none',
                padding: '10px 20px',
                borderRadius: '5px',
                cursor: 'pointer',
                marginTop: '10px'
              }}
            >
              Refresh Page
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default AccountList;
