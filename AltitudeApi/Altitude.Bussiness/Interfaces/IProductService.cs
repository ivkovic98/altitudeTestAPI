using Altitude.Bussiness.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Bussiness.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseModel>> GetAllAsync();
        Task<ProductResponseModel?> GetByIdAsync(Guid id);
        Task CreateAsync(ProductCreateModel model);
        Task<bool> UpdateAsync(Guid id, ProductCreateModel model);
        Task<bool> DeleteAsync(Guid id);
    }
}
