using BusinessObject.Data;
using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ImportedStockRepository : BaseRepository<ImportedStock>, IImportedStockRepository
    {
        public ImportedStockRepository(FarmProductsApiContext context) : base(context)
        {
        }
    }
}
