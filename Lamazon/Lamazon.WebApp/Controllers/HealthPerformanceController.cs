using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lamazon.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.WebApp.Controllers
{
    [Authorize]
    public class HealthPerformanceController : Controller
    {
        private readonly IProductService _productService;

        public HealthPerformanceController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        public IActionResult HealthCheck()
        {
            try
            {
                _productService.GetAllProducts();
                return Ok("Health check is in great state!");
            }
            catch(Exception ex)
            {
                return Ok($"Unhealthy! Message: {ex.Message}");
            }
        }

        [AllowAnonymous]
        public IActionResult PerformanceCheck()
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                for (int i = 0; i < 1000; i++)
                {
                    _productService.GetAllProducts();
                }

                stopwatch.Stop();

                double ems = stopwatch.ElapsedMilliseconds;

                if (ems < 350)
                    return Ok($"App is good! Elapsed ms:{ems}");
                else
                    return Ok($"Slow performance! Elapsed:{ems}");
            }
            catch(Exception ex)
            {
                return Ok($"App broke! Message: {ex.Message}");
            }
        }
    }
}