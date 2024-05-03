using Bogus;
using eCommerce.MVC.DTOs;
using eCommerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.MVC.Controllers;
public class HomeController : Controller
{
    private List<Product> products = new();
    private static List<ShoppingCart> shoppingCarts = new();
    public HomeController()
    {
        Faker faker = new();

        Product product1 = new()
        {
            Name = faker.Commerce.ProductName(),
            ImageUrl = "https://cdn.yemek.com/uploads/2023/06/domates-kac-kalori-shutter-4.jpg",
            Description = faker.Commerce.ProductDescription(),
            Price = faker.Commerce.Price(symbol: "TRY") //altgr+t harfi => t�rk liras� sembol� atar
        };

        products.Add(product1);

        faker = new();

        Product product2 = new()
        {
            Name = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            Price = faker.Commerce.Price(symbol: "TRY"),
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Kiwi_%28Actinidia_chinensis%29_1_Luc_Viatour.jpg/640px-Kiwi_%28Actinidia_chinensis%29_1_Luc_Viatour.jpg"
        };

        products.Add(product2);
    }
    public IActionResult Index() //action - action methodlar
    {
        return View(products);
    }    

    public IActionResult Contact()
    {
        return View();
    }
   
    public IActionResult ShoppingCart()
    {
        decimal total = 0;
        foreach (var item in shoppingCarts)
        {
            total += Convert.ToDecimal(item.Price.Replace("TRY", ""));
        }

        ViewBag.Total = total;
        //TempData["Total"] = total;

        return View(shoppingCarts);
    }

    [HttpGet]
    public IActionResult AddShoppingCart(int index)
    {
        Product product = products[index];

        ShoppingCart shoppingCart = new()
        {
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price
        };

        shoppingCarts.Add(shoppingCart);

        return RedirectToAction("Index","Home");
    }

    [HttpPost]
    public IActionResult Pay(PayDto request)
    {
        return RedirectToAction("ShoppingCart");
    }

}