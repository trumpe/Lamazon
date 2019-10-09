using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamazon.Domain.Models;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.WebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            _userService.Login(model);

            if (User.IsInRole("supplier"))
                return RedirectToAction("ListAllOrders", "Orders");

            return RedirectToAction("ListProducts", "Products");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            _userService.Register(model);
            return RedirectToAction("ListProducts", "Products");
        }

        public IActionResult LogOut()
        {
            _userService.Logout();
            return RedirectToAction("Login", "Users");
        }

        [Authorize(Roles = "admin")]
        public IActionResult ListUsers()
        {
            return View(_userService.GetAllUsers());
        }
    }
}