using MvcMovie.Models;
using System.Collections.Generic;

namespace MvcMovie.Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetOrderById(int? id);

        OrderDetail EditOrder(OrderDetail order);

        Order GetOrderByIds(int? orderId);

        void DeleteOrder(int orderId);
    }
}