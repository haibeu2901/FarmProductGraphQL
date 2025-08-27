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
    public class ImportedStockService : IImportedStockService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportedStockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ImportedStock>> CreateImportedStockAsync(ImportedStock importedStock)
        {
            await _unitOfWork.ImportedStockRepository.AddAsync(importedStock);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<ImportedStock>
            {
                Data = importedStock,
                Succeeded = true,
                Message = "Imported stock created successfully."
            };
        }

        public async Task<ApiResponse<bool>> DeleteImportedStockAsync(int id)
        {
            await _unitOfWork.ImportedStockRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Succeeded = true,
                Message = "Imported stock deleted successfully."
            };
        }

        public async Task<ApiResponse<List<ImportedStock>>> GetAllImportedStocksAsync()
        {
            var importedStocks = await _unitOfWork.ImportedStockRepository.GetAllAsync();
            return new ApiResponse<List<ImportedStock>>
            {
                Data = importedStocks,
                Succeeded = true,
                Message = "Imported stocks list retrieved successfully."
            };
        }

        public async Task<ApiResponse<ImportedStock>> GetImportedStockByIdAsync(int id)
        {
            var importedStock = await _unitOfWork.ImportedStockRepository.GetByIdAsync(id);
            return new ApiResponse<ImportedStock>
            {
                Data = importedStock,
                Succeeded = true,
                Message = "Imported stock retrieved successfully."
            };
        }

        public async Task<ApiResponse<ImportedStock>> UpdateImportedStockAsync(ImportedStock importedStock)
        {
            var existingStock = await _unitOfWork.ImportedStockRepository.GetByIdAsync(importedStock.ImportId);
            if (existingStock == null)
            {
                return new ApiResponse<ImportedStock>
                {
                    Succeeded = false,
                    Message = "Imported stock not found."
                };
            }

            existingStock.StockBeforeUpdate = importedStock.StockBeforeUpdate;
            existingStock.UpdatedStockQuantity = importedStock.UpdatedStockQuantity;
            existingStock.StockAfterUpdate = importedStock.StockAfterUpdate;
            existingStock.UpdatedAt = importedStock.UpdatedAt;
            existingStock.UpdatedBy = importedStock.UpdatedBy;

            await  _unitOfWork.ImportedStockRepository.UpdateAsync(existingStock);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<ImportedStock>
            {
                Data = existingStock,
                Succeeded = true,
                Message = "Imported stock updated successfully."
            };
        }
    }
}
