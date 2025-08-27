using BusinessObject.Models;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<List<Account>>> GetAllAccountsAsync();
        Task<ApiResponse<Account>> GetAccountByIdAsync(int id);
        Task<ApiResponse<Account>> CreateAccountAsync(Account account);
        Task<ApiResponse<Account>> UpdateAccountAsync(Account account);
        Task<ApiResponse<bool>> DeleteAccountAsync(int id);
    }
}
