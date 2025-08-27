using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IImportedStockRepository ImportedStockRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}
