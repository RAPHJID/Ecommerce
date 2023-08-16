
using Ecommerce.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Ecommerce.Services.IServices
{
    internal interface IPurchaseInterface
    {

        Task<SuccessMessage> CreatePurchaseAsync(AddPurchase purchase);
        Task<SuccessMessage> UpdatePurchaseAsync(Purchase purchase);
        Task<SuccessMessage> DeletePurchaseAsync(string id);
        Task<Purchase> GetPurchaseAsync(string id);
        Task<List<Purchase>> GetAllPurchasesAsync();

    }
}