using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.Enums;
using Lamazon.WebModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.WebApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [Authorize(Roles = "customer, supplier, admin")]
        public IActionResult OrderDetails(int orderId)
        {
            return View(_orderService.GetOrderById(orderId));
        }

        [Authorize(Roles = "customer")]
        public IActionResult ListUserOrders()
        {
            UserViewModel user = _userService.GetCurrentUser(User.Identity.Name);

            ViewBag.UserOption = UserOptions.Customer;
            ViewBag.WebAppBaseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            return View("Index", _orderService.GetUserOrders(user.Id));
        }

        [Authorize(Roles = "customer")]
        public IActionResult FinishOrder(int orderId)
        {
            _orderService.ChangeStatus(orderId, StatusTypeViewModel.Processing);

            return RedirectToAction(nameof(ListUserOrders));
        }

        [Authorize(Roles = "customer")]
        public IActionResult CancelOrder(int orderId)
        {
            _orderService.ChangeStatus(orderId, StatusTypeViewModel.Canceled);

            return RedirectToAction(nameof(ListUserOrders));
        }

        [Authorize(Roles = "supplier, admin")]
        public IActionResult ListAllOrders()
        {
            ViewBag.UserOption = UserOptions.Supplier;
            ViewBag.WebAppBaseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            return View("Index", _orderService.GetAllOrders());
        }

        [Authorize(Roles = "supplier, admin")]
        public IActionResult ConfirmOrder(int orderId)
        {
            _orderService.ChangeStatus(orderId, StatusTypeViewModel.Confirmed);

            return RedirectToAction(nameof(ListAllOrders));
        }

        [Authorize(Roles = "supplier, admin")]
        public IActionResult DeclineOrder(int orderId)
        {
            _orderService.ChangeStatus(orderId, StatusTypeViewModel.Declined);

            return RedirectToAction(nameof(ListAllOrders));
        }

        [Authorize(Roles = "supplier, admin")]
        public IActionResult MarkAsDelivered(int orderId)
        {
            _orderService.ChangeStatus(orderId, StatusTypeViewModel.Delivered);

            return RedirectToAction(nameof(ListAllOrders));
        }
    }
}