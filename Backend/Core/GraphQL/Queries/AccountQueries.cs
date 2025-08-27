using BusinessObject.Data;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Core.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class AccountQueries
    {
        public async Task<List<Account>> GetAllAccountAsync([Service] IAccountService accountService)
        {
            var response = await accountService.GetAllAccountsAsync();
            return response.Succeeded ? response.Data : new List<Account>();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Account> GetAccounts([Service] FarmProductsApiContext context)
        {
            return context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Account?> GetAccountById(int id, [Service] FarmProductsApiContext context)
        {
            return await context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Account?> GetAccountByUsername(string username, [Service] FarmProductsApiContext context)
        {
            return await context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(a => a.Username == username);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Account> GetAccountsByRole(byte role, [Service] FarmProductsApiContext context)
        {
            return context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product)
                .Where(a => a.Role == role);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Account> GetActiveAccounts([Service] FarmProductsApiContext context)
        {
            return context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product)
                .Where(a => a.Status == true);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Account> GetOwnerAccount([Service] FarmProductsApiContext context)
        {
            return context.Accounts
                .Include(a => a.OrderCustomers)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.OrderStaffs)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .Include(a => a.ImportedStocks)
                    .ThenInclude(i => i.Product)
                .Where(a => a.Status == true);
        }
    }
} 