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
    internal class ProductService : IProductInterface
    {   

        private readonly HttpClient _httpClient;
        private readonly string _url = " http://localhost:3000/products";
        public ProductService()
        {
            _httpClient = new HttpClient(); 
        }
        
        public async Task<SuccessMessage> CreateProductAsync(AddProduct product)
        {

            
          var content = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_url, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Created Successfully" };
            }

            throw new Exception("Product Creation Failed");
        }

        public async Task<SuccessMessage> DeleteProductAsync(string id)
        {
            var response = await _httpClient.DeleteAsync(_url+"/"+id);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Deleted  Successfully" };
            }

            throw new Exception("Product Deletion Failed");
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {

            var response = await _httpClient.GetAsync(_url);
            var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                return products;
            }

            throw new Exception("Cant Get products");
        }

        public async  Task<Product> GetProductAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url + "/" + id);
            var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync()); 

            if (response.IsSuccessStatusCode)
            {
                return product;
            }

            throw new Exception("Cant Get product");
        }

        public async Task<SuccessMessage> UpdateProductAsync(Product product)
        {

            var content = JsonConvert.SerializeObject(product);
            
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(_url + "/" + product.Id,bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Product Updated   Successfully" };
            }

            throw new Exception("Product Updating  Failed");
        }
    }
}
