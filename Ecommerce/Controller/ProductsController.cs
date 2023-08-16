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
    public class PurchasesController
    {
        PurchaseService purchaseService = new PurchaseService();

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
                await PurchasesController.Init();

            }
            else
            {
               await new PurchasesController().MenuRedirect(input);// instance method 
            }
           
        }

        public async  Task MenuRedirect(string id )
        {
          switch(id)
            {
                case "1":
                   await AddnewPurchase();
                    break;
                case "2":
                   await ViewPurchases();
                    break;
                case "3":
                    await updateaPurchase();
                    break;
                case "4":
                    await DeleteaPurchase();
                    break;
                default:
                   await  PurchasesController.Init();
                    break;
            }
        }

        public async Task AddnewPurchase()
        {

            Console.WriteLine("Enter item Name:");
            var purchaseName = Console.ReadLine();


            Console.WriteLine("Enter item Price:");
            var purchasePrice = Console.ReadLine();
            //Creat AddBook Instance
            var newPurchase = new AddPurchase()
            {
                Name = purchaseName,
                Price = purchasePrice
            };

            //call Service

            try
            {
                //if it goes right 
               var res= await purchaseService.CreatePurchaseAsync(newPurchase);
               Console.WriteLine(res.Message);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task updateaPurchase()
        {
            await ViewPurchases();
            Console.WriteLine("Enter the Id of the item you want to update");
            var id = Console.ReadLine();
            Console.WriteLine("Enter item Name:");
            var bookTitle = Console.ReadLine();

            Console.WriteLine("Enter item Price:");
            var purchasePrice = Console.ReadLine();
            //validate
            var updatedPurchase = new Purchase()
            {   
                Id=id,
                Name = purchaseName,
                Price = purchasePrice
            };

            try
            {
                var res = await purchaseService.UpdatePurchaseAsync(updatedPurchase);
                Console.WriteLine(res.Message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task ViewPurchases()
        {
            try
            {
                var purchases = await purchaseService.GetAllPurchasesAsync();
                foreach (var purchase in purchases)
                {
                    await Console.Out.WriteLineAsync($"{purchase.Id} . {purchase.Price}");
                    Console.WriteLine("View One of the Goods");
                    var id =Console.ReadLine();
                    await ViewOnePurchase(id);
                }
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        public async Task ViewOnePurchase(string id)
        {
         
            try
            {
                var res = await purchaseService.GetPurchaseAsync(id);
                Console.WriteLine(res.Price);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteaPurchase()
        {
            await ViewPurchases();
            Console.WriteLine("Enter the Id of the Purchase you want to Delete");
            var id = Console.ReadLine();

            try
            {
                var res = await purchaseService.DeletePurchaseAsync(id);
                Console.WriteLine(res.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
