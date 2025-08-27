using BusinessObject.Data;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FarmProductsApiContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImportedStockRepository _importedStockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public UnitOfWork(
            FarmProductsApiContext context,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            IImportedStockRepository importedStockRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository
            )
        {
            this._context = context;
            this._accountRepository = accountRepository;
            this._categoryRepository = categoryRepository;
            this._importedStockRepository = importedStockRepository;
            this._productRepository = productRepository;
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
        }

        public IAccountRepository AccountRepository => _accountRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository;

        public IImportedStockRepository ImportedStockRepository => _importedStockRepository;

        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;

        public IOrderRepository OrderRepository => _orderRepository;

        public IProductRepository ProductRepository => _productRepository;

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
