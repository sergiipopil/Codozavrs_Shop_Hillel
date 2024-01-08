using ShopWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Business.Classes;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManagement _productManagement;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _productManagement = new ProductManagement();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productsDto = await _productManagement.GetAllProducts();
            IList<ProductModel> productList = productsDto.Select(x => new ProductModel()
            {
                Weight = x.Weight,
                Count = x.Count,
                Expiration = x.Expiration,
                Id = x.Id,
                Price = x.Price,
                Production = x.Production,
                Title = x.Title
            }).ToList();
            return View(productList);
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
                    ProductDto productDto = new ProductDto()
                    {
                        Count = product.Count,
                        Expiration = product.Expiration,
                        Id = product.Id,
                        Price = product.Price,
                        Production = product.Production,
                        Title = product.Title,
                        Weight = product.Weight
                    };
                    _productManagement.AddProduct(productDto);
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
                ProductDto productDto = await _productManagement.GetProductById(Id);
                ProductModel product = new ProductModel()
                {
                    Weight = productDto.Weight,
                    Count = productDto.Count,
                    Expiration = productDto.Expiration,
                    Id = productDto.Id,
                    Price = productDto.Price,
                    Production = productDto.Production,
                    Title = productDto.Title
                };

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
                TempData["errorMessage"] = ex.Message;
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
                    ProductDto productDto = new ProductDto()
                    {
                        Count = product.Count,
                        Expiration = product.Expiration,
                        Id = product.Id,
                        Price = product.Price,
                        Production = product.Production,
                        Title = product.Title,
                        Weight = product.Weight
                    };
                    _productManagement.EditProduct(productDto);
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
                _productManagement.DeleteProduct(Id);
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