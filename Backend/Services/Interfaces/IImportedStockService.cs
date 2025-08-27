using BusinessObject.Models;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IImportedStockService
    {
        Task<ApiResponse<List<ImportedStock>>> GetAllImportedStocksAsync();
        Task<ApiResponse<ImportedStock>> GetImportedStockByIdAsync(int id);
        Task<ApiResponse<ImportedStock>> CreateImportedStockAsync(ImportedStock importedStock);
        Task<ApiResponse<ImportedStock>> UpdateImportedStockAsync(ImportedStock importedStock);
        Task<ApiResponse<bool>> DeleteImportedStockAsync(int id);
    }
}
