using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamazon.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lamazon.WebModels.ViewModels;
using Lamazon.WebModels.Enums;
using Newtonsoft.Json;

namespace Lamazon.WebApp.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IOrderService _orderService;

        public InvoicesController(IInvoiceService invoiceService, IOrderService orderService)
        {
            _invoiceService = invoiceService;
            _orderService = orderService;
        }

        [Authorize(Roles = "customer")]
        [HttpPost]
        public IActionResult Pay(InvoiceViewModel invoice)
        {
            _invoiceService.CreateInvoice(
                invoice
            );

            _orderService.ChangeStatus(invoice.OrderId, StatusTypeViewModel.Paid);

            return RedirectToAction("ListUserOrders", "Orders");
        }

        // Used for vanilla JavaScript ajax
        [Authorize(Roles = "customer, admin")]
        public string ShowInvoice(int orderId)
        {
            InvoiceViewModel invoice = _invoiceService.GetInvoice(orderId);

            var invoiceJs = new
            {
                invoice.Id,
                PaymentMethod = Enum.GetName(typeof(PaymentTypeViewModel), invoice.PaymentMethod),
                invoice.Address,
                invoice.Price,
                invoice.OrderId
            };

            return JsonConvert.SerializeObject(invoiceJs);
        }
    }
}