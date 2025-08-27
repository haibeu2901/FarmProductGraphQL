import React from 'react';
import { useQuery, gql } from '@apollo/client';
import AccountCard from './AccountCard';
import './AccountListSimple.css';

const GET_ACCOUNTS = gql`
  query {
    allAccount {
      accountId
      fullName
      username
      password
      email
      phoneNumber
      address
    }
  }
`;

const AccountListSimple = () => {
  const { loading, error, data } = useQuery(GET_ACCOUNTS);

  if (loading) return <div className="loading">Loading accounts...</div>;
  if (error) return <div className="error">Error: {error.message}</div>;

  return (
    <div className="account-list-container">
      <h1 className="page-title">Account Directory</h1>
      <div className="accounts-grid">
        {data.allAccount.map((account, index) => (
          <AccountCard 
            key={account.accountId || index} 
            account={account} 
            index={index} 
          />
        ))}
      </div>
    </div>
  );
};

export default AccountListSimple;
