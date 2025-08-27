import React from 'react';
import './AccountCard.css';

const AccountCard = ({ account, index }) => {
  return (
    <div key={account.accountId || account.id || index} className="account-card">
      <h3 className="account-name">
        {account.fullName || account.name || account.username || 'No Name'}
      </h3>
      <p className="account-id">
        ID: {account.accountId || account.id || 'N/A'}
      </p>
      <div className="account-details">
        <span className="account-username">
          @{account.username || 'No Username'}
        </span>
        <span className="account-password">
          {account.password ? account.password : 'No Password'}
        </span>
      </div>
      {account.email && (
        <div className="account-email">
          📧 {account.email}
        </div>
      )}
      {account.role && (
        <div className="account-role">
          🔑 {account.role}
        </div>
      )}
      {account.phoneNumber && (
        <div className="account-phone">
          📱 {account.phoneNumber}
        </div>
      )}
      {account.address && (
        <div className="account-address">
          📍 {account.address}
        </div>
      )}
    </div>
  );
};

export default AccountCard;
