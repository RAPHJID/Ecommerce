using Ecommerce.Models;
using Ecommerce.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    internal class PurchaseService : IPurchaseInterface
    {   

        private readonly HttpClient _httpClient;
        private readonly string _url = " http://localhost:3000/products";
        public PurchaseService()
        {
            _httpClient = new HttpClient(); 
        }
        
        public async Task<SuccessMessage> CreatePurchaseAsync(AddPurchase purchase)
        {

            
          var content = JsonConvert.SerializeObject(purchase);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_url, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Purchase Created Successfully" };
            }

            throw new Exception("Purchase Creation Failed");
        }

        public async Task<SuccessMessage> DeletePurchaseAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(_url+"/"+id);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Purchase Deleted  Successfully" };
            }

            throw new Exception("Purchase Deletion Failed");
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {

            var response = await _httpClient.GetAsync(_url);
            var purchases = JsonConvert.DeserializeObject<List<Purchase>>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return purchases;
            }

            throw new Exception("Cant Get purchases");
        }

        public async  Task<Purchase> GetPurchaseAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            var purchase = JsonConvert.DeserializeObject<Purchase>(await response.Content.ReadAsStringAsync()); 

            if (response.IsSuccessStatusCode)
            {
                return purchase;
            }

            throw new Exception("Cant Get purchase");
        }

        public async Task<SuccessMessage> UpdatePurchaseAsync(Purchase purchase)
        {

            var content = JsonConvert.SerializeObject(purchase);
            
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_url + "/" + purchase.Id,bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Purchase Updated   Successfully" };
            }

            throw new Exception("Purchase Updating  Failed");
        }
    }
}