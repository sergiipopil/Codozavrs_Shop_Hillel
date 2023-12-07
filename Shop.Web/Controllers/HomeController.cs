using ShopWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        DapperORM dapper = new DapperORM();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await dapper.ExecReturnList<ProductModel>("GetAllProducts", null);
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            try
            {
                ModelState.Remove("Weight");
                ModelState.Remove("Production");
                ModelState.Remove("Expiration");
                if (ModelState.IsValid)
                {
                    await dapper.ExecWithoutReturn("CreateProduct",
                        new
                        {
                            Title = product.Title,
                            Price = product.Price,
                            Count = product.Count,
                            Weight = product.Weight,
                            Production = product.Production,
                            Expiration = product.Expiration
                        });
                    TempData["successMessage"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            try
            {
                ProductModel product =
                    await dapper.ExecReturnObject<ProductModel>("GetProductById", new { Id = Id });
                if (product != null)
                {
                    return View(product);
                }
                else
                {
                    TempData["errorMessage"] = "Product is unavailable";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"]=ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            try
            {
                ModelState.Remove("Weight");
                ModelState.Remove("Production");
                ModelState.Remove("Expiration");
                if (ModelState.IsValid)
                {
                    await dapper.ExecWithoutReturn("UpdateProductById",
                        new
                        {
                            Id = product.Id,
                            Title = product.Title,
                            Price = product.Price,
                            Count = product.Count,
                            Weight = product.Weight,
                            Production = product.Production,
                            Expiration = product.Expiration,
                        });
                    TempData["successMessage"] = "Product updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await dapper.ExecWithoutReturn("DeleteProductById",
                    new
                    {
                        Id = Id
                    });
                TempData["successMessage"] = "Product deleted successfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}