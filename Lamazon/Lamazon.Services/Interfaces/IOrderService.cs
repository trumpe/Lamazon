using Lamazon.WebModels.Enums;
using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lamazon.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> GetAllOrders();
        IEnumerable<OrderViewModel> GetUserOrders(string userId);
        OrderViewModel GetOrderById(int id);
        OrderViewModel GetCurrentOrder(string userId);
        void CreateOrder(OrderViewModel order);
        void ChangeStatus(int orderId, StatusTypeViewModel status);
        void AddProduct(int orderId, int productId);
        void RemoveProduct(int orderId, int productId);
    }
}
