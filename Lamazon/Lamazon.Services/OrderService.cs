using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.DataAccess.Repositories;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Lamazon.Services.Helpers;
using Lamazon.Services.Interfaces;
using Lamazon.WebModels.Enums;
using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<OrderProduct> _orderProductRepo;
        private readonly IUserRepository<User> _userRepo;

        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> orderRepo, 
            IRepository<OrderProduct> orderProductRepo,
            IUserRepository<User> userRepo,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _orderProductRepo = orderProductRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public IEnumerable<OrderViewModel> GetAllOrders()
        {

            //return _orderRepo.GetAll()
            //    .Select(o => _mapper.Map<OrderViewModel>(o))
            //    .ToList();
            return _mapper.Map<IEnumerable<OrderViewModel>>(
                _orderRepo.GetAll()
            );
        }

        public IEnumerable<OrderViewModel> GetUserOrders(string userId)
        {
            //return _orderRepo.GetAll()
            //    .Where(o => o.UserId == userId)
            //    .Select(o => _mapper.Map<OrderViewModel>(o))
            //    .ToList();
            return _mapper.Map<IEnumerable<OrderViewModel>>(
                _orderRepo.GetAll()
                    .Where(o => o.UserId == userId)
            );
        }

        public OrderViewModel GetOrderById(int orderId)
        {
            Order order = _orderRepo.GetById(orderId);
            if (order == null)
                throw new Exception("Order does not exist");

            return _mapper.Map<OrderViewModel>(order);
        }

        public OrderViewModel GetCurrentOrder(string userId)
        {
            Order order = _orderRepo.GetAll()
                                    .LastOrDefault(o => o.UserId == userId);

            if (order == null || order.Status != StatusType.Init)
            {
                CreateOrder(new OrderViewModel { User = new UserViewModel { Id = userId } });
                return GetCurrentOrder(userId);
            }

            return _mapper.Map<OrderViewModel>(order);
        }

        public void CreateOrder(OrderViewModel order)
        {
            _orderRepo.Insert(
                _mapper.Map<Order>(order)
            );
        }

        public void ChangeStatus(int orderId, StatusTypeViewModel status)
        {
            _orderRepo.Update(
                new Order
                {
                    Id = orderId,
                    Status = (StatusType)status
                }
            );
        }

        public void AddProduct(int orderId, int productId)
        {
            _orderProductRepo.Insert(
                new OrderProduct
                {
                    OrderId = orderId,
                    ProductId = productId
                }
            );
        }

        public void RemoveProduct(int orderId, int productId)
        {
            _orderProductRepo.Delete(
                int.Parse($"{orderId}{productId}")
            );
        }
    }
}
