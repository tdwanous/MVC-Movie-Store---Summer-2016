using MvcMovie.Models;
using MvcMovie.Models.Identity;
using MvcMovie.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderDetailService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<OrderDetail> GetOrderById(int? orderId)
        {
            var orderItems = _dbContext.OrderDetails.Where(i => i.OrderId == orderId);
            return orderItems.ToList();
        }

        public OrderDetail EditOrder(OrderDetail order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return order;
        }

        public Order GetOrderByIds(int? orderId)
        {
            return _dbContext.Orders.Find(orderId);
        }

        public void DeleteOrder(int orderId)
        {
            var orderToRemove = _dbContext.Orders.Find(orderId);
            if(orderToRemove != null)
            {
                _dbContext.Orders.Remove(orderToRemove);
            }

            var orderDetailsToRemove = _dbContext.OrderDetails.Where(i => i.OrderId == orderId);
            if(orderDetailsToRemove != null)
            {
                _dbContext.OrderDetails.RemoveRange(orderDetailsToRemove);
            }   
            _dbContext.SaveChanges();
        }
    }
}