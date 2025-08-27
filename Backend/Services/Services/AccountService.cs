using BusinessObject.Models;
using Repository;
using Services.Interfaces;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<Account>> CreateAccountAsync(Account account)
        {
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<Account>
            {
                Data = account,
                Succeeded = true,
                Message = "Account created successfully."
            };
        }

        public async Task<ApiResponse<bool>> DeleteAccountAsync(int id)
        {
            await _unitOfWork.AccountRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Succeeded = true,
                Message = "Account deleted successfully."
            };
        }

        public async Task<ApiResponse<Account>> GetAccountByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            return new ApiResponse<Account>
            {
                Data = account,
                Succeeded = true,
                Message = "Account retrieved successfully."
            };
        }

        public async Task<ApiResponse<List<Account>>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            return new ApiResponse<List<Account>>
            {
                Data = accounts,
                Succeeded = true,
                Message = "Accounts list retrieved successfully."
            };
        }

        public async Task<ApiResponse<Account>> UpdateAccountAsync(Account account)
        {
            var existingAccount = await _unitOfWork.AccountRepository.GetByIdAsync(account.AccountId);
            if (existingAccount == null)
            {
                return new ApiResponse<Account>
                {
                    Succeeded = false,
                    Message = "Account not found."
                };
            }
            existingAccount.FullName = account.FullName;
            existingAccount.Email = account.Email;
            existingAccount.PhoneNumber = account.PhoneNumber;
            existingAccount.Address = account.Address;
            await _unitOfWork.AccountRepository.UpdateAsync(existingAccount);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<Account>
            {
                Data = existingAccount,
                Succeeded = true,
                Message = "Account updated successfully."
            };
        }
    }
}
