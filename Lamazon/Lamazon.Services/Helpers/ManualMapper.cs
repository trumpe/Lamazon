using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Lamazon.WebModels.Enums;
using Lamazon.WebModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lamazon.Services.Helpers
{
    public class ManualMapper
    {
        //public UserViewModel UserToViewModel(User user)
        //{
        //    return new UserViewModel
        //    {
        //        Username = user.Username,
        //        Email = user.Email,
        //        FullName = $"{user.Firstname} {user.Lastname}",
        //        Address = user.Address,
        //    };
        //}

        //public User UserToDomainModel(RegisterViewModel regModel)
        //{
        //    return new User
        //    {
        //        Username = regModel.Username,
        //        Email = regModel.Email,
        //        Password = regModel.Password,
        //        Firstname = regModel.FirstName,
        //        Lastname = regModel.LastName,
        //        Address = regModel.Address,
        //        RoleId = 3
        //    };
        //}

        //public ProductViewModel ProductToViewModel(Product product)
        //{
        //    return new ProductViewModel
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Category = (CategoryTypeViewModel)product.Category,
        //        Price = product.Price
        //    };
        //}

        //public Product ProductToDomainModel(ProductViewModel product)
        //{
        //    return new Product
        //    {
        //        Name = product.Name,
        //        Description = product.Description,
        //        Category = (CategoryType)product.Category,
        //        Price = product.Price
        //    };
        //}

        //public OrderViewModel OrderToViewModel(Order order)
        //{
        //    return new OrderViewModel
        //    {
        //        Id = order.Id,
        //        Status = (StatusTypeViewModel)order.Status,
        //        User = UserToViewModel(order.User),
        //        Products = order.OrdersProducts
        //            .Select(op => ProductToViewModel(op.Product))
        //            .ToList()
        //    };
        //}

        //public Order OrderToDomainModel(OrderViewModel order)
        //{
        //    return new Order
        //    {
        //        DateCreated = DateTime.UtcNow,
        //        Status = StatusType.Init,
        //        Paid = false,
        //        UserId = order.User.Id
        //    };
        //}

        //public OrderProduct OrderProductToDomain(int orderId, int productId)
        //{
        //    return new OrderProduct
        //    {
        //        OrderId = orderId,
        //        ProductId = productId
        //    };
        //}
    }
}
