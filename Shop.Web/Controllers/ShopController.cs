using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Web.Models;

namespace Shop.Web.Controllers
{
    public class ShopController : Controller
    {
        DapperORM dapper = new DapperORM();
        // GET: ShopController
        public async Task<IActionResult> Index()
        {
            Task<IEnumerable<ShopModel>> shopsTask = dapper.ExecReturnList<ShopModel>("GetAllShops", null);
            // get result from async method
            IEnumerable<ShopModel> shops = await shopsTask;
            List<ShopModel> shopsList = shops.ToList();
            return View(shopsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ShopModel shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dapper.ExecWithoutReturn("CreateShop",
                         new
                         {
                             Name = shop.Name,
                             Location = shop.Location,
                             IsOpened = shop.IsOpened
                         });
                    TempData["successMessage"] = "Shop created successfully";
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
                ShopModel shop =
                    await dapper.ExecReturnObject<ShopModel>("GetShopById", new { ShopId = Id });
                if (shop != null)
                {
                    return View(shop);
                }
                else
                {
                    TempData["errorMessage"] = "Shop is unavailable";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        public async Task <IActionResult> Edit(ShopModel shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dapper.ExecWithoutReturn("UpdateShopById",
                        new
                        {
                            ShopId = shop.ID,
                            Name = shop.Name,
                            Location = shop.Location,
                            IsOpened = shop.IsOpened
                        });
                    TempData["successMessage"] = "Shop updated successfully";
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
                await dapper.ExecWithoutReturn("DeleteShopById",
                    new
                    {
                        ShopId = Id
                    });

                TempData["successMessage"] = "Shop deleted successfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
