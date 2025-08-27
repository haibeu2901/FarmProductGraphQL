using BusinessObject.Data;
using BusinessObject.Models;
using BusinessObject.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(FarmProductsApiContext context) : base(context)
        {
        }

        public async Task<Order?> GetLatestOrderAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(p => p.ProductCategory)
                .OrderByDescending(o => o.OrderDate)
                    .ThenByDescending(o => o.OrderId)
                .FirstOrDefaultAsync();
        }

        public override async Task<PaginatedResult<Order>> GetPaginatedAsync(PaginationRequest request)
        {
            var totalCount = await _context.Orders.CountAsync();
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                   .ThenInclude(od => od.Product)
                   .ThenInclude(p => p.ProductCategory)
                .OrderByDescending(o => o.OrderDate)
                    .ThenByDescending(o => o.OrderId)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
