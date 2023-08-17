using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Services;
using Ecommerce.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Controller
{
    public class ProductsController
    {
        ProductService productService = new ProductService();

        public async static  Task  Init()
        {
            Console.WriteLine("Hello welcome to my clothes shop");
            Console.WriteLine("1. Add a Item");
            Console.WriteLine("2. View Items");
            Console.WriteLine("3. Update a Item");
            Console.WriteLine("4. Delete a Item");

            var input = Console.ReadLine();
            var validateResults=Validation.Validate(new List<string> { input });
            if (!validateResults)
            {
                await ProductsController.Init();

            }
            else
            {
               await new ProductsController().MenuRedirect(input);
            }
           
        }

        public async  Task MenuRedirect(string id )
        {
          switch(id)
            {
                case "1":
                   await AddnewProduct();
                    break;
                case "2":
                   await ViewProducts();
                    break;
                case "3":
                    await updateaProduct();
                    break;
                case "4":
                    await DeleteaProduct();
                    break;
                    
                default:
                   await  ProductsController.Init();
                    break;
            }
        }

        public async Task AddnewProduct()
        {

            Console.WriteLine("Enter item Name:");
            var productName = Console.ReadLine();


            Console.WriteLine("Enter item Price:");
            var productPrice = Console.ReadLine();
            var newProduct = new AddProduct()
            {
                Name = productName,
                Price = productPrice
            };

            try
            {
               var res= await productService.CreateProductAsync(newProduct);
               Console.WriteLine(res.Message);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task updateaProduct()
        {
            await ViewProducts();
            Console.WriteLine("Enter the Id of the item you want to update");
            var id = Console.ReadLine();
            Console.WriteLine("Enter item Name:");
            var productName = Console.ReadLine();

            Console.WriteLine("Enter item Price:");
            var productPrice = Console.ReadLine();
            var updatedProduct = new Product()
            {   
                Id=id,
                Name = productName,
                Price = productPrice
            };

            try
            {
                var res = await productService.UpdateProductAsync(updatedProduct);
                Console.WriteLine(res.Message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task ViewProducts()
        {
            try
            {
                var products = await productService.GetAllProductsAsync();
                foreach (var product in products)
                {
               
                    Console.WriteLine($"Id: {product.Id}, name:{product.Name}. price :{product.Price}");
                }

            }
            catch(Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        public async Task ViewOneProduct(string id)
        {
         
            try
            {
                var res = await productService.GetProductAsync(id);
               
                Console.WriteLine($"Product: {res.Name} - Price: {res.Price}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteaProduct()
        {
            await ViewProducts();
            Console.WriteLine("Enter the Id of the Product you want to Delete");
            var id = Console.ReadLine();

            try
            {
                var res = await productService.DeleteProductAsync(id);
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
