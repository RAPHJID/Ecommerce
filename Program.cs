using Ecommerce.Controller;
class Program
{
    public async static Task Main(string[] args)
    {
        await ProductsController.Init();
    }
}