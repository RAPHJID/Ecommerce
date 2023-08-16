
using Ecommerce.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Ecommerce.Services.IServices
{
    internal interface IProductInterface
    {

        Task<SuccessMessage> CreateProductAsync(AddProduct product);
        Task<SuccessMessage> UpdateProductAsync(Product product);
        Task<SuccessMessage> DeleteProductAsync(string id);
        Task<Product> GetProductAsync(string id);
        Task<List<Product>> GetAllProductAsync();

    }
}