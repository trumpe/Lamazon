using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Lamazon.WebApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public ProductsController(
            IProductService productService, 
            IOrderService orderService,
            IUserService userService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        [Authorize(Roles = "customer, admin")]
        public IActionResult ListProducts()
        {
            return View("Index", _productService.GetAllProducts());
        }

        [Authorize(Roles = "customer")]
        public IActionResult AddToCart(int productId)
        {
            UserViewModel user = _userService.GetCurrentUser(User.Identity.Name);
            OrderViewModel order = _orderService.GetCurrentOrder(user.Id);

            try
            {
                _orderService.AddProduct(order.Id, productId);
                Log.Information($"{User.Identity.Name}: added to cart: {_productService.GetProductById(productId).Name}.");
            }
            catch(Exception ex)
            {
                Log.Error($"{User.Identity.Name}: failed to add to cart: {_productService.GetProductById(productId).Name}. " +
                    $"Reason: {ex.Message}");
            }

            return RedirectToAction("ListProducts");
        }
    }
}